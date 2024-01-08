using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frmSaoNhieuNgayDienBien : Form
    {
        int id;
        Action action;
        //1: Diễn biến 2: Chăm sóc
        int loai;
        int KieuSao = 1; //Sao không cần dịch vụ 0: không cần , 1: Cần
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DateTime ngayKB;
        public frmSaoNhieuNgayDienBien(int _id, Action _action, int _loai, DateTime _ngayKB)
        {
            InitializeComponent();
            this.id = _id;
            this.action = _action;
            this.loai = _loai;
            this.ngayKB = _ngayKB;
        }
        public frmSaoNhieuNgayDienBien(int _id, Action _action, int _loai, DateTime _ngayKB, int _KieuSao)
        {
            InitializeComponent();
            this.id = _id;
            this.action = _action;
            this.loai = _loai;
            this.ngayKB = _ngayKB;
            this.KieuSao = _KieuSao;
        }
        private void frmNhapNgaySaoDienBien_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DateTime.Now.Date;
            dtDenNgay.DateTime = DateTime.Now.Date;
            if (loai == 1)
                this.Text = "Sao diễn biến";
            else
                this.Text = "Sao chăm sóc";
        }

        List<QLBV.FormNhap.frm_ChonDichVuSaoDienBien.ChonDichVu> listDichVus;
        public void DlgChonDichVu(List<QLBV.FormNhap.frm_ChonDichVuSaoDienBien.ChonDichVu> data)
        {
            listDichVus = data;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            listDichVus = null;
            if (dtTuNgay.EditValue == null || dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Ngày sao không được để trống!", "Thông báo");
                return;
            }
            if (dtTuNgay.DateTime.Date > dtDenNgay.DateTime.Date)
            {
                MessageBox.Show("Ngày từ không được lớn hơn ngày đến!", "Thông báo");
                return;
            }
            if (dtTuNgay.DateTime < ngayKB.Date)
            {
                MessageBox.Show(string.Format("Ngày từ không được nhỏ hơn ngày khám {0}!", ngayKB.Date.ToString("dd/MM/yyyy")), "Thông báo");
                return;
            }
            var db = dataContext.DienBiens.FirstOrDefault(o => o.ID == id);
            if (db == null)
            {
                MessageBox.Show(loai == 1 ? "Không tìm thấy diễn biến" : "Không tìm thấy chăm sóc", "Thông báo");
                return;
            }

            var saoTungay = dtTuNgay.DateTime.Date;
            var saoDenngay = dtDenNgay.DateTime.Date;
            TimeSpan span = saoDenngay - saoTungay;
            var solan = span.Days + 1;

            if ((DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") && loai == 1 && KieuSao == 0)
            {
                frm_ChonDichVuSaoDienBien frm = new frm_ChonDichVuSaoDienBien(id, DlgChonDichVu, solan);
                frm.ShowDialog();
                if (listDichVus == null)
                {
                    MessageBox.Show("Không sao được diễn biến");
                    return;
                }
            }

            Dictionary<int, int> dic = new Dictionary<int, int>();
            List<DichVu> listMessage = new List<DichVu>();

            while (saoTungay.Date <= saoDenngay.Date)
            {
                var ngaySao = saoTungay.Date.AddHours(db.NgayNhap.Value.Hour).AddMinutes(db.NgayNhap.Value.Minute).AddSeconds(db.NgayNhap.Value.Second).AddMilliseconds(db.NgayNhap.Value.Millisecond);
                DienBien dienbien = new DienBien();
                LibraryStore.Mapper.DataObjectMapper.Map<DienBien>(dienbien, db);
                dienbien.ID = 0;
                dienbien.NgayNhap = ngaySao;
                dataContext.DienBiens.Add(dienbien);
                dataContext.SaveChanges();

                if (loai == 1)
                {
                    #region Sao đơn thuốc
                    var dthuocs = dataContext.DThuocs.Where(o => o.IDDienBien == id).ToList();
                    if (dthuocs != null && dthuocs.Count > 0)
                    {
                        List<int> message = new List<int>();
                        foreach (var dt in dthuocs)
                        {
                            if (dt.KieuDon == 1 || dt.KieuDon == 0)
                            {
                                DThuoc dthuoc = new DThuoc();
                                LibraryStore.Mapper.DataObjectMapper.Map<DThuoc>(dthuoc, dt);
                                dthuoc.IDDon = 0;
                                dthuoc.IDDienBien = dienbien.ID;
                                dthuoc.NgayKe = dienbien.NgayNhap;
                                dataContext.DThuocs.Add(dthuoc);
                                dataContext.SaveChanges();

                                var dthuocCts = dataContext.DThuoccts.Where(o => o.IDDon == dt.IDDon).ToList();
                                if (dthuocCts != null && dthuocCts.Count > 0)
                                {
                                    foreach (var dtct in dthuocCts)
                                    {
                                        double _soluongton = 0;
                                        double _soluongkt = dtct.SoLuong;
                                        if (dt.KieuDon != 2)
                                        {
                                            _soluongton = DungChung.Ham._checkTon_KD(dataContext, dtct.MaDV ?? 0, dtct.MaKXuat ?? 0, dtct.DonGia, 0, dtct.SoLo);
                                        }
                                        else
                                        {

                                            var tungay = DungChung.Ham.NgayTu(saoTungay);
                                            var denngay = DungChung.Ham.NgayDen(saoTungay);
                                            var duoc = (from nduoc in dataContext.DThuocs.Where(p => p.MaBNhan == db.MaBNhan && p.MaKXuat == dtct.MaKXuat).Where(p => p.MaCB == dt.MaCB)//&& ((ktratt && p.Status == 0) || (p.KieuDon == 2 ? true : p.Status == 1)))//  )
                                                        join nhapduoc in dataContext.DThuoccts.Where(p => p.MaDV == dtct.MaDV && p.DonGia == dtct.DonGia).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on nduoc.IDDon equals nhapduoc.IDDon
                                                        where ((nhapduoc.Status == 0) || (nduoc.KieuDon == 2 ? true : nhapduoc.Status == 1))//  )
                                                        group new { nhapduoc, nduoc } by new { nhapduoc.MaDV } into kq
                                                        select new { kq.Key, soluong = kq.Sum(p => p.nhapduoc.SoLuong) }).ToList();
                                            _soluongton = duoc.Sum(p => p.soluong);
                                            _soluongkt = _soluongkt * (-1);
                                        }
                                        if (_soluongkt > _soluongton)
                                        {
                                            message.Add(dtct.MaDV ?? 0);
                                        }
                                        else
                                        {
                                            DThuocct dthuocCt = new DThuocct();
                                            LibraryStore.Mapper.DataObjectMapper.Map<DThuocct>(dthuocCt, dtct);
                                            dthuocCt.IDDonct = 0;
                                            dthuocCt.IDDon = dthuoc.IDDon;
                                            dthuocCt.Status = 0;
                                            dthuocCt.SoPL = 0;
                                            dthuocCt.NgayNhap = dienbien.NgayNhap;
                                            dataContext.DThuoccts.Add(dthuocCt);
                                            dataContext.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                        if (message != null && message.Count > 0)
                        {
                            foreach (var item in message)
                            {
                                var dv = dataContext.DichVus.FirstOrDefault(o => o.MaDV == item);
                                if (dv != null)
                                {
                                    listMessage.Add(dv);
                                }
                            }
                        }
                    }
                    #endregion

                    #region Sao CLS

                    if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                    {
                        if (listDichVus != null)
                        {
                            foreach (var item in listDichVus)
                            {
                                if (dic.ContainsKey(item.MaDV) && (dic[item.MaDV] >= item.SoLan || dic[item.MaDV] >= solan))
                                    continue;
                                CL cls = dataContext.CLS.FirstOrDefault(o => o.IdCLS == item.IDCLS);
                                if (cls != null)
                                {
                                    var ngayCD = saoTungay.Date.AddHours(7).AddMinutes(50);
                                    CL cl = new CL();
                                    LibraryStore.Mapper.DataObjectMapper.Map<CL>(cl, cls);
                                    cl.IdCLS = 0;
                                    cl.IDDienBien = dienbien.ID;
                                    cl.NgayThang = ngayCD;
                                    cl.Status = 0;
                                    cl.MaCBth = null;
                                    cl.NgayTH = null;
                                    dataContext.CLS.Add(cl);
                                    if (dataContext.SaveChanges() > 0)
                                    {
                                        ChiDinh chidinh = dataContext.ChiDinhs.FirstOrDefault(o => o.IDCD == item.IDCD);
                                        if (chidinh != null)
                                        {
                                            ChiDinh cd = new ChiDinh();
                                            LibraryStore.Mapper.DataObjectMapper.Map<ChiDinh>(cd, chidinh);
                                            cd.IDCD = 0;
                                            cd.IdCLS = cl.IdCLS;
                                            cd.NgayTH = null;
                                            cd.MaCBth = null;
                                            cd.DSCBTH = null;
                                            cd.Status = 0;
                                            dataContext.ChiDinhs.Add(cd);
                                            if (dataContext.SaveChanges() > 0)
                                            {
                                                var dvct = dataContext.DichVucts.Where(o => o.MaDV == cd.MaDV).ToList();
                                                if (dvct != null && dvct.Count > 0)
                                                {
                                                    foreach (var ct in dvct)
                                                    {
                                                        CLSct themmoiCL = new CLSct();
                                                        themmoiCL.IDCD = cd.IDCD;
                                                        themmoiCL.MaDVct = ct.MaDVct;
                                                        themmoiCL.Status = 0;
                                                        themmoiCL.STTHT = ct.STT;
                                                        dataContext.CLScts.Add(themmoiCL);
                                                        dataContext.SaveChanges();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (dic.ContainsKey(item.MaDV))
                                {
                                    dic[item.MaDV] = dic[item.MaDV] + 1;
                                }
                                else
                                {
                                    dic.Add(item.MaDV, 1);
                                }
                            }
                        }
                    }

                    #endregion

                    usDieuTri_34019 us = new usDieuTri_34019();
                    us.LoadYLenh(dienbien.ID);
                }
                saoTungay = saoTungay.AddDays(1);
            }

            if (listMessage.Count > 0)
            {
                var message = (from m in listMessage
                               select new { TenDV = (m.TenDV + "(" + m.MaDV + ")") }).Distinct().ToList();
                MessageBox.Show(string.Format("Thuốc/vật tư: {0} không đủ số lượng để xuất", string.Join(", ", message)), "Thông báo");
            }

            MessageBox.Show(loai == 1 ? "Sao diễn biến thành công!" : "Sao chăm sóc thành công!", "Thông báo");
            if (action != null)
                action();
            this.Close();
        }
    }
}
