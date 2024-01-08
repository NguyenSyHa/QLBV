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

namespace QLBV.FormNhap
{
    public partial class frm_Check_moi : DevExpress.XtraEditors.XtraForm
    {
        private readonly QLBVEntities _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;

        int _id = 0;
        int _mbn = 0;
        int dem = 0;
        int _status = 0;
        int _sopl = 0, _inpl = 0;
        //DateTime _ngaytu = DateTime.Now;
        //DateTime _ngayden = DateTime.Now;
        // _status 1: xóa xuất dược ngoại trú
        // _status 2: update số phiếu lĩnh vào bảng donthuocct
        int[] _arr;
        // _status 3: update số phiếu lĩnh cho khoa là 0 vào bảng donthuocct, 
        // _status 4: xóa xuất dược nội trú
        //_status 6: hủy đơn của BN ngoai tru update status 2;
        //_status 7:  hủy plinh update status 2;
        //_status 8: hủy đơn BN chưa thanh toán
        int iddon;
        bool _InPLTheoDon = false;// true: in theo đơn (20001); false: In phiếu lĩnh thường
        public frm_Check_moi()
        {
            dems = 2;
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mbn"></param>
        /// <param name="sts"></param>
        /// <param name="kt">để phân biệt với hàm frm_Check_moi khác, kt có thể == true hoặc false </param>
        public frm_Check_moi(int id, int mbn, int sts, bool kt)// _status 1
        {
            InitializeComponent();
            _id = id;
            _mbn = mbn;
            _status = sts;
        }
        private void MyForm_CloseOnStart(object sender, EventArgs e)
        {
            this.Close();
        }
        bool _thuocthang = false;
        public frm_Check_moi(int[] ar, int sts, bool thuocthang, bool inPLTheoDon, int inpl) // _status 2 dùng cho tạo phiếu lĩnh
        {
            InitializeComponent();
            _arr = ar;
            _status = sts;
            _thuocthang = thuocthang;
            _InPLTheoDon = inPLTheoDon;
            //_ngaytu = ngaytu;
            //_ngayden = ngayden;
            _inpl = inpl;//nếu là 1 thì in phiếu từ form phiếu lĩnh
        }

        bool CheckMK = false;
        public frm_Check_moi(int[] ar, int sts, bool thuocthang, bool inPLTheoDon) // _status 2 dùng cho in theo đơn
        {
            InitializeComponent();
            _arr = ar;
            _status = sts;
            _thuocthang = thuocthang;
            _InPLTheoDon = inPLTheoDon;
            //_ngaytu = ngaytu;
            //_ngayden = ngayden;
        }
        public frm_Check_moi(int id, int sts) // _status 3
        {
            InitializeComponent();
            iddon = id;
            _status = sts;
        }

        private class _lBN
        {
            public int MaBN { get; set; }
        }
        public frm_Check_moi(int id, int sopl, int sts) // _status 4
        {
            InitializeComponent();
            iddon = id;
            _sopl = sopl;
            _status = sts;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string matkhau = "";
            var mk = _dataContext.ADMINs.Where(p => p.MaCB == DungChung.Bien.MaCB).Select(p => p.MatK).ToList();
            if (mk.Count > 0)
                matkhau = mk.First();
            if (dem <= 2)
            {
                if (QLBV_Library.QLBV_Ham.MaHoa(txtMatKhau.Text) == matkhau || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || CheckMK) // sưa lại matkhau= txtmatKhau.text
                {

                    //try
                    //{
                    switch (_status)
                    {
                        case 1:
                            TaoPL(_id, _mbn, true);
                            break;
                        case 2:
                            List<string[]> dsPLDaTao = new List<string[]>();
                            List<string[]> lsoPL = TaoPL(_arr, ref dsPLDaTao);
                            foreach (string[] pl in lsoPL)
                            {
                                if (_thuocthang == false)
                                {

                                    if (_InPLTheoDon)
                                    {
                                        int _soPL = Convert.ToInt32(pl[0]);
                                        FormNhap.frmPhieulinh._InPhieuLinh_20001(_soPL, 1);
                                    }
                                    else
                                        InPhieu(pl, 2);
                                }
                                else
                                {
                                    if (pl[0].Length > 0)
                                    {

                                        int _soPL = Convert.ToInt32(pl[0]);

                                        if (_InPLTheoDon)
                                        {
                                            FormNhap.frmPhieulinh._InPhieuLinh_20001(_soPL, 0);


                                            //FormNhap.frmPhieulinh._InPhieuThuocDY_20001(_soPL);//
                                        }
                                        else
                                            FormNhap.frmPhieulinh._InPhieuThuocDY(_soPL);
                                        this.Dispose();
                                    }
                                }
                            }
                            foreach (string[] pl in dsPLDaTao)
                            {
                                if (_thuocthang == false)
                                {

                                    if (_InPLTheoDon)
                                    {
                                        int _soPL = Convert.ToInt32(pl[0]);
                                        FormNhap.frmPhieulinh._InPhieuLinh_20001(_soPL, 1);
                                    }

                                }
                                else
                                {
                                    if (pl[0].Length > 0)
                                    {

                                        int _soPL = Convert.ToInt32(pl[0]);

                                        if (_InPLTheoDon)
                                        {
                                            FormNhap.frmPhieulinh._InPhieuLinh_20001(_soPL, 0);
                                            //FormNhap.frmPhieulinh._InPhieuThuocDY_20001(_soPL);//
                                        }
                                        else
                                            FormNhap.frmPhieulinh._InPhieuThuocDY(_soPL);
                                        this.Dispose();
                                    }
                                }
                            }
                            break;
                        case 3:
                            List<string[]> pl1 = TaoPL_1(iddon);
                            foreach (string[] pl in pl1)
                            {
                                if (DungChung.Bien.MaBV == "27022" && pl[2] != "5")
                                {
                                    InPhieu(pl, 2);
                                }
                                else
                                {
                                    InPhieu(pl, 3);
                                }
                            }
                            break;
                        case 4:
                            TaoPL(iddon, _sopl);
                            break;
                        case 5:
                            var idd = _dataContext.DThuoccts.Where(p => p.SoPL == iddon).Select(p => p.IDDon).ToList();
                            foreach (var i in idd)
                            {
                                //var sua = _dataContext.DThuocs.Single(p => p.IDDon == i);
                                //sua.SoPL = 0;
                                var sua = _dataContext.DThuocs.Single(p => p.IDDon == i);
                                List<DThuocct> ldtct = _dataContext.DThuoccts.Where(p => p.IDDon == iddon).ToList();
                                foreach (DThuocct dt in ldtct)
                                {
                                    dt.SoPL = 0;
                                }
                                _dataContext.SaveChanges();
                            }
                            this.Dispose();
                            break;
                        case 6:
                            HuyDon(iddon);
                            break;
                        case 7:
                            TaoPL(iddon);
                            break;
                        case 8:
                            if (_id > 0)
                            {
                                //var sua8 = _dataContext.DThuocs.Single(p => p.IDDon == _id);
                                //sua8.Status = 3;
                                List<DThuocct> ldtct = _dataContext.DThuoccts.Where(p => p.IDDon == _id).ToList();
                                foreach (DThuocct dt in ldtct)
                                {
                                    if (dt.Status != -1)
                                        dt.Status = 3;
                                }
                                _dataContext.SaveChanges();
                            }
                            var sua81 = _dataContext.BenhNhans.Single(p => p.MaBNhan == _mbn);
                            sua81.Status = 3;
                            _dataContext.SaveChanges();
                            this.Dispose();
                            break;
                    }
                }
                else
                {
                    if (!CheckMK)
                    {
                        MessageBox.Show("Sai mật khẩu");
                        dem++;
                    }
                }
            }
            else
            {
                this.Dispose();
                MessageBox.Show("không xóa được, sai mật khẩu nhiều lần");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Check_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
            {
                CheckMK = true;
                btnOK_Click(null, null);
                this.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mbn"></param>
        /// <param name="kt">Biến cho thêm để phân biệt với hàm TaoPL khác; có thể truyền true hoặc false</param>
        /// <returns></returns>
        private bool TaoPL(int id, int mbn, bool kt) // xoá xuất dược của bệnh nhân ngoại trú
        {
            _id = id;
            _mbn = mbn;
            var xoact = _dataContext.NhapDcts.Where(p => p.IDNhap == _id).ToList();
            foreach (var xoa in xoact)
            {
                var _xoa = _dataContext.NhapDcts.Single(p => p.IDNhapct == (xoa.IDNhapct));
                _dataContext.NhapDcts.Remove(_xoa);
                _dataContext.SaveChanges();
            }
            var xoac = _dataContext.NhapDs.Single(p => p.IDNhap == (_id));
            _dataContext.NhapDs.Remove(xoac);
            _dataContext.SaveChanges();
            var iddt = (_dataContext.DThuocs.Where(p => p.MaBNhan == (_mbn)).Where(p => p.PLDV == 1).Select(p => p.IDDon)).ToList();
            int idxoa = 0;
            if (iddt.Count > 0)
                idxoa = iddt.First();
            //var dthuoc = _dataContext.DThuocs.Single(p => p.IDDon == (idxoa));
            //dthuoc.Status = 0;
            List<DThuocct> ldtct = _dataContext.DThuoccts.Where(p => p.IDDon == idxoa).ToList();
            foreach (DThuocct dt in ldtct)
            {
                if (dt.Status != -1)
                    dt.Status = 0;
            }
            _dataContext.SaveChanges();
            if (_dataContext.SaveChanges() >= 0)
            { return true; }
            else
            {
                return false;
            }
        }
        public static int _TaoSPL(QLBV_Database.QLBVEntities _data, int _makp)
        {
            int _soPL = 1;

            var maxdt = _data.DThuoccts.Where(p => _makp <= 0 ? true : p.MaKP == _makp).OrderByDescending(p => p.SoPL).Select(p => p.SoPL).ToList();
            try
            {
                if (maxdt.Count > 0)
                {
                    _soPL = maxdt.First() + 1;
                }
            }
            catch (Exception)
            {
                _soPL = 1;
            }

            return _soPL;
        }

        /// <summary>
        /// Lấy số phiếu lĩnh max theo đơn thuốc chi tiết
        /// </summary>
        /// <param name="_makp"></param>
        /// <returns></returns>
        public static int _TaoSPL(int _makp)
        {
            int _soPL = 1;
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var maxdt = (from dt in _data.DThuocs.Where(p => _makp <= 0 ? true : p.MaKP == _makp)
                         join dtct in _data.DThuoccts
                             on dt.IDDon equals dtct.IDDon
                         select dtct).Max(p => p.SoPL);
            try
            {
                if (maxdt != null)
                {
                    _soPL = maxdt + 1;
                }
            }
            catch (Exception)
            {
                _soPL = 1;
            }

            return _soPL;
        }

        public List<string[]> TaoPL(int[] ar, ref List<string[]> dsPLDaTao)// tạo phiếu lĩnh cho các bệnh nhân trong khoa. trả về số pl
        {
            List<string[]> lSoPL = new List<string[]>();
            _arr = ar;
            int _makp = 0;
            if (DungChung.Bien.MaBV == "19048")
            {
                int id = 0;
                if (_arr.Length > 0)
                    id = _arr[0];
                var kp = _dataContext.DThuocs.Where(p => p.IDDon == id).Select(p => new { p.MaKP, p.MaKXuat }).ToList();
                if (kp.Count > 0)
                {
                    _makp = kp.First().MaKP == null ? 0 : kp.First().MaKP.Value;

                }

            }
            #region
            var _ldichvu = (from dv in _dataContext.DichVus.Where(p => p.PLoai == 1)
                            join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new { dv.MaDV, tn.TenRG }).ToList();
            var _dthuoc = (from dtct in _dataContext.DThuoccts
                           where _arr.Contains(dtct.IDDonct)
                           select new { dtct }).ToList();
            var qdt = (from dtct in _dthuoc
                       join dv in _ldichvu on dtct.dtct.MaDV equals dv.MaDV
                       select new { dtct.dtct.IDDon, dtct.dtct.IDDonct, dtct.dtct.SoPL, TenRG = DungChung.Bien.MaBV == "14017" ? "TPL" : ((dv.TenRG != null && dv.TenRG.Contains("Thuốc thường")) ? "Thuốc thường" : dv.TenRG) }).ToList();

            var qtn = (from dt in qdt group dt by new { dt.TenRG } into kq select new { kq.Key.TenRG, arrriddonct = kq.Select(p => p.IDDonct).ToArray() }).ToList();

            foreach (var a in qtn)
            {

                int _soPL = 1;
                bool ktra = true;
                int tang = 0, a1 = 0;
                while (ktra)
                {
                    SoPL soPLMoi = new SoPL();
                    try
                    {
                        _soPL = _TaoSPL(_makp);
                        _soPL = tang + _soPL;
                        soPLMoi.MaKP = 0;
                        soPLMoi.SoPL1 = _soPL;
                        soPLMoi.Status = 0;
                        soPLMoi.NgayNhap = DateTime.Now;
                        soPLMoi.PhanLoai = 1;
                        soPLMoi.DSIdDonct = string.Join(";", a.arrriddonct);
                        soPLMoi.NoiTru = -1;
                        _dataContext.SoPLs.Add(soPLMoi);
                        if (_dataContext.SaveChanges() > 0)
                        {
                            ktra = false;
                        }
                        else
                        {
                            var removeSoPL = _dataContext.SoPLs.FirstOrDefault(o => o.SoPL1 == soPLMoi.SoPL1);
                            if (removeSoPL != null)
                            {
                                _dataContext.SoPLs.Remove(removeSoPL);
                                _dataContext.SaveChanges();
                            }
                            else
                            {
                                _dataContext.SoPLs.Remove(soPLMoi);
                                _dataContext.SaveChanges();
                            }

                            ktra = true;
                            tang++;
                            a1++;
                            if (a1 > 10)
                            {
                                MessageBox.Show("Lỗi tạo phiếu");
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var removeSoPL = _dataContext.SoPLs.FirstOrDefault(o => o.SoPL1 == soPLMoi.SoPL1);
                        if (removeSoPL != null)
                        {
                            _dataContext.SoPLs.Remove(removeSoPL);
                            _dataContext.SaveChanges();
                        }
                        else
                        {
                            _dataContext.SoPLs.Remove(soPLMoi);
                            _dataContext.SaveChanges();
                        }
                        ktra = true;
                        tang++;
                        a1++;
                        if (a1 > 10)
                        {
                            MessageBox.Show("Lỗi tạo phiếu: " + ex);
                            break;
                        }
                    }
                }
                List<int> _lIDDon = new List<int>();
                foreach (var item in a.arrriddonct)
                {
                    var donthuoc = _dataContext.DThuoccts.Single(p => p.IDDonct == item);
                    donthuoc.SoPL = _soPL;
                    _lIDDon.Add(donthuoc.IDDon ?? 0);
                }

                if (a.arrriddonct.Length > 0)
                    _dataContext.SaveChanges();

                _lIDDon = _lIDDon.Select(p => p).Distinct().ToList();

                string[] _ds = new string[2];
                _dataContext.SaveChanges();
                _ds[0] = _soPL.ToString();
                _ds[1] = _makp.ToString();
                lSoPL.Add(_ds);
            }
            //foreach (var pl in qPL1) // đã linh
            //{
            //    string[] _ds = new string[2] { "", "" };
            //    _ds[0] = pl.ToString();
            //    _ds[1] = _makp.ToString();
            //    dsPLDaTao.Add(_ds);
            //}

            #endregion

            return lSoPL;

        }
        public List<string[]> TaoPL_1(int id)// tạo phiếu lĩnh với trường hợp lĩnh cho khoa. trả về số phiếu lĩnh
        {
            List<string[]> lSoPL = new List<string[]>();
            iddon = id;
            int _makp = 0;
            if (DungChung.Bien.MaBV == "19048")
            {

                var kp = _dataContext.DThuocs.Where(p => p.IDDon == iddon).Select(p => p.MaKP).ToList();
                if (kp.Count > 0)
                    _makp = kp.First() == null ? 0 : kp.First().Value;
            }

            var qdt = (from dtct in _dataContext.DThuoccts.Where(p => p.SoPL == 0).Where(p => p.IDDon == iddon)
                       join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                       join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new { dtct.IDDon, dtct.IDDonct, dtct.SoPL, TenRG = DungChung.Bien.MaBV == "14017" ? "TPL" : ((tn.TenRG != null && tn.TenRG.Contains("Thuốc thường")) ? "Thuốc thường" : tn.TenRG), dv.PLoai }).ToList();
            var qtn = (from dt in qdt group dt by new { dt.TenRG, dt.PLoai } into kq select new { kq.Key.PLoai, kq.Key.TenRG, arrIdonct = kq.Select(p => p.IDDonct).ToArray(), }).ToList();
            foreach (var a in qtn)
            {
                string[] _ds = new string[3] { "", "", "" };
                //int _soPL = 1;
                int _soPL = 1;
                bool ktra = true;
                int tang = 0, a1 = 0;
                while (ktra)
                {
                    SoPL soPLMoi = new SoPL();
                    try
                    {
                        _soPL = _TaoSPL(_makp);
                        _soPL = tang + _soPL;
                        soPLMoi.MaKP = 0;
                        soPLMoi.SoPL1 = _soPL;
                        soPLMoi.Status = 0;
                        soPLMoi.NgayNhap = DateTime.Now;
                        soPLMoi.PhanLoai = 1;
                        soPLMoi.DSIdDonct = string.Join(";", a.arrIdonct);
                        soPLMoi.NoiTru = -1;
                        _dataContext.SoPLs.Add(soPLMoi);
                        _dataContext.SaveChanges();
                        ktra = false;

                    }
                    catch (Exception ex)
                    {
                        _dataContext.SoPLs.Remove(soPLMoi);
                        ktra = true;
                        tang++;
                        a1++;
                        if (a1 > 10)
                        {
                            break;
                            MessageBox.Show("Lỗi tạo phiếu: " + ex);
                        }
                    }
                }
                #region
                //var qdtct = (from donct in qdt.Where(p => p.TenRG == a.TenRG).Where(p => p.SoPL <= 0)
                //             join dtct in _dthuoc on donct.IDDonct equals dtct.dtct.IDDonct
                //             //_dataContext.DThuoccts on donct.IDDonct equals dtct.IDDonct 
                //             select dtct).ToList();

                //var qdt1 = (from dt in qdt.Where(p => p.TenRG == a.TenRG).Where(p => p.SoPL <= 0)
                //            join dthuoc in _dataContext.DThuocs on dt.IDDon equals dthuoc.IDDon
                //            select dthuoc).Distinct().ToList();

                //bool ktra = true;// kiểm tra xem có lưu được vào bảng số phiếu lĩnh ko
                //int tang = 0;
                //while (ktra)
                //{

                //    _soPL = _TaoSPL(_makp);
                //    _soPL = tang + _soPL;
                //    SoPL soPLMoi = new SoPL();
                //    try
                //    {                           
                //        soPLMoi.MaKP = 0;
                //        soPLMoi.SoPL1 = _soPL;
                //        soPLMoi.Status = 0;
                //        soPLMoi.PhanLoai = 1;
                //        soPLMoi.DSIdDonct = string.Join(",", qdtct.Select(p => p.dtct.IDDonct.ToString()));
                //        soPLMoi.NoiTru = -1;
                //        _dataContext.SoPLs.Add(soPLMoi);
                //        _dataContext.SaveChanges();
                //        ktra = false;
                //    }
                //    catch (Exception ex)
                //    {

                //        _dataContext.SoPLs.Remove(soPLMoi);
                //        ktra = true;
                //        if (_soPL - tang == _TaoSPL(_makp))// số phiếu lĩnh trong đơn thuốc chi tiết chưa thay đổi
                //            tang++;
                //        else
                //            tang = 0;

                //    }
                //}
                #endregion
                foreach (var item in a.arrIdonct)
                {
                    var donthuoc = _dataContext.DThuoccts.Single(p => p.IDDonct == item);
                    donthuoc.SoPL = _soPL;
                }
                _dataContext.SaveChanges();
                _ds[0] = _soPL.ToString();
                _ds[1] = _makp.ToString();
                _ds[2] = a.PLoai != null ? a.PLoai.ToString() : ""; ;
                lSoPL.Add(_ds);
            }
            return lSoPL;

        }
        public bool TaoPL(int id, int sopl)// xoá xuất dược với phiếu lĩnh cho các bệnh nhân trong khoa/ cho khoa
        {
            iddon = id;
            _sopl = sopl;
            var xoact4 = _dataContext.NhapDcts.Where(p => p.IDNhap == iddon).ToList();
            foreach (var xoa in xoact4)
            {
                var _xoa = _dataContext.NhapDcts.Single(p => p.IDNhapct == (xoa.IDNhapct));
                _dataContext.NhapDcts.Remove(_xoa);
                _dataContext.SaveChanges();
            }
            var xoac4 = _dataContext.NhapDs.Single(p => p.IDNhap == iddon);
            _dataContext.NhapDs.Remove(xoac4);
            _dataContext.SaveChanges();
            var iddt4 = (_dataContext.DThuoccts.Where(p => p.SoPL == _sopl).Select(p => p.IDDon)).Distinct().ToList();
            foreach (var id1 in iddt4)
            {
                //var dthuoc4 = _dataContext.DThuocs.Single(p => p.IDDon == id1);
                //dthuoc4.Status = 0;
                List<DThuocct> ldtct = _dataContext.DThuoccts.Where(p => p.IDDon == id1).ToList();
                foreach (DThuocct dt in ldtct)
                {
                    if (dt.Status != -1)
                        dt.Status = 0;
                }
                _dataContext.SaveChanges();
            }
            return true;
        }
        public bool TaoPL(int pl)// huỷ đơn phiếu lĩnh
        {
            iddon = pl;
            var sua7 = _dataContext.DThuoccts.Where(p => p.SoPL == iddon).Select(p => p.IDDon).Distinct().ToList();
            foreach (int upd in sua7)
            {
                //var su = _dataContext.DThuocs.Single(p => p.IDDon == upd);
                List<DThuocct> ldtct = _dataContext.DThuoccts.Where(p => p.IDDon == upd).ToList();
                foreach (DThuocct dt in ldtct)
                {
                    if (dt.Status != -1)
                        dt.Status = 2;
                }
                //su.Status = 2;
                _dataContext.SaveChanges();
            }
            return true;
        }
        public bool HuyDon(int id)// huỷ đơn
        {
            iddon = id;
            //var sua6 = _dataContext.DThuocs.Single(p => p.IDDon == iddon);
            //sua6.Status = 2;

            List<DThuocct> ldtct = _dataContext.DThuoccts.Where(p => p.IDDon == iddon).ToList();
            foreach (DThuocct dt in ldtct)
            {
                if (dt.Status != -1)
                    dt.Status = 2;
            }
            _dataContext.SaveChanges();
            return true;
        }
        #region phiếu lĩnh đông y TT01
        public static void _InPhieuThuocDY_TT01(int _idDon)
        {
            //if (DungChung.Bien.MaBV == "20001")
            //{
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            BaoCao.Rep_PLThuoctheothang_TT44 rep = new BaoCao.Rep_PLThuoctheothang_TT44();
            frmIn frm = new frmIn();
            // List<Thuocthang> _BC = new List<Thuocthang>();
            // _BC.Clear();
            var a = DungChung.Bien.MaBV == "14017" ?
                    (from dt in Data.DThuocs.Where(p => p.IDDon == _idDon)
                     join dtct in Data.DThuoccts on dt.IDDon equals dtct.IDDon
                     join dv in Data.DichVus on dtct.MaDV equals dv.MaDV
                     group new { dt, dtct, dv } by new { GhiChuDT = dt.GhiChu, dt.KieuDon, dtct.GhiChu, dtct.MaDV, dv.TenDV, dv.DonVi, dv.TenRG, dt.NgayKe, dtct.Loai } into kq
                     select new
                     {
                         GhiChuDT = kq.Key.GhiChuDT,
                         kq.Key.KieuDon,
                         Madv = kq.Key.MaDV,
                         TenDV = kq.Key.TenDV,
                         kq.Key.NgayKe,
                         kq.Key.DonVi,
                         Soluong = kq.Sum(p => p.dtct.SoLuong),
                         Thanhtien = kq.Sum(p => p.dtct.ThanhTien),
                         kq.Key.Loai,
                         kq.Key.GhiChu
                     }).OrderBy(p => p.TenDV).ToList() :
                     (from dt in Data.DThuocs.Where(p => p.IDDon == _idDon)
                      join dtct in Data.DThuoccts on dt.IDDon equals dtct.IDDon
                      join dv in Data.DichVus on dtct.MaDV equals dv.MaDV
                      group new { dt, dtct, dv } by new { GhiChuDT = dt.GhiChu, dt.KieuDon, dtct.MaDV, dtct.GhiChu, dv.TenDV, dv.DonVi, dv.TenRG, dt.NgayKe, dtct.Loai } into kq
                      select new
                      {
                          GhiChuDT = kq.Key.GhiChuDT,
                          kq.Key.KieuDon,
                          Madv = kq.Key.MaDV,
                          TenDV = DungChung.Bien.MaBV == "24009" ? kq.Key.TenRG : kq.Key.TenDV,
                          kq.Key.NgayKe,
                          kq.Key.DonVi,
                          Soluong = kq.Sum(p => p.dtct.SoLuong),
                          Thanhtien = kq.Sum(p => p.dtct.ThanhTien),
                          kq.Key.Loai,
                          kq.Key.GhiChu
                      }).OrderBy(p => p.TenDV).ToList();
            if (a.Count > 0 && a.First().NgayKe != null)
            {
                DateTime ngayky = Convert.ToDateTime(a.First().NgayKe);
                if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "14017")
                {
                    rep.ngaythangky.Value = "Ngày " + ngayky.Day + " tháng " + ngayky.Month + " năm " + ngayky.Year;
                }
                else
                {
                    rep.ngaythangky.Value = "Ngày.....tháng.....năm 20...";
                }
            }
            string tungay = "          ";
            string denngay = "          ";
            string sothang = "";// số thang thuốc
            var qdt = Data.DThuocs.Where(p => p.IDDon == _idDon).OrderBy(p => p.NgayKe).ToList();
            if (qdt.Count > 0)
            {
                var qbs = (from dt in qdt join bs in Data.CanBoes on dt.MaCB equals bs.MaCB select bs.TenCB).FirstOrDefault();
                //sothang = qdt.Count == 0 ? "" : qdt.Count.ToString();
                sothang = a.Count == 0 ? "" : a.First().Loai.ToString();
                tungay = qdt.First().NgayKe.Value.ToString("dd/MM/yyyy");
                denngay = qdt.Last().NgayKe.Value.ToString("dd/MM/yyyy");

                if (sothang == "0")
                    sothang = "";
                if (DungChung.Bien.MaBV == "08204")
                    sothang = "5";
                if (qbs != null)
                    rep.cel_BacSy.Text = qbs;
                rep.TenBS.Value = qbs;
                // double songay =  (a.Last().NgayKe.Value - a.First().NgayKe.Value).TotalDays + 1;
            }
            tungay = "...................";
            denngay = "...................";

            rep.SoPL.Value = _idDon;
            if (a.Count > 0 && a.First().KieuDon != null && a.First().KieuDon == 2)
            {
                rep.xrTieuDe.Text = "PHIẾU TRẢ THUỐC THANG";
            }
            var bg = (from dt in Data.DThuocs.Where(p => p.IDDon == _idDon)
                      join bn in Data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                      join bnkb in Data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                      join kp in Data.KPhongs on dt.MaKP equals kp.MaKP
                      where (dt.MaKP == bnkb.MaKP)
                      select new { kp.TenKP, bnkb.GhiChu, bnkb.Buong, bnkb.Giuong, bn.MaBNhan, bn.Tuoi, bn.DChi, bn.GTinh, bn.SThe, bn.TenBNhan, bn.NoiTru, bnkb.ChanDoan, bnkb.IDKB, bn.MaCS }).FirstOrDefault();
            string thuocsudung = "";
            string Luuy = "", cachS = "", cachU = "";
            if (bg != null)
            {
                if (DungChung.Bien.MaBV == "14017")
                {
                    rep.Khoa.Value = DungChung.Bien.TenCQ.ToUpper();


                }

                int _int_maBN = bg.MaBNhan;
                int idkb = bg.IDKB;
                if (!string.IsNullOrEmpty(a.First().GhiChuDT))
                {
                    if (a.First().GhiChuDT.Contains(";"))
                    {
                        string[] _arrr = a.First().GhiChuDT.Split(';');
                        if (_arrr.Count() >= 3)
                            thuocsudung = string.Format("từ ngày {0} đến ngày {1} Số thang {2}", _arrr[1], _arrr[2], _arrr[0]);
                        if (_arrr.Count() >= 4)
                            Luuy = _arrr[3];
                        if (_arrr.Count() >= 5)
                        {
                            if (DungChung.Bien.MaBV == "14017")
                            {
                                string[] _arr = _arrr[4].Split('-');
                                if (_arr.Count() == 2)
                                    cachS = "- " + _arr[1];
                                if (_arr.Count() >= 3)
                                    cachS = "- " + _arr[1] + "- " + _arr[2];

                            }
                            else
                            {
                                cachS = _arrr[4];
                            }

                        }

                        if (_arrr.Count() >= 6)
                            cachU = _arrr[5];
                    }
                    else
                    {
                        thuocsudung = a.First().GhiChuDT.ToString();
                    }
                }
                if (bg.NoiTru == 1)
                    rep.xrTieuDe.Text = "ĐƠN THUỐC THUỐC THANG ĐIỀU TRỊ NỘI TRÚ";
                else
                    rep.xrTieuDe.Text = "ĐƠN THUỐC THUỐC THANG ĐIỀU TRỊ NGOẠI TRÚ";
                rep.Tuoi.Value = "Tuổi: " + (DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(Data, _int_maBN) : DungChung.Ham.TuoitheoThang(Data, bg.MaBNhan, DungChung.Bien.formatAge));
                rep.Hoten.Value = "Họ tên: " + bg.TenBNhan;
                rep.TenBN.Value = bg.TenBNhan;
                rep.Diachi.Value = "Địa chỉ: " + bg.DChi;
                rep.celNguoiBenh.Text = DungChung.Bien.MaBV == "14017" ? bg.TenBNhan : "";

                if (DungChung.Bien.MaBV == "14017")
                {
                    string[] _MaICDarr = DungChung.Ham.getMaICDarrFull_SL(Data, _int_maBN, DungChung.Bien.GetICD, idkb);
                    string[] icd = _MaICDarr[0].Split(';');
                    string[] tenicd = _MaICDarr[1].Split(';');
                    string lydo = "";
                    if (icd.Length <= tenicd.Length)
                    {
                        for (int i = 0; i < icd.Length; i++)
                        {
                            lydo += " " + icd[i] + "-" + tenicd[i] + ";";
                        }
                        if (icd.Length < tenicd.Length)
                        {
                            int cut1 = tenicd.Length - icd.Length;
                            int cut = tenicd.Length - cut1;
                            string mab1k = DungChung.Ham.FreshString(string.Join(";", tenicd.Skip(cut)));
                            lydo += " " + mab1k + ";";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < tenicd.Length; i++)
                        {
                            lydo += " " + icd[i] + "-" + tenicd[i] + ";";
                        }
                    }

                    rep.Chandoan.Value = "Chẩn đoán: " + DungChung.Ham.FreshString(lydo);
                }
                else
                {
                    rep.Chandoan.Value = "Chẩn đoán: " + (DungChung.Bien.MaBV == "14018" ? DungChung.Ham.GetChanDoanKB(Data, _int_maBN) : DungChung.Ham.getMaICDarr(Data, _int_maBN, DungChung.Bien.GetICD, 0)[1]);
                }
                rep.MaICD.Value = "Mã bệnh: " + DungChung.Ham.getMaICDarr(Data, _int_maBN, DungChung.Bien.GetICD, 0)[0];
                rep.Gtinh.Value = (bg.GTinh == null || bg.GTinh.Value == 0) ? ("Nam/Nữ: Nữ") : ("Nam/Nữ: Nam");

                if (!string.IsNullOrEmpty(a.First().GhiChuDT))
                    rep.paraSoNgay.Value = "Thuốc sử dụng " + thuocsudung;
                else
                    rep.paraSoNgay.Value = "Thuốc sử dụng từ ngày: " + tungay + " đến ngày: " + denngay + " . Số thang: " + sothang;
                if (bg.SThe != null && bg.SThe.Length == 15)
                {
                    rep.celBH.Text = "X";
                    rep.celBH1.Text = bg.SThe.Substring(0, 3);
                    rep.cel_BH2.Text = bg.SThe.Substring(3, 2);
                    rep.cel_BH3.Text = bg.SThe.Substring(5, 2);
                    rep.cel_BH4.Text = bg.SThe.Substring(7, 3);
                    rep.cel_BH5.Text = bg.SThe.Substring(10, 5);
                    rep.colMaDK.Text = bg.MaCS;
                }
                else
                    rep.cel_VP.Text = "X";
            }
            else
            {
                rep.Tuoi.Value = "Tuổi:";
                rep.Diachi.Value = "Địa chỉ:";
                rep.Chandoan.Value = "Chẩn đoán:";
                rep.Gtinh.Value = "Nam/Nữ:";
                rep.Hoten.Value = "Họ tên:";
            }
            //rep.celMS.Text = "MS: " + _idDon.ToString();
            rep.CachS.Value = "Cách sắc thuốc: " + "\n" + cachS;
            rep.CachU.Value = "Cách uống: " + cachU;
            rep.LuuY.Value = "Những điều cần lưu ý: " + Luuy;
            rep.NgayHenKham.Value = "Hẹn ngày khám lại (nếu cần thiết):...............................................";
            //rep.HDSD.Value = DungChung.Ham.FreshString(HDSD);
            rep.BindingData();
            rep.DataSource = a;
            rep.CreateDocument();
            //rep.DataMember = "Table";
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
            //}
            //else
            //{
            //    QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //    BaoCao.Rep_PLThuoctheothang_TT01 rep = new BaoCao.Rep_PLThuoctheothang_TT01();
            //    frmIn frm = new frmIn();
            //    // List<Thuocthang> _BC = new List<Thuocthang>();
            //    // _BC.Clear();
            //    var a = (from dt in Data.DThuocs.Where(p => p.IDDon == _idDon)
            //             join dtct in Data.DThuoccts on dt.IDDon equals dtct.IDDon
            //             join dv in Data.DichVus on dtct.MaDV equals dv.MaDV
            //             group new { dt, dtct, dv } by new { dt.GhiChu, dt.KieuDon, dtct.MaDV, dv.TenDV, dv.DonVi, dv.TenRG, dt.NgayKe, dtct.Loai } into kq
            //             select new
            //             {
            //                 kq.Key.GhiChu,
            //                 kq.Key.KieuDon,
            //                 Madv = kq.Key.MaDV,
            //                 TenDV = DungChung.Bien.MaBV == "24009" ? kq.Key.TenRG : kq.Key.TenDV,
            //                 kq.Key.NgayKe,
            //                 kq.Key.DonVi,
            //                 Soluong = kq.Sum(p => p.dtct.SoLuong),
            //                 Thanhtien = kq.Sum(p => p.dtct.ThanhTien),
            //                 kq.Key.Loai
            //             }).OrderBy(p => p.TenDV).ToList();
            //    if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30007")
            //    {
            //        rep.SubBand2.Visible = false;
            //        rep.SubBand1.Visible = true;
            //    }
            //    else
            //    {
            //        rep.SubBand2.Visible = true;
            //        rep.SubBand1.Visible = false;
            //    }
            //    if (a.Count > 0 && a.First().NgayKe != null)
            //    {
            //        DateTime ngayky = Convert.ToDateTime(a.First().NgayKe);
            //        if (DungChung.Bien.MaBV == "27001")
            //        {
            //            rep.ngaythangky.Value = "Ngày " + ngayky.Day + " tháng " + ngayky.Month + " năm " + ngayky.Year;
            //        }
            //        else
            //        {
            //            rep.ngaythangky.Value = "Ngày.....tháng.....năm 20...";
            //        }
            //    }
            //    string tungay = "          ";
            //    string denngay = "          ";
            //    string sothang = "";// số thang thuốc
            //    var qdt = Data.DThuocs.Where(p => p.IDDon == _idDon).OrderBy(p => p.NgayKe).ToList();
            //    if (qdt.Count > 0)
            //    {
            //        var qbs = (from dt in qdt join bs in Data.CanBoes on dt.MaCB equals bs.MaCB select bs.TenCB).FirstOrDefault();
            //        //sothang = qdt.Count == 0 ? "" : qdt.Count.ToString();
            //        sothang = a.Count == 0 ? "" : a.First().Loai.ToString();
            //        tungay = qdt.First().NgayKe.Value.ToString("dd/MM/yyyy");
            //        denngay = qdt.Last().NgayKe.Value.ToString("dd/MM/yyyy");

            //        if (sothang == "0")
            //            sothang = "";
            //        if (DungChung.Bien.MaBV == "08204")
            //            sothang = "5";
            //        if (qbs != null)
            //            rep.cel_BacSy.Text = qbs;
            //        // double songay =  (a.Last().NgayKe.Value - a.First().NgayKe.Value).TotalDays + 1;
            //    }
            //    tungay = "...................";
            //    denngay = "...................";

            //    rep.SoPL.Value = _idDon;
            //    if (a.Count > 0 && a.First().KieuDon != null && a.First().KieuDon == 2)
            //    {
            //        rep.xrTieuDe.Text = "PHIẾU TRẢ THUỐC THANG";
            //    }
            //    var bg = (from dt in Data.DThuocs.Where(p => p.IDDon == _idDon)
            //              join bn in Data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
            //              join bnkb in Data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
            //              join kp in Data.KPhongs on dt.MaKP equals kp.MaKP
            //              where (dt.MaKP == bnkb.MaKP)
            //              select new { kp.TenKP, bnkb.GhiChu, bn.MaBNhan, bn.Tuoi, bn.DChi, bn.GTinh, bn.SThe, bn.TenBNhan, bn.NoiTru, bnkb.ChanDoan, bn.MaCS }).FirstOrDefault();
            //    string thuocsudung = "";
            //    string HDSD = "";
            //    if (bg != null)
            //    {
            //        if (DungChung.Bien.MaBV == "01830")
            //        {
            //            rep.Khoa.Value = bg.TenKP;
            //            rep.MauSo.Value = "(Thông tư 01/2016/TT-BYT)";
            //        }
            //        int _int_maBN = bg.MaBNhan;

            //        if (!string.IsNullOrEmpty(a.First().GhiChu))
            //        {
            //            if (a.First().GhiChu.Contains(";"))
            //            {
            //                thuocsudung = a.First().GhiChu.Split(';')[0];
            //                HDSD = a.First().GhiChu.Split(';')[1];
            //            }
            //            else
            //            {
            //                thuocsudung = a.First().GhiChu.ToString();
            //            }
            //        }
            //        rep.Tuoi.Value = "Tuổi: " + DungChung.Ham.TuoitheoThang(Data, bg.MaBNhan, DungChung.Bien.formatAge);
            //        rep.Hoten.Value = "Họ tên: " + bg.TenBNhan;
            //        rep.Diachi.Value = "Địa chỉ: " + bg.DChi;
            //        rep.Chandoan.Value = "Chẩn đoán: " + DungChung.Ham.getMaICDarr(Data, _int_maBN, DungChung.Bien.GetICD, 0)[1];
            //        if (bg.NoiTru == 1)
            //            rep.xrTieuDe.Text = "ĐƠN THUỐC THANG NỘI TRÚ";
            //        else
            //            rep.xrTieuDe.Text = "ĐƠN THUỐC THANG NGOẠI TRÚ";
            //        rep.Gtinh.Value = (bg.GTinh == null || bg.GTinh.Value == 0) ? ("Nam/Nữ: Nữ") : ("Nam/Nữ: Nam");

            //        if (!string.IsNullOrEmpty(a.First().GhiChu))
            //            rep.paraSoNgay.Value = "Thuốc sử dụng " + thuocsudung;
            //        else
            //            rep.paraSoNgay.Value = "Thuốc sử dụng từ ngày: " + tungay + " đến ngày: " + denngay + " . Số thang: " + sothang;
            //        if (bg.SThe != null && bg.SThe.Length == 15)
            //        {
            //            rep.celBH.Text = "X";
            //            rep.celBH1.Text = bg.SThe.Substring(0, 3);
            //            rep.cel_BH2.Text = bg.SThe.Substring(3, 2);
            //            rep.cel_BH3.Text = bg.SThe.Substring(5, 2);
            //            rep.cel_BH4.Text = bg.SThe.Substring(7, 3);
            //            rep.cel_BH5.Text = bg.SThe.Substring(10, 5);
            //            rep.colMaDK.Text = bg.MaCS;
            //        }
            //        else
            //            rep.cel_VP.Text = "X";
            //    }
            //    else
            //    {
            //        rep.Tuoi.Value = "Tuổi:";
            //        rep.Diachi.Value = "Địa chỉ:";
            //        rep.Chandoan.Value = "Chẩn đoán:";
            //        rep.Gtinh.Value = "Nam/Nữ:";
            //        rep.Hoten.Value = "Họ tên:";
            //    }
            //    rep.celMS.Text = "MS: " + _idDon.ToString();
            //    rep.HDSD.Value = DungChung.Ham.FreshString(HDSD);
            //    rep.BindingData();
            //    rep.DataSource = a;
            //    rep.CreateDocument();
            //    //rep.DataMember = "Table";
            //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //    frm.ShowDialog();
            //}

        }
        #endregion
        #region phiếu lĩnh đông y TT01 MẪU A5
        public static void _InPhieuThuocDY_TT01_A5(int _idDon)
        {

            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            BaoCao.Rep_PLThuoctheothang_TT01_A5 rep = new BaoCao.Rep_PLThuoctheothang_TT01_A5();
            frmIn frm = new frmIn();
            // List<Thuocthang> _BC = new List<Thuocthang>();
            // _BC.Clear();
            var a = (from dt in Data.DThuocs.Where(p => p.IDDon == _idDon)
                     join dtct in Data.DThuoccts on dt.IDDon equals dtct.IDDon
                     join dv in Data.DichVus on dtct.MaDV equals dv.MaDV
                     group new { dt, dtct, dv } by new { dt.GhiChu, dt.KieuDon, dtct.MaDV, dv.TenDV, dv.DonVi, dv.TenRG, dt.NgayKe, dtct.Loai, dv.MaTam } into kq
                     select new
                     {
                         kq.Key.Loai,
                         kq.Key.GhiChu,
                         kq.Key.KieuDon,
                         Madv = kq.Key.MaDV,
                         TenDV = DungChung.Bien.MaBV == "24009" ? kq.Key.TenRG : kq.Key.TenDV,
                         kq.Key.DonVi,
                         Soluong = kq.Sum(p => p.dtct.SoLuong),
                         Thanhtien = kq.Sum(p => p.dtct.ThanhTien),
                         kq.Key.NgayKe,
                         kq.Key.MaTam
                     }).OrderBy(p => p.TenDV).ToList();
            string tungay = "          ";
            string denngay = "          ";
            string sothang = "";// số thang thuốc
            var qdt = Data.DThuocs.Where(p => p.IDDon == _idDon).OrderBy(p => p.NgayKe).ToList();
            if (qdt.Count > 0)
            {
                var qbs = (from dt in qdt join bs in Data.CanBoes on dt.MaCB equals bs.MaCB select bs.TenCB).FirstOrDefault();
                sothang = qdt.Count == 0 ? "" : qdt.Count.ToString();
                tungay = qdt.First().NgayKe.Value.ToString("dd/MM/yyyy");
                denngay = qdt.Last().NgayKe.Value.ToString("dd/MM/yyyy");
                if (DungChung.Bien.MaBV == "14017")
                {
                    if (qdt.First().GhiChu != "")
                    {
                        sothang = qdt.First().GhiChu.Split(';')[0];
                        tungay = qdt.First().GhiChu.Split(';')[1];
                        denngay = qdt.First().GhiChu.Split(';')[2];

                    }
                }

                if (sothang == "0")
                    sothang = "";
                if (DungChung.Bien.MaBV == "08204")
                    sothang = "5";
                if (qbs != null)
                {
                    rep.celBacSy.Text = qbs;
                    rep.celTenBS.Text = "BS: " + qbs;
                }
                // double songay =  (a.Last().NgayKe.Value - a.First().NgayKe.Value).TotalDays + 1;
            }
            //if (DungChung.Bien.MaBV != "24297") // ngay tu, ngay den '.....' co san tu truoc
            //{
            //    tungay = "...................";
            //    denngay = "...................";
            //}


            rep.SoPL.Value = _idDon;
            if (a.Count > 0 && a.First().KieuDon != null && a.First().KieuDon == 2)
            {
                rep.xrTieuDe.Text = "PHIẾU TRẢ THUỐC THANG";
            }
            var bg = (from dt in Data.DThuocs.Where(p => p.IDDon == _idDon)
                      join bn in Data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                      join bnkb in Data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                      join kp in Data.KPhongs on dt.MaKP equals kp.MaKP
                      where (dt.MaKP == bnkb.MaKP)
                      select new { kp.IsDongY, bnkb.GhiChu, bn.MaBNhan, bn.Tuoi, bn.DChi, bn.GTinh, bn.SThe, bn.TenBNhan, bn.NoiTru, bnkb.ChanDoan, bn.MaCS }).FirstOrDefault();
            if (bg != null)
            {
                int _int_maBN = bg.MaBNhan;
                rep.celMS.Text = DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "14017" ? "Mã bệnh: " + DungChung.Ham.getMaICDarr(Data, _int_maBN, DungChung.Bien.GetICD, 0)[0] : "MS: " + _idDon.ToString();
                rep.txtMaICD.Text = "Mã bệnh: " + DungChung.Ham.getMaICDarr(Data, _int_maBN, DungChung.Bien.GetICD, 0)[0];
                rep.Tuoi.Value = "Tuổi: " + DungChung.Ham.TuoitheoThang(Data, bg.MaBNhan, DungChung.Bien.formatAge);
                rep.Hoten.Value = "Họ tên: " + bg.TenBNhan.ToUpper();
                rep.Diachi.Value = "Địa chỉ: " + bg.DChi;
                string mabenh = "";
                if (bg.IsDongY == true && DungChung.Bien.MaBV == "24297")
                {
                    rep.Chandoan.Value = "Chẩn đoán: " + DungChung.Ham.GetChanDoanKB_24297(Data, _int_maBN);
                    string lICD = DungChung.Ham.getMaICDarr(Data, _int_maBN, DungChung.Bien.GetICD, 0)[0];
                    var _licd10 = Data.ICD10.ToList();
                    string[] arrICD = lICD.Split(new char[] { ';' });
                    if (arrICD.Count() > 0)
                    {
                        for (int i = 0; i < arrICD.Count(); i++)
                        {
                            if (!string.IsNullOrEmpty(arrICD[i]) && arrICD[i] != " ")
                                mabenh += DungChung.Ham.GetMaYHCT(arrICD[i].Trim(), _licd10)[0] + ";";
                        }
                    }
                    rep.celMS.Text = "Mã bệnh: " + mabenh;
                }
                else
                {
                    rep.Chandoan.Value = "Chẩn đoán: " + DungChung.Ham.getMaICDarr(Data, _int_maBN, DungChung.Bien.GetICD, 0)[1];
                }

                if (bg.NoiTru == 1)
                    rep.xrTieuDe2.Text = rep.xrTieuDe.Text = "ĐƠN THUỐC THUỐC THANG ĐIỀU TRỊ NỘI TRÚ";
                else
                    rep.xrTieuDe2.Text = rep.xrTieuDe.Text = "ĐƠN THUỐC THUỐC THANG ĐIỀU TRỊ NGOẠI TRÚ";
                rep.Gtinh.Value = (bg.GTinh == null || bg.GTinh.Value == 0) ? ("Giới tính: Nữ") : ("Giới tính: Nam");

                if (bg.SThe != null && bg.SThe.Length == 15)
                {
                    rep.celBH.Text = "X";
                    rep.celBH2.Text = "X";
                    rep.celBH1.Text = bg.SThe.Substring(0, 3);
                    rep.cel_BH2.Text = bg.SThe.Substring(3, 2);
                    rep.cel_BH3.Text = bg.SThe.Substring(5, 2);
                    rep.cel_BH4.Text = bg.SThe.Substring(7, 3);
                    rep.cel_BH5.Text = bg.SThe.Substring(10, 5);
                    rep.colMaDK.Text = bg.MaCS;
                }
                else
                {
                    rep.cel_VP.Text = "X";
                    rep.celVP2.Text = "X";
                }
            }
            else
            {
                rep.celMS.Text = "MS: " + _idDon.ToString();
                rep.Tuoi.Value = "Tuổi:";
                rep.Diachi.Value = "Địa chỉ:";
                rep.Chandoan.Value = "Chẩn đoán:";
                rep.Chandoan.Value = "Nam/Nữ:";
                rep.Hoten.Value = "Họ tên:";
            }
            if (DungChung.Bien.MaBV == "24297")
                sothang = a.First().Loai.ToString();
            denngay = (Convert.ToDateTime(tungay).AddDays(Convert.ToDouble(sothang) - 1)).ToString("dd/MM/yyyy");
            rep.txtSoNgay1.Text = "Thuốc sử dụng từ ngày: " + tungay + " đến ngày: " + denngay;
            rep.txtSoThang.Text = sothang;
            if (!string.IsNullOrEmpty(bg.GhiChu))
            {
                rep.paraSoNgay.Value = DungChung.Bien.MaBV == "24297" ? "Thuốc sử dụng từ ngày: " + tungay + " đến ngày: " + denngay + " . Số thang: " + sothang : "Thuốc sử dụng " + bg.GhiChu;
            }
            else
            {
                rep.paraSoNgay.Value = "Thuốc sử dụng từ ngày: " + tungay + " đến ngày: " + denngay + " . Số thang: " + sothang;
            }
            if (a.Count > 0)
                rep.HDSD.Value = a.First().GhiChu;
            if (DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "14017")
                rep.Ngaythang.Value = "Ngày " + Convert.ToDateTime(a.First().NgayKe).Day + " tháng " + Convert.ToDateTime(a.First().NgayKe).Month + " năm " + Convert.ToDateTime(a.First().NgayKe).Year;
            rep.celTenBN.Text = DungChung.Bien.MaBV == "14017" ? bg.TenBNhan : "";
            rep.BindingData();
            rep.DataSource = a;
            rep.CreateDocument();
            //rep.DataMember = "Table";
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();


        }
        #endregion

        public class BC
        {
            public string TenDV { set; get; }
            public string TenHC { set; get; }
            public string TenRG { set; get; }
            public string HamLuong { set; get; }
            public string MaTam { set; get; }
            public int MaDV { set; get; }
            public string SoDK { set; get; }
            public string DonVi { set; get; }
            public double? SoLuong { set; get; }
            public double? ThanhTien { set; get; }
            public double? DonGia { set; get; }
            public int? LoaiDuoc { set; get; }

            public string SoLo { get; set; }

            public DateTime? HanDung { get; set; }
            public string TenRGDV { get; set; }

            public int LoaiDV { get; set; }

            public string TenRGTN { get; set; }

            public string GhiChu { set; get; }

        }

        public void InPhieu_14007(string[] sql_kp, int stt)
        {
            int _soPL = Convert.ToInt32(sql_kp[0]);
            int _makp = 0;
            if (sql_kp.Length > 0 && sql_kp[1] != null)
                _makp = String.IsNullOrEmpty(sql_kp[1]) ? 0 : Convert.ToInt32(sql_kp[1]);
            if (DungChung.Bien.MaBV != "19048")
                _makp = 0;
            _status = stt;
            bool mauA5 = false;// in mẫu A5 
            string _maCQCQ = "";
            var qCQCQ = _dataContext.BenhViens.Where(panelControl1 => panelControl1.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (qCQCQ != null)
                _maCQCQ = qCQCQ.MaChuQuan;

            List<DonGiaDV> qDonGiaDV = new List<DonGiaDV>();
            try
            {
                qDonGiaDV = _dataContext.DonGiaDVs.Where(p => p.Status == true).ToList();
            }
            catch (Exception ex)
            { }
            switch (_status)
            {
                case 2:
                    #region case2
                    string _dtuong = "";
                    var ktdtuong3 = (from bn in _dataContext.BenhNhans
                                     join dtbn in _dataContext.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                                     join dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp) on bn.MaBNhan equals dt.MaBNhan
                                     join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                     select new { dt.NgayKe, bn.IDDTBN, bn.DTuong, bn.NoThe, bn.NoiTru, dtbn.MoTa, dtct.Loai }).ToList();
                    var ktdtuong = (from bn in ktdtuong3
                                    group new { bn } by new { bn.IDDTBN, bn.DTuong, bn.NoThe, bn.NoiTru } into kq
                                    select new { kq.Key.IDDTBN, kq.Key.DTuong, kq.Key.NoThe, kq.Key.NoiTru, SoThang = kq.Max(p => p.bn.Loai), }).ToList();
                    if (ktdtuong.Count > 1)
                    {
                        _dtuong = "";
                    }
                    else
                    {
                        if (ktdtuong.Count > 0 && ktdtuong.First().NoThe == true)
                        {
                            _dtuong = "(Dành cho đối tượng dịch vụ _ nợ thẻ BHYT) \n";
                        }
                        else
                        {
                            var ktdtuong2 = (from bn in ktdtuong3
                                             select new { bn.MoTa }).ToList();
                            if (ktdtuong2.Count > 0)
                            {
                                _dtuong = "(Dành cho đối tượng " + ktdtuong2.First().MoTa + " ) \n";
                            }
                        }
                    }

                    var ngay = ktdtuong3.Select(p => p.NgayKe).OrderBy(p => p.Value).ToList();
                    //var sothang = ktdtuong.Select(p => p.SoThang).ToList();
                    //int sthang = 1;
                    //if(sothang.Count > 0)
                    //{
                    //sthang = sothang.First();
                    //}
                    string ngay1 = "";
                    string ngay2 = "";
                    if (ngay.Count > 0)
                    {
                        ngay1 = ngay.First().Value.ToShortDateString();
                        ngay2 = ngay.Last().Value.ToShortDateString();
                    }
                    frmIn frm = new frmIn();
                    //thuốc thường: LoaiDV = 0;
                    //Hóa chất, LoaiDV = 1;
                    //vật tư y tế, LoaiDV = 2
                    //Thuốc gây nghiện, LoaiDV = 3
                    //Thuốc hướng tâm thần, LoaiDV = 4
                    //Thuốc trẻ em, LoaiDV = 5
                    //Thuốc đông y, LoaiDV = 6

                    var bph0 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on kd.IDDon equals dtct.IDDon
                                join kp in _dataContext.KPhongs
                                    on kd.MaKP equals kp.MaKP
                                join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                                join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                select new
                                {
                                    kp.TenKP,
                                    kd.LoaiDuoc,
                                    kd.MaKXuat,
                                    kd.MaKP,
                                    kd.KieuDon,
                                    tn.TenRG,
                                    kd.NgayKe,
                                    dtct.XHH
                                }).ToList();

                    if (ngay1 == "" && ngay2 == "")
                    {
                        var ngayKhoLinh = bph0.Select(p => p.NgayKe).OrderBy(p => p.Value).ToList();

                        if (ngayKhoLinh.Count > 0)
                        {
                            ngay1 = ngayKhoLinh.First().Value.ToShortDateString();
                            ngay2 = ngayKhoLinh.Last().Value.ToShortDateString();
                        }
                    }
                    var bph = (from a in bph0
                               select new
                               {
                                   a.TenKP,
                                   a.MaKXuat,
                                   a.KieuDon,
                                   a.MaKP,
                                   a.TenRG,
                                   a.XHH,
                                   LoaiDV = a.TenRG.Contains("Thuốc thường") ? 0 : (a.TenRG.Contains("Hóa chất") ? 1 : (a.TenRG.Contains("Vật tư y tế") ? 2 : (a.TenRG.Contains("Thuốc gây nghiện") ? 3 : (a.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (a.TenRG.Contains("Thuốc trẻ em") ? 5 : (a.TenRG.Contains("Thuốc đông y") ? 6 : 0))))))
                               }).ToList();
                    if (bph.Count > 0)
                    {
                        int kieudon = 0;
                        kieudon = bph.First().KieuDon.Value;

                        List<int> lLoaiDuocCt = bph.Select(p => p.LoaiDV).Distinct().ToList();

                        #region trả thuốc
                        #region Dùng form trả thuốc hoặc trả dược cho bệnh viên 30002
                        if ((kieudon == 2 || kieudon == 4) && DungChung.Bien.MaBV == "30002")
                        {
                            if ((lLoaiDuocCt.Contains(3) || lLoaiDuocCt.Contains(4)))// thuốc gây nghiện hướng tâm thần
                            {
                                #region 30002
                                if (DungChung.Bien.MaBV == "30002")
                                {

                                    var q0 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                              join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                              join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                              join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                              group new { kdct, dv, kd, tn } by new { dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, dv.TenRG, TenRGTN = tn.TenRG } into kq
                                              select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV)), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc, TenRG = kq.Key.TenRGTN }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                    var q = (from a in q0
                                             select new
                                             {
                                                 a.TenDV,
                                                 a.DonGia,
                                                 a.DonVi,
                                                 a.LoaiDuoc,
                                                 a.MaDV,
                                                 a.MaTam,
                                                 a.SoDK,
                                                 a.SoLuong,
                                                 a.ThanhTien,
                                                 LoaiDV = a.TenRG.Contains("Thuốc thường") ? 0 : (a.TenRG.Contains("Hóa chất") ? 1 : (a.TenRG.Contains("Vật tư y tế") ? 2 : (a.TenRG.Contains("Thuốc gây nghiện") ? 3 : (a.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (a.TenRG.Contains("Thuốc trẻ em") ? 5 : (a.TenRG.Contains("Thuốc đông y") ? 6 : 0))))))
                                             }).ToList();
                                    if (q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Count() > 0)
                                    {
                                        BaoCao.PhieutrathuocGNHTT_A5 rep = new BaoCao.PhieutrathuocGNHTT_A5();
                                        //rep.Nguo.Value = DungChung.Bien.NguoiLapBieu;
                                        if (DungChung.Bien.MaBV == "12001")
                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                        rep.MauSo.Value = "MS:.../BV-01";

                                        rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        this.Dispose();
                                    }
                                }
                                #endregion
                            }
                            if (lLoaiDuocCt.Contains(0) || lLoaiDuocCt.Contains(1) || lLoaiDuocCt.Contains(2) || lLoaiDuocCt.Contains(5) || lLoaiDuocCt.Contains(6))
                            {
                                #region PLThuoc VTYT 30002
                                if (DungChung.Bien.MaBV == "30002")
                                {


                                    var q = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                             join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                             join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                             join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                             group new { kdct, dv, kd, tn } by new { dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, dv.TenRG, kd.LoaiDuoc, TenRGTN = tn.TenRG } into kq
                                             select new
                                             {
                                                 TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV)),
                                                 kq.Key.MaTam,
                                                 kq.Key.MaDV,
                                                 kq.Key.SoDK,
                                                 DonVi = kq.Key.DonVi,
                                                 SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1),
                                                 ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1),
                                                 DonGia = kq.Key.DonGia,
                                                 LoaiDuoc = kq.Key.LoaiDuoc,
                                                 LoaiDV = kq.Key.TenRGTN.Contains("Thuốc thường") ? 0 : (kq.Key.TenRGTN.Contains("Hóa chất") ? 1 : (kq.Key.TenRGTN.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRGTN.Contains("Thuốc đông y") ? 6 : 0))))))
                                             }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                    if (q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).Count() > 0)
                                    {
                                        BaoCao.PhieuTrathuocVTYT_A5 rep = new BaoCao.PhieuTrathuocVTYT_A5();
                                        rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                        rep.MauSo.Value = "MS:05D/BV-01";


                                        rep.DataSource = q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        this.Dispose();
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                        else
                            if (kieudon == 2 && (DungChung.Bien.MaBV != "30003" || (!lLoaiDuocCt.Contains(3) && !lLoaiDuocCt.Contains(4))))
                        {
                            if (DungChung.Bien.MaBV == "14018")
                            {
                                var q33 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                           join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                           join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                           join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                           select new { TenRGTN = tn.TenRG, kd.NgayKe, dv.SoDK, dv.TenRG, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, dv.TenDV, dv.TenHC, kdct, dv.MaTam, dv.HamLuong, LoaiDV = (kdct.LoaiDV == 3 || kdct.LoaiDV == 4) ? 1 : 0 }).ToList();


                                var q = (from kd in q33
                                         group new { kd } by new { kd.TenRGTN, kd.SoDK, kd.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kd.TenHC, kd.HamLuong, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.MaDV, kd.TenDV, kd.TenRG, SoLo = kd.kdct.SoLo ?? "" } into kq
                                         select new BC
                                         {
                                             HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Key.SoLo),
                                             SoLo = kq.Key.SoLo,
                                             MaDV = kq.Key.MaDV ?? 0,
                                             TenDV = kq.Key.TenDV,
                                             HamLuong = kq.Key.HamLuong,
                                             TenHC = kq.Key.TenHC,
                                             TenRG = kq.Key.TenRG,
                                             MaTam = kq.Key.MaTam,
                                             DonGia = kq.Key.DonGia,
                                             SoDK = kq.Key.SoDK,
                                             DonVi = kq.Key.DonVi,
                                             LoaiDuoc = kq.Key.LoaiDuoc,
                                             SoLuong = kq.Sum(p => p.kd.kdct.SoLuong) * (-1),
                                             ThanhTien = kq.Sum(p => p.kd.kdct.ThanhTien) * (-1)
                                         }).OrderBy(p => p.TenDV).ThenBy(p => p.DonVi).ThenBy(p => p.DonGia).ToList();

                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add("SoPL", _soPL);
                                string tenKhoNhan = "";
                                int idKN = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                var khoNhan = _dataContext.KPhongs.FirstOrDefault(p => p.MaKP == idKN);
                                if (khoNhan != null)
                                    tenKhoNhan = khoNhan.TenKP;
                                string tenKhoXuat = "";
                                int idKX = bph.First().MaKP == null ? 0 : bph.First().MaKP.Value;
                                var khoXuat = _dataContext.KPhongs.FirstOrDefault(p => p.MaKP == idKX);
                                if (khoXuat != null)
                                    tenKhoXuat = khoXuat.TenKP;
                                dic.Add("TenKhoNhan", tenKhoNhan);
                                dic.Add("TenKhoXuat", tenKhoXuat);
                                dic.Add("LyDoNhap", "Nhập trả");

                                dic.Add("SoTienChu", (DungChung.Ham.DocTienBangChu(q.Sum(o => o.ThanhTien) ?? 0, "") + " đồng"));

                                DungChung.Ham.Print(DungChung.PrintConfig.Rep_PhieuNhapKhoTraThuocThua_14018, q, dic, false);
                            }
                            else
                            {
                                if ((lLoaiDuocCt.Contains(3) || lLoaiDuocCt.Contains(4)))// thuốc gây nghiện hướng tâm thần
                                {
                                    #region 30007 _GN HTT _A5
                                    if (DungChung.Bien.MaBV == "30007")
                                    {

                                        var q0 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                  join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                  join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                  join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                  group new { kdct, dv, kd, tn } by new { dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, dv.TenRG, TenRGTN = tn.TenRG } into kq
                                                  select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV)), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc, TenRG = kq.Key.TenRGTN }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var q = (from a in q0
                                                 select new
                                                 {
                                                     a.TenDV,
                                                     a.DonGia,
                                                     a.DonVi,
                                                     a.LoaiDuoc,
                                                     a.MaDV,
                                                     a.MaTam,
                                                     a.SoDK,
                                                     a.SoLuong,
                                                     a.ThanhTien,
                                                     LoaiDV = a.TenRG.Contains("Thuốc thường") ? 0 : (a.TenRG.Contains("Hóa chất") ? 1 : (a.TenRG.Contains("Vật tư y tế") ? 2 : (a.TenRG.Contains("Thuốc gây nghiện") ? 3 : (a.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (a.TenRG.Contains("Thuốc trẻ em") ? 5 : (a.TenRG.Contains("Thuốc đông y") ? 6 : 0))))))
                                                 }).ToList();
                                        if (q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Count() > 0)
                                        {
                                            BaoCao.PhieutrathuocGNHTT_A5 rep = new BaoCao.PhieutrathuocGNHTT_A5();
                                            //rep.Nguo.Value = DungChung.Bien.NguoiLapBieu;
                                            if (DungChung.Bien.MaBV == "12001")
                                                rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                            rep.MauSo.Value = "MS:.../BV-01";

                                            rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    #endregion
                                    #region 01071
                                    else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                    {
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        var q0 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                  join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                  join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                  join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                  group new { kdct, dv, kd, tn } by new { TenRGTN = tn.TenRG, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.TenHC, kdct.DonVi, kdct.MaDV, dv.TenDV, dv.TenRG, kd.LoaiDuoc, dv.SoDK, SoLo = "" } into kq
                                                  select new BC { SoLo = kq.Key.SoLo, HanDung = null, TenDV = kq.Key.TenDV, HamLuong = kq.Key.HamLuong, TenHC = kq.Key.TenHC, TenRG = kq.Key.TenRG, TenRGTN = kq.Key.TenRGTN, MaDV = kq.Key.MaDV ?? 0, MaTam = kq.Key.MaTam, DonGia = kq.Key.DonGia, SoDK = kq.Key.SoDK, DonVi = kq.Key.DonVi, LoaiDuoc = kq.Key.LoaiDuoc, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1) }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var q = (from a in q0
                                                 select new
                                                 {
                                                     a.TenDV,
                                                     a.DonGia,
                                                     a.DonVi,
                                                     a.LoaiDuoc,
                                                     a.MaDV,
                                                     a.MaTam,
                                                     a.SoDK,
                                                     a.SoLuong,
                                                     a.ThanhTien,
                                                     a.HamLuong,
                                                     a.HanDung,
                                                     a.SoLo,
                                                     a.TenHC,
                                                     a.TenRG,

                                                     LoaiDV = a.TenRGTN.Contains("Thuốc thường") ? 0 : (a.TenRGTN.Contains("Hóa chất") ? 1 : (a.TenRGTN.Contains("Vật tư y tế") ? 2 : (a.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (a.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (a.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (a.TenRGTN.Contains("Thuốc đông y") ? 6 : 0))))))
                                                 }).ToList();
                                        if (q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Count() > 0)
                                        {
                                            BaoCao.PhieulinhthuocGNHTT2lien_01071 rep = new BaoCao.PhieulinhthuocGNHTT2lien_01071();
                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                            rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    #endregion
                                    #region BV khác
                                    else
                                    {

                                        var q0 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                  join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                  join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                  join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                  group new { kdct, dv, kd, tn } by new { TenRGTN = tn.TenRG, dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, dv.TenRG, kd.LoaiDuoc, dv.SoDK, SoLo = DungChung.Bien.MaBV == "27023" ? kdct.SoLo : "", HanDung = DungChung.Bien.MaBV == "27023" ? kdct.HanDung : null } into kq
                                                  //  select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : (DungChung.Bien.MaBV == "12122" ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG :kq.Key.TenDV))), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                                  select new BC { SoLo = kq.Key.SoLo, HanDung = kq.Key.HanDung, TenDV = kq.Key.TenDV, HamLuong = kq.Key.HamLuong, TenHC = kq.Key.TenHC, TenRG = kq.Key.TenRG, TenRGTN = kq.Key.TenRGTN, MaTam = kq.Key.MaTam, DonGia = kq.Key.DonGia, SoDK = kq.Key.SoDK, DonVi = kq.Key.DonVi, LoaiDuoc = kq.Key.LoaiDuoc, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1) }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var q = (from a in q0
                                                 select new BC
                                                 {
                                                     TenDV = a.TenDV,
                                                     DonGia = a.DonGia,
                                                     DonVi = a.DonVi,
                                                     LoaiDuoc = a.LoaiDuoc,
                                                     MaDV = a.MaDV,
                                                     MaTam = a.MaTam,
                                                     SoDK = a.SoDK,
                                                     SoLuong = a.SoLuong,
                                                     ThanhTien = a.ThanhTien,
                                                     HamLuong = a.HamLuong,
                                                     HanDung = a.HanDung,
                                                     SoLo = a.SoLo,
                                                     TenHC = a.TenHC,
                                                     TenRG = a.TenRG,

                                                     LoaiDV = a.TenRGTN.Contains("Thuốc thường") ? 0 : (a.TenRGTN.Contains("Hóa chất") ? 1 : (a.TenRGTN.Contains("Vật tư y tế") ? 2 : (a.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (a.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (a.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (a.TenRGTN.Contains("Thuốc đông y") ? 6 : 0))))))
                                                 }).ToList();
                                        foreach (BC a in q)
                                        {
                                            if (DungChung.Bien.MaBV == "30007")
                                            {
                                                a.TenDV = a.TenDV + " (" + a.TenHC + ": " + a.HamLuong + ") ";
                                            }
                                            else if (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009")
                                            {
                                                a.TenDV = a.TenDV + " " + a.HamLuong;
                                            }
                                            else if (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009")
                                            {
                                                a.TenDV = a.TenRG ?? "";
                                            }
                                            else if (DungChung.Bien.MaBV == "27023")
                                            {
                                                a.TenDV = a.TenDV + (string.IsNullOrEmpty(a.HamLuong) ? "" : " (" + a.HamLuong + ") ");
                                            }
                                            //if (DungChung.Bien.MaBV == "27023")
                                            //{
                                            //    var qDGdv = qDonGiaDV.Where(p => p.MaDV == a.MaDV && p.DonGiaN == a.DonGia).FirstOrDefault();
                                            //    if (qDGdv != null)
                                            //    {
                                            //        a.SoLo = qDGdv.SoLo;
                                            //        a.HanDung = qDGdv.HanDung;
                                            //    }
                                            //}
                                        }
                                        if (q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Count() > 0)
                                        {
                                            #region 24009
                                            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27023")
                                            {
                                                int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                                BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT(0);
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " Đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                rep.xrTableCell28.Text = "";
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                    case 2:
                                                        rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        break;
                                                }



                                                rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC GÂY NGHIỆN, THUỐC HƯỚNG TÂM THẦN, THUỐC TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:01D/BV-01";



                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #region bv 27023
                                            //else if (DungChung.Bien.MaBV == "27023")
                                            //{

                                            //    BaoCao.PhieutrathuocGNHTT_27023 rep = new BaoCao.PhieutrathuocGNHTT_27023();
                                            //    rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            //    rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            //    rep.SoPL.Value = _soPL.ToString();
                                            //    rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            //    rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            //    rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            //    rep.Khoa.Value = bph.First().TenKP;
                                            //    int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            //    var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                            //    if (tenkho.Count > 0)
                                            //        rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                            //    rep.MauSo.Value = "MS:.../BV-01";

                                            //    rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                            //    rep.BindingData();
                                            //    rep.CreateDocument();
                                            //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            //    frm.ShowDialog();
                                            //    this.Dispose();
                                            //}
                                            #endregion
                                            #region bv khác
                                            else
                                            {
                                                BaoCao.PhieutrathuocGNHTT rep = new BaoCao.PhieutrathuocGNHTT();
                                                //rep.Nguo.Value = DungChung.Bien.NguoiLapBieu;
                                                if (DungChung.Bien.MaBV == "12001")
                                                    rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                                rep.MauSo.Value = "MS:.../BV-01";

                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                                if (lLoaiDuocCt.Contains(0) || lLoaiDuocCt.Contains(1) || lLoaiDuocCt.Contains(2) || lLoaiDuocCt.Contains(5) || lLoaiDuocCt.Contains(6))
                                {

                                    #region BV khác
                                    var q33 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                               join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                               join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                               join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                               select new { TenRGTN = tn.TenRG, kd.NgayKe, dv.SoDK, dv.TenRG, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, dv.TenDV, dv.TenHC, kdct, dv.MaTam, dv.HamLuong, LoaiDV = (kdct.LoaiDV == 3 || kdct.LoaiDV == 4) ? 1 : 0 }).ToList();


                                    var q = (from kd in q33
                                             group new { kd } by new { kd.TenRGTN, kd.SoDK, kd.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kd.TenHC, kd.HamLuong, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.MaDV, kd.TenDV, kd.TenRG, SoLo = kd.kdct.SoLo ?? "" } into kq
                                             // select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : (DungChung.Bien.MaBV == "12122" ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV))), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                             select new BC
                                             {
                                                 LoaiDV = kq.Key.TenRGTN.Contains("Thuốc thường") ? 0 : (kq.Key.TenRGTN.Contains("Hóa chất") ? 1 : (kq.Key.TenRGTN.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRGTN.Contains("Thuốc đông y") ? 6 : 0))))))
                                                 ,
                                                 HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Key.SoLo),
                                                 SoLo = kq.Key.SoLo,
                                                 MaDV = kq.Key.MaDV ?? 0,
                                                 TenDV = kq.Key.TenDV,
                                                 HamLuong = kq.Key.HamLuong,
                                                 TenHC = kq.Key.TenHC,
                                                 TenRG = kq.Key.TenRG,
                                                 MaTam = kq.Key.MaTam,
                                                 DonGia = kq.Key.DonGia,
                                                 SoDK = kq.Key.SoDK,
                                                 DonVi = kq.Key.DonVi,
                                                 LoaiDuoc = kq.Key.LoaiDuoc,
                                                 SoLuong = kq.Sum(p => p.kd.kdct.SoLuong) * (-1),
                                                 ThanhTien = kq.Sum(p => p.kd.kdct.ThanhTien) * (-1)
                                             }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();

                                    foreach (BC a in q)
                                    {
                                        if (DungChung.Bien.MaBV == "30007")
                                        {
                                            a.TenDV = a.TenDV + " (" + a.TenHC + ": " + a.HamLuong + ") ";
                                        }
                                        else if (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                        {
                                            a.TenDV = a.TenDV + " " + a.HamLuong;
                                        }
                                        else if (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009")
                                        {
                                            a.TenDV = a.TenRG ?? "";
                                        }
                                        else if (DungChung.Bien.MaBV == "27023")
                                        {
                                            a.TenDV = a.TenDV + (string.IsNullOrEmpty(a.HamLuong) ? "" : " (" + a.HamLuong + ") ");
                                        }
                                        //if (DungChung.Bien.MaBV == "27023")
                                        //{
                                        //    var qDGdv = qDonGiaDV.Where(p => p.MaDV == a.MaDV && p.DonGiaN == a.DonGia).FirstOrDefault();
                                        //    if (qDGdv != null)
                                        //    {
                                        //        a.SoLo = qDGdv.SoLo;
                                        //        a.HanDung = qDGdv.HanDung;
                                        //    }
                                        //}
                                    }
                                    #region 27023
                                    if (DungChung.Bien.MaBV == "27023")
                                    {
                                        if (q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).Count() > 0)
                                        {
                                            BaoCao.PhieuTrathuocVTYT_27023 rep = new BaoCao.PhieuTrathuocVTYT_27023();
                                            rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                            rep.MauSo.Value = "MS:05D/BV-01";

                                            rep.DataSource = q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).OrderBy(p => p.DonVi).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    #endregion
                                    #region 01071
                                    else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                    {
                                        if (q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).Count() > 0)
                                        {
                                            BaoCao.PhieulinhthuocVTYT_A5_2lien_01071 rep = new BaoCao.PhieulinhthuocVTYT_A5_2lien_01071();
                                            //rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                            rep.MauSo.Value = "MS:05D/BV-01";

                                            rep.DataSource = q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).OrderBy(p => p.DonVi).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    #endregion
                                    #region bv khác
                                    else
                                    {
                                        if (q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).Count() > 0)
                                        {
                                            if (DungChung.Bien.MaBV == "34019")
                                            {
                                                BaoCao.PhieuTrathuocVTYT_A5 rep = new BaoCao.PhieuTrathuocVTYT_A5();
                                                rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                                rep.MauSo.Value = "MS:05D/BV-01";
                                                var qbc34019 = q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).OrderBy(p => p.DonVi).ToList();
                                                rep.DataSource = qbc34019;
                                                //rep.lblTongSoKhoan.Text = "Tổng số khoản: " + qbc34019.Count();
                                                //rep.celThoiGian.Text = "Ngày " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            else
                                            {
                                                BaoCao.PhieuTrathuocVTYT rep = new BaoCao.PhieuTrathuocVTYT();
                                                rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                                rep.MauSo.Value = "MS:05D/BV-01";

                                                rep.DataSource = q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                        }
                                    }
                                    #endregion

                                    #endregion
                                }
                            }
                        }
                        #endregion
                        #region kiểu đơn không phải là kiểu đơn trả thuốc
                        else
                        {
                            kieudon = bph.First().KieuDon.Value;
                            var q2 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                      join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                      join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                      join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                      select new { kd, kdct, dv, tn }).ToList();

                            var q = (from kd in q2
                                     group kd by new
                                     {
                                         kd.dv.TenHC,
                                         kd.dv.HamLuong,
                                         TenRGTN = kd.tn.TenRG,
                                         kd.dv.MaTam,
                                         kd.kdct.DonGia,
                                         kd.kdct.DonVi,
                                         kd.kdct.MaDV,
                                         kd.dv.TenDV,
                                         TenRGDV = kd.dv.TenRG,
                                         kd.kd.LoaiDuoc,
                                         kd.kdct.XHH,
                                         LoaiDV = kd.tn.TenRG.Contains("Thuốc thường") ? 0 : (kd.tn.TenRG.Contains("Hóa chất") ? 1 : (kd.tn.TenRG.Contains("Vật tư y tế") ? 2 : (kd.tn.TenRG.Contains("Thuốc gây nghiện") ? 3 : (kd.tn.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (kd.tn.TenRG.Contains("Thuốc trẻ em") ? 5 : (kd.tn.TenRG.Contains("Thuốc đông y") ? 6 : 0)))))),
                                         SoLo = DungChung.Bien.MaBV == "27023" ? kd.kdct.SoLo : "",
                                         HanDung = DungChung.Bien.MaBV == "27023" ? kd.kdct.HanDung : null
                                     } into kq
                                     select new BC { TenHC = kq.Key.TenHC, LoaiDV = kq.Key.LoaiDV, SoLo = kq.Key.SoLo, HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Key.SoLo), TenRGTN = kq.Key.TenRGTN, TenRGDV = kq.Key.TenRGDV, TenDV = kq.Key.TenDV, HamLuong = kq.Key.HamLuong, MaTam = kq.Key.MaTam, MaDV = kq.Key.MaDV ?? 0, DonVi = kq.Key.DonVi, SoLuong = (kieudon == 2 && DungChung.Bien.MaBV == "30003") ? (-kq.Sum(p => p.kdct.SoLuong)) : kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                            foreach (BC a in q)
                            {
                                if (DungChung.Bien.MaBV == "30007")
                                {
                                    a.TenDV = a.TenDV + " (" + a.TenHC + ": " + a.HamLuong + ") ";
                                }
                                else if (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009")
                                {
                                    a.TenDV = a.TenDV + " " + a.HamLuong;
                                }

                                else if (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009")
                                {
                                    a.TenDV = a.TenRGDV ?? "";
                                }
                                else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                    a.TenDV = a.TenDV + " " + a.HamLuong;
                                else if (DungChung.Bien.MaBV == "27023")
                                {
                                    a.TenDV = a.TenDV + (string.IsNullOrEmpty(a.HamLuong) ? "" : " (" + a.HamLuong + ") ");
                                }
                                //if (DungChung.Bien.MaBV == "27023")
                                //{
                                //    var qDGdv = qDonGiaDV.Where(p => p.MaDV == a.MaDV && p.DonGiaN == a.DonGia).FirstOrDefault();
                                //    if (qDGdv != null)
                                //    {
                                //        a.SoLo = qDGdv.SoLo;
                                //        a.HanDung = qDGdv.HanDung;
                                //    }
                                //}
                            }

                            // int loaiduoc = bph.First().LoaiDuoc.Value;

                            #region (loaiduoc == 3 || loaiduoc == 4)
                            if (lLoaiDuocCt.Contains(3) || lLoaiDuocCt.Contains(4))// thuốc gây nghiện hướng tâm thần
                            {
                                if (q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Count() > 0)
                                {
                                    #region   ( "30009" , "30003", "19048","30004","30002")
                                    if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "19048" || DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "30002")// || DungChung.Bien.MaBV == "01071") // dung 0609
                                    {
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                        BaoCao.PhieulinhthuocGNHTT2lien rep = new BaoCao.PhieulinhthuocGNHTT2lien();
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        if (DungChung.Bien.MaBV == "12001")
                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                            case 2:
                                                rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                break;
                                        }

                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                        rep.MauSo.Value = "MS:08";
                                        if (DungChung.Bien.MaBV == "30002")
                                        {
                                            double a = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Sum(p => p.ThanhTien) ?? 0;
                                            rep.celThanhTien2.Text = a.ToString("###,###.00");
                                            rep.colTongG.Text = a.ToString("###,###.00");
                                        }



                                        rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        this.Dispose();
                                    }

                                    #endregion
                                    else
                                    {
                                        #region 24009
                                        if (DungChung.Bien.MaBV == "24009")
                                        {
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                            BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT(6);
                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            rep.xrTableCell28.Text = "";
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }



                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN, THUỐC HƯỚNG TÂM THẦN, THUỐC TIỀN CHẤT");
                                            rep.MauSo.Value = "MS:01D/BV-01";



                                            rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                        #endregion
                                        #region 01071
                                        else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                        {
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                            BaoCao.PhieulinhthuocGNHTT2lien_01071 rep = new BaoCao.PhieulinhthuocGNHTT2lien_01071();
                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            if (DungChung.Bien.MaBV == "12001")
                                                rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                case 2:
                                                    rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    break;
                                            }




                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");

                                            rep.MauSo.Value = "MS:08";



                                            rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                        #endregion
                                        #region 30002
                                        else if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "14018")
                                        {
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                            BaoCao.PhieulinhthuocGNHTT2lien rep = new BaoCao.PhieulinhthuocGNHTT2lien();
                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }



                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep.MauSo.Value = "MS:08";


                                            rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                        #endregion
                                        else
                                        {
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                            #region 27023
                                            if (DungChung.Bien.MaBV == "27023")
                                            {
                                                BaoCao.PhieulinhthuocGNHTT_27023 rep = new BaoCao.PhieulinhthuocGNHTT_27023();
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #region 30007 _ A5 1 liên
                                            else if (DungChung.Bien.MaBV == "30007")
                                            {
                                                BaoCao.PhieulinhthuocGNHTT_A5_1lien rep = new BaoCao.PhieulinhthuocGNHTT_A5_1lien();
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";

                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #region bv khác
                                            else if (DungChung.Bien.MaBV == "34019")
                                            {
                                                BaoCao.PhieulinhthuocGNHTT_34019 rep = new BaoCao.PhieulinhthuocGNHTT_34019();
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                {
                                                    if (DungChung.Bien.MaBV == "01830" && tenkho.First().PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                                                    {
                                                        rep.Kholinhnew.Value = "Lĩnh bù về: " + tenkho.First().TenKP;
                                                        rep.Kholinh.Value = "Kho lĩnh: kho gây nghiện hướng thần";
                                                    }
                                                    else
                                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                }


                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";

                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            else
                                            {
                                                BaoCao.PhieulinhthuocGNHTT rep = new BaoCao.PhieulinhthuocGNHTT();
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                {
                                                    if (DungChung.Bien.MaBV == "01830" && tenkho.First().PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                                                    {
                                                        rep.Kholinhnew.Value = "Lĩnh bù về: " + tenkho.First().TenKP;
                                                        rep.Kholinh.Value = "Kho lĩnh: kho gây nghiện hướng thần";
                                                    }
                                                    else
                                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                }


                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";

                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion

                                        }
                                    }
                                }
                            }
                            #endregion
                            if (lLoaiDuocCt.Contains(0) || lLoaiDuocCt.Contains(1) || lLoaiDuocCt.Contains(2) || lLoaiDuocCt.Contains(5) || lLoaiDuocCt.Contains(6))
                            {
                                if (lLoaiDuocCt.Contains(6))// thuốc đông y
                                {
                                    #region 12001
                                    if (DungChung.Bien.MaBV == "12001")
                                    {
                                        var q61 = (from bn in _dataContext.BenhNhans
                                                   join kd in _dataContext.DThuocs on bn.MaBNhan equals kd.MaBNhan
                                                   join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                   join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                   join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                   select new
                                                   {
                                                       kd.MaKP,
                                                       bn.TenBNhan,
                                                       bn.DChi,
                                                       bn.MaBNhan,
                                                       MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong,
                                                       DTuong = bn.DTuong == null ? "" : bn.DTuong,
                                                       TenDV = dv.TenDV,
                                                       dv.MaDV,
                                                       dv.MaTam,
                                                       DonVi = dv.DonVi,
                                                       SoLuong = kdct.SoLuong,
                                                       DonGia = kdct.DonGia,
                                                       LoaiDuoc = kd.LoaiDuoc,
                                                       tn.TenRG,
                                                   }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var q6 = (from bn in q61
                                                  where (_makp == 0 ? true : bn.MaKP == _makp)
                                                  group new { bn } by new { bn.MaTam, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.DonGia, bn.DonVi, bn.MaDV, bn.TenDV, bn.LoaiDuoc, bn.TenRG } into kq
                                                  select new
                                                  {
                                                      kq.Key.TenBNhan,
                                                      kq.Key.DChi,
                                                      TenDV = kq.Key.TenDV,
                                                      kq.Key.MaDV,
                                                      kq.Key.MaTam,
                                                      DonVi = kq.Key.DonVi,
                                                      SoLuong139 = kq.Where(p => ((p.bn.DTuong == ("BHYT")) && (p.bn.MaDTuong == ("DT") || p.bn.MaDTuong == ("HN") || p.bn.MaDTuong == ("DK")))).Sum(p => p.bn.SoLuong),
                                                      SoLuongTE = kq.Where(p => p.bn.DTuong == ("BHYT") && p.bn.MaDTuong == ("TE")).Sum(p => p.bn.SoLuong),
                                                      SoLuongBHYT = kq.Where(p => p.bn.DTuong == ("BHYT") && p.bn.MaDTuong != "DT" && p.bn.MaDTuong != "HN" && p.bn.MaDTuong != "DK" && p.bn.MaDTuong != "TE").Sum(p => p.bn.SoLuong),
                                                      SoLuongDichVu = kq.Where(p => p.bn.DTuong == ("Dịch vụ")).Sum(p => p.bn.SoLuong),
                                                      SoLuong = kq.Sum(p => p.bn.SoLuong),
                                                      DonGia = kq.Key.DonGia,
                                                      LoaiDuoc = kq.Key.LoaiDuoc,
                                                      LoaiDV = kq.Key.TenRG.Contains("Thuốc thường") ? 0 : (kq.Key.TenRG.Contains("Hóa chất") ? 1 : (kq.Key.TenRG.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRG.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRG.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRG.Contains("Thuốc đông y") ? 6 : 0)))))),
                                                  }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (q6.Where(p => p.LoaiDV == 6).Count() > 0)
                                        {
                                            BaoCao.PhieulinhthuocVTYT_TD rep = new BaoCao.PhieulinhthuocVTYT_TD(6);
                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;

                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }


                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                            rep.MauSo.Value = "MS:...D/BV-01";

                                            if (DungChung.Bien.MaBV == "30009")
                                            {
                                                var q7 = (from dongy in q6
                                                          group new { dongy } by new { dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc, dongy.LoaiDV } into kq
                                                          select new { TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc, kq.Key.LoaiDV }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                                rep.DataSource = q7.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();
                                            }
                                            else
                                            {
                                                rep.DataSource = q6.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();
                                            }
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    else if (DungChung.Bien.MaBV == "30002")
                                    {
                                        var q6 = (
                                                    from bn in _dataContext.BenhNhans
                                                    join kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp) on bn.MaBNhan equals kd.MaBNhan
                                                    join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                    join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                    join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                    group new { kdct, dv, kd, bn, tn } by new { dv.TenHC, dv.HamLuong, dv.MaTam, bn.Tuoi, kd.MaBNhan, bn.TenBNhan, bn.DChi, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, tn.TenRG } into kq
                                                    select new
                                                    {
                                                        kq.Key.MaBNhan,
                                                        kq.Key.TenBNhan,
                                                        kq.Key.MaTam,
                                                        Tuoi = kq.Key.Tuoi,
                                                        kq.Key.DChi,
                                                        TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV),
                                                        kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        SoLuong = kq.Sum(p => p.kdct.SoLuong),
                                                        DonGia = kq.Key.DonGia,
                                                        LoaiDuoc = kq.Key.LoaiDuoc,
                                                        LoaiDV = kq.Key.TenRG.Contains("Thuốc thường") ? 0 : (kq.Key.TenRG.Contains("Hóa chất") ? 1 : (kq.Key.TenRG.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRG.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRG.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRG.Contains("Thuốc đông y") ? 6 : 0)))))),
                                                    }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (q6.Where(p => p.LoaiDV == 6).Count() > 0)
                                        {
                                            BaoCao.PhieulinhthuocVTYT_A5 rep = new BaoCao.PhieulinhthuocVTYT_A5();

                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }


                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                            rep.MauSo.Value = "MS:...D/BV-01";


                                            var q7 = (from dongy in q6.Where(p => p.LoaiDV == 6)
                                                      group new { dongy } by new { dongy.MaTam, dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc } into kq
                                                      select new { TenDV = kq.Key.TenDV, kq.Key.MaTam, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                            rep.DataSource = q7.OrderBy(p => p.DonVi).ToList();

                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    #endregion
                                    #region bv khác
                                    else
                                    {

                                        var q61 = (from
                                                       bn in _dataContext.BenhNhans
                                                   join
                                                       kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp) on bn.MaBNhan equals kd.MaBNhan
                                                   join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                   join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                   join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                   select new { bn.MaBNhan, bn.Tuoi, bn.DChi, bn.GTinh, bn.TenBNhan, kd, kdct, dv, tn }).ToList();
                                        var q6 = (
                                            from kd in q61
                                            group kd by new { kd.kd.GhiChu, kd.kd.IDDon_Mau, kd.kdct.Loai, kd.GTinh, kd.dv.TenHC, kd.dv.HamLuong, kd.dv.MaTam, kd.Tuoi, kd.kd.MaBNhan, kd.TenBNhan, kd.DChi, kd.kdct.DonGia, kd.kdct.ThanhTien, kd.kdct.DonVi, kd.kdct.MaDV, kd.dv.TenDV, kd.kd.LoaiDuoc, kd.dv.TenRG, SoLo = DungChung.Bien.MaBV == "27023" ? kd.kdct.SoLo : "", HanDung = DungChung.Bien.MaBV == "27023" ? kd.kdct.HanDung : null, TenRGTN = kd.tn.TenRG } into kq
                                            select new
                                            {
                                                kq.Key.MaBNhan,
                                                SoLo = kq.Key.SoLo,
                                                HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Key.SoLo),
                                                kq.Key.TenBNhan,
                                                kq.Key.MaTam,
                                                SoThang = kq.Key.Loai,
                                                IDDonMau = kq.Key.IDDon_Mau,
                                                Tuoi = kq.Key.Tuoi,
                                                GTinh = kq.Key.GTinh,
                                                kq.Key.DChi,
                                                TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : ((DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009") ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : (DungChung.Bien.MaBV == "27023") ? (kq.Key.TenDV + (string.IsNullOrEmpty(kq.Key.HamLuong) ? "" : " (" + kq.Key.HamLuong + ") ")) : kq.Key.TenDV))),
                                                kq.Key.MaDV,
                                                DonVi = kq.Key.DonVi,
                                                GhiChu = kq.Key.GhiChu,
                                                SoLuong = kq.Sum(p => p.kdct.SoLuong),
                                                DonGia = kq.Key.DonGia,
                                                ThanhTien = kq.Key.ThanhTien,
                                                LoaiDuoc = kq.Key.LoaiDuoc,
                                                LoaiDV = kq.Key.TenRGTN.Contains("Thuốc thường") ? 0 : (kq.Key.TenRGTN.Contains("Hóa chất") ? 1 : (kq.Key.TenRGTN.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRGTN.Contains("Thuốc đông y") ? 6 : 0)))))),
                                            }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (q6.Where(p => p.LoaiDV == 6).Count() > 0)
                                        {
                                            #region 27023
                                            if (DungChung.Bien.MaBV == "27023")
                                            {
                                                BaoCao.PhieulinhthuocVTYT_27023 rep = new BaoCao.PhieulinhthuocVTYT_27023(6);

                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }


                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";


                                                if (DungChung.Bien.MaBV == "30009")
                                                {
                                                    var q7 = (from dongy in q6.Where(p => p.LoaiDV == 6)
                                                              group new { dongy } by new { dongy.MaTam, dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc } into kq
                                                              select new { TenDV = kq.Key.TenDV, kq.Key.MaTam, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                                    rep.DataSource = q7.OrderBy(p => p.DonVi).ToList();
                                                }
                                                else
                                                {
                                                    rep.DataSource = q6.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();
                                                }
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion

                                            #region 14017
                                            #region 14017
                                            else if (DungChung.Bien.MaBV == "14017")
                                            {
                                                var listBN = (from _lbn in q6.Where(p => p.LoaiDV == 6)
                                                              group _lbn by new { _lbn.MaBNhan } into kq
                                                              select new
                                                              {
                                                                  kq.Key.MaBNhan,
                                                              }).ToList();

                                                for (int i = 0; i < listBN.Count; i++)
                                                {
                                                    BaoCao.PhieulinhthuocVTYT_14017 rep = new BaoCao.PhieulinhthuocVTYT_14017(6);
                                                    int? mabn = listBN[i].MaBNhan;
                                                    rep.mabn.Value = mabn;
                                                    //q6.Where(p => p.MaBNhan == mabn).ToList();
                                                    var ttbn = (from rs in q6.Where(p => p.MaBNhan == mabn)
                                                                    //join dm in _dataContext.DThuocMaus on rs.IDDonMau equals dm.IDDonMau
                                                                join bnkb in _dataContext.BNKBs on rs.MaBNhan equals bnkb.MaBNhan
                                                                select new
                                                                {
                                                                    TenBN = rs.TenBNhan,
                                                                    Buong = bnkb.Buong,
                                                                    Giuong = bnkb.Giuong,
                                                                    Tuoi = rs.Tuoi,
                                                                    GioiTinh = rs.GTinh,
                                                                    //TenDon = dm.TenDTM,
                                                                    SoThang = rs.GhiChu,
                                                                    ThanhTien = rs.ThanhTien
                                                                }).ToList().FirstOrDefault();
                                                    var DTMau = (from rs in q6.Where(p => p.MaBNhan == mabn)
                                                                 join dm in _dataContext.DThuocMaus on rs.IDDonMau equals dm.IDDonMau
                                                                 join bnkb in _dataContext.BNKBs on rs.MaBNhan equals bnkb.MaBNhan
                                                                 select new
                                                                 {
                                                                     TenDon = dm.TenDTM,
                                                                 }).ToList().FirstOrDefault();
                                                    if (DTMau != null)
                                                    {
                                                        rep.DonMau.Value = DTMau.TenDon;
                                                    }
                                                    //string[] arrListStr = ttbn.SoThang.Split(';');
                                                    string[] arrListStr = ttbn.SoThang.Split(new char[] { ';' });

                                                    rep.sothang.Value = arrListStr[0];
                                                    string theongay = "";
                                                    if (DungChung.Bien.MaBV == "14017" && arrListStr.Count() > 1)
                                                    {
                                                        theongay += "Từ ngày: " + arrListStr[1];
                                                    }
                                                    if (DungChung.Bien.MaBV == "14017" && arrListStr.Count() > 2)
                                                    {
                                                        theongay += " đến ngày: " + arrListStr[2];
                                                    }
                                                    rep.theongay.Value = theongay;
                                                    rep.tuoi.Value = ttbn.Tuoi;
                                                    rep.TenBN.Value = ttbn.TenBN;
                                                    if (ttbn.GioiTinh == 0)
                                                    {
                                                        rep.GioiTinh.Value = "Nữ";
                                                    }
                                                    else
                                                    {
                                                        rep.GioiTinh.Value = "Nam";
                                                    }

                                                    rep.buong.Value = ttbn.Buong;
                                                    rep.giuong.Value = ttbn.Giuong;
                                                    rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                    //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                    rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                    rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                    rep.SoPL.Value = _soPL.ToString();
                                                    rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                    rep.Benhvien.Value = DungChung.Bien.TenCQ;

                                                    rep.Khoa.Value = bph.First().TenKP;
                                                    if (tenkho.Count > 0)
                                                        rep.Kholinh.Value = tenkho.First().TenKP;
                                                    switch (kieudon)
                                                    {
                                                        case 0:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                            break;
                                                        case 1:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                            break;
                                                        case 5:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                            break;
                                                            //    case 2:
                                                            //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                            //break;
                                                    }



                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                    rep.MauSo.Value = "MS:...D/BV-01";

                                                    rep.DataSource = q6.Where(p => p.MaBNhan == mabn).ToList();

                                                    rep.BindingData();
                                                    //rep.DataMember = "";
                                                    rep.CreateDocument();
                                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                    frm.ShowDialog();
                                                    this.Dispose();
                                                }
                                                return;

                                            }
                                            #endregion
                                            #endregion
                                            #region bv khác
                                            else
                                            #region 27021
                                                    if (DungChung.Bien.MaBV == "27021")
                                            {
                                                BaoCao.PhieulinhthuocVTYT27021 rep = new BaoCao.PhieulinhthuocVTYT27021(6);

                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }



                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";



                                                rep.DataSource = q6.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();

                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #region 01071
                                            else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                            {
                                                BaoCao.PhieulinhthuocVTYT_A5_2lien_01071 rep = new BaoCao.PhieulinhthuocVTYT_A5_2lien_01071(6);

                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }


                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";


                                                rep.DataSource = q6.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();

                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #region chung

                                            else
                                            {
                                                BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT(6);

                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }



                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                if (DungChung.Bien.MaBV == "30009")
                                                {
                                                    var q7 = (from dongy in q6.Where(p => p.LoaiDV == 6)
                                                              group new { dongy } by new { dongy.MaTam, dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc } into kq
                                                              select new { TenDV = kq.Key.TenDV, kq.Key.MaTam, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                                    rep.DataSource = q7.OrderBy(p => p.DonVi).ToList();
                                                }
                                                else
                                                {
                                                    rep.DataSource = q6.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();
                                                }
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #endregion
                                        }
                                    }
                                    #endregion
                                    //}
                                }
                                if (lLoaiDuocCt.Contains(0) || lLoaiDuocCt.Contains(1) || lLoaiDuocCt.Contains(2) || lLoaiDuocCt.Contains(5))
                                {

                                    #region 12001
                                    if (DungChung.Bien.MaBV == "12001")
                                    {
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        var qTD3 = (from
                                                     bn in _dataContext.BenhNhans
                                                    join
                                                        kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp) on bn.MaBNhan equals kd.MaBNhan
                                                    join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                    join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                    join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                    select new { bn.Tuoi, bn.DChi, bn.MaDTuong, bn.DTuong, bn.TenBNhan, kd, kdct, dv, tn }).ToList();
                                        var qTD2 = (from kd in qTD3
                                                    group kd by new { kd.dv.TenHC, kd.dv.HamLuong, kd.dv.MaTam, kd.MaDTuong, kd.DTuong, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.MaDV, kd.dv.TenDV, kd.kd.LoaiDuoc, TenRGTN = kd.tn.TenRG } into kq
                                                    select new
                                                    {
                                                        kq.Key.MaTam,
                                                        MaDTuong = kq.Key.MaDTuong,
                                                        DTuong = kq.Key.DTuong,
                                                        TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : (DungChung.Bien.MaBV == "12122" ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : kq.Key.TenDV)),
                                                        kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        SoLuong = kq.Sum(p => p.kdct.SoLuong),
                                                        DonGia = kq.Key.DonGia,
                                                        LoaiDuoc = kq.Key.LoaiDuoc,
                                                        LoaiDV = kq.Key.TenRGTN.Contains("Thuốc thường") ? 0 : (kq.Key.TenRGTN.Contains("Hóa chất") ? 1 : (kq.Key.TenRGTN.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRGTN.Contains("Thuốc đông y") ? 6 : 0)))))),
                                                    }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var qTD = (from kqe in qTD2
                                                   group kqe by new { kqe.DonGia, kqe.MaTam, kqe.DonVi, kqe.MaDV, kqe.TenDV, kqe.LoaiDuoc, kqe.LoaiDV } into kq
                                                   select new
                                                   {
                                                       kq.Key.MaTam,
                                                       TenDV = kq.Key.TenDV,
                                                       MaDV = kq.Key.MaDV,
                                                       DonVi = kq.Key.DonVi,
                                                       SoLuong = kq.Sum(p => p.SoLuong),
                                                       SoLuong139 = kq.Where(p => p.MaDTuong == ("DT") || p.MaDTuong == ("HN") || p.MaDTuong == ("DK")).Sum(p => p.SoLuong),
                                                       SoLuongTE = kq.Where(p => p.MaDTuong.Contains("TE")).Sum(p => p.SoLuong),
                                                       SoLuongBHYT = kq.Where(p => p.DTuong == ("BHYT") && p.MaDTuong != "DT" && p.MaDTuong != "HN" && p.MaDTuong != "DK" && p.MaDTuong != "TE").Sum(p => p.SoLuong),
                                                       SoLuongDichVu = kq.Where(p => p.DTuong == ("Dịch vụ")).Sum(p => p.SoLuong),
                                                       DonGia = kq.Key.DonGia,
                                                       LoaiDuoc = kq.Key.LoaiDuoc,
                                                       kq.Key.LoaiDV
                                                   }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (i != 4 && i != 3)
                                            {
                                                if (qTD.Where(p => p.LoaiDV == i).Count() > 0)
                                                {
                                                    BaoCao.PhieulinhthuocVTYT_TD rep = new BaoCao.PhieulinhthuocVTYT_TD();
                                                    rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                    rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                    rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                    rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                    rep.SoPL.Value = _soPL.ToString();
                                                    rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                    rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                    rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                    rep.Khoa.Value = bph.First().TenKP;
                                                    if (tenkho.Count > 0)
                                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                    switch (kieudon)
                                                    {
                                                        case 0:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                            break;
                                                        case 1:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                            break;
                                                        case 5:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                            break;
                                                            //    case 2:
                                                            //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                            //break;
                                                    }


                                                    switch (i)
                                                    {
                                                        case 0:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                            rep.MauSo.Value = "MS:01D/BV-01";
                                                            break;
                                                        case 1:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                            rep.MauSo.Value = "MS:02D/BV-01";
                                                            break;
                                                        case 2:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                            rep.MauSo.Value = "MS:03D/BV-01";
                                                            break;
                                                        case 3:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                            rep.MauSo.Value = "MS:08";
                                                            break;
                                                        case 4:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                            rep.MauSo.Value = "MS:08";
                                                            break;
                                                        case 5:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                            rep.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                        case 6:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                            rep.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                    }


                                                    rep.DataSource = qTD.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                    rep.BindingData();
                                                    //rep.DataMember = "";
                                                    rep.CreateDocument();
                                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                    frm.ShowDialog();
                                                    this.Dispose();
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    #region 30002
                                    else if (DungChung.Bien.MaBV == "30002")
                                    {
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (i != 4 && i != 3)
                                            {
                                                if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                {
                                                    int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                    var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                                    BaoCao.PhieulinhthuocVTYT_A5 rep = new BaoCao.PhieulinhthuocVTYT_A5();
                                                    rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                    //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                    rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                    rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                    rep.SoPL.Value = _soPL.ToString();
                                                    rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                    rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                    rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                    rep.Khoa.Value = bph.First().TenKP;
                                                    if (tenkho.Count > 0)
                                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                    switch (kieudon)
                                                    {
                                                        case 0:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                            break;
                                                        case 1:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                            break;
                                                        case 5:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                            break;
                                                            //    case 2:
                                                            //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                            //break;
                                                    }


                                                    switch (i)
                                                    {
                                                        case 0:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                            rep.MauSo.Value = "MS:01D/BV-01";
                                                            break;
                                                        case 1:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                            rep.MauSo.Value = "MS:02D/BV-01";
                                                            break;
                                                        case 2:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                            rep.MauSo.Value = "MS:03D/BV-01";
                                                            break;
                                                        case 3:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                            rep.MauSo.Value = "MS:08";
                                                            break;
                                                        case 4:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                            rep.MauSo.Value = "MS:08";
                                                            break;
                                                        case 5:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                            rep.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                        case 6:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                            rep.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                    }

                                                    if (DungChung.Bien.MaBV == "30002")
                                                    {
                                                        double a = q.Where(p => p.LoaiDV == i).Sum(p => p.ThanhTien) ?? 0;
                                                        rep.colTongG.Text = a.ToString("###,###.00");
                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.MaDV).ToList();
                                                    }
                                                    else
                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                    rep.BindingData();
                                                    //rep.DataMember = "";
                                                    rep.CreateDocument();
                                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                    frm.ShowDialog();
                                                    this.Dispose();
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    #region bv khác
                                    else
                                    {

                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        #region 27023
                                        if (DungChung.Bien.MaBV == "27023")
                                        {
                                            q = (from bc in q
                                                 select new BC { TenHC = bc.TenHC, LoaiDV = bc.LoaiDV, SoLo = bc.SoLo, HanDung = bc.HanDung, TenRGTN = bc.TenRGTN, TenRGDV = bc.TenRGDV, TenDV = bc.TenDV, HamLuong = bc.HamLuong, MaTam = bc.MaTam, MaDV = bc.MaDV, DonVi = bc.DonVi, SoLuong = bc.SoLuong, DonGia = bc.DonGia, ThanhTien = bc.ThanhTien, LoaiDuoc = bc.LoaiDuoc }).ToList();
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (i != 4 && i != 3)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        BaoCao.PhieulinhthuocVTYT_27023 rep = new BaoCao.PhieulinhthuocVTYT_27023();
                                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep.SoPL.Value = _soPL.ToString();
                                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                        rep.Khoa.Value = bph.First().TenKP;
                                                        if (tenkho.Count > 0)
                                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                        switch (kieudon)
                                                        {
                                                            case 0:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                                break;
                                                            case 1:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                break;
                                                            case 5:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                break;
                                                                //    case 2:
                                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                //break;
                                                        }


                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                                break;
                                                            case 2:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                                break;
                                                            case 3:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                break;
                                                            case 4:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                break;
                                                            case 5:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }


                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep.BindingData();
                                                        //rep.DataMember = "";
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                        this.Dispose();
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        #region bv khác
                                        else
                                        #region 27021
                                                if (DungChung.Bien.MaBV == "27021")
                                        {
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (i != 4 && i != 3)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        BaoCao.PhieulinhthuocVTYT27021 rep = new BaoCao.PhieulinhthuocVTYT27021();
                                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep.SoPL.Value = _soPL.ToString();
                                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                        rep.Khoa.Value = bph.First().TenKP;
                                                        if (tenkho.Count > 0)
                                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                        switch (kieudon)
                                                        {
                                                            case 0:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                                break;
                                                            case 1:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                break;
                                                            case 5:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                break;
                                                                //    case 2:
                                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                //break;
                                                        }


                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                                break;
                                                            case 2:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                                break;

                                                            case 5:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }


                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep.BindingData();
                                                        //rep.DataMember = "";
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                        this.Dispose();
                                                    }
                                                }
                                            }
                                        }
                                        #endregion 27021
                                        #region 01071
                                        else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                        {
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (i != 4 && i != 3)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        BaoCao.PhieulinhthuocVTYT_A5_2lien_01071 rep = new BaoCao.PhieulinhthuocVTYT_A5_2lien_01071();
                                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep.SoPL.Value = _soPL.ToString();
                                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                        int makpx = 0;
                                                        bool _linhbututruc = false;
                                                        bool khoaKeTuTruc = false;
                                                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                                        {

                                                            int makp = _makp;
                                                            if (makp <= 0)
                                                            {
                                                                var qdtct = _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).FirstOrDefault();
                                                                if (qdtct != null)
                                                                    makp = qdtct.MaKP.Value;
                                                            }

                                                            var kt = _dataContext.KPhongs.Where(p => p.MaKP == makp && p.PLoai.Contains("Tủ trực")).Select(p => p.TenKP).FirstOrDefault();
                                                            if (kt != null)
                                                                khoaKeTuTruc = true;

                                                        }
                                                        if (bph.First().MaKXuat != null)
                                                        {
                                                            makpx = Convert.ToInt32(bph.First().MaKXuat);
                                                            var ktratutruca = _dataContext.KPhongs.Where(p => p.MaKP == makpx && p.PLoai.Contains("Tủ trực")).Select(p => p.TenKP).FirstOrDefault();
                                                            if (ktratutruca != null)
                                                                _linhbututruc = true;
                                                        }
                                                        if (_linhbututruc)
                                                        {
                                                            var khox = _dataContext.KPhongs.Where(p => p.MaKP == makpx).Select(p => p.TenKP).FirstOrDefault();
                                                            if (khox != null)
                                                                rep.Khoa.Value = khox.ToString();
                                                        }
                                                        else
                                                            rep.Khoa.Value = bph.First().TenKP;
                                                        if (tenkho.Count > 0)
                                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                        switch (kieudon)
                                                        {
                                                            case 0:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                                break;
                                                            case 1:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                break;
                                                            case 5:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                break;

                                                        }


                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                                break;
                                                            case 2:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                                break;
                                                            case 3:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                break;
                                                            case 4:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                break;
                                                            case 5:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }
                                                        if (khoaKeTuTruc)
                                                        {
                                                            string doituong = "";
                                                            int iddtbn = bph.First().XHH;
                                                            if (iddtbn == 99)
                                                                doituong = "Tất cả";
                                                            else
                                                            {
                                                                var dtuong = _dataContext.DTBNs.Where(p => p.IDDTBN == iddtbn).FirstOrDefault();
                                                                if (dtuong != null)
                                                                    doituong = dtuong.DTBN1;
                                                            }
                                                            if (doituong != "")
                                                                rep.DoiTuong.Value = "Đối tượng: " + doituong;
                                                        }

                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep.BindingData();
                                                        //rep.DataMember = "";
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                        this.Dispose();
                                                    }
                                                }
                                            }


                                        }
                                        #endregion
                                        #region 27022 có đơn giá, thành tiền
                                        else if (DungChung.Bien.MaBV == "27022")
                                        {
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (i != 4 && i != 3)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        BaoCao.PhieulinhthuocVTYT_CoDonGia rep = new BaoCao.PhieulinhthuocVTYT_CoDonGia();
                                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        string nguoiphat = "";
                                                        //var Nguoiphat = _dataContext.KPhongs.Where(p => p.MaKP == tekho).Select(p => p.NguoiPhat).FirstOrDefault();
                                                        //if (Nguoiphat != null)
                                                        //    nguoiphat = Nguoiphat;
                                                        rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                        rep.SoPL.Value = _soPL.ToString();
                                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                        rep.Khoa.Value = bph.First().TenKP;
                                                        bool tutruc = false;
                                                        if (tenkho.Count > 0)
                                                        {
                                                            if (tenkho.First().PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                                                                tutruc = true;
                                                            if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                rep.KhoLinhNew.Value = "Lĩnh bù về: " + tenkho.First().TenKP;
                                                            else
                                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                        }
                                                        switch (kieudon)
                                                        {
                                                            case 0:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                                break;
                                                            case 1:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                break;
                                                            case 5:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                break;
                                                            case 3:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Lĩnh về khoa";
                                                                break;
                                                            case 4:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Khoa trả dược";
                                                                break;
                                                            case 2:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Trả thuốc";
                                                                break;
                                                                //    case 2:
                                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                //break;
                                                        }


                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.Kholinh.Value = "Kho lĩnh: Kho nội trú";
                                                                break;
                                                            case 1:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                break;
                                                            case 2:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                break;
                                                            case 3:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                break;
                                                            case 4:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                break;
                                                            case 5:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }


                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep.BindingData();
                                                        //rep.DataMember = "";
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                        this.Dispose();
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        #region chung
                                        else
                                        {
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (i != 4 && i != 3)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        #region 31049
                                                        if (DungChung.Bien.MaBV == "34019")
                                                        {
                                                            BaoCao.PhieulinhthuocVTYT_A5 rep = new BaoCao.PhieulinhthuocVTYT_A5();
                                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;

                                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                            rep.SoPL.Value = _soPL.ToString();
                                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                            rep.Khoa.Value = bph.First().TenKP;
                                                            bool tutruc = false;
                                                            if (tenkho.Count > 0)
                                                            {
                                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                            }
                                                            switch (kieudon)
                                                            {
                                                                case 0:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";

                                                                    break;
                                                                case 1:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                    break;
                                                                case 5:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                    break;

                                                            }


                                                            switch (i)
                                                            {
                                                                case 0:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                    rep.MauSo.Value = "MS:01D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho nội trú";
                                                                    break;
                                                                case 1:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                    rep.MauSo.Value = "MS:02D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                    break;
                                                                case 2:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                    rep.MauSo.Value = "MS:03D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                    break;
                                                                case 3:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                    rep.MauSo.Value = "MS:08";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                    break;
                                                                case 4:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                    rep.MauSo.Value = "MS:08";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                    break;
                                                                case 5:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                                    break;
                                                                case 6:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                                    break;
                                                            }

                                                            var qbc34019 = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                            rep.DataSource = qbc34019;
                                                            rep.lblTongSoKhoan.Text = "Tổng số khoản: " + qbc34019.Count();
                                                            rep.celThoiGian.Text = "Ngày " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                                                            rep.BindingData();
                                                            rep.CreateDocument();
                                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                            frm.ShowDialog();
                                                            this.Dispose();


                                                        }
                                                        #endregion 31049
                                                        #region chung
                                                        else
                                                        {

                                                            BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT();
                                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                            string nguoiphat = "";
                                                            //var Nguoiphat = _dataContext.KPhongs.Where(p => p.MaKP == tekho).Select(p => p.NguoiPhat).FirstOrDefault();
                                                            //if (Nguoiphat != null)
                                                            //    nguoiphat = Nguoiphat;
                                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                            rep.SoPL.Value = _soPL.ToString();
                                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                            rep.Khoa.Value = bph.First().TenKP;
                                                            bool tutruc = false;
                                                            if (tenkho.Count > 0)
                                                            {
                                                                if (tenkho.First().PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                                                                    tutruc = true;
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.KhoLinhNew.Value = "Lĩnh bù về: " + tenkho.First().TenKP;
                                                                else
                                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                            }
                                                            switch (kieudon)
                                                            {
                                                                case 0:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";

                                                                    break;
                                                                case 1:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                    break;
                                                                case 5:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                    break;
                                                                    //    case 2:
                                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                    //break;
                                                            }


                                                            switch (i)
                                                            {
                                                                case 0:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                    rep.MauSo.Value = "MS:01D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho nội trú";
                                                                    break;
                                                                case 1:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                    rep.MauSo.Value = "MS:02D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                    break;
                                                                case 2:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                    rep.MauSo.Value = "MS:03D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                    break;
                                                                case 3:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                    rep.MauSo.Value = "MS:08";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                    break;
                                                                case 4:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                    rep.MauSo.Value = "MS:08";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                    break;
                                                                case 5:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                                    break;
                                                                case 6:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                                    break;
                                                            }


                                                            rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                            rep.BindingData();
                                                            //rep.DataMember = "";
                                                            rep.CreateDocument();
                                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                            frm.ShowDialog();
                                                            this.Dispose();
                                                        }
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        #endregion
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                    break;
                    #endregion
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Spl_kp"></param>
        /// <param name="stt">2: Lĩnh cho bệnh nhân; 3: Lĩnh về khoa</param>
        public void InPhieu(string[] Spl_kp, int stt)
        {
            int _soPL = Convert.ToInt32(Spl_kp[0]);
            int _makp = 0;
            if (Spl_kp.Length > 0 && Spl_kp[1] != null)
                _makp = String.IsNullOrEmpty(Spl_kp[1]) ? 0 : Convert.ToInt32(Spl_kp[1]);
            if (DungChung.Bien.MaBV != "19048")
                _makp = 0;
            _status = stt;
            bool mauA5 = false;// in mẫu A5 
            string _maCQCQ = "";
            var qCQCQ = _dataContext.BenhViens.Where(panelControl1 => panelControl1.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (qCQCQ != null)
                _maCQCQ = qCQCQ.MaChuQuan;

            List<DonGiaDV> qDonGiaDV = new List<DonGiaDV>();
            try
            {
                qDonGiaDV = _dataContext.DonGiaDVs.Where(p => p.Status == true).ToList();
            }
            catch (Exception ex)
            { }
            switch (_status)
            {
                case 2:
                    #region case2
                    string _dtuong = "";
                    var ktdtuong3 = (from bn in _dataContext.BenhNhans
                                     join dtbn in _dataContext.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                                     join dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp) on bn.MaBNhan equals dt.MaBNhan
                                     join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                     select new { dt.NgayKe, bn.IDDTBN, bn.DTuong, bn.NoThe, bn.NoiTru, dtbn.MoTa, dtct.Loai }).ToList();
                    var ktdtuong = (from bn in ktdtuong3
                                    group new { bn } by new { bn.IDDTBN, bn.DTuong, bn.NoThe, bn.NoiTru } into kq
                                    select new { kq.Key.IDDTBN, kq.Key.DTuong, kq.Key.NoThe, kq.Key.NoiTru, SoThang = kq.Max(p => p.bn.Loai), }).ToList();
                    if (ktdtuong.Count > 1)
                    {
                        _dtuong = "";
                    }
                    else
                    {
                        if (ktdtuong.Count > 0 && ktdtuong.First().NoThe == true)
                        {
                            _dtuong = "(Dành cho đối tượng dịch vụ _ nợ thẻ BHYT) \n";
                        }
                        else
                        {
                            var ktdtuong2 = (from bn in ktdtuong3
                                             select new { bn.MoTa }).ToList();
                            if (ktdtuong2.Count > 0)
                            {
                                _dtuong = "(Dành cho đối tượng " + ktdtuong2.First().MoTa + " ) \n";
                            }
                        }
                    }

                    var ngay = ktdtuong3.Select(p => p.NgayKe).OrderBy(p => p.Value).ToList();
                    //var sothang = ktdtuong.Select(p => p.SoThang).ToList();
                    //int sthang = 1;
                    //if(sothang.Count > 0)
                    //{
                    //sthang = sothang.First();
                    //}
                    string ngay1 = "";
                    string ngay2 = "";
                    if (ngay.Count > 0)
                    {
                        ngay1 = ngay.First().Value.ToShortDateString();
                        ngay2 = ngay.Last().Value.ToShortDateString();
                    }
                    frmIn frm = new frmIn();
                    //thuốc thường: LoaiDV = 0;
                    //Hóa chất, LoaiDV = 1;
                    //vật tư y tế, LoaiDV = 2
                    //Thuốc gây nghiện, LoaiDV = 3
                    //Thuốc hướng tâm thần, LoaiDV = 4
                    //Thuốc trẻ em, LoaiDV = 5
                    //Thuốc đông y, LoaiDV = 6

                    var bph0 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on kd.IDDon equals dtct.IDDon
                                join kp in _dataContext.KPhongs
                                    on kd.MaKP equals kp.MaKP
                                join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                                join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                select new
                                {
                                    kp.TenKP,
                                    kd.LoaiDuoc,
                                    kd.MaKXuat,
                                    kd.MaKP,
                                    kd.KieuDon,
                                    tn.TenRG,
                                    kd.NgayKe,
                                    dtct.XHH,
                                    kd.GhiChu,
                                    kp.PLoai,
                                    kd.MaBNhanChiTiet
                                }).ToList();

                    if (ngay1 == "" && ngay2 == "")
                    {
                        var ngayKhoLinh = bph0.Select(p => p.NgayKe).OrderBy(p => p.Value).ToList();

                        if (ngayKhoLinh.Count > 0)
                        {
                            ngay1 = ngayKhoLinh.First().Value.ToShortDateString();
                            ngay2 = ngayKhoLinh.Last().Value.ToShortDateString();
                        }
                    }
                    var bph = (from a in bph0
                               select new
                               {
                                   a.TenKP,
                                   a.MaKXuat,
                                   a.KieuDon,
                                   a.MaKP,
                                   a.TenRG,
                                   a.XHH,
                                   a.GhiChu,
                                   a.PLoai,
                                   a.MaBNhanChiTiet,
                                   LoaiDV = a.TenRG.Contains("Thuốc thường") ? 0 : (a.TenRG.Contains("Hóa chất") ? 1 : (a.TenRG.Contains("Vật tư y tế") ? 2 : (a.TenRG.Contains("Thuốc gây nghiện") ? 3 : (a.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (a.TenRG.Contains("Thuốc trẻ em") ? 5 : (a.TenRG.Contains("Thuốc đông y") ? 6 : 0))))))
                               }).ToList();
                    if (bph.Count > 0)
                    {
                        int kieudon = 0;
                        kieudon = bph.First().KieuDon.Value;

                        List<int> lLoaiDuocCt = bph.Select(p => p.LoaiDV).Distinct().ToList();
                        List<string> tieunhom = bph.Select(p => p.TenRG).Distinct().ToList();

                        #region trả thuốc
                        #region Dùng form trả thuốc hoặc trả dược cho bệnh viên 30002
                        if ((kieudon == 2 || kieudon == 4) && DungChung.Bien.MaBV == "30002")
                        {
                            if ((lLoaiDuocCt.Contains(3) || lLoaiDuocCt.Contains(4)))// thuốc gây nghiện hướng tâm thần
                            {
                                #region 30002
                                if (DungChung.Bien.MaBV == "30002")
                                {

                                    var q0 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                              join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                              join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                              join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                              group new { kdct, dv, kd, tn } by new { dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, dv.TenRG, TenRGTN = tn.TenRG } into kq
                                              select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV)), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc, TenRG = kq.Key.TenRGTN }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                    var q = (from a in q0
                                             select new
                                             {
                                                 a.TenDV,
                                                 a.DonGia,
                                                 a.DonVi,
                                                 a.LoaiDuoc,
                                                 a.MaDV,
                                                 a.MaTam,
                                                 a.SoDK,
                                                 a.SoLuong,
                                                 a.ThanhTien,
                                                 LoaiDV = a.TenRG.Contains("Thuốc thường") ? 0 : (a.TenRG.Contains("Hóa chất") ? 1 : (a.TenRG.Contains("Vật tư y tế") ? 2 : (a.TenRG.Contains("Thuốc gây nghiện") ? 3 : (a.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (a.TenRG.Contains("Thuốc trẻ em") ? 5 : (a.TenRG.Contains("Thuốc đông y") ? 6 : 0))))))
                                             }).ToList();
                                    if (q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Count() > 0)
                                    {
                                        BaoCao.PhieutrathuocGNHTT_A5 rep = new BaoCao.PhieutrathuocGNHTT_A5();
                                        //rep.Nguo.Value = DungChung.Bien.NguoiLapBieu;
                                        if (DungChung.Bien.MaBV == "12001")
                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                        rep.MauSo.Value = "MS:.../BV-01";

                                        rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        this.Dispose();
                                    }
                                }
                                #endregion
                            }
                            if (lLoaiDuocCt.Contains(0) || lLoaiDuocCt.Contains(1) || lLoaiDuocCt.Contains(2) || lLoaiDuocCt.Contains(5) || lLoaiDuocCt.Contains(6))
                            {
                                #region PLThuoc VTYT 30002
                                if (DungChung.Bien.MaBV == "30002")
                                {


                                    var q = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                             join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                             join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                             join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                             group new { kdct, dv, kd, tn } by new { dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, dv.TenRG, kd.LoaiDuoc, TenRGTN = tn.TenRG } into kq
                                             select new
                                             {
                                                 TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV)),
                                                 kq.Key.MaTam,
                                                 kq.Key.MaDV,
                                                 kq.Key.SoDK,
                                                 DonVi = kq.Key.DonVi,
                                                 SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1),
                                                 ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1),
                                                 DonGia = kq.Key.DonGia,
                                                 LoaiDuoc = kq.Key.LoaiDuoc,
                                                 LoaiDV = kq.Key.TenRGTN.Contains("Thuốc thường") ? 0 : (kq.Key.TenRGTN.Contains("Hóa chất") ? 1 : (kq.Key.TenRGTN.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRGTN.Contains("Thuốc đông y") ? 6 : 0))))))
                                             }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                    if (q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).Count() > 0)
                                    {
                                        BaoCao.PhieuTrathuocVTYT_A5 rep = new BaoCao.PhieuTrathuocVTYT_A5();
                                        rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                        rep.MauSo.Value = "MS:05D/BV-01";


                                        rep.DataSource = q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        this.Dispose();
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                        else
                            if (kieudon == 2 && (DungChung.Bien.MaBV != "30003" || (!lLoaiDuocCt.Contains(3) && !lLoaiDuocCt.Contains(4))))
                        {
                            if (DungChung.Bien.MaBV == "14018")
                            {
                                var q33 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                           join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                           join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                           join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                           select new { TenRGTN = tn.TenRG, kd.NgayKe, dv.SoDK, dv.TenRG, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, dv.TenDV, dv.TenHC, kdct, dv.MaTam, dv.HamLuong, LoaiDV = (kdct.LoaiDV == 3 || kdct.LoaiDV == 4) ? 1 : 0 }).ToList();


                                var q = (from kd in q33
                                         group new { kd } by new { kd.TenRGTN, kd.SoDK, kd.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kd.TenHC, kd.HamLuong, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.MaDV, kd.TenDV, kd.TenRG, SoLo = kd.kdct.SoLo ?? "" } into kq
                                         select new BC
                                         {
                                             HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Key.SoLo),
                                             SoLo = kq.Key.SoLo,
                                             MaDV = kq.Key.MaDV ?? 0,
                                             TenDV = kq.Key.TenDV,
                                             HamLuong = kq.Key.HamLuong,
                                             TenHC = kq.Key.TenHC,
                                             TenRG = kq.Key.TenRG,
                                             MaTam = kq.Key.MaTam,
                                             DonGia = kq.Key.DonGia,
                                             SoDK = kq.Key.SoDK,
                                             DonVi = kq.Key.DonVi,
                                             LoaiDuoc = kq.Key.LoaiDuoc,
                                             SoLuong = kq.Sum(p => p.kd.kdct.SoLuong) * (-1),
                                             ThanhTien = kq.Sum(p => p.kd.kdct.ThanhTien) * (-1)
                                         }).OrderBy(p => p.TenDV).ThenBy(p => p.DonVi).ThenBy(p => p.DonGia).ToList();

                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add("SoPL", _soPL);
                                string tenKhoNhan = "";
                                int idKN = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                var khoNhan = _dataContext.KPhongs.FirstOrDefault(p => p.MaKP == idKN);
                                if (khoNhan != null)
                                    tenKhoNhan = khoNhan.TenKP;
                                string tenKhoXuat = "";
                                int idKX = bph.First().MaKP == null ? 0 : bph.First().MaKP.Value;
                                var khoXuat = _dataContext.KPhongs.FirstOrDefault(p => p.MaKP == idKX);
                                if (khoXuat != null)
                                    tenKhoXuat = khoXuat.TenKP;
                                dic.Add("TenKhoNhan", tenKhoNhan);
                                dic.Add("TenKhoXuat", tenKhoXuat);
                                dic.Add("LyDoNhap", "Nhập trả");

                                dic.Add("SoTienChu", (DungChung.Ham.DocTienBangChu(q.Sum(o => o.ThanhTien) ?? 0, "") + " đồng"));

                                DungChung.Ham.Print(DungChung.PrintConfig.Rep_PhieuNhapKhoTraThuocThua_14018, q, dic, false);
                            }
                            else
                            {
                                if ((lLoaiDuocCt.Contains(3) || lLoaiDuocCt.Contains(4)))// thuốc gây nghiện hướng tâm thần
                                {
                                    #region 30007 _GN HTT _A5
                                    if (DungChung.Bien.MaBV == "30007")
                                    {

                                        var q0 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                  join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                  join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                  join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                  group new { kdct, dv, kd, tn } by new { dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, dv.TenRG, TenRGTN = tn.TenRG } into kq
                                                  select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV)), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc, TenRG = kq.Key.TenRGTN }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var q = (from a in q0
                                                 select new
                                                 {
                                                     a.TenDV,
                                                     a.DonGia,
                                                     a.DonVi,
                                                     a.LoaiDuoc,
                                                     a.MaDV,
                                                     a.MaTam,
                                                     a.SoDK,
                                                     a.SoLuong,
                                                     a.ThanhTien,
                                                     LoaiDV = a.TenRG.Contains("Thuốc thường") ? 0 : (a.TenRG.Contains("Hóa chất") ? 1 : (a.TenRG.Contains("Vật tư y tế") ? 2 : (a.TenRG.Contains("Thuốc gây nghiện") ? 3 : (a.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (a.TenRG.Contains("Thuốc trẻ em") ? 5 : (a.TenRG.Contains("Thuốc đông y") ? 6 : 0))))))
                                                 }).ToList();
                                        if (q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Count() > 0)
                                        {
                                            BaoCao.PhieutrathuocGNHTT_A5 rep = new BaoCao.PhieutrathuocGNHTT_A5();
                                            //rep.Nguo.Value = DungChung.Bien.NguoiLapBieu;
                                            if (DungChung.Bien.MaBV == "12001")
                                                rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                            rep.MauSo.Value = "MS:.../BV-01";

                                            rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    #endregion
                                    #region 01071
                                    else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                    {
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        var q0 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                  join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                  join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                  join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                  group new { kdct, dv, kd, tn } by new { TenRGTN = tn.TenRG, dv.HamLuong, dv.MaTam, kdct.DonGia, dv.TenHC, kdct.DonVi, kdct.MaDV, dv.TenDV, dv.TenRG, kd.LoaiDuoc, dv.SoDK, SoLo = "" } into kq
                                                  select new BC { SoLo = kq.Key.SoLo, HanDung = null, TenDV = kq.Key.TenDV, HamLuong = kq.Key.HamLuong, TenHC = kq.Key.TenHC, TenRG = kq.Key.TenRG, TenRGTN = kq.Key.TenRGTN, MaDV = kq.Key.MaDV ?? 0, MaTam = kq.Key.MaTam, DonGia = kq.Key.DonGia, SoDK = kq.Key.SoDK, DonVi = kq.Key.DonVi, LoaiDuoc = kq.Key.LoaiDuoc, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1) }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var q = (from a in q0
                                                 select new
                                                 {
                                                     a.TenDV,
                                                     a.DonGia,
                                                     a.DonVi,
                                                     a.LoaiDuoc,
                                                     a.MaDV,
                                                     a.MaTam,
                                                     a.SoDK,
                                                     a.SoLuong,
                                                     a.ThanhTien,
                                                     a.HamLuong,
                                                     a.HanDung,
                                                     a.SoLo,
                                                     a.TenHC,
                                                     a.TenRG,

                                                     LoaiDV = a.TenRGTN.Contains("Thuốc thường") ? 0 : (a.TenRGTN.Contains("Hóa chất") ? 1 : (a.TenRGTN.Contains("Vật tư y tế") ? 2 : (a.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (a.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (a.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (a.TenRGTN.Contains("Thuốc đông y") ? 6 : 0))))))
                                                 }).ToList();
                                        if (q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Count() > 0)
                                        {
                                            BaoCao.PhieulinhthuocGNHTT2lien_01071 rep = new BaoCao.PhieulinhthuocGNHTT2lien_01071();
                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                            rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                            rep.BindingData();
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    #endregion
                                    #region BV khác
                                    else
                                    {

                                        var q0 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                  join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                  join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                  join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                  group new { kdct, dv, kd, tn } by new { TenRGTN = tn.TenRG, dv.TenHC, dv.HamLuong, dv.MaTam, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, dv.TenRG, kd.LoaiDuoc, dv.SoDK, SoLo = DungChung.Bien.MaBV == "27023" ? kdct.SoLo : "", HanDung = DungChung.Bien.MaBV == "27023" ? kdct.HanDung : null } into kq
                                                  //  select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : (DungChung.Bien.MaBV == "12122" ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG :kq.Key.TenDV))), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                                  select new BC { SoLo = kq.Key.SoLo, HanDung = kq.Key.HanDung, TenDV = kq.Key.TenDV, HamLuong = kq.Key.HamLuong, TenHC = kq.Key.TenHC, TenRG = kq.Key.TenRG, TenRGTN = kq.Key.TenRGTN, MaTam = kq.Key.MaTam, DonGia = kq.Key.DonGia, SoDK = kq.Key.SoDK, DonVi = kq.Key.DonVi, LoaiDuoc = kq.Key.LoaiDuoc, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1) }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var q = (from a in q0
                                                 select new BC
                                                 {
                                                     TenDV = a.TenDV,
                                                     DonGia = a.DonGia,
                                                     DonVi = a.DonVi,
                                                     LoaiDuoc = a.LoaiDuoc,
                                                     MaDV = a.MaDV,
                                                     MaTam = a.MaTam,
                                                     SoDK = a.SoDK,
                                                     SoLuong = a.SoLuong,
                                                     ThanhTien = a.ThanhTien,
                                                     HamLuong = a.HamLuong,
                                                     HanDung = a.HanDung,
                                                     SoLo = a.SoLo,
                                                     TenHC = a.TenHC,
                                                     TenRG = a.TenRG,

                                                     LoaiDV = a.TenRGTN.Contains("Thuốc thường") ? 0 : (a.TenRGTN.Contains("Hóa chất") ? 1 : (a.TenRGTN.Contains("Vật tư y tế") ? 2 : (a.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (a.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (a.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (a.TenRGTN.Contains("Thuốc đông y") ? 6 : 0))))))
                                                 }).ToList();
                                        foreach (BC a in q)
                                        {
                                            if (DungChung.Bien.MaBV == "30007")
                                            {
                                                a.TenDV = a.TenDV + " (" + a.TenHC + ": " + a.HamLuong + ") ";
                                            }
                                            else if (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009")
                                            {
                                                a.TenDV = a.TenDV + " " + a.HamLuong;
                                            }
                                            else if (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009")
                                            {
                                                a.TenDV = a.TenRG ?? "";
                                            }
                                            else if (DungChung.Bien.MaBV == "27023")
                                            {
                                                a.TenDV = a.TenDV + (string.IsNullOrEmpty(a.HamLuong) ? "" : " (" + a.HamLuong + ") ");
                                            }
                                            //if (DungChung.Bien.MaBV == "27023")
                                            //{
                                            //    var qDGdv = qDonGiaDV.Where(p => p.MaDV == a.MaDV && p.DonGiaN == a.DonGia).FirstOrDefault();
                                            //    if (qDGdv != null)
                                            //    {
                                            //        a.SoLo = qDGdv.SoLo;
                                            //        a.HanDung = qDGdv.HanDung;
                                            //    }
                                            //}
                                        }
                                        if (q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Count() > 0)
                                        {
                                            #region 24009
                                            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27023")
                                            {
                                                int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                                BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT(0);
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                rep.xrTableCell28.Text = "";
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                    case 2:
                                                        rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        break;
                                                }



                                                rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC GÂY NGHIỆN, THUỐC HƯỚNG TÂM THẦN, THUỐC TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:01D/BV-01";



                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #region bv 27023
                                            //else if (DungChung.Bien.MaBV == "27023")
                                            //{

                                            //    BaoCao.PhieutrathuocGNHTT_27023 rep = new BaoCao.PhieutrathuocGNHTT_27023();
                                            //    rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            //    rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            //    rep.SoPL.Value = _soPL.ToString();
                                            //    rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            //    rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            //    rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            //    rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            //    rep.Khoa.Value = bph.First().TenKP;
                                            //    int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            //    var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                            //    if (tenkho.Count > 0)
                                            //        rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                            //    rep.MauSo.Value = "MS:.../BV-01";

                                            //    rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                            //    rep.BindingData();
                                            //    rep.CreateDocument();
                                            //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            //    frm.ShowDialog();
                                            //    this.Dispose();
                                            //}
                                            #endregion
                                            #region bv khác
                                            else
                                            {
                                                BaoCao.PhieutrathuocGNHTT rep = new BaoCao.PhieutrathuocGNHTT();
                                                //rep.Nguo.Value = DungChung.Bien.NguoiLapBieu;
                                                if (DungChung.Bien.MaBV == "12001")
                                                    rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                                rep.MauSo.Value = "MS:.../BV-01";

                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                                if (lLoaiDuocCt.Contains(0) || lLoaiDuocCt.Contains(1) || lLoaiDuocCt.Contains(2) || lLoaiDuocCt.Contains(5) || lLoaiDuocCt.Contains(6))
                                {

                                    #region BV khác
                                    var q33 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                               join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                               join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                               join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                               select new { TenRGTN = tn.TenRG, kd.NgayKe, dv.SoDK, dv.TenRG, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, dv.TenDV, dv.TenHC, kdct, dv.MaTam, dv.HamLuong, LoaiDV = (kdct.LoaiDV == 3 || kdct.LoaiDV == 4) ? 1 : 0, kd.GhiChu, }).ToList();


                                    var q = (from kd in q33
                                             group new { kd } by new { kd.TenRGTN, kd.SoDK, kd.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kd.TenHC, kd.HamLuong, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.MaDV, kd.TenDV, kd.TenRG, SoLo = kd.kdct.SoLo ?? "", kd.GhiChu } into kq
                                             // select new { TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : (DungChung.Bien.MaBV == "12122" ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV))), kq.Key.MaTam, kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                             select new BC
                                             {
                                                 LoaiDV = kq.Key.TenRGTN.Contains("Thuốc thường") ? 0 : (kq.Key.TenRGTN.Contains("Hóa chất") ? 1 : (kq.Key.TenRGTN.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRGTN.Contains("Thuốc đông y") ? 6 : 0))))))
                                                 ,
                                                 HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Key.SoLo),
                                                 SoLo = kq.Key.SoLo,
                                                 MaDV = kq.Key.MaDV ?? 0,
                                                 TenDV = kq.Key.TenDV,
                                                 HamLuong = kq.Key.HamLuong,
                                                 TenHC = kq.Key.TenHC,
                                                 TenRG = kq.Key.TenRG,
                                                 MaTam = kq.Key.MaTam,
                                                 DonGia = kq.Key.DonGia,
                                                 SoDK = kq.Key.SoDK,
                                                 DonVi = kq.Key.DonVi,
                                                 LoaiDuoc = kq.Key.LoaiDuoc,
                                                 SoLuong = kq.Sum(p => p.kd.kdct.SoLuong) * (-1),
                                                 ThanhTien = kq.Sum(p => p.kd.kdct.ThanhTien) * (-1),
                                                 GhiChu = kq.Key.GhiChu,

                                             }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();

                                    foreach (BC a in q)
                                    {
                                        if (DungChung.Bien.MaBV == "30007")
                                        {
                                            a.TenDV = a.TenDV + " (" + a.TenHC + ": " + a.HamLuong + ") ";
                                        }
                                        else if (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                        {
                                            a.TenDV = a.TenDV + " " + a.HamLuong;
                                        }
                                        else if (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009")
                                        {
                                            a.TenDV = a.TenRG ?? "";
                                        }
                                        else if (DungChung.Bien.MaBV == "27023")
                                        {
                                            a.TenDV = a.TenDV + (string.IsNullOrEmpty(a.HamLuong) ? "" : " (" + a.HamLuong + ") ");
                                        }
                                        //if (DungChung.Bien.MaBV == "27023")
                                        //{
                                        //    var qDGdv = qDonGiaDV.Where(p => p.MaDV == a.MaDV && p.DonGiaN == a.DonGia).FirstOrDefault();
                                        //    if (qDGdv != null)
                                        //    {
                                        //        a.SoLo = qDGdv.SoLo;
                                        //        a.HanDung = qDGdv.HanDung;
                                        //    }
                                        //}
                                    }
                                    #region 27023
                                    if (DungChung.Bien.MaBV == "27023")
                                    {
                                        if (q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).Count() > 0)
                                        {
                                            BaoCao.PhieuTrathuocVTYT_27023 rep = new BaoCao.PhieuTrathuocVTYT_27023();
                                            rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                            rep.MauSo.Value = "MS:05D/BV-01";

                                            rep.DataSource = q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).OrderBy(p => p.DonVi).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    #endregion
                                    #region 01071
                                    else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                    {
                                        if (q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).Count() > 0)
                                        {
                                            BaoCao.PhieulinhthuocVTYT_A5_2lien_01071 rep = new BaoCao.PhieulinhthuocVTYT_A5_2lien_01071();
                                            //rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                            rep.MauSo.Value = "MS:05D/BV-01";

                                            rep.DataSource = q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).OrderBy(p => p.DonVi).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    #endregion
                                    #region bv khác
                                    else
                                    {
                                        if (q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).Count() > 0)
                                        {
                                            if (DungChung.Bien.MaBV == "34019")
                                            {
                                                BaoCao.PhieuTrathuocVTYT_A5 rep = new BaoCao.PhieuTrathuocVTYT_A5();
                                                rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                                rep.MauSo.Value = "MS:05D/BV-01";
                                                var qbc34019 = q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).OrderBy(p => p.DonVi).ToList();
                                                rep.DataSource = qbc34019;
                                                //rep.lblTongSoKhoan.Text = "Tổng số khoản: " + qbc34019.Count();
                                                //rep.celThoiGian.Text = "Ngày " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            else if (DungChung.Bien.MaBV == "14017")
                                            {
                                                for (int i = 0; i <= 6; i++)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        BaoCao.PhieuTrathuocVTYT rep = new BaoCao.PhieuTrathuocVTYT();
                                                        rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep.SoPL.Value = _soPL.ToString();
                                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;

                                                        rep.Khoa.Value = bph.First().TenKP.ToUpper();
                                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                                        if (tenkho.Count > 0)
                                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                                                        if (tekho == 27)
                                                        {
                                                            string a = bph.First().GhiChu == null ? "" : bph.First().GhiChu;
                                                            if (a != "" && a.Contains(";") && a.Split(';').Length >= 3)
                                                            {
                                                                ngay1 = a.Split(';')[1];
                                                                ngay2 = a.Split(';')[2];
                                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                            }
                                                            else
                                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                        }
                                                        else
                                                            rep.theongay.Value = "";
                                                        rep.MauSo.Value = "MS:05D/BV-01";

                                                        rep.xrLabel1.Text = DungChung.Ham.NgaySangChu(Convert.ToDateTime(q33.First().NgayKe));
                                                        rep.xrTableCell67.Text = q.Where(p => p.LoaiDV == i).Count().ToString();
                                                        //rep.xrTableCell75.Text = q.Sum(p => p.ThanhTien).ToString();
                                                        //rep.TTien.Value = q.First().ThanhTien;
                                                        //rep.xrTableCell75.Text = q.First().ThanhTien.ToString();
                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.TenDV).ToList();
                                                        rep.BindingData();
                                                        //rep.DataMember = "";
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                        this.Dispose();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                BaoCao.PhieuTrathuocVTYT rep = new BaoCao.PhieuTrathuocVTYT();
                                                rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                                rep.MauSo.Value = "MS:05D/BV-01";
                                                rep.xrTableCell67.Text = q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).Count().ToString();
                                                rep.DataSource = q.Where(p => p.LoaiDV != 3 && p.LoaiDV != 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                        }
                                    }
                                    #endregion

                                    #endregion
                                }
                            }
                        }

                        #endregion
                        #region kiểu đơn không phải là kiểu đơn trả thuốc
                        else
                        {
                            kieudon = bph.First().KieuDon.Value;
                            int check_linh_ve_khoa = 0;

                            if (kieudon == 3)
                            {
                                int mbnct = 0;
                                if(bph.First().MaBNhanChiTiet!=null)
                                {
                                    mbnct = bph.First().MaBNhanChiTiet.Value;
                                }
                                
                                string ploai = bph.First().PLoai;
                                if (ploai == "Tủ trực")
                                {
                                    check_linh_ve_khoa = 1; //linhx ve khoa
                                }
                                if (ploai == "Tủ trực" && mbnct > 0 && mbnct != null)
                                {
                                    check_linh_ve_khoa = 2; //linh bu tu truc
                                }
                                if (ploai == "Tủ trực" && (mbnct == 0 || mbnct == null))
                                {
                                    check_linh_ve_khoa = 3; //linh bu tu truc
                                }

                            }
                            var q2 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                      join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                      join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                      join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                      select new { kd, kdct, dv, tn }).ToList();

                            var q = (from kd in q2
                                     group kd by new
                                     {
                                         kd.dv.TenHC,
                                         kd.dv.HamLuong,
                                         TenRGTN = kd.tn.TenRG,
                                         kd.dv.MaTam,
                                         kd.kdct.DonGia,
                                         kd.kdct.DonVi,
                                         kd.kdct.MaDV,
                                         kd.dv.TenDV,
                                         TenRGDV = kd.dv.TenRG,
                                         kd.kd.LoaiDuoc,
                                         kd.kdct.XHH,
                                         LoaiDV = kd.tn.TenRG.Contains("Thuốc thường") ? 0 : (kd.tn.TenRG.Contains("Hóa chất") ? 1 : (kd.tn.TenRG.Contains("Vật tư y tế") ? 2 : (kd.tn.TenRG.Contains("Thuốc gây nghiện") ? 3 : (kd.tn.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (kd.tn.TenRG.Contains("Thuốc trẻ em") ? 5 : (kd.tn.TenRG.Contains("Thuốc đông y") ? 6 : 0)))))),
                                         SoLo = DungChung.Bien.MaBV == "27023" ? kd.kdct.SoLo : "",
                                         HanDung = DungChung.Bien.MaBV == "27023" ? kd.kdct.HanDung : null
                                     } into kq
                                     select new BC { TenHC = kq.Key.TenHC, LoaiDV = kq.Key.LoaiDV, SoLo = kq.Key.SoLo, HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Key.SoLo), TenRGTN = kq.Key.TenRGTN, TenRGDV = kq.Key.TenRGDV, TenDV = kq.Key.TenDV, HamLuong = kq.Key.HamLuong, MaTam = kq.Key.MaTam, MaDV = kq.Key.MaDV ?? 0, DonVi = kq.Key.DonVi, SoLuong = (kieudon == 2 && DungChung.Bien.MaBV == "30003") ? (-kq.Sum(p => p.kdct.SoLuong)) : kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                            foreach (BC a in q)
                            {
                                if (DungChung.Bien.MaBV == "30007")
                                {
                                    a.TenDV = a.TenDV + " (" + a.TenHC + ": " + a.HamLuong + ") ";
                                }
                                else if (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009")
                                {
                                    a.TenDV = a.TenDV + " " + a.HamLuong;
                                }

                                else if (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009")
                                {
                                    a.TenDV = a.TenRGDV ?? "";
                                }
                                else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                    a.TenDV = a.TenDV + " " + a.HamLuong;
                                else if (DungChung.Bien.MaBV == "27023")
                                {
                                    a.TenDV = a.TenDV + (string.IsNullOrEmpty(a.HamLuong) ? "" : " (" + a.HamLuong + ") ");
                                }
                                //if (DungChung.Bien.MaBV == "27023")
                                //{
                                //    var qDGdv = qDonGiaDV.Where(p => p.MaDV == a.MaDV && p.DonGiaN == a.DonGia).FirstOrDefault();
                                //    if (qDGdv != null)
                                //    {
                                //        a.SoLo = qDGdv.SoLo;
                                //        a.HanDung = qDGdv.HanDung;
                                //    }
                                //}
                            }

                            // int loaiduoc = bph.First().LoaiDuoc.Value;

                            #region (loaiduoc == 3 || loaiduoc == 4)
                            if (lLoaiDuocCt.Contains(3) || lLoaiDuocCt.Contains(4))// thuốc gây nghiện hướng tâm thần
                            {
                                if (q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Count() > 0)
                                {
                                    #region   ( "30009" , "30003", "19048","30004","30002")
                                    if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "19048" || DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "30002")// || DungChung.Bien.MaBV == "01071") // dung 0609
                                    {
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                        BaoCao.PhieulinhthuocGNHTT2lien rep = new BaoCao.PhieulinhthuocGNHTT2lien();
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        if (DungChung.Bien.MaBV == "12001")
                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                        rep.Khoa.Value = bph.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                        switch (kieudon)
                                        {
                                            case 0:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                break;
                                            case 1:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                break;
                                            case 5:
                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                break;
                                            case 2:
                                                rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                break;
                                        }

                                        rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                        rep.MauSo.Value = "MS:08";
                                        if (DungChung.Bien.MaBV == "30002")
                                        {
                                            double a = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Sum(p => p.ThanhTien) ?? 0;
                                            rep.celThanhTien2.Text = a.ToString("###,###.00");
                                            rep.colTongG.Text = a.ToString("###,###.00");
                                        }



                                        rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();
                                        this.Dispose();
                                    }

                                    #endregion
                                    else
                                    {
                                        #region 24009
                                        if (DungChung.Bien.MaBV == "24009")
                                        {
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                            BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT(6);
                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            rep.xrTableCell28.Text = "";
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }



                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN, THUỐC HƯỚNG TÂM THẦN, THUỐC TIỀN CHẤT");
                                            rep.MauSo.Value = "MS:01D/BV-01";



                                            rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                        #endregion
                                        #region 01071
                                        else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789")
                                        {
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                            BaoCao.PhieulinhthuocGNHTT2lien_01071 rep = new BaoCao.PhieulinhthuocGNHTT2lien_01071();
                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            if (DungChung.Bien.MaBV == "12001")
                                                rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                case 2:
                                                    rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    break;
                                                case 3:
                                                    if (check_linh_ve_khoa == 1)
                                                    {
                                                        rep.Chuthich.Value = "Loại phiếu: Lĩnh về khoa";
                                                    }
                                                    if (check_linh_ve_khoa == 2)
                                                    {
                                                        rep.Chuthich.Value = "Loại phiếu: Lĩnh bù tủ trực";
                                                    }
                                                    if (check_linh_ve_khoa == 3)
                                                    {
                                                        rep.Chuthich.Value = "Loại phiếu: Bổ sung tủ trực";
                                                    }
                                                    break;
                                            }




                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");

                                            rep.MauSo.Value = "MS:08";



                                            rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }

                                        #endregion
                                        #region 30002
                                        else if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "14018")
                                        {
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();

                                            BaoCao.PhieulinhthuocGNHTT2lien rep = new BaoCao.PhieulinhthuocGNHTT2lien();
                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }



                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                            rep.MauSo.Value = "MS:08";


                                            rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                        #endregion
                                        else
                                        {
                                            int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                            #region 27023
                                            if (DungChung.Bien.MaBV == "27023")
                                            {
                                                BaoCao.PhieulinhthuocGNHTT_27023 rep = new BaoCao.PhieulinhthuocGNHTT_27023();
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";
                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #region 30007 _ A5 1 liên
                                            else if (DungChung.Bien.MaBV == "30007")
                                            {
                                                BaoCao.PhieulinhthuocGNHTT_A5_1lien rep = new BaoCao.PhieulinhthuocGNHTT_A5_1lien();
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";

                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }


                                            #endregion
                                            #region bv khác

                                            else if (DungChung.Bien.MaBV == "24012" && (tieunhom.Contains("Thuốc gây nghiện") || tieunhom.Contains("Thuốc hướng tâm thần")))
                                            {

                                                bool tt = false;
                                                if (tieunhom.Contains("Thuốc hướng tâm thần"))
                                                    tt = true;
                                                BaoCao.PhieulinhthuocGNHTT_24012 rep = new BaoCao.PhieulinhthuocGNHTT_24012(tt);
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;


                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                {
                                                    if (DungChung.Bien.MaBV == "01830" && tenkho.First().PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                                                    {
                                                        rep.Kholinhnew.Value = "Lĩnh bù về: " + tenkho.First().TenKP;
                                                        rep.Kholinh.Value = "Kho lĩnh: kho gây nghiện hướng thần";
                                                    }
                                                    else
                                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                }


                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }
                                                if (!tt)
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN");
                                                else
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN");
                                                rep.MauSo.Value = "MS:08";

                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }

                                            else if (DungChung.Bien.MaBV == "34019")
                                            {
                                                BaoCao.PhieulinhthuocGNHTT_34019 rep = new BaoCao.PhieulinhthuocGNHTT_34019();
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                {
                                                    if (DungChung.Bien.MaBV == "01830" && tenkho.First().PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                                                    {
                                                        rep.Kholinhnew.Value = "Lĩnh bù về: " + tenkho.First().TenKP;
                                                        rep.Kholinh.Value = "Kho lĩnh: kho gây nghiện hướng thần";
                                                    }
                                                    else
                                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                }


                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }
                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                rep.MauSo.Value = "MS:08";

                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            else
                                            {
                                                BaoCao.PhieulinhthuocGNHTT rep = new BaoCao.PhieulinhthuocGNHTT();
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                {
                                                    if (DungChung.Bien.MaBV == "01830" && tenkho.First().PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                                                    {
                                                        rep.Kholinhnew.Value = "Lĩnh bù về: " + tenkho.First().TenKP;
                                                        rep.Kholinh.Value = "Kho lĩnh: kho gây nghiện hướng thần";
                                                    }
                                                    else
                                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                }


                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }

                                                if (DungChung.Bien.MaBV == "24012" && tieunhom.Contains("Thuốc hướng tâm thần"))
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN");
                                                else
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");

                                                rep.MauSo.Value = "MS:08";

                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion

                                        }
                                    }
                                }
                            }
                            //#endregion
                            if (lLoaiDuocCt.Contains(0) || lLoaiDuocCt.Contains(1) || lLoaiDuocCt.Contains(2) || lLoaiDuocCt.Contains(5) || lLoaiDuocCt.Contains(6))
                            {
                                if (lLoaiDuocCt.Contains(6))// thuốc đông y
                                {
                                    #region 12001
                                    if (DungChung.Bien.MaBV == "12001")
                                    {
                                        var q61 = (from bn in _dataContext.BenhNhans
                                                   join kd in _dataContext.DThuocs on bn.MaBNhan equals kd.MaBNhan
                                                   join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                   join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                   join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                   select new
                                                   {
                                                       kd.MaKP,
                                                       bn.TenBNhan,
                                                       bn.DChi,
                                                       bn.MaBNhan,
                                                       MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong,
                                                       DTuong = bn.DTuong == null ? "" : bn.DTuong,
                                                       TenDV = dv.TenDV,
                                                       dv.MaDV,
                                                       dv.MaTam,
                                                       DonVi = dv.DonVi,
                                                       SoLuong = kdct.SoLuong,
                                                       DonGia = kdct.DonGia,
                                                       LoaiDuoc = kd.LoaiDuoc,
                                                       tn.TenRG,
                                                   }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var q6 = (from bn in q61
                                                  where (_makp == 0 ? true : bn.MaKP == _makp)
                                                  group new { bn } by new { bn.MaTam, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.DonGia, bn.DonVi, bn.MaDV, bn.TenDV, bn.LoaiDuoc, bn.TenRG } into kq
                                                  select new
                                                  {
                                                      kq.Key.TenBNhan,
                                                      kq.Key.DChi,
                                                      TenDV = kq.Key.TenDV,
                                                      kq.Key.MaDV,
                                                      kq.Key.MaTam,
                                                      DonVi = kq.Key.DonVi,
                                                      SoLuong139 = kq.Where(p => ((p.bn.DTuong == ("BHYT")) && (p.bn.MaDTuong == ("DT") || p.bn.MaDTuong == ("HN") || p.bn.MaDTuong == ("DK")))).Sum(p => p.bn.SoLuong),
                                                      SoLuongTE = kq.Where(p => p.bn.DTuong == ("BHYT") && p.bn.MaDTuong == ("TE")).Sum(p => p.bn.SoLuong),
                                                      SoLuongBHYT = kq.Where(p => p.bn.DTuong == ("BHYT") && p.bn.MaDTuong != "DT" && p.bn.MaDTuong != "HN" && p.bn.MaDTuong != "DK" && p.bn.MaDTuong != "TE").Sum(p => p.bn.SoLuong),
                                                      SoLuongDichVu = kq.Where(p => p.bn.DTuong == ("Dịch vụ")).Sum(p => p.bn.SoLuong),
                                                      SoLuong = kq.Sum(p => p.bn.SoLuong),
                                                      DonGia = kq.Key.DonGia,
                                                      LoaiDuoc = kq.Key.LoaiDuoc,
                                                      LoaiDV = kq.Key.TenRG.Contains("Thuốc thường") ? 0 : (kq.Key.TenRG.Contains("Hóa chất") ? 1 : (kq.Key.TenRG.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRG.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRG.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRG.Contains("Thuốc đông y") ? 6 : 0)))))),
                                                  }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (q6.Where(p => p.LoaiDV == 6).Count() > 0)
                                        {
                                            BaoCao.PhieulinhthuocVTYT_TD rep = new BaoCao.PhieulinhthuocVTYT_TD(6);
                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;

                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }


                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                            rep.MauSo.Value = "MS:...D/BV-01";

                                            if (DungChung.Bien.MaBV == "30009")
                                            {
                                                var q7 = (from dongy in q6
                                                          group new { dongy } by new { dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc, dongy.LoaiDV } into kq
                                                          select new { TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc, kq.Key.LoaiDV }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                                rep.DataSource = q7.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();
                                            }
                                            else
                                            {
                                                rep.DataSource = q6.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();
                                            }
                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    else if (DungChung.Bien.MaBV == "30002")
                                    {
                                        var q6 = (
                                                    from bn in _dataContext.BenhNhans
                                                    join kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp) on bn.MaBNhan equals kd.MaBNhan
                                                    join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                    join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                    join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                    group new { kdct, dv, kd, bn, tn } by new { dv.TenHC, dv.HamLuong, dv.MaTam, bn.Tuoi, kd.MaBNhan, bn.TenBNhan, bn.DChi, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, tn.TenRG } into kq
                                                    select new
                                                    {
                                                        kq.Key.MaBNhan,
                                                        kq.Key.TenBNhan,
                                                        kq.Key.MaTam,
                                                        Tuoi = kq.Key.Tuoi,
                                                        kq.Key.DChi,
                                                        TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV),
                                                        kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        SoLuong = kq.Sum(p => p.kdct.SoLuong),
                                                        DonGia = kq.Key.DonGia,
                                                        LoaiDuoc = kq.Key.LoaiDuoc,
                                                        LoaiDV = kq.Key.TenRG.Contains("Thuốc thường") ? 0 : (kq.Key.TenRG.Contains("Hóa chất") ? 1 : (kq.Key.TenRG.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRG.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRG.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRG.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRG.Contains("Thuốc đông y") ? 6 : 0)))))),
                                                    }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (q6.Where(p => p.LoaiDV == 6).Count() > 0)
                                        {
                                            BaoCao.PhieulinhthuocVTYT_A5 rep = new BaoCao.PhieulinhthuocVTYT_A5();

                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep.SoPL.Value = _soPL.ToString();
                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                            rep.Khoa.Value = bph.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                            switch (kieudon)
                                            {
                                                case 0:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                    break;
                                                case 1:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                    break;
                                                case 5:
                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                    break;
                                                    //    case 2:
                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                    //break;
                                            }


                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                            rep.MauSo.Value = "MS:...D/BV-01";


                                            var q7 = (from dongy in q6.Where(p => p.LoaiDV == 6)
                                                      group new { dongy } by new { dongy.MaTam, dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc } into kq
                                                      select new { TenDV = kq.Key.TenDV, kq.Key.MaTam, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                            rep.DataSource = q7.OrderBy(p => p.DonVi).ToList();

                                            rep.BindingData();
                                            //rep.DataMember = "";
                                            rep.CreateDocument();
                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            frm.ShowDialog();
                                            this.Dispose();
                                        }
                                    }
                                    #endregion
                                    #region bv khác
                                    else
                                    {

                                        var q61 = (from
                                                       bn in _dataContext.BenhNhans
                                                   join
                                                       kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp) on bn.MaBNhan equals kd.MaBNhan
                                                   join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                   join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                   join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                   select new { bn.MaBNhan, bn.Tuoi, bn.DChi, bn.TenBNhan, kd, kdct, dv, tn }).ToList();
                                        var q6 = (
                                            from kd in q61
                                            group kd by new { kd.dv.TenHC, kd.dv.HamLuong, kd.dv.MaTam, kd.Tuoi, kd.kd.MaBNhan, kd.TenBNhan, kd.DChi, kd.kdct.DonGia, kd.kdct.ThanhTien, kd.kdct.DonVi, kd.kdct.MaDV, kd.dv.TenDV, kd.kd.LoaiDuoc, kd.dv.TenRG, SoLo = DungChung.Bien.MaBV == "27023" ? kd.kdct.SoLo : "", HanDung = DungChung.Bien.MaBV == "27023" ? kd.kdct.HanDung : null, TenRGTN = kd.tn.TenRG } into kq
                                            select new
                                            {
                                                kq.Key.MaBNhan,
                                                SoLo = kq.Key.SoLo,
                                                HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Key.SoLo),
                                                kq.Key.TenBNhan,
                                                kq.Key.MaTam,
                                                Tuoi = kq.Key.Tuoi,
                                                kq.Key.DChi,
                                                TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : ((DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009") ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : (DungChung.Bien.MaBV == "27023") ? (kq.Key.TenDV + (string.IsNullOrEmpty(kq.Key.HamLuong) ? "" : " (" + kq.Key.HamLuong + ") ")) : kq.Key.TenDV))),
                                                kq.Key.MaDV,
                                                DonVi = kq.Key.DonVi,
                                                SoLuong = kq.Sum(p => p.kdct.SoLuong),
                                                DonGia = kq.Key.DonGia,
                                                ThanhTien = kq.Key.ThanhTien,
                                                LoaiDuoc = kq.Key.LoaiDuoc,
                                                LoaiDV = kq.Key.TenRGTN.Contains("Thuốc thường") ? 0 : (kq.Key.TenRGTN.Contains("Hóa chất") ? 1 : (kq.Key.TenRGTN.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRGTN.Contains("Thuốc đông y") ? 6 : 0)))))),
                                            }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (q6.Where(p => p.LoaiDV == 6).Count() > 0)
                                        {
                                            #region 27023
                                            if (DungChung.Bien.MaBV == "27023")
                                            {
                                                BaoCao.PhieulinhthuocVTYT_27023 rep = new BaoCao.PhieulinhthuocVTYT_27023(6);

                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }


                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";


                                                if (DungChung.Bien.MaBV == "30009")
                                                {
                                                    var q7 = (from dongy in q6.Where(p => p.LoaiDV == 6)
                                                              group new { dongy } by new { dongy.MaTam, dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc } into kq
                                                              select new { TenDV = kq.Key.TenDV, kq.Key.MaTam, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                                    rep.DataSource = q7.OrderBy(p => p.DonVi).ToList();
                                                }
                                                else
                                                {
                                                    rep.DataSource = q6.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();
                                                }
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #region bv khác
                                            else
                                            #region 27021
                                                    if (DungChung.Bien.MaBV == "27021")
                                            {
                                                BaoCao.PhieulinhthuocVTYT27021 rep = new BaoCao.PhieulinhthuocVTYT27021(6);

                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }



                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";



                                                rep.DataSource = q6.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();

                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #region 01071
                                            else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                            {
                                                BaoCao.PhieulinhthuocVTYT_A5_2lien_01071 rep = new BaoCao.PhieulinhthuocVTYT_A5_2lien_01071(6);

                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }


                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";


                                                rep.DataSource = q6.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();

                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #region chung

                                            else
                                            {
                                                BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT(6);

                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                rep.Khoa.Value = bph.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }



                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                if (DungChung.Bien.MaBV == "30009")
                                                {
                                                    var q7 = (from dongy in q6.Where(p => p.LoaiDV == 6)
                                                              group new { dongy } by new { dongy.MaTam, dongy.DonGia, dongy.DonVi, dongy.MaDV, dongy.TenDV, dongy.LoaiDuoc } into kq
                                                              select new { TenDV = kq.Key.TenDV, kq.Key.MaTam, kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.dongy.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                                    rep.DataSource = q7.OrderBy(p => p.DonVi).ToList();
                                                }
                                                else
                                                {
                                                    rep.DataSource = q6.Where(p => p.LoaiDV == 6).OrderBy(p => p.DonVi).ToList();
                                                }
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            #endregion
                                        }
                                    }
                                    #endregion
                                    //}
                                }
                                if (lLoaiDuocCt.Contains(0) || lLoaiDuocCt.Contains(1) || lLoaiDuocCt.Contains(2) || lLoaiDuocCt.Contains(5))
                                {

                                    #region 12001
                                    if (DungChung.Bien.MaBV == "12001")
                                    {
                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        var qTD3 = (from
                                                     bn in _dataContext.BenhNhans
                                                    join
                                                        kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp) on bn.MaBNhan equals kd.MaBNhan
                                                    join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                    join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                    join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                    select new { bn.Tuoi, bn.DChi, bn.MaDTuong, bn.DTuong, bn.TenBNhan, kd, kdct, dv, tn }).ToList();
                                        var qTD2 = (from kd in qTD3
                                                    group kd by new { kd.dv.TenHC, kd.dv.HamLuong, kd.dv.MaTam, kd.MaDTuong, kd.DTuong, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.MaDV, kd.dv.TenDV, kd.kd.LoaiDuoc, TenRGTN = kd.tn.TenRG } into kq
                                                    select new
                                                    {
                                                        kq.Key.MaTam,
                                                        MaDTuong = kq.Key.MaDTuong,
                                                        DTuong = kq.Key.DTuong,
                                                        TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : (DungChung.Bien.MaBV == "12122" ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : kq.Key.TenDV)),
                                                        kq.Key.MaDV,
                                                        DonVi = kq.Key.DonVi,
                                                        SoLuong = kq.Sum(p => p.kdct.SoLuong),
                                                        DonGia = kq.Key.DonGia,
                                                        LoaiDuoc = kq.Key.LoaiDuoc,
                                                        LoaiDV = kq.Key.TenRGTN.Contains("Thuốc thường") ? 0 : (kq.Key.TenRGTN.Contains("Hóa chất") ? 1 : (kq.Key.TenRGTN.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRGTN.Contains("Thuốc đông y") ? 6 : 0)))))),
                                                    }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        var qTD = (from kqe in qTD2
                                                   group kqe by new { kqe.DonGia, kqe.MaTam, kqe.DonVi, kqe.MaDV, kqe.TenDV, kqe.LoaiDuoc, kqe.LoaiDV } into kq
                                                   select new
                                                   {
                                                       kq.Key.MaTam,
                                                       TenDV = kq.Key.TenDV,
                                                       MaDV = kq.Key.MaDV,
                                                       DonVi = kq.Key.DonVi,
                                                       SoLuong = kq.Sum(p => p.SoLuong),
                                                       SoLuong139 = kq.Where(p => p.MaDTuong == ("DT") || p.MaDTuong == ("HN") || p.MaDTuong == ("DK")).Sum(p => p.SoLuong),
                                                       SoLuongTE = kq.Where(p => p.MaDTuong.Contains("TE")).Sum(p => p.SoLuong),
                                                       SoLuongBHYT = kq.Where(p => p.DTuong == ("BHYT") && p.MaDTuong != "DT" && p.MaDTuong != "HN" && p.MaDTuong != "DK" && p.MaDTuong != "TE").Sum(p => p.SoLuong),
                                                       SoLuongDichVu = kq.Where(p => p.DTuong == ("Dịch vụ")).Sum(p => p.SoLuong),
                                                       DonGia = kq.Key.DonGia,
                                                       LoaiDuoc = kq.Key.LoaiDuoc,
                                                       kq.Key.LoaiDV
                                                   }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (i != 4 && i != 3)
                                            {
                                                if (qTD.Where(p => p.LoaiDV == i).Count() > 0)
                                                {
                                                    BaoCao.PhieulinhthuocVTYT_TD rep = new BaoCao.PhieulinhthuocVTYT_TD();
                                                    rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                    rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                    rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                    rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                    rep.SoPL.Value = _soPL.ToString();
                                                    rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                    rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                    rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                    rep.Khoa.Value = bph.First().TenKP;
                                                    if (tenkho.Count > 0)
                                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                    switch (kieudon)
                                                    {
                                                        case 0:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                            break;
                                                        case 1:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                            break;
                                                        case 5:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                            break;
                                                            //    case 2:
                                                            //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                            //break;
                                                    }


                                                    switch (i)
                                                    {
                                                        case 0:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                            rep.MauSo.Value = "MS:01D/BV-01";
                                                            break;
                                                        case 1:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                            rep.MauSo.Value = "MS:02D/BV-01";
                                                            break;
                                                        case 2:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                            rep.MauSo.Value = "MS:03D/BV-01";
                                                            break;
                                                        case 3:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                            rep.MauSo.Value = "MS:08";
                                                            break;
                                                        case 4:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                            rep.MauSo.Value = "MS:08";
                                                            break;
                                                        case 5:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                            rep.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                        case 6:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                            rep.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                    }


                                                    rep.DataSource = qTD.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                    rep.BindingData();
                                                    //rep.DataMember = "";
                                                    rep.CreateDocument();
                                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                    frm.ShowDialog();
                                                    this.Dispose();
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    #region 30002
                                    else if (DungChung.Bien.MaBV == "30002")
                                    {
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (i != 4 && i != 3)
                                            {
                                                if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                {
                                                    int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                                    var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                                    BaoCao.PhieulinhthuocVTYT_A5 rep = new BaoCao.PhieulinhthuocVTYT_A5();
                                                    rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                    //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                    rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                    rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                    rep.SoPL.Value = _soPL.ToString();
                                                    rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                    rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                    rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                    rep.Khoa.Value = bph.First().TenKP;
                                                    if (tenkho.Count > 0)
                                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                    switch (kieudon)
                                                    {
                                                        case 0:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                            break;
                                                        case 1:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                            break;
                                                        case 5:
                                                            rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                            break;
                                                            //    case 2:
                                                            //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                            //break;
                                                    }


                                                    switch (i)
                                                    {
                                                        case 0:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                            rep.MauSo.Value = "MS:01D/BV-01";
                                                            break;
                                                        case 1:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                            rep.MauSo.Value = "MS:02D/BV-01";
                                                            break;
                                                        case 2:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                            rep.MauSo.Value = "MS:03D/BV-01";
                                                            break;
                                                        case 3:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                            rep.MauSo.Value = "MS:08";
                                                            break;
                                                        case 4:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                            rep.MauSo.Value = "MS:08";
                                                            break;
                                                        case 5:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                            rep.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                        case 6:
                                                            rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                            rep.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                    }

                                                    if (DungChung.Bien.MaBV == "30002")
                                                    {
                                                        double a = q.Where(p => p.LoaiDV == i).Sum(p => p.ThanhTien) ?? 0;
                                                        rep.colTongG.Text = a.ToString("###,###.00");
                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.MaDV).ToList();
                                                    }
                                                    else
                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                    rep.BindingData();
                                                    //rep.DataMember = "";
                                                    rep.CreateDocument();
                                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                    frm.ShowDialog();
                                                    this.Dispose();
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    #region bv khác
                                    else
                                    {

                                        int tekho = bph.First().MaKXuat == null ? 0 : bph.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        #region 27023
                                        if (DungChung.Bien.MaBV == "27023")
                                        {
                                            q = (from bc in q
                                                 select new BC { TenHC = bc.TenHC, LoaiDV = bc.LoaiDV, SoLo = bc.SoLo, HanDung = bc.HanDung, TenRGTN = bc.TenRGTN, TenRGDV = bc.TenRGDV, TenDV = bc.TenDV, HamLuong = bc.HamLuong, MaTam = bc.MaTam, MaDV = bc.MaDV, DonVi = bc.DonVi, SoLuong = bc.SoLuong, DonGia = bc.DonGia, ThanhTien = bc.ThanhTien, LoaiDuoc = bc.LoaiDuoc }).ToList();
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (i != 4 && i != 3)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        BaoCao.PhieulinhthuocVTYT_27023 rep = new BaoCao.PhieulinhthuocVTYT_27023();
                                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep.SoPL.Value = _soPL.ToString();
                                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                        rep.Khoa.Value = bph.First().TenKP;
                                                        if (tenkho.Count > 0)
                                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                        switch (kieudon)
                                                        {
                                                            case 0:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                                break;
                                                            case 1:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                break;
                                                            case 5:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                break;
                                                                //    case 2:
                                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                //break;
                                                        }


                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                                break;
                                                            case 2:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                                break;
                                                            case 3:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                break;
                                                            case 4:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                break;
                                                            case 5:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }


                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep.BindingData();
                                                        //rep.DataMember = "";
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                        this.Dispose();
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        #region bv khác
                                        else
                                        #region 27021
                                                if (DungChung.Bien.MaBV == "27021")
                                        {
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (i != 4 && i != 3)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        BaoCao.PhieulinhthuocVTYT27021 rep = new BaoCao.PhieulinhthuocVTYT27021();
                                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep.SoPL.Value = _soPL.ToString();
                                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                        rep.Khoa.Value = bph.First().TenKP;
                                                        if (tenkho.Count > 0)
                                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                        switch (kieudon)
                                                        {
                                                            case 0:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                                break;
                                                            case 1:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                break;
                                                            case 5:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                break;
                                                                //    case 2:
                                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                //break;
                                                        }


                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                                break;
                                                            case 2:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                                break;

                                                            case 5:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }


                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep.BindingData();
                                                        //rep.DataMember = "";
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                        this.Dispose();
                                                    }
                                                }
                                            }
                                        }
                                        #endregion 27021
                                        #region 01071
                                        else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                        {
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (i != 4 && i != 3)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        BaoCao.PhieulinhthuocVTYT_A5_2lien_01071 rep = new BaoCao.PhieulinhthuocVTYT_A5_2lien_01071();
                                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep.SoPL.Value = _soPL.ToString();
                                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                        int makpx = 0;
                                                        bool _linhbututruc = false;
                                                        bool khoaKeTuTruc = false;
                                                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                                        {

                                                            int makp = _makp;
                                                            if (makp <= 0)
                                                            {
                                                                var qdtct = _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).FirstOrDefault();
                                                                if (qdtct != null)
                                                                    makp = qdtct.MaKP.Value;
                                                            }

                                                            var kt = _dataContext.KPhongs.Where(p => p.MaKP == makp && p.PLoai.Contains("Tủ trực")).Select(p => p.TenKP).FirstOrDefault();
                                                            if (kt != null)
                                                                khoaKeTuTruc = true;

                                                        }
                                                        if (bph.First().MaKXuat != null)
                                                        {
                                                            makpx = Convert.ToInt32(bph.First().MaKXuat);
                                                            var ktratutruca = _dataContext.KPhongs.Where(p => p.MaKP == makpx && p.PLoai.Contains("Tủ trực")).Select(p => p.TenKP).FirstOrDefault();
                                                            if (ktratutruca != null)
                                                                _linhbututruc = true;
                                                        }
                                                        if (_linhbututruc)
                                                        {
                                                            var khox = _dataContext.KPhongs.Where(p => p.MaKP == makpx).Select(p => p.TenKP).FirstOrDefault();
                                                            if (khox != null)
                                                                rep.Khoa.Value = khox.ToString();
                                                        }
                                                        else
                                                            rep.Khoa.Value = bph.First().TenKP;
                                                        if (tenkho.Count > 0)
                                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                        switch (kieudon)
                                                        {
                                                            case 0:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                                break;
                                                            case 1:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                break;
                                                            case 5:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                break;

                                                        }

                                                        var TD1 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                                   join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                                   select dt).ToList();//.Select(p => p.KieuDon).ToList();
                                                        var TD2 = (from td in TD1
                                                                   join kp in _dataContext.KPhongs.Where(p => p.PLoai.Contains("Tủ trực")) on td.MaKP equals kp.MaKP
                                                                   select kp).ToList();

                                                        //if (TD1.First().KieuDon.Value != 2) // minhvd
                                                        //{
                                                        //    rep.Chuthich.Value = "Loại phiếu: Lĩnh về khoa";
                                                        //}
                                                        //else
                                                        //{ rep.Chuthich.Value = "Loại phiếu: Trả thuốc"; }

                                                        if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                                        {
                                                            if (TD2.Count > 0)
                                                            {
                                                                if (TD1.First().MaBNhanChiTiet != null && TD2.First().PLoai == "Tủ trực")
                                                                {
                                                                    rep.Chuthich.Value = "Loại phiếu: Lĩnh bù tủ trực";
                                                                }
                                                                else if (TD1.First().MaBNhanChiTiet == null && TD2.First().PLoai == "Tủ trực")
                                                                {
                                                                    rep.Chuthich.Value = "Loại phiếu: Bổ sung tủ trực";
                                                                }
                                                                else if (TD1.First().KieuDon.Value != 2)
                                                                {
                                                                    rep.Chuthich.Value = "Loại phiếu: Lĩnh về khoa";
                                                                }
                                                                else
                                                                {
                                                                    rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                }
                                                            }
                                                        }


                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                                break;
                                                            case 2:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                                break;
                                                            case 3:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                break;
                                                            case 4:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                break;
                                                            case 5:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }
                                                        if (khoaKeTuTruc)
                                                        {
                                                            string doituong = "";
                                                            int iddtbn = bph.First().XHH;
                                                            if (iddtbn == 99)
                                                                doituong = "Tất cả";
                                                            else
                                                            {
                                                                var dtuong = _dataContext.DTBNs.Where(p => p.IDDTBN == iddtbn).FirstOrDefault();
                                                                if (dtuong != null)
                                                                    doituong = dtuong.DTBN1;
                                                            }
                                                            if (doituong != "")
                                                                rep.DoiTuong.Value = "Đối tượng: " + doituong;
                                                        }

                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep.BindingData();
                                                        //rep.DataMember = "";
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                        this.Dispose();
                                                    }
                                                }
                                            }


                                        }
                                        /*
                                        else if ( DungChung.Bien.MaBV == "24012")
                                        {
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (i != 4 && i != 3)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        BaoCao.PhieulinhthuocVTYT_CoDonGia rep = new BaoCao.PhieulinhthuocVTYT_CoDonGia();
                                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep.SoPL.Value = _soPL.ToString();
                                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                        int makpx = 0;
                                                        bool _linhbututruc = false;
                                                        bool khoaKeTuTruc = false;
                                                        
                                                        if (bph.First().MaKXuat != null)
                                                        {
                                                            makpx = Convert.ToInt32(bph.First().MaKXuat);
                                                            var ktratutruca = _dataContext.KPhongs.Where(p => p.MaKP == makpx && p.PLoai.Contains("Tủ trực")).Select(p => p.TenKP).FirstOrDefault();
                                                            if (ktratutruca != null)
                                                                _linhbututruc = true;
                                                        }
                                                        if (_linhbututruc)
                                                        {
                                                            var khox = _dataContext.KPhongs.Where(p => p.MaKP == makpx).Select(p => p.TenKP).FirstOrDefault();
                                                            if (khox != null)
                                                                rep.Khoa.Value = khox.ToString();
                                                        }
                                                        else
                                                            rep.Khoa.Value = bph.First().TenKP;
                                                        if (tenkho.Count > 0)
                                                            rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                        switch (kieudon)
                                                        {
                                                            case 0:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                                break;
                                                            case 1:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                break;
                                                            case 5:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                break;

                                                        }

                                                        var TD1 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                                   join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                                   select dt).ToList();//.Select(p => p.KieuDon).ToList();
                                                        var TD2 = (from td in TD1
                                                                   join kp in _dataContext.KPhongs.Where(p => p.PLoai.Contains("Tủ trực")) on td.MaKP equals kp.MaKP
                                                                   select kp).ToList();

                                                       
                                                        if (DungChung.Bien.MaBV == "24012")
                                                        {
                                                            if (TD2.Count > 0)
                                                            {
                                                                if (TD1.First().MaBNhanChiTiet != null && TD2.First().PLoai == "Tủ trực")
                                                                {
                                                                    rep.Chuthich.Value = "Loại phiếu: Lĩnh bù tủ trực";
                                                                }
                                                                else if (TD1.First().MaBNhanChiTiet == null && TD2.First().PLoai == "Tủ trực")
                                                                {
                                                                    rep.Chuthich.Value = "Loại phiếu: Bổ sung tủ trực";
                                                                }
                                                                else if (TD1.First().KieuDon.Value != 2)
                                                                {
                                                                    rep.Chuthich.Value = "Loại phiếu: Lĩnh về khoa";
                                                                }
                                                                else
                                                                {
                                                                    rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                }
                                                            }
                                                        }


                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                                break;
                                                            case 2:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                                break;
                                                            case 3:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                break;
                                                            case 4:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                break;
                                                            case 5:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }
                                                        if (khoaKeTuTruc)
                                                        {
                                                            string doituong = "";
                                                            int iddtbn = bph.First().XHH;
                                                            if (iddtbn == 99)
                                                                doituong = "Tất cả";
                                                            else
                                                            {
                                                                var dtuong = _dataContext.DTBNs.Where(p => p.IDDTBN == iddtbn).FirstOrDefault();
                                                                if (dtuong != null)
                                                                    doituong = dtuong.DTBN1;
                                                            }
                                                            if (doituong != "")
                                                                rep.DoiTuong.Value = "Đối tượng: " + doituong;
                                                        }

                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep.BindingData();
                                                        //rep.DataMember = "";
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                        this.Dispose();
                                                    }
                                                }
                                            }
                                        }*/

                                        #endregion
                                        #region 27022 có đơn giá, thành tiền
                                        else if (DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "24012")
                                        {
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (i != 4 && i != 3)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        BaoCao.PhieulinhthuocVTYT_CoDonGia rep = new BaoCao.PhieulinhthuocVTYT_CoDonGia();
                                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        string nguoiphat = "";
                                                        //var Nguoiphat = _dataContext.KPhongs.Where(p => p.MaKP == tekho).Select(p => p.NguoiPhat).FirstOrDefault();
                                                        //if (Nguoiphat != null)
                                                        //    nguoiphat = Nguoiphat;
                                                        rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                        rep.SoPL.Value = _soPL.ToString();
                                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                        rep.Khoa.Value = bph.First().TenKP;
                                                        bool tutruc = false;
                                                        if (tenkho.Count > 0)
                                                        {
                                                            if (tenkho.First().PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                                                                tutruc = true;
                                                            if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                rep.KhoLinhNew.Value = "Lĩnh bù về: " + tenkho.First().TenKP;
                                                            else
                                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                        }
                                                        switch (kieudon)
                                                        {
                                                            case 0:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";
                                                                break;
                                                            case 1:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                break;
                                                            case 5:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                break;
                                                            case 3:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Lĩnh về khoa";
                                                                break;
                                                            case 4:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Khoa trả dược";
                                                                break;
                                                            case 2:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Trả thuốc";
                                                                break;
                                                            case -1:
                                                                rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoại trú";
                                                                break;
                                                                //    case 2:
                                                                //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                //break;
                                                        }


                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep.MauSo.Value = "MS:01D/BV-01";
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.Kholinh.Value = "Kho lĩnh: Kho nội trú";
                                                                break;
                                                            case 1:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep.MauSo.Value = "MS:02D/BV-01";
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                break;
                                                            case 2:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                rep.MauSo.Value = "MS:03D/BV-01";
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                break;
                                                            case 3:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                break;
                                                            case 4:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                rep.MauSo.Value = "MS:08";
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                break;
                                                            case 5:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }


                                                        rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.TenDV).ToList();
                                                        rep.BindingData();
                                                        //rep.DataMember = "";
                                                        rep.CreateDocument();
                                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                        frm.ShowDialog();
                                                        this.Dispose();
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        #region chung
                                        else
                                        {
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (i != 4 && i != 3)
                                                {
                                                    if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                                    {
                                                        #region 31049
                                                        if (DungChung.Bien.MaBV == "34019")
                                                        {
                                                            BaoCao.PhieulinhthuocVTYT_A5 rep = new BaoCao.PhieulinhthuocVTYT_A5();
                                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;

                                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                            rep.SoPL.Value = _soPL.ToString();
                                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                            rep.theongay.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
                                                            rep.Khoa.Value = bph.First().TenKP;
                                                            bool tutruc = false;
                                                            if (tenkho.Count > 0)
                                                            {
                                                                rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                            }
                                                            switch (kieudon)
                                                            {
                                                                case 0:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";

                                                                    break;
                                                                case 1:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                    break;
                                                                case 5:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                    break;

                                                            }


                                                            switch (i)
                                                            {
                                                                case 0:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                    rep.MauSo.Value = "MS:01D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho nội trú";
                                                                    break;
                                                                case 1:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                    rep.MauSo.Value = "MS:02D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                    break;
                                                                case 2:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                    rep.MauSo.Value = "MS:03D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                    break;
                                                                case 3:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                    rep.MauSo.Value = "MS:08";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                    break;
                                                                case 4:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                    rep.MauSo.Value = "MS:08";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                    break;
                                                                case 5:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                                    break;
                                                                case 6:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                                    break;
                                                            }

                                                            var qbc34019 = q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                            rep.DataSource = qbc34019;
                                                            rep.lblTongSoKhoan.Text = "Tổng số khoản: " + qbc34019.Count();
                                                            rep.celThoiGian.Text = "Ngày " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                                                            rep.BindingData();
                                                            rep.CreateDocument();
                                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                            frm.ShowDialog();
                                                            this.Dispose();


                                                        }
                                                        #endregion 31049
                                                        #region chung
                                                        else
                                                        {

                                                            BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT();
                                                            rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                            rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                            rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                            string nguoiphat = "";
                                                            //var Nguoiphat = _dataContext.KPhongs.Where(p => p.MaKP == tekho).Select(p => p.NguoiPhat).FirstOrDefault();
                                                            //if (Nguoiphat != null)
                                                            //    nguoiphat = Nguoiphat;
                                                            var TD1 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                                       join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                                       select dt).ToList();//.Select(p => p.KieuDon).ToList();
                                                            var TD2 = (from td in TD1
                                                                       join kp in _dataContext.KPhongs.Where(p => p.PLoai.Contains("Tủ trực")) on td.MaKP equals kp.MaKP
                                                                       select kp).ToList();

                                                            if (TD1.First().KieuDon.Value != 2) // minhvd
                                                            {
                                                                rep.Chuthich.Value = "Loại phiếu: Lĩnh về khoa";
                                                            }
                                                            else
                                                            { rep.Chuthich.Value = "Loại phiếu: Trả thuốc"; }

                                                            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                                            {
                                                                if (TD2.Count > 0)
                                                                {
                                                                    if (TD1.First().MaBNhanChiTiet != null && TD2.First().PLoai == "Tủ trực")
                                                                    {
                                                                        rep.Chuthich.Value = "Loại phiếu: Lĩnh bù tủ trực";
                                                                    }
                                                                    else if (TD1.First().MaBNhanChiTiet == null && TD2.First().PLoai == "Tủ trực")
                                                                    {
                                                                        rep.Chuthich.Value = "Loại phiếu: Bổ sung tủ trực";
                                                                    }
                                                                    else if (TD1.First().KieuDon.Value != 2)
                                                                    {
                                                                        rep.Chuthich.Value = "Loại phiếu: Lĩnh về khoa";
                                                                    }
                                                                    else
                                                                    {
                                                                        rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                    }
                                                                }
                                                            }

                                                            rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                            rep.SoPL.Value = _soPL.ToString();
                                                            rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                            rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                            rep.theongay.Value = "Từ ngày " + ngay1 + " Đến ngày " + ngay2;
                                                            rep.Khoa.Value = bph.First().TenKP.ToUpper();
                                                            rep.TongTien.Value = q.Where(c => c.LoaiDV == i).Select(c => c.ThanhTien).Sum();
                                                            if (DungChung.Bien.MaBV == "14017")
                                                                rep.txtNgayThang1.Text = "Ngày " + Convert.ToDateTime(ngay1).Day + " tháng " + Convert.ToDateTime(ngay1).Month + " năm " + Convert.ToDateTime(ngay1).Year;
                                                            bool tutruc = false;
                                                            if (tenkho.Count > 0)
                                                            {
                                                                if (tenkho.First().PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                                                                    tutruc = true;
                                                                if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                    rep.KhoLinhNew.Value = "Lĩnh bù về: " + tenkho.First().TenKP;
                                                                else
                                                                    rep.Kholinh.Value = DungChung.Bien.MaBV == "14017" ? tenkho.First().TenKP : "Kho lĩnh: " + tenkho.First().TenKP;
                                                            }
                                                            switch (kieudon)
                                                            {
                                                                case 0:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Hàng ngày";

                                                                    break;
                                                                case 1:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Bổ sung";
                                                                    break;
                                                                case 5:
                                                                    rep.Chuthich.Value = _dtuong + "Loại phiếu: Ngoài giờ (trực)";
                                                                    break;
                                                                    //    case 2:
                                                                    //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                    //break;
                                                            }


                                                            switch (i)
                                                            {
                                                                case 0:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                    rep.MauSo.Value = "MS:01D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho nội trú";
                                                                    if (DungChung.Bien.MaBV == "14017")
                                                                    {
                                                                        rep.titleMaThuoc.Text = "Mã thuốc";
                                                                        rep.titleTenThuoc.Text = "Tên thuốc, hàm lượng";
                                                                    }
                                                                    break;
                                                                case 1:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                    rep.MauSo.Value = "MS:02D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                    if (DungChung.Bien.MaBV == "14017")
                                                                    {
                                                                        rep.titleMaThuoc.Text = "Mã HC";
                                                                        rep.titleTenThuoc.Text = "Tên hóa chất";
                                                                    }
                                                                    break;
                                                                case 2:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                                                                    rep.MauSo.Value = "MS:03D/BV-01";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho vật tư y tế";
                                                                    if (DungChung.Bien.MaBV == "14017")
                                                                    {
                                                                        rep.titleMaThuoc.Text = "Mã VT";
                                                                        rep.titleTenThuoc.Text = "Tên vật tư";
                                                                    }
                                                                    break;
                                                                case 3:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                    rep.MauSo.Value = "MS:08";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                    break;
                                                                case 4:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                                    rep.MauSo.Value = "MS:08";
                                                                    if (DungChung.Bien.MaBV == "01830" && tutruc)
                                                                        rep.Kholinh.Value = "Kho lĩnh: Kho gây nghiện hướng thần";
                                                                    break;
                                                                case 5:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                                    break;
                                                                case 6:
                                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                    rep.MauSo.Value = "MS:...D/BV-01";
                                                                    break;
                                                            }


                                                            rep.DataSource = DungChung.Bien.MaBV == "14017" ? q.Where(p => p.LoaiDV == i).OrderBy(p => p.TenDV).ToList() : q.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                            rep.BindingData();
                                                            //rep.DataMember = "";
                                                            rep.CreateDocument();
                                                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                                            frm.ShowDialog();
                                                            this.Dispose();
                                                        }
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        #endregion
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }


                    break;
                #endregion

                case 3:
                    #region case 3

                    //rep3.Ngaythang.Value = ngay.ToString().Substring(0, 10);
                    var TD = (from dt in _dataContext.DThuocs
                              join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                              select dt).Select(p => p.KieuDon).ToList();
                    
                    if (TD.Count > 0)
                    {
                        #region Kiểu đơn không phải khoa trả thuốc
                        if (TD.First().Value != 4)
                        {
                            var q33 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                       join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                       join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                       join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                       select new
                                       {
                                           kd.NgayKe,
                                           dv.TenRG,
                                           kd.MaKP,
                                           kd.MaKXuat,
                                           kd.LoaiDuoc,
                                           dv.TenDV,
                                           dv.PLoai,
                                           dv.TenHC,
                                           kdct,
                                           dv.MaTam,
                                           dv.HamLuong,
                                           TenRGTN = tn.TenRG
                                       }).ToList();



                            var q3 = (from kd in q33
                                      group new { kd } by new { kd.kdct.MaDV, kd.TenDV } into kq
                                      select new
                                      {
                                          Solo = kq.Select(p => p.kd.kdct.SoLo).FirstOrDefault(),
                                          HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Select(p => p.kd.kdct.SoLo).FirstOrDefault()),
                                          DonGia = kq.Select(p => p.kd.kdct.DonGia).FirstOrDefault(),
                                          NgayKe = kq.Select(p => p.kd.NgayKe).FirstOrDefault(),
                                          HamLuong = kq.Select(p => p.kd.HamLuong).FirstOrDefault(),
                                          MaTam = kq.Select(p => p.kd.MaTam).FirstOrDefault(),
                                          MaKP = kq.Select(p => p.kd.MaKP).FirstOrDefault(),
                                          MaKXuat = kq.Select(p => p.kd.MaKXuat).FirstOrDefault(),
                                          XHH = kq.Select(p => p.kd.kdct.XHH).FirstOrDefault(),
                                          TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Select(p => p.kd.TenHC).FirstOrDefault() + ": " + kq.Select(p => p.kd.HamLuong).FirstOrDefault() + ") ") : ((DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009" || (DungChung.Bien.MaBV == "27023")) ? (kq.Key.TenDV + (string.IsNullOrEmpty(kq.Select(p => p.kd.HamLuong).FirstOrDefault()) ? "" : " (" + kq.Select(p => p.kd.HamLuong).FirstOrDefault() + ") ")) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Select(p => p.kd.TenRG).FirstOrDefault() : ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") ? kq.Key.TenDV + " " + kq.Select(p => p.kd.HamLuong).FirstOrDefault() : kq.Key.TenDV)))),
                                          kq.Key.MaDV,
                                          PLoai = kq.Select(p => p.kd.PLoai).FirstOrDefault(),
                                          DonVi = kq.Select(p => p.kd.kdct.DonVi).FirstOrDefault(),
                                          ThanhTien = kq.Sum(p => p.kd.kdct.ThanhTien),
                                          SoLuong = kq.Sum(p => p.kd.kdct.SoLuong),
                                          LoaiDuoc = kq.Select(p => p.kd.LoaiDuoc).FirstOrDefault(),
                                          LoaiDV = kq.Select(p => p.kd.TenRGTN).FirstOrDefault().Contains("Thuốc thường") ? 0 : (kq.Select(p => p.kd.TenRGTN).FirstOrDefault().Contains("Hóa chất") ? 1 : (kq.Select(p => p.kd.TenRGTN).FirstOrDefault().Contains("Vật tư y tế") ? 2 : (kq.Select(p => p.kd.TenRGTN).FirstOrDefault().Contains("Thuốc gây nghiện") ? 3 : (kq.Select(p => p.kd.TenRGTN).FirstOrDefault().Contains("Thuốc hướng tâm thần") ? 4 : (kq.Select(p => p.kd.TenRGTN).FirstOrDefault().Contains("Thuốc trẻ em") ? 5 : (kq.Select(p => p.kd.TenRGTN).FirstOrDefault().Contains("Thuốc đông y") ? 6 : (kq.Select(p => p.kd.TenRGTN).FirstOrDefault().Contains("Y cụ") ? 7 : 0)))))))
                                      }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();

                            //var q = (from kd in _dataContext.DThuocs.Where(p => p.SoPL == _soPL && (_makp == 0 ? true : p.MaKP == _makp))
                            //         join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                            //         join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                            //         group new { kdct, dv, kd } by new { dv.TenHC, dv.HamLuong, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, kd.NgayKe } into kq
                            //         select new { kq.Key.NgayKe, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                            //  int loaiduoc = q3.First().LoaiDuoc.Value;
                            List<int> lLoaiDuocCt = q3.Select(p => p.LoaiDV).Distinct().ToList();
                            if (q3.Count > 0)
                            {
                                int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                var ten = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                string _ploaiKP = "";
                                if (ten.Count > 0)
                                {
                                    _ploaiKP = ten.First().PLoai;
                                }
                                bool _inPhieuDT = true;
                                if (DungChung.Bien.MaBV == "30003")
                                {
                                    string hoimauin = "In phiếu dự trù thuốc?";
                                    if (TD.First().Value == 4)
                                        hoimauin = "In phiếu trả thuốc?";
                                    DialogResult _result = MessageBox.Show(hoimauin, "Hỏi mẫu in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_result == DialogResult.No)
                                        _inPhieuDT = false;
                                }
                                if (DungChung.Bien.MaBV == "27021")
                                    _inPhieuDT = false;

                                #region khoa phòng là khoa dược
                                if (_ploaiKP.Contains("Khoa dược") && _inPhieuDT && DungChung.Bien.MaBV != "20001")
                                {
                                    tenkp = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                    var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();

                                    frmIn frm3 = new frmIn();
                                    if(DungChung.Bien.MaBV == "24012")
                                    {
                                        if (lLoaiDuocCt.Contains(0) || lLoaiDuocCt.Contains(1) || lLoaiDuocCt.Contains(2) || lLoaiDuocCt.Contains(5) || lLoaiDuocCt.Contains(6) || lLoaiDuocCt.Contains(7))
                                        {
                                            for (int i = 0; i < 8; i++)
                                            {
                                                if (i != 3 && i != 4 && q3.Where(p => p.LoaiDV == i).Count() > 0)
                                                {
                                                    #region 30002
                                                    #endregion
                                                    #region bv khác
                                                        if (DungChung.Bien.MaBV == "24012")
                                                        {
                                                            BaoCao.Phieulinhchokhoa_24012 rep3 = new BaoCao.Phieulinhchokhoa_24012();

                                                            //rep3.Ycu.Value = i;
                                                            string nguoiphat = "";
                                                            if (tenkho.Count > 0)
                                                                if (tenkho.First().NguoiPhat != null)
                                                                    nguoiphat = tenkho.First().NguoiPhat;
                                                            rep3.Tennguoilinh1.Value = DungChung.Bien.NguoiLapBieu;
                                                            rep3.Tennguoiphat1.Value = DungChung.Bien.ThuKho;
                                                            rep3.Tentruongkhoaduoc1.Value = DungChung.Bien.TruongKhoaDuoc;
                                                            rep3.Tentruongkhoalamsang1.Value = DungChung.Bien.TruongKhoaLS;
                                                            rep3.SoPL1.Value = _soPL.ToString() + "/" + DateTime.Now.Year;
                                                            rep3.Boyte1.Value = DungChung.Bien.TenCQCQ;
                                                            rep3.Benhvien1.Value = DungChung.Bien.TenCQ;
                                                            var TD1 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                                       join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                                       select dt).ToList();
                                                            var TD2 = (from td in TD1
                                                                       join kp in _dataContext.KPhongs.Where(p => p.PLoai.Contains("Tủ trực")) on td.MaKP equals kp.MaKP
                                                                       select kp).ToList();

                                                            if (TD1.First().KieuDon.Value != 2)
                                                            {
                                                                rep3.LoaiPL1.Value = "Loại phiếu: Lĩnh về khoa";
                                                            }
                                                            else
                                                            { rep3.LoaiPL1.Value = "Loại phiếu: Trả thuốc"; }

                                                            if (TD2.Count > 0)
                                                            {
                                                                if (TD1.First().MaBNhanChiTiet != null && TD2.First().PLoai == "Tủ trực")
                                                                {
                                                                    rep3.LoaiPL1.Value = "Loại phiếu: Lĩnh bù tủ trực";
                                                                }
                                                                else if (TD1.First().MaBNhanChiTiet == null && TD2.First().PLoai == "Tủ trực")
                                                                {
                                                                    rep3.LoaiPL1.Value = "Loại phiếu: Bổ sung tủ trực";
                                                                }
                                                                else if (TD1.First().KieuDon.Value != 2)
                                                                {
                                                                    rep3.LoaiPL1.Value = "Loại phiếu: Lĩnh về khoa";
                                                                }
                                                                else
                                                                {
                                                                    rep3.LoaiPL1.Value = "Loại phiếu: Trả thuốc";
                                                                }
                                                            }

                                                            if (ten.Count > 0)
                                                                rep3.Khoa1.Value = ten.First().TenKP.ToUpper();
                                                            if (tenkho.Count > 0)
                                                                rep3.Kholinh1.Value = tenkho.First().TenKP;
                                                            switch (i)
                                                            {
                                                                case 0:
                                                                    rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH THUỐC");
                                                                    rep3.MauSo1.Value = "MS:01D/BV-01";
                                                                    break;
                                                                case 1:
                                                                    rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                    rep3.MauSo1.Value = "MS:02D/BV-01";
                                                                    break;
                                                                case 2:
                                                                    rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ");
                                                                    rep3.MauSo1.Value = "MS:03D/BV-01";

                                                                    break;
                                                                case 3:
                                                                    rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN");
                                                                    rep3.MauSo1.Value = "MS:...D/BV-01";
                                                                    break;
                                                                case 4:
                                                                    rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH THUỐC HƯỚNG TÂM THẦN");
                                                                    rep3.MauSo1.Value = "MS:09D/BV-01";
                                                                    break;
                                                                case 5:
                                                                    rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                    rep3.MauSo1.Value = "MS:...D/BV-01";
                                                                    break;
                                                                case 6:
                                                                    rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                    rep3.MauSo1.Value = "MS:...D/BV-01";
                                                                    break;
                                                                case 7:
                                                                    rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH Y CỤ");
                                                                    rep3.MauSo1.Value = "MS:...D/BV-01";
                                                                    break;
                                                            }

                                                            rep3.theongay1.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                                            var sopl = (from pl in _dataContext.SoPLs.Where(p => p.SoPL1 == _soPL)
                                                                        select pl).Select(p => p.NgayNhap).ToList();
                                                            if (sopl.Count > 0)
                                                            {
                                                                rep3.ngaytaophieu1.Value = DungChung.Ham.NgaySangChu(sopl.First().Value);
                                                            }
                                                            else
                                                            {
                                                                rep3.ngaytaophieu1.Value = "Ngày...tháng...năm 2022";
                                                            }
                                                            rep3.DataSource = q3.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                            rep3.BindingData();
                                                            //rep.DataMember = "";
                                                            rep3.CreateDocument();
                                                            frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                                            frm3.ShowDialog();
                                                        }
                                                        #endregion
                                                        #endregion
                                                    #endregion
                                                }
                                            }
                                        }
                                    }    
                                    #region 27023
                                    if (DungChung.Bien.MaBV == "27023")
                                    {
                                        BaoCao.rep_dutruthuoc_27023 rep3 = new BaoCao.rep_dutruthuoc_27023();
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep3.SoPL.Value = _soPL.ToString();
                                        rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep3.Benhvien.Value = DungChung.Bien.TenCQ;
                                        if (ten.Count > 0)
                                            rep3.Khoa.Value = ten.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep3.Kholinh.Value = "Kính gửi:  " + tenkho.First().TenKP;
                                        rep3.Loaiphieulinh.Value = "DỰ TRÙ THUỐC";
                                        rep3.MauSo.Value = "MS:06D/BV-01";


                                        rep3.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);

                                        //var qbc = (from bc in q3
                                        //          select new {SoLo = GetSoLo(qDonGiaDV, bc.MaDV, bc.DonGia), HanDung = GetHanDung(qDonGiaDV, bc.MaDV, bc.DonGia), bc.DonGia, bc.NgayKe, bc.MaTam, bc.MaKP, bc.MaKXuat, TenDV = bc.TenDV, bc.MaDV, DonVi = bc.DonVi, ThanhTien = bc.ThanhTien, SoLuong = bc.SoLuong, LoaiDuoc = bc.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        rep3.DataSource = q3; //qbc.OrderBy(p => p.DonVi).ToList();
                                        rep3.BindingData();
                                        //rep.DataMember = "";
                                        rep3.CreateDocument();
                                        frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                        frm3.ShowDialog();
                                    }
                                    #endregion
                                    #region 01071
                                    else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                    {
                                        BaoCao.rep_dutruthuoc_A5_2lien rep3 = new BaoCao.rep_dutruthuoc_A5_2lien();
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep3.SoPL.Value = _soPL.ToString();
                                        rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep3.Benhvien.Value = DungChung.Bien.TenCQ;
                                        if (ten.Count > 0)
                                            rep3.Khoa.Value = ten.First().TenKP;
                                        if (tenkho.Count > 0)
                                            rep3.Kholinh.Value = "Kính gửi:  " + tenkho.First().TenKP;
                                        rep3.Loaiphieulinh.Value = "DỰ TRÙ THUỐC";
                                        rep3.MauSo.Value = "MS:06D/BV-01";


                                        rep3.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                        rep3.DataSource = q3.OrderBy(p => p.DonVi).ToList();
                                        rep3.BindingData();
                                        //rep.DataMember = "";
                                        rep3.CreateDocument();
                                        frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                        frm3.ShowDialog();
                                    }
                                    #endregion
                                    #region bv khác
                                    else
                                    {
                                        if (DungChung.Bien.MaBV != "24012")
                                        {
                                            BaoCao.rep_dutruthuoc rep3 = new BaoCao.rep_dutruthuoc();
                                            //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                            rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                            rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                            rep3.SoPL.Value = _soPL.ToString();
                                            rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                            rep3.Benhvien.Value = DungChung.Bien.TenCQ;
                                            if (ten.Count > 0)
                                                rep3.Khoa.Value = ten.First().TenKP;
                                            if (tenkho.Count > 0)
                                                rep3.Kholinh.Value = "Kính gửi:  " + tenkho.First().TenKP;
                                            rep3.Loaiphieulinh.Value = "DỰ TRÙ THUỐC";
                                            rep3.MauSo.Value = "MS:06D/BV-01";


                                            rep3.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                            rep3.DataSource = q3.OrderBy(p => p.DonVi).ToList();
                                            rep3.BindingData();
                                            //rep.DataMember = "";
                                            rep3.CreateDocument();
                                            frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                            frm3.ShowDialog();
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                                #region khoa phòng không phải khoa dược
                                else
                                {

                                    tenkp = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                    var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                    //int loaiduoc = q3.First().LoaiDuoc.Value;
                                    #region thuốc gây nghiện hướng tâm thần
                                    if (lLoaiDuocCt.Contains(3) || lLoaiDuocCt.Contains(4))
                                    {
                                        string _dtuong1 = "";
                                        var ktdtuong1 = (from bn in _dataContext.BenhNhans
                                                         join dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                         on bn.MaBNhan equals dt.MaBNhan
                                                         join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                         group new { bn } by new { bn.IDDTBN, bn.DTuong, bn.NoThe } into kq
                                                         select new { kq.Key.IDDTBN, kq.Key.DTuong, kq.Key.NoThe }).ToList();
                                        if (ktdtuong1.Count > 1)
                                        {
                                            _dtuong = "";
                                        }
                                        else
                                        {
                                            if (ktdtuong1.Count > 0 && ktdtuong1.First().NoThe == true)
                                            {
                                                _dtuong1 = "(Dành cho đối tượng dịch vụ _ nợ thẻ BHYT) \n";
                                            }
                                            else
                                            {
                                                var ktdtuong2 = (from bn in _dataContext.BenhNhans
                                                                 join dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp) on bn.MaBNhan equals dt.MaBNhan
                                                                 join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                                 join dtbn in _dataContext.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                                                                 select new { dtbn.MoTa }).ToList();
                                                if (ktdtuong2.Count > 0)
                                                {
                                                    _dtuong1 = "(Dành cho đối tượng " + ktdtuong2.First().MoTa + " ) \n";
                                                }
                                            }
                                        }
                                        var bph1 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                    join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on kd.IDDon equals dtct.IDDon
                                                    join kp in _dataContext.KPhongs
                                                        on kd.MaKP equals kp.MaKP
                                                    select new { kp.TenKP, kd.LoaiDuoc, kd.MaKXuat, kd.KieuDon, kp.PLoai, kd.MaBNhanChiTiet }).ToList();


                                        int kieudon = bph1.First().KieuDon.Value;
                                        int loaiphieu = 0;
                                        if (kieudon == 3)
                                        {
                                            if (bph1.First().PLoai == "Tủ trực")
                                                loaiphieu = 1; //linh ve khoa
                                            if (bph1.First().PLoai == "Tủ trực" && (bph1.First().MaBNhanChiTiet != null))
                                                loaiphieu = 2; //linh bu tu truc
                                            if (bph1.First().PLoai == "Tủ trực" && (bph1.First().MaBNhanChiTiet == null))
                                                loaiphieu = 3; //bổ sung tủ trực
                                        }

                                        var q333 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                    join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                                    join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                                    join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                                    select new { kd.NgayKe, dv.SoDK, dv.TenRG, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, dv.TenDV, dv.TenHC, kdct, dv.MaTam, dv.HamLuong, TenRGTN = tn.TenRG }).ToList();


                                        var q = (from kd in q333
                                                 group new { kd } by new { kd.TenHC, kd.HamLuong, kd.MaTam, kd.TenRG, kd.kdct.DonGia, kd.kdct.DonVi, kd.kdct.MaDV, kd.TenDV, kd.LoaiDuoc, TenRGDV = kd.TenRG, SoLo = kd.kdct.SoLo ?? "", kd.TenRGTN } into kq
                                                 select new
                                                 {
                                                     kq.Key.HamLuong,
                                                     kq.Key.SoLo,
                                                     HanDung = DungChung.Ham.getHanDung(kq.Key.MaDV ?? 0, kq.Key.SoLo),
                                                     kq.Key.MaDV,
                                                     kq.Key.DonGia,
                                                     kq.Key.TenRG,
                                                     kq.Key.MaTam,
                                                     kq.Key.TenRGTN,
                                                     TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : ((DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009") ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009 ") ? kq.Key.TenRGDV : DungChung.Bien.MaBV == "27023" ? (kq.Key.TenDV + (string.IsNullOrEmpty(kq.Key.HamLuong) ? "" : " (" + kq.Key.HamLuong + ") ")) : kq.Key.TenDV))),
                                                     DonVi = kq.Key.DonVi,
                                                     SoLuong = (DungChung.Bien.MaBV == "30003" && kieudon == 4) ? (-kq.Sum(p => p.kd.kdct.SoLuong)) : kq.Sum(p => p.kd.kdct.SoLuong),
                                                     LoaiDuoc = kq.Key.LoaiDuoc,
                                                     LoaiDV = kq.Key.TenRGTN.Contains("Thuốc thường") ? 0 : (kq.Key.TenRGTN.Contains("Hóa chất") ? 1 : (kq.Key.TenRGTN.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRGTN.Contains("Thuốc đông y") ? 6 : 0))))))
                                                 }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                                        var ngay3 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                     join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                     select dt).Select(p => p.NgayKe).OrderBy(p => p.Value).ToList();
                                        string ngay4 = "";
                                        string ngay5 = "";
                                        if (ngay3.Count > 0)
                                        {
                                            ngay4 = ngay3.First().ToString().Substring(0, 10);
                                            ngay5 = ngay3.Last().ToString().Substring(0, 10);
                                        }

                                        frmIn frm4 = new frmIn();
                                        int tekho = bph1.First().MaKXuat == null ? 0 : bph1.First().MaKXuat.Value;
                                        var tenkho1 = _dataContext.KPhongs.Where(p => p.MaKP == (tekho)).ToList();
                                        if (q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).Count() > 0)
                                        {
                                            #region 08204
                                            if (DungChung.Bien.MaBV == "08204")
                                            {

                                                BaoCao.PhieulinhthuocGNHTT rep = new BaoCao.PhieulinhthuocGNHTT();
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                if (DungChung.Bien.MaBV == "12001")
                                                    rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay4 + " đến ngày: " + ngay5;
                                                rep.Khoa.Value = bph1.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 3:
                                                        rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh cho khoa";
                                                        break;
                                                    case 4:
                                                        rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Trả lại của khoa";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }



                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");

                                                rep.MauSo.Value = "MS:08";



                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm4.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm4.ShowDialog();
                                                this.Dispose();


                                            }
                                            #endregion
                                            else
                                            #region 24009
                                                if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27023")
                                                {
                                                BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT(0);
                                                rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                if (DungChung.Bien.MaBV == "12001")
                                                    rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                rep.theongay.Value = "Từ ngày: " + ngay4 + " đến ngày: " + ngay5;
                                                rep.Khoa.Value = bph1.First().TenKP;
                                                if (tenkho.Count > 0)
                                                    rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                switch (kieudon)
                                                {
                                                    case 0:
                                                        rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Hàng ngày";
                                                        break;
                                                    case 1:
                                                        rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Bổ sung";
                                                        break;
                                                    case 3:
                                                        rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh cho khoa";
                                                        break;
                                                    case 4:
                                                        rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Trả lại của khoa";
                                                        break;
                                                    case 5:
                                                        rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Ngoài giờ (trực)";
                                                        break;
                                                        //    case 2:
                                                        //rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                        //break;
                                                }



                                                rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN, THUỐC HƯỚNG TÂM THẦN, THUỐC TIỀN CHẤT");

                                                rep.MauSo.Value = "MS:01D/BV-01";



                                                rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm4.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm4.ShowDialog();
                                                this.Dispose();
                                            }
                                            #endregion
                                            else
                                            {
                                                #region 27023
                                                //if (DungChung.Bien.MaBV == "27023")
                                                //{
                                                //    BaoCao.PhieulinhthuocGNHTT2lien_27023 rep = new BaoCao.PhieulinhthuocGNHTT2lien_27023();
                                                //    rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                //    if (DungChung.Bien.MaBV == "12001")
                                                //        rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                //    rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                //    rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                //    rep.SoPL.Value = _soPL.ToString();
                                                //    rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                //    rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                //    rep.theongay.Value = "Từ ngày: " + ngay4 + " đến ngày: " + ngay5;
                                                //    rep.Khoa.Value = bph1.First().TenKP;
                                                //    if (tenkho.Count > 0)
                                                //        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                //    switch (kieudon)
                                                //    {
                                                //        case 0:
                                                //            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Hàng ngày";
                                                //            break;
                                                //        case 1:
                                                //            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Bổ sung";
                                                //            break;
                                                //        case 3:
                                                //            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh cho khoa";
                                                //            break;
                                                //        case 4:
                                                //            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Trả lại của khoa";
                                                //            break;
                                                //        case 5:
                                                //            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Ngoài giờ (trực)";
                                                //            break;
                                                //        case 2:
                                                //            rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                //            break;
                                                //    }



                                                //    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");

                                                //    rep.MauSo.Value = "MS:08";

                                                //    //var qbc = (from a in q
                                                //    //         select new { SoLo = GetSoLo(qDonGiaDV, a.MaDV, a.DonGia), HanDung = GetHanDung(qDonGiaDV, a.MaDV, a.DonGia), a.TenRG, a.MaTam, TenDV = a.TenDV, a.MaDV, DonVi = a.DonVi, SoLuong = a.SoLuong, DonGia = a.DonGia, LoaiDuoc = a.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                                                //    rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                //    rep.BindingData();
                                                //    //rep.DataMember = "";
                                                //    rep.CreateDocument();
                                                //    frm4.prcIN.PrintingSystem = rep.PrintingSystem;
                                                //    frm4.ShowDialog();
                                                //    this.Dispose();
                                                //}
                                                #endregion
                                                #region 30007_ A5 - 1 liên
                                                if (DungChung.Bien.MaBV == "30007")
                                                {
                                                    BaoCao.PhieulinhthuocGNHTT_A5_1lien rep = new BaoCao.PhieulinhthuocGNHTT_A5_1lien();
                                                    rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                    if (DungChung.Bien.MaBV == "12001")
                                                        rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                    rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                    rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                    rep.SoPL.Value = _soPL.ToString();
                                                    rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                    rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                    rep.theongay.Value = "Từ ngày: " + ngay4 + " đến ngày: " + ngay5;
                                                    rep.Khoa.Value = bph1.First().TenKP;
                                                    if (tenkho.Count > 0)
                                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                    switch (kieudon)
                                                    {
                                                        case 0:
                                                            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Hàng ngày";
                                                            break;
                                                        case 1:
                                                            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Bổ sung";
                                                            break;
                                                        case 3:
                                                            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh cho khoa";
                                                            break;
                                                        case 4:
                                                            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Trả lại của khoa";
                                                            break;
                                                        case 5:
                                                            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Ngoài giờ (trực)";
                                                            break;
                                                        case 2:
                                                            rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                            break;
                                                    }
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                    rep.MauSo.Value = "MS:08";
                                                    rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                    rep.BindingData();
                                                    //rep.DataMember = "";
                                                    rep.CreateDocument();
                                                    frm4.prcIN.PrintingSystem = rep.PrintingSystem;
                                                    frm4.ShowDialog();
                                                    this.Dispose();
                                                }
                                                #endregion
                                                else if (DungChung.Bien.MaBV == "24012" && (q.First().TenRGTN.Contains("Thuốc gây nghiện") || q.First().TenRGTN.Contains("Thuốc hướng tâm thần")))
                                                {
                                                    bool tt = false;
                                                    if (q.First().TenRGTN.Contains("Thuốc hướng tâm thần"))
                                                        tt = true;
                                                    BaoCao.PhieulinhthuocGNHTT_24012 rep1 = new BaoCao.PhieulinhthuocGNHTT_24012(tt);
                                                    var sopl = (from pl in _dataContext.SoPLs.Where(p => p.SoPL1 == _soPL)
                                                                select pl).Select(p => p.NgayNhap).ToList();
                                                    if (sopl.Count > 0)
                                                    {
                                                        rep1.ngaytaophieu1.Value = DungChung.Ham.NgaySangChu(sopl.First().Value);
                                                    }
                                                    else
                                                    {
                                                        rep1.ngaytaophieu1.Value = "Ngày...tháng...năm 2022";
                                                    }
                                                    rep1.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                    rep1.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                    rep1.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                    rep1.SoPL.Value = _soPL.ToString();
                                                    rep1.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                    rep1.Benhvien.Value = DungChung.Bien.TenCQ;
                                                    rep1.theongay.Value = "Từ ngày: " + ngay4 + " đến ngày: " + ngay5;
                                                    rep1.Khoa.Value = bph1.First().TenKP;
                                                    if (tenkho.Count > 0)
                                                    {
                                                        rep1.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                    }
                                                    switch (kieudon)
                                                    {
                                                        case 0:
                                                            rep1.Chuthich.Value = _dtuong1 + "Loại phiếu: Hàng ngày";
                                                            break;
                                                        case 1:
                                                            rep1.Chuthich.Value = _dtuong1 + "Loại phiếu: Bổ sung";
                                                            break;
                                                        case 3:
                                                            rep1.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh về khoa";
                                                            break;
                                                        case 4:
                                                            rep1.Chuthich.Value = _dtuong1 + "Loại phiếu: Trả lại của khoa";
                                                            break;
                                                        case 5:
                                                            rep1.Chuthich.Value = _dtuong1 + "Loại phiếu: Ngoài giờ (trực)";
                                                            break;
                                                        case 2:
                                                            rep1.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                            break;
                                                    }
                                                    if (!tt)
                                                        rep1.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN");
                                                    else
                                                        rep1.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN");
                                                    rep1.MauSo.Value = "MS:08";

                                                    rep1.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                    rep1.BindingData();
                                                    rep1.CreateDocument();
                                                    frm4.prcIN.PrintingSystem = rep1.PrintingSystem;
                                                    frm4.ShowDialog();
                                                    this.Dispose();
                                                }
                                                #region bv khac
                                                else
                                                {
                                                    BaoCao.PhieulinhthuocGNHTT2lien rep = new BaoCao.PhieulinhthuocGNHTT2lien();
                                                    rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                    if (DungChung.Bien.MaBV == "12001")
                                                        rep.Tennguoiphat.Value = DungChung.Bien.ThuKho;
                                                    rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                    rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                    rep.SoPL.Value = _soPL.ToString();
                                                    rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                    rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                                    rep.theongay.Value = "Từ ngày: " + ngay4 + " đến ngày: " + ngay5;
                                                    rep.Khoa.Value = bph1.First().TenKP;
                                                    if (tenkho.Count > 0)
                                                        rep.Kholinh.Value = "Kho lĩnh: " + tenkho.First().TenKP;
                                                    switch (kieudon)
                                                    {
                                                        case 0:
                                                            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Hàng ngày";
                                                            break;
                                                        case 1:
                                                            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Bổ sung";
                                                            break;
                                                        case 3:
                                                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24012")
                                                            {
                                                                if (loaiphieu <= 1)
                                                                    rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh về khoa";
                                                                if (loaiphieu == 2)
                                                                    rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh bù tủ trực";
                                                                if (loaiphieu == 3)
                                                                    rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh bổ sung tủ trực";
                                                            }
                                                            else
                                                                rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Lĩnh về khoa";
                                                            break;
                                                        case 4:
                                                            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Trả lại của khoa";
                                                            break;
                                                        case 5:
                                                            rep.Chuthich.Value = _dtuong1 + "Loại phiếu: Ngoài giờ (trực)";
                                                            break;
                                                        case 2:
                                                            rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                            break;
                                                    }
                                                    rep.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                                    rep.MauSo.Value = "MS:08";
                                                    rep.DataSource = q.Where(p => p.LoaiDV == 3 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                                    rep.BindingData();
                                                    //rep.DataMember = "";
                                                    rep.CreateDocument();
                                                    frm4.prcIN.PrintingSystem = rep.PrintingSystem;
                                                    frm4.ShowDialog();
                                                    this.Dispose();
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                    #endregion

                                    if (lLoaiDuocCt.Contains(0) || lLoaiDuocCt.Contains(1) || lLoaiDuocCt.Contains(2) || lLoaiDuocCt.Contains(5) || lLoaiDuocCt.Contains(6) || lLoaiDuocCt.Contains(7))
                                    {
                                        for (int i = 0; i < 8; i++)
                                        {
                                            if (i != 3 && i != 4 && q3.Where(p => p.LoaiDV == i).Count() > 0)
                                            {
                                                #region 30002
                                                if (DungChung.Bien.MaBV == "30002")
                                                {
                                                    frmIn frm3 = new frmIn();
                                                    BaoCao.Phieulinhchokhoa_A5 rep3 = new BaoCao.Phieulinhchokhoa_A5();
                                                    //var kt1 = (from dt in _dataContext.DThuocs.Where(p => p.SoPL == _soPL && (_makp == 0 ? true : p.MaKP == _makp))
                                                    //           join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                                    //           join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                                                    //           join tn in _dataContext.TieuNhomDVs.Where(p => p.TenRG == "Y cụ") on dv.IdTieuNhom equals tn.IdTieuNhom
                                                    //           select dt).ToList();
                                                    //if (kt1.Count > 0)
                                                    //{
                                                    //    loaiduoc = 7;
                                                    //    rep3.Ycu.Value = 7;
                                                    //}

                                                    rep3.Ycu.Value = i;
                                                    rep3.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                    //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                    rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                    rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                    rep3.SoPL.Value = _soPL.ToString();
                                                    rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                    rep3.Benhvien.Value = DungChung.Bien.TenCQ;
                                                    var TD1 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                               join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                               select dt).Select(p => p.KieuDon).ToList();
                                                    if (TD1.First().Value != 2)
                                                    {
                                                        rep3.LoaiPL.Value = "Loại phiếu: Lĩnh về khoa";
                                                    }
                                                    else
                                                    { rep3.LoaiPL.Value = "Loại phiếu: Trả thuốc"; }
                                                    if (ten.Count > 0)
                                                        rep3.Khoa.Value = ten.First().TenKP;
                                                    if (tenkho.Count > 0)
                                                        rep3.Kholinh.Value = "Kho lĩnh  " + tenkho.First().TenKP;
                                                    switch (i)
                                                    {
                                                        case 0:
                                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                            rep3.MauSo.Value = "MS:01D/BV-01";
                                                            break;
                                                        case 1:
                                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                            rep3.MauSo.Value = "MS:02D/BV-01";
                                                            break;
                                                        case 2:
                                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ");
                                                            rep3.MauSo.Value = "MS:03D/BV-01";
                                                            break;
                                                        case 3:
                                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN");
                                                            rep3.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                        case 4:
                                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC HƯỚNG TÂM THẦN");
                                                            rep3.MauSo.Value = "MS:09D/BV-01";
                                                            break;
                                                        case 5:
                                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                            rep3.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                        case 6:
                                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                            rep3.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                        case 7:
                                                            rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH Y CỤ");
                                                            rep3.MauSo.Value = "MS:...D/BV-01";
                                                            break;
                                                    }

                                                    rep3.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                                    if (DungChung.Bien.MaBV == "30002")
                                                    {
                                                        double a = q3.Where(p => p.LoaiDV == i).Sum(p => p.ThanhTien);
                                                        rep3.colTongG.Text = a.ToString("###,###.00");
                                                        rep3.DataSource = q3.Where(p => p.LoaiDV == i).OrderBy(p => p.MaDV).ToList();
                                                    }
                                                    else
                                                        rep3.DataSource = q3.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                    //rep3.DataSource = q3.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                    rep3.BindingData();
                                                    //rep.DataMember = "";
                                                    rep3.CreateDocument();
                                                    frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                                    frm3.ShowDialog();
                                                }
                                                #endregion
                                                #region bv khác
                                                else
                                                {

                                                    //var kt1 = (from dt in _dataContext.DThuocs.Where(p => p.SoPL == _soPL && (_makp == 0 ? true : p.MaKP == _makp))
                                                    //           join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                                    //           join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                                                    //           join tn in _dataContext.TieuNhomDVs.Where(p => p.TenRG == "Y cụ") on dv.IdTieuNhom equals tn.IdTieuNhom
                                                    //           select dt).ToList();
                                                    frmIn frm3 = new frmIn();
                                                    #region 27023
                                                    if (DungChung.Bien.MaBV == "27023")
                                                    {
                                                        BaoCao.Phieulinhchokhoa_27023 rep3 = new BaoCao.Phieulinhchokhoa_27023();
                                                        //if (kt1.Count > 0)
                                                        //{
                                                        //    loaiduoc = 7;
                                                        //    rep3.Ycu.Value = 7;
                                                        //}
                                                        rep3.Ycu.Value = i;
                                                        rep3.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                                        rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep3.SoPL.Value = _soPL.ToString();
                                                        rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep3.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        var TD1 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                                   join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                                   select dt).Select(p => p.KieuDon).ToList();
                                                        if (TD1.First().Value != 2)
                                                        {
                                                            rep3.LoaiPL.Value = "Loại phiếu: Lĩnh về khoa";
                                                        }
                                                        else
                                                        { rep3.LoaiPL.Value = "Loại phiếu: Trả thuốc"; }
                                                        if (ten.Count > 0)
                                                            rep3.Khoa.Value = ten.First().TenKP;
                                                        if (tenkho.Count > 0)
                                                            rep3.Kholinh.Value = "Kho lĩnh  " + tenkho.First().TenKP;
                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep3.MauSo.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep3.MauSo.Value = "MS:02D/BV-01";
                                                                break;
                                                            case 2:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ");
                                                                rep3.MauSo.Value = "MS:03D/BV-01";
                                                                break;
                                                            case 3:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 4:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC HƯỚNG TÂM THẦN");
                                                                rep3.MauSo.Value = "MS:09D/BV-01";
                                                                break;
                                                            case 5:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 7:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH Y CỤ");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }

                                                        rep3.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                                        rep3.DataSource = q3.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep3.BindingData();
                                                        //rep.DataMember = "";
                                                        rep3.CreateDocument();
                                                        frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                                        frm3.ShowDialog();
                                                    }
                                                    #endregion
                                                    #region 27022 có đơn giá, thanh tiền

                                                    if (DungChung.Bien.MaBV == "27022")
                                                    {
                                                        #region New 27022
                                                        //BaoCao.PhieuVPP_27022 rep5 = new BaoCao.PhieuVPP_27022();
                                                        //rep5.NguoiLinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        //rep5.ThuKho.Value = DungChung.Bien.ThuKho;
                                                        //rep5.TruongKP.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        //rep5.KeToan.Value = DungChung.Bien.KeToanTruong;
                                                        //rep5.TruongKP.Value = DungChung.Bien.
                                                        //rep5.Ycu.Value = i;
                                                        //rep5.SoPL.Value = _soPL.ToString();
                                                        //rep5.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        //rep5.BenhVien.Value = DungChung.Bien.TenCQ;




                                                        //var TD1 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                        //join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                        //select dt).Select(p => p.KieuDon).ToList();
                                                        //if (ten.Count > 0)
                                                        //  rep5.Khoa.Value = "KHOA(PHÒNG):  " + ten.First().TenKP.ToUpper();
                                                        //if (tenkho.Count > 0)
                                                        //  rep5.Kholinh.Value = "Kho Lĩnh:  " + tenkho.First().TenKP;
                                                        //switch (i)
                                                        //{
                                                        //  case 0:
                                                        //    if (q3.Where(p => p.PLoai == 5).Count() > 0)
                                                        //  {
                                                        //    rep5.Loaiphieulinh.Value = ("PHIẾU LĨNH VĂN PHÒNG PHẨM");
                                                        //}
                                                        //else rep5.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                        //break;
                                                        //case 1:
                                                        //  rep5.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                        //rep4.Mauso.Value = "MS:02D/BV-01";
                                                        //   break;
                                                        //case 2:
                                                        //  rep5.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ");

                                                        //rep4.Mauso.Value = "MS:03D/BV-01";
                                                        //break;
                                                        //case 3:
                                                        //  rep5.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN");
                                                        //rep4.Mauso.Value = "MS:...D/BV-01";
                                                        //break;
                                                        //case 4:
                                                        //  rep5.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC HƯỚNG TÂM THẦN");
                                                        // rep4.Mauso.Value = "MS:09D/BV-01";
                                                        //break;
                                                        //case 5:
                                                        //  rep5.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                        //rep4.Mauso.Value = "MS:...D/BV-01";
                                                        //break;
                                                        //case 6:
                                                        //  rep5.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                        //rep4.Mauso.Value = "MS:...D/BV-01";
                                                        //break;
                                                        //case 7:
                                                        //  rep5.Loaiphieulinh.Value = ("PHIẾU LĨNH Y CỤ");
                                                        //rep4.Mauso.Value = "MS:...D/BV-01";
                                                        //break;

                                                        //}
                                                        //rep5.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                                        //rep5.DataSource = q3.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        //rep5.BindingData();
                                                        //rep5.CreateDocument();
                                                        //frm3.prcIN.PrintingSystem = rep5.PrintingSystem;
                                                        //frm3.ShowDialog();
                                                        #endregion

                                                        #region Old 27022

                                                        BaoCao.PhieulinhthuocVTYT_CoDonGia rep3 = new BaoCao.PhieulinhthuocVTYT_CoDonGia();
                                                        rep3.Ycu.Value = i;
                                                        string nguoiphat = "";
                                                        if (tenkho.Count > 0)
                                                            if (tenkho.First().NguoiPhat != null)
                                                                nguoiphat = tenkho.First().NguoiPhat;
                                                        rep3.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        rep3.Tennguoiphat.Value = DungChung.Bien.ThuKho; //DungChung.Bien.Nguoiphat;
                                                        rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep3.SoPL.Value = _soPL.ToString();
                                                        rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep3.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        var TD1 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                                   join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                                   select dt).Select(p => p.KieuDon).ToList();
                                                        if (TD1.First().Value != 2)
                                                        {
                                                            rep3.Chuthich.Value = "Loại phiếu: Lĩnh về khoa";
                                                        }
                                                        else
                                                        { rep3.Chuthich.Value = "Loại phiếu: Trả thuốc"; }
                                                        if (ten.Count > 0)
                                                            rep3.Khoa.Value = ten.First().TenKP;
                                                        if (tenkho.Count > 0)
                                                            rep3.Kholinh.Value = "Kho lĩnh:  " + tenkho.First().TenKP;
                                                        switch (i)
                                                        {
                                                            case 0:
                                                                // start HIS-1435
                                                                if (q3.Where(p => p.PLoai == 5).Count() > 0)
                                                                {
                                                                    rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH VĂN PHÒNG PHẨM");
                                                                    rep3.CPloai.Value = 5;
                                                                    rep3.Tentruongkhoaduoc.Value = DungChung.Bien.NguoiLapBieu;
                                                                    rep3.Tennguoilinh.Value = DungChung.Bien.TruongKhoaDuoc;
                                                                    rep3.Tentruongkhoalamsang.Value = DungChung.Bien.KeToanTruong;
                                                                }
                                                                else rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                // end HIS-1435
                                                                rep3.MauSo.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep3.MauSo.Value = "MS:02D/BV-01";
                                                                break;
                                                            case 2:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ");
                                                                rep3.MauSo.Value = "MS:03D/BV-01";
                                                                break;
                                                            case 3:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 4:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC HƯỚNG TÂM THẦN");
                                                                rep3.MauSo.Value = "MS:09D/BV-01";
                                                                break;
                                                            case 5:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 7:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH Y CỤ");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }
                                                        rep3.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                                        rep3.DataSource = q3.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep3.BindingData();
                                                        rep3.CreateDocument();
                                                        //----------------------

                                                        frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                                        frm3.ShowDialog();



                                                        #endregion
                                                    }
                                                    #endregion
                                                    #region 01071
                                                    else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                                    {
                                                        BaoCao.PhieulinhthuocVTYT_A5_2lien_01071 rep3 = new BaoCao.PhieulinhthuocVTYT_A5_2lien_01071();

                                                        rep3.Ycu.Value = i;

                                                        rep3.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep3.SoPL.Value = _soPL.ToString();
                                                        rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep3.Benhvien.Value = DungChung.Bien.TenCQ;

                                                        var TD1 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                                   join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                                   select dt).ToList();//.Select(p => p.KieuDon).ToList();
                                                        var TD2 = (from td in TD1
                                                                   join kp in _dataContext.KPhongs.Where(p => p.PLoai.Contains("Tủ trực")) on td.MaKP equals kp.MaKP
                                                                   select kp).ToList();

                                                        if (TD1.First().KieuDon.Value != 2)
                                                        {
                                                            rep3.Chuthich.Value = "Loại phiếu: Lĩnh về khoa";
                                                        }
                                                        else
                                                        { rep3.Chuthich.Value = "Loại phiếu: Trả thuốc"; }

                                                        if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                                        {
                                                            if (TD2.Count > 0)
                                                            {
                                                                if (TD1.First().MaBNhanChiTiet != null && TD2.First().PLoai == "Tủ trực")
                                                                {
                                                                    rep3.Chuthich.Value = "Loại phiếu: Lĩnh bù tủ trực";
                                                                }
                                                                else if (TD1.First().MaBNhanChiTiet == null && TD2.First().PLoai == "Tủ trực")
                                                                {
                                                                    rep3.Chuthich.Value = "Loại phiếu: Bổ sung tủ trực";
                                                                }
                                                                else if (TD1.First().KieuDon.Value != 2)
                                                                {
                                                                    rep3.Chuthich.Value = "Loại phiếu: Lĩnh về khoa";
                                                                }
                                                                else
                                                                {
                                                                    rep3.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                                                }
                                                            }
                                                        }
                                                        if (ten.Count > 0)
                                                            rep3.Khoa.Value = ten.First().TenKP;
                                                        if (tenkho.Count > 0)
                                                            rep3.Kholinh.Value = "Kho lĩnh  " + tenkho.First().TenKP;
                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep3.MauSo.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep3.MauSo.Value = "MS:02D/BV-01";
                                                                break;
                                                            case 2:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ");
                                                                rep3.MauSo.Value = "MS:03D/BV-01";
                                                                break;
                                                            case 3:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 4:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC HƯỚNG TÂM THẦN");
                                                                rep3.MauSo.Value = "MS:09D/BV-01";
                                                                break;
                                                            case 5:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 7:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH Y CỤ");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                        }
                                                        #region in đối tượng
                                                        bool khoaKeTuTruc = false;
                                                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                                        {
                                                            int makp = _makp;
                                                            if (makp <= 0)
                                                            {
                                                                var qdtct = _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).FirstOrDefault();
                                                                if (qdtct != null)
                                                                    makp = qdtct.MaKP.Value;
                                                            }

                                                            var kt = _dataContext.KPhongs.Where(p => p.MaKP == makp && p.PLoai.Contains("Tủ trực")).Select(p => p.TenKP).FirstOrDefault();
                                                            if (kt != null)
                                                                khoaKeTuTruc = true;

                                                            if (khoaKeTuTruc)
                                                            {
                                                                string doituong = "";
                                                                int iddtbn = q3.First().XHH;
                                                                if (iddtbn == 99)
                                                                    doituong = "Tất cả";
                                                                else
                                                                {
                                                                    var dtuong = _dataContext.DTBNs.Where(p => p.IDDTBN == iddtbn).FirstOrDefault();
                                                                    if (dtuong != null)
                                                                        doituong = dtuong.DTBN1;
                                                                }
                                                                if (doituong != "")
                                                                    rep3.DoiTuong.Value = "Đối tượng: " + doituong;
                                                            }

                                                        }
                                                        #endregion
                                                        if (TD1.Count > 0)
                                                        {
                                                            rep3.theongay.Value = "Từ ngày " + TD1.Min(p => p.NgayKe.Value).ToString("dd/MM/yyyy") + " đến ngày " + TD1.Max(p => p.NgayKe.Value).ToString("dd/MM/yyyy");  // DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                                        }
                                                        rep3.DataSource = q3.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep3.BindingData();
                                                        //rep.DataMember = "";
                                                        rep3.CreateDocument();
                                                        frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                                        frm3.ShowDialog();
                                                    }
                                                    else if (DungChung.Bien.MaBV == "24012")
                                                    {
                                                        BaoCao.Phieulinhchokhoa_24012 rep3 = new BaoCao.Phieulinhchokhoa_24012();

                                                        //rep3.Ycu.Value = i;
                                                        string nguoiphat = "";
                                                        if (tenkho.Count > 0)
                                                            if (tenkho.First().NguoiPhat != null)
                                                                nguoiphat = tenkho.First().NguoiPhat;
                                                        rep3.Tennguoilinh1.Value = DungChung.Bien.NguoiLapBieu;
                                                        rep3.Tennguoiphat1.Value = DungChung.Bien.ThuKho;
                                                        rep3.Tentruongkhoaduoc1.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep3.Tentruongkhoalamsang1.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep3.SoPL1.Value = _soPL.ToString() + "/" + DateTime.Now.Year ;
                                                        rep3.Boyte1.Value = DungChung.Bien.TenCQCQ;
                                                        rep3.Benhvien1.Value = DungChung.Bien.TenCQ;
                                                        var TD1 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                                   join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                                   select dt).ToList();
                                                        var TD2 = (from td in TD1
                                                                   join kp in _dataContext.KPhongs.Where(p => p.PLoai.Contains("Tủ trực")) on td.MaKP equals kp.MaKP
                                                                   select kp).ToList();

                                                        if (TD1.First().KieuDon.Value != 2)
                                                        {
                                                            rep3.LoaiPL1.Value = "Loại phiếu: Lĩnh về khoa";
                                                        }
                                                        else
                                                        { rep3.LoaiPL1.Value = "Loại phiếu: Trả thuốc"; }

                                                        if (TD2.Count > 0)
                                                        {
                                                            if (TD1.First().MaBNhanChiTiet != null && TD2.First().PLoai == "Tủ trực")
                                                            {
                                                                rep3.LoaiPL1.Value = "Loại phiếu: Lĩnh bù tủ trực";
                                                            }
                                                            else if (TD1.First().MaBNhanChiTiet == null && TD2.First().PLoai == "Tủ trực")
                                                            {
                                                                rep3.LoaiPL1.Value = "Loại phiếu: Bổ sung tủ trực";
                                                            }
                                                            else if (TD1.First().KieuDon.Value != 2)
                                                            {
                                                                rep3.LoaiPL1.Value = "Loại phiếu: Lĩnh về khoa";
                                                            }
                                                            else
                                                            {
                                                                rep3.LoaiPL1.Value = "Loại phiếu: Trả thuốc";
                                                            }
                                                        }
                                                        
                                                        if (ten.Count > 0)
                                                            rep3.Khoa1.Value =  ten.First().TenKP.ToUpper();
                                                        if (tenkho.Count > 0)
                                                            rep3.Kholinh1.Value = tenkho.First().TenKP;
                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep3.MauSo1.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep3.MauSo1.Value = "MS:02D/BV-01";
                                                                break;
                                                            case 2:
                                                                rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ");
                                                                rep3.MauSo1.Value = "MS:03D/BV-01";

                                                                break;
                                                            case 3:
                                                                rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN");
                                                                rep3.MauSo1.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 4:
                                                                rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH THUỐC HƯỚNG TÂM THẦN");
                                                                rep3.MauSo1.Value = "MS:09D/BV-01";
                                                                break;
                                                            case 5:
                                                                rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep3.MauSo1.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep3.MauSo1.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 7:
                                                                rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH Y CỤ");
                                                                rep3.MauSo1.Value = "MS:...D/BV-01";
                                                                break;
                                                        }

                                                        rep3.theongay1.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                                        var sopl = (from pl in _dataContext.SoPLs.Where(p => p.SoPL1 == _soPL)
                                                                    select pl).Select(p => p.NgayNhap).ToList();
                                                        if (sopl.Count > 0)
                                                        {
                                                            rep3.ngaytaophieu1.Value = DungChung.Ham.NgaySangChu(sopl.First().Value);
                                                        }
                                                        else
                                                        {
                                                            rep3.ngaytaophieu1.Value = "Ngày...tháng...năm 2022";
                                                        }
                                                        rep3.DataSource = q3.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep3.BindingData();
                                                        //rep.DataMember = "";
                                                        rep3.CreateDocument();
                                                        frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                                        frm3.ShowDialog();
                                                    }
                                                    #endregion
                                                    else
                                                    #region bv khác
                                                    {
                                                        BaoCao.Phieulinhchokhoa rep3 = new BaoCao.Phieulinhchokhoa();

                                                        rep3.Ycu.Value = i;
                                                        string nguoiphat = "";
                                                        //if (tenkho.Count > 0)
                                                        //    if (tenkho.First().NguoiPhat != null)
                                                        //        nguoiphat = tenkho.First().NguoiPhat;
                                                        rep3.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                                        rep3.Tennguoiphat.Value = DungChung.Bien.ThuKho; //DungChung.Bien.Nguoiphat;
                                                        rep3.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                        rep3.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                        rep3.SoPL.Value = DungChung.Bien.MaBV == "14017" ? _soPL.ToString() + "/" + DateTime.Now.Year : _soPL.ToString();
                                                        rep3.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                        rep3.Benhvien.Value = DungChung.Bien.TenCQ;
                                                        var TD1 = (from dt in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                                                   join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on dt.IDDon equals dtct.IDDon
                                                                   select dt).ToList();//.Select(p => p.KieuDon).ToList();
                                                        var TD2 = (from td in TD1
                                                                   join kp in _dataContext.KPhongs.Where(p => p.PLoai.Contains("Tủ trực")) on td.MaKP equals kp.MaKP
                                                                   select kp).ToList();

                                                        if (TD1.First().KieuDon.Value != 2)
                                                        {
                                                            rep3.LoaiPL.Value = "Loại phiếu: Lĩnh về khoa";
                                                        }
                                                        else
                                                        { rep3.LoaiPL.Value = "Loại phiếu: Trả thuốc"; }

                                                        if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                                        {
                                                            if (TD2.Count > 0)
                                                            {
                                                                if (TD1.First().MaBNhanChiTiet != null && TD2.First().PLoai == "Tủ trực")
                                                                {
                                                                    rep3.LoaiPL.Value = "Loại phiếu: Lĩnh bù tủ trực";
                                                                }
                                                                else if (TD1.First().MaBNhanChiTiet == null && TD2.First().PLoai == "Tủ trực")
                                                                {
                                                                    rep3.LoaiPL.Value = "Loại phiếu: Bổ sung tủ trực";
                                                                }
                                                                else if (TD1.First().KieuDon.Value != 2)
                                                                {
                                                                    rep3.LoaiPL.Value = "Loại phiếu: Lĩnh về khoa";
                                                                }
                                                                else
                                                                {
                                                                    rep3.LoaiPL.Value = "Loại phiếu: Trả thuốc";
                                                                }
                                                            }
                                                        }
                                                        if (ten.Count > 0)
                                                            rep3.Khoa.Value = DungChung.Bien.MaBV == "14017" ? ten.First().TenKP.ToUpper() : ten.First().TenKP;
                                                        if (tenkho.Count > 0)
                                                            rep3.Kholinh.Value = DungChung.Bien.MaBV == "14017" ? tenkho.First().TenKP : "Kho lĩnh  " + tenkho.First().TenKP;
                                                        switch (i)
                                                        {
                                                            case 0:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC");
                                                                rep3.MauSo.Value = "MS:01D/BV-01";
                                                                break;
                                                            case 1:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH HÓA CHẤT");
                                                                rep3.MauSo.Value = "MS:02D/BV-01";
                                                                if (DungChung.Bien.MaBV == "14017")
                                                                {
                                                                    rep3.celTenDV.Text = "Tên hóa chất";
                                                                }
                                                                break;
                                                            case 2:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ");
                                                                rep3.MauSo.Value = "MS:03D/BV-01";
                                                                if (DungChung.Bien.MaBV == "14017")
                                                                {
                                                                    rep3.celTenDV.Text = "Tên vật tư";
                                                                }
                                                                break;
                                                            case 3:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC GÂY NGHIỆN");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 4:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC HƯỚNG TÂM THẦN");
                                                                rep3.MauSo.Value = "MS:09D/BV-01";
                                                                break;
                                                            case 5:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC TRẺ EM");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 6:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH THUỐC ĐÔNG Y");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                break;
                                                            case 7:
                                                                rep3.Loaiphieulinh.Value = ("PHIẾU LĨNH Y CỤ");
                                                                rep3.MauSo.Value = "MS:...D/BV-01";
                                                                if (DungChung.Bien.MaBV == "14017")
                                                                {
                                                                    rep3.celTenDV.Text = "Tên y cụ";
                                                                }
                                                                break;
                                                        }

                                                        rep3.theongay.Value = rep3.txtNgayThang1.Text = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                                        rep3.TongTien.Value = q3.Where(p => p.LoaiDV == i).Sum(p => p.ThanhTien).ToString();
                                                        rep3.DataSource = q3.Where(p => p.LoaiDV == i).OrderBy(p => p.DonVi).ToList();
                                                        rep3.BindingData();
                                                        //rep.DataMember = "";
                                                        rep3.CreateDocument();
                                                        frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                                                        frm3.ShowDialog();
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                        #region kiểu đơn khoa trả thuốc
                        else
                        {
                            //var q3 = (from kd in _dataContext.DThuocs.Where(p => p.SoPL == _soPL && (_makp == 0 ? true : p.MaKP == _makp))
                            //          join kdct in _dataContext.DThuoccts on kd.IDDon equals kdct.IDDon
                            //          join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                            //          group new { kdct, dv, kd } by new { dv.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.NgayKe, dv.TenRG, dv.HamLuong } into kq
                            //          select new {SoLo = GetSoLo(qDonGiaDV, kq.Key.MaDV, kq.Key.DonGia), HanDung = GetHanDung(qDonGiaDV, kq.Key.MaDV, kq.Key.DonGia), kq.Key.MaTam, kq.Key.NgayKe, kq.Key.MaKP, kq.Key.MaKXuat, TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : (DungChung.Bien.MaBV == "26007" ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : kq.Key.TenDV), kq.Key.MaDV, DonVi = kq.Key.DonVi, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                            var q3 = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                      join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL) on kd.IDDon equals kdct.IDDon
                                      join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                      join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                      group new { kdct, dv, kd, tn } by new { dv.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.NgayKe, dv.TenRG, dv.HamLuong, SoLo = DungChung.Bien.MaBV == "27023" ? kdct.SoLo : "", HanDung = DungChung.Bien.MaBV == "27023" ? kdct.HanDung : null, TenRGTN = tn.TenRG } into kq
                                      select new
                                      {
                                          kq.Key.SoLo,
                                          kq.Key.HanDung,
                                          kq.Key.MaDV,
                                          kq.Key.DonGia,
                                          kq.Key.MaTam,
                                          kq.Key.NgayKe,
                                          kq.Key.MaKP,
                                          kq.Key.MaKXuat,
                                          TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : ((DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "30009") ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : kq.Key.TenDV),
                                          DonVi = kq.Key.DonVi,
                                          ThanhTien = kq.Sum(p => p.kdct.ThanhTien),
                                          SoLuong = kq.Sum(p => p.kdct.SoLuong),
                                          LoaiDuoc = kq.Key.LoaiDuoc,
                                          LoaiDV = kq.Key.TenRGTN.Contains("Thuốc thường") ? 0 : (kq.Key.TenRGTN.Contains("Hóa chất") ? 1 : (kq.Key.TenRGTN.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRGTN.Contains("Thuốc đông y") ? 6 : (kq.Key.TenRGTN.Contains("Y cụ") ? 7 : 0)))))))
                                      }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                            List<int> lLoaiDuocCt = q3.Select(p => p.LoaiDV).Distinct().ToList();

                            #region 30002
                            if (DungChung.Bien.MaBV == "30002")
                            {
                                frmIn frm3 = new frmIn();
                                BaoCao.PhieuTrathuocVTYT_A5 rep = new BaoCao.PhieuTrathuocVTYT_A5();
                                rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                rep.SoPL.Value = _soPL.ToString();
                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                var q = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                         join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                         join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                         group new { kdct, dv, kd } by new { dv.TenHC, dv.HamLuong, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, kd.NgayKe, dv.TenRG } into kq
                                         select new { kq.Key.NgayKe, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : (DungChung.Bien.MaBV == "12122" ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV))), kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();


                                rep.theongay.Value = DungChung.Ham.NgaySangChu(q.First().NgayKe.Value);
                                //var q3 = (from kd in _dataContext.DThuocs.Where(p => p.SoPL == _soPL && (_makp == 0 ? true : p.MaKP == _makp))
                                //          join kdct in _dataContext.DThuoccts on kd.IDDon equals kdct.IDDon
                                //          join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                //          group new { kdct, dv, kd } by new { dv.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.NgayKe } into kq
                                //          select new { kq.Key.MaTam, kq.Key.NgayKe, kq.Key.MaKP, kq.Key.MaKXuat, TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();

                                if (q3.Count > 0)
                                {
                                    int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                    var ten = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                    rep.Khoa.Value = ten.First().TenKP;


                                    int tenkpx = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                    var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkpx)).ToList();

                                    rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                                }


                                rep.MauSo.Value = "MS:05D/BV-01";

                                rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                rep.BindingData();
                                //rep.DataMember = "";
                                rep.CreateDocument();
                                frm3.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm3.ShowDialog();
                                this.Dispose();
                            }
                            #endregion
                            if ((DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "01830") && (lLoaiDuocCt.Contains(3) || lLoaiDuocCt.Contains(4)))
                            {
                                #region bv 30003
                                //var q = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                //         join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                //         join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                //         join tn in _dataContext.TieuNhomDVs.Where(p => p.TenRG.Contains("Thuốc gây nghiện") || p.TenRG.Contains("Thuốc hướng tâm thần")) on dv.IdTieuNhom equals tn.IdTieuNhom
                                //         group new { kdct, dv, kd } by new { dv.TenHC, dv.HamLuong,kdct.MaKXuat, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, kd.NgayKe } into kq
                                //         select new { kq.Key.NgayKe,kq.Key.MaKXuat, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : kq.Key.TenDV), kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                if (q3.Count() > 0)
                                {
                                    if (DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "01830")
                                    {


                                        frmIn frm3 = new frmIn();
                                        BaoCao.PhieulinhthuocGNHTT2lien rep = new BaoCao.PhieulinhthuocGNHTT2lien();
                                        rep.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);
                                        int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                        var ten = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                        rep.Khoa.Value = ten.First().TenKP;


                                        int tenkpx = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkpx)).ToList();

                                        rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;

                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                        rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC THÀNH PHẨM GÂY NGHIỆN, THUỐC THÀNH PHẨM HƯỚNG TÂM THẦN, THUỐC THÀNH PHẨM TIỀN CHẤT");
                                        rep.DataSource = q3.Where(p => p.LoaiDV == 4 || p.LoaiDV == 3).OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm3.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm3.ShowDialog();
                                        this.Dispose();

                                    }
                                    else
                                    {
                                        frmIn frm3 = new frmIn();
                                        BaoCao.PhieulinhthuocVTYT rep = new BaoCao.PhieulinhthuocVTYT(6);
                                        rep.Tennguoilinh.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = DungChung.Ham.NgaySangChu(q3.First().NgayKe.Value);

                                        rep.xrTableCell28.Text = "";
                                        int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                        var ten = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                        rep.Khoa.Value = ten.First().TenKP;


                                        int tenkpx = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                        var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkpx)).ToList();

                                        rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                                        rep.Chuthich.Value = "Loại phiếu: Trả thuốc";
                                        rep.Loaiphieulinh.Value = ("PHIẾU TRẢ THUỐC GÂY NGHIỆN, THUỐC HƯỚNG TÂM THẦN, THUỐC TIỀN CHẤT");
                                        rep.MauSo.Value = "MS:01D/BV-01";
                                        rep.DataSource = q3.Where(p => p.LoaiDV == 4 || p.LoaiDV == 4).OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm3.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm3.ShowDialog();
                                        this.Dispose();

                                    }
                                }
                                #endregion

                            }
                            else
                            {

                                //var q = (from kd in _dataContext.DThuocs.Where(p => p.SoPL == _soPL && (_makp == 0 ? true : p.MaKP == _makp))
                                //         join kdct in _dataContext.DThuoccts.Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                //         join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                //         group new { kdct, dv, kd } by new { dv.TenHC, dv.HamLuong, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, kd.NgayKe, dv.TenRG } into kq
                                //         select new {SoLo = GetSoLo(qDonGiaDV, kq.Key.MaDV, kq.Key.DonGia),HanDung = GetHanDung(qDonGiaDV, kq.Key.MaDV, kq.Key.DonGia), kq.Key.NgayKe, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : ((DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "26007") ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV))), kq.Key.MaDV, kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();

                                var q = (from kd in _dataContext.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp)
                                         join kdct in _dataContext.DThuoccts.Where(p => p.SoPL == _soPL).Where(p => p.Status >= 0) on kd.IDDon equals kdct.IDDon
                                         join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                         join tn in _dataContext.TieuNhomDVs.Where(p => (DungChung.Bien.MaBV == "30003") ? (!p.TenRG.Contains("Thuốc gây nghiện") && !p.TenRG.Contains("Thuốc hướng tâm thần")) : true)
                                         on dv.IdTieuNhom equals tn.IdTieuNhom
                                         group new { kdct, dv, kd } by new { dv.TenHC, dv.HamLuong, kdct.DonGia, dv.SoDK, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.LoaiDuoc, kd.NgayKe, dv.TenRG, SoLo = DungChung.Bien.MaBV == "27023" ? kdct.SoLo : "", HanDung = DungChung.Bien.MaBV == "27023" ? kdct.HanDung : null, TenRGTN = tn.TenRG, dv.MaTam } into kq
                                         select new { kq.Key.SoLo, kq.Key.HanDung, kq.Key.MaDV, kq.Key.DonGia, kq.Key.NgayKe, TenDV = (DungChung.Bien.MaBV == "30007" ? (kq.Key.TenDV + " (" + kq.Key.TenHC + ": " + kq.Key.HamLuong + ") ") : ((DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30009") ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") ? kq.Key.TenDV + " " + kq.Key.HamLuong : DungChung.Bien.MaBV == "27023" ? (kq.Key.TenDV + (string.IsNullOrEmpty(kq.Key.HamLuong) ? "" : " (" + kq.Key.HamLuong + ") ")) : kq.Key.TenDV)))), kq.Key.SoDK, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kdct.SoLuong) * (-1), ThanhTien = kq.Sum(p => p.kdct.ThanhTien) * (-1), LoaiDuoc = kq.Key.LoaiDuoc, LoaiDV = kq.Key.TenRGTN.Contains("Thuốc thường") ? 0 : (kq.Key.TenRGTN.Contains("Hóa chất") ? 1 : (kq.Key.TenRGTN.Contains("Vật tư y tế") ? 2 : (kq.Key.TenRGTN.Contains("Thuốc gây nghiện") ? 3 : (kq.Key.TenRGTN.Contains("Thuốc hướng tâm thần") ? 4 : (kq.Key.TenRGTN.Contains("Thuốc trẻ em") ? 5 : (kq.Key.TenRGTN.Contains("Thuốc đông y") ? 6 : 0)))))), kq.Key.MaTam }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                if (q.Count() > 0)
                                {
                                    #region 27023
                                    if (DungChung.Bien.MaBV == "27023")
                                    {
                                        frmIn frm3 = new frmIn();
                                        BaoCao.PhieuTrathuocVTYT_27023 rep = new BaoCao.PhieuTrathuocVTYT_27023();
                                        rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = DungChung.Ham.NgaySangChu(q.First().NgayKe.Value);
                                        //var q3 = (from kd in _dataContext.DThuocs.Where(p => p.SoPL == _soPL && (_makp == 0 ? true : p.MaKP == _makp))
                                        //          join kdct in _dataContext.DThuoccts on kd.IDDon equals kdct.IDDon
                                        //          join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                        //          group new { kdct, dv, kd } by new { dv.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.NgayKe } into kq
                                        //          select new { kq.Key.MaTam, kq.Key.NgayKe, kq.Key.MaKP, kq.Key.MaKXuat, TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        if (q3.Count > 0)
                                        {
                                            int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                            var ten = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                            rep.Khoa.Value = ten.First().TenKP;


                                            int tenkpx = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkpx)).ToList();

                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                                        }


                                        rep.MauSo.Value = "MS:05D/BV-01";
                                        //var qbc = (from a in q
                                        //          select new { SoLo = GetSoLo(qDonGiaDV, a.MaDV, a.DonGia), HanDung = GetHanDung(qDonGiaDV, a.MaDV, a.DonGia),  a.NgayKe, TenDV = a.TenDV, a.MaDV, DonVi = a.DonVi, ThanhTien = a.ThanhTien, SoLuong = a.SoLuong, DonGia = a.DonGia, LoaiDuoc = a.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm3.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm3.ShowDialog();
                                        this.Dispose();
                                    }
                                    #endregion
                                    #region 01071
                                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                    {
                                        frmIn frm3 = new frmIn();
                                        BaoCao.PhieuTrathuocVTYT_A5_2lien rep = new BaoCao.PhieuTrathuocVTYT_A5_2lien();
                                        rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = DungChung.Ham.NgaySangChu(q.First().NgayKe.Value);
                                        //var q3 = (from kd in _dataContext.DThuocs.Where(p => p.SoPL == _soPL && (_makp == 0 ? true : p.MaKP == _makp))
                                        //          join kdct in _dataContext.DThuoccts on kd.IDDon equals kdct.IDDon
                                        //          join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                        //          group new { kdct, dv, kd } by new { dv.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.NgayKe } into kq
                                        //          select new { kq.Key.MaTam, kq.Key.NgayKe, kq.Key.MaKP, kq.Key.MaKXuat, TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        if (q3.Count > 0)
                                        {
                                            int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                            var ten = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                            rep.Khoa.Value = ten.First().TenKP;


                                            int tenkpx = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkpx)).ToList();

                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                                        }


                                        rep.MauSo.Value = "MS:05D/BV-01";

                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm3.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm3.ShowDialog();
                                        this.Dispose();
                                    }
                                    #endregion
                                    #region 14017
                                    else if (DungChung.Bien.MaBV == "14017")
                                    {
                                        frmIn frm1 = new frmIn();
                                        for (int i = 0; i <= 6; i++)
                                        {
                                            if (q.Where(p => p.LoaiDV == i).Count() > 0)
                                            {
                                                BaoCao.PhieuTrathuocVTYT rep = new BaoCao.PhieuTrathuocVTYT();
                                                rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                                //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;

                                                rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                                rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                                rep.SoPL.Value = _soPL.ToString();
                                                rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                                rep.Benhvien.Value = DungChung.Bien.TenCQ;

                                                if (q3.Count > 0)
                                                {
                                                    int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                                    var ten = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                                    rep.Khoa.Value = ten.First().TenKP.ToUpper();


                                                    int tenkpx = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                                    var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkpx)).ToList();

                                                    rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                                                }
                                                rep.MauSo.Value = "MS:05D/BV-01";

                                                rep.xrLabel1.Text = DungChung.Ham.NgaySangChu(Convert.ToDateTime(q.First().NgayKe));
                                                rep.xrTableCell67.Text = q.Where(p => p.LoaiDV == i).Count().ToString() + " khoản";
                                                //rep.xrTableCell75.Text = q.Sum(p => p.ThanhTien).ToString();
                                                //rep.TTien.Value = q.First().ThanhTien;
                                                //rep.xrTableCell75.Text = q.First().ThanhTien.ToString();
                                                rep.DataSource = q.Where(p => p.LoaiDV == i).OrderBy(p => p.TenDV).ToList();
                                                rep.BindingData();
                                                //rep.DataMember = "";
                                                rep.CreateDocument();
                                                frm1.prcIN.PrintingSystem = rep.PrintingSystem;
                                                frm1.ShowDialog();
                                                this.Dispose();
                                            }
                                        }
                                    }
                                    #endregion
                                    #region bv khác
                                    else
                                    {
                                        frmIn frm3 = new frmIn();
                                        BaoCao.PhieuTrathuocVTYT rep = new BaoCao.PhieuTrathuocVTYT();
                                        rep.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
                                        //rep.Tennguoiphat.Value = DungChung.Bien.Nguoiphat;
                                        rep.Tentruongkhoaduoc.Value = DungChung.Bien.TruongKhoaDuoc;
                                        rep.Tentruongkhoalamsang.Value = DungChung.Bien.TruongKhoaLS;
                                        rep.SoPL.Value = _soPL.ToString();
                                        rep.Boyte.Value = DungChung.Bien.TenCQCQ;
                                        rep.Benhvien.Value = DungChung.Bien.TenCQ;
                                        rep.theongay.Value = DungChung.Ham.NgaySangChu(q.First().NgayKe.Value);
                                        //var q3 = (from kd in _dataContext.DThuocs.Where(p => p.SoPL == _soPL && (_makp == 0 ? true : p.MaKP == _makp))
                                        //          join kdct in _dataContext.DThuoccts on kd.IDDon equals kdct.IDDon
                                        //          join dv in _dataContext.DichVus on kdct.MaDV equals dv.MaDV
                                        //          group new { kdct, dv, kd } by new { dv.MaTam, kd.MaKP, kd.MaKXuat, kd.LoaiDuoc, kdct.DonGia, kdct.DonVi, kdct.MaDV, dv.TenDV, kd.NgayKe } into kq
                                        //          select new { kq.Key.MaTam, kq.Key.NgayKe, kq.Key.MaKP, kq.Key.MaKXuat, TenDV = kq.Key.TenDV, kq.Key.MaDV, DonVi = kq.Key.DonVi, ThanhTien = kq.Sum(p => p.kdct.ThanhTien), SoLuong = kq.Sum(p => p.kdct.SoLuong), DonGia = kq.Key.DonGia, LoaiDuoc = kq.Key.LoaiDuoc }).OrderBy(p => p.DonVi).ThenBy(p => p.TenDV).ThenBy(p => p.DonGia).ToList();
                                        if (q3.Count > 0)
                                        {
                                            int tenkp = q3.First().MaKP == null ? 0 : q3.First().MaKP.Value;
                                            var ten = _dataContext.KPhongs.Where(p => p.MaKP == (tenkp)).ToList();
                                            rep.Khoa.Value = ten.First().TenKP;


                                            int tenkpx = q3.First().MaKXuat == null ? 0 : q3.First().MaKXuat.Value;
                                            var tenkho = _dataContext.KPhongs.Where(p => p.MaKP == (tenkpx)).ToList();

                                            rep.Kholinh.Value = "Kho trả: " + tenkho.First().TenKP;
                                        }


                                        rep.MauSo.Value = "MS:05D/BV-01";

                                        rep.DataSource = q.OrderBy(p => p.DonVi).ToList();
                                        rep.BindingData();
                                        //rep.DataMember = "";
                                        rep.CreateDocument();
                                        frm3.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm3.ShowDialog();
                                        this.Dispose();
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                    this.Dispose();
                    break;


            }
        }

        //private DateTime? GetHanDung(List<DonGiaDV> qDonGiaDV, int? maDV, double DonGia)
        //{
        //    if (qDonGiaDV.Count > 0)
        //    {
        //        var qdg = qDonGiaDV.Where(p => p.MaDV == maDV && p.DonGiaN == DonGia).FirstOrDefault();
        //        if (qdg != null && maDV != null)
        //            return qdg.HanDung;
        //        else
        //            return null;
        //    }
        //    else
        //        return null;
        //}

        //private string GetSoLo(List<DonGiaDV> qDonGiaDV, int? maDV, double DonGia)
        //{
        //    if (qDonGiaDV.Count > 0)
        //    {
        //        var qdg = qDonGiaDV.Where(p => p.MaDV == maDV && p.DonGiaN == DonGia).FirstOrDefault();
        //        if (qdg != null && maDV != null)
        //            return qdg.SoLo;
        //        else
        //            return "";
        //    }
        //    else
        //        return null;
        //}
        int dems = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            dems++;
            if ((DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") && dems == 1)
            {
                btnOK_Click(sender, e);
                this.Close();
            }
        }
    }
}