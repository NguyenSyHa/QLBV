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
    public partial class frmNhapNgaySaoDienBien : Form
    {
        int id;
        Action action;
        //1: Diễn biến 2: Chăm sóc
        int loai;
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DienBien db;
        DateTime ngayKB;
        public frmNhapNgaySaoDienBien(int _id, Action _action, int _loai, DateTime _ngayKB)
        {
            InitializeComponent();
            this.id = _id;
            this.action = _action;
            this.loai = _loai;
            this.ngayKB = _ngayKB;
        }

        private void frmNhapNgaySaoDienBien_Load(object sender, EventArgs e)
        {
            db = dataContext.DienBiens.FirstOrDefault(o => o.ID == id);
            if (db != null)
                dtNgaySao.DateTime = db.NgayNhap.Value;
            else
                dtNgaySao.DateTime = DateTime.Now;
            if (loai == 1)
            {
                this.Text = "Sao diễn biến";
                this.label1.Text = "Ngày sao diễn biến:";
            }
            else
            {
                this.Text = "Sao chăm sóc";
                this.label1.Text = "Ngày sao chăm sóc:";
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtNgaySao.EditValue == null)
                {
                    MessageBox.Show("Ngày sao không được để trống!", "Thông báo");
                    return;
                }

                if (db == null)
                {
                    MessageBox.Show(loai == 1 ? "Không tìm thấy diễn biến" : "Không tìm thấy chăm sóc", "Thông báo");
                    return;
                }
                if (dtNgaySao.DateTime < ngayKB)
                {
                    MessageBox.Show(string.Format("Ngày sao không được nhỏ hơn ngày khám {0}!", ngayKB.ToString("dd/MM/yyyy HH:mm:ss")), "Thông báo");
                    return;
                }
                DienBien dienbien = new DienBien();
                LibraryStore.Mapper.DataObjectMapper.Map<DienBien>(dienbien, db);
                dienbien.ID = 0;
                dienbien.NgayNhap = dtNgaySao.DateTime;
                dataContext.DienBiens.Add(dienbien);
                dataContext.SaveChanges();

                if (loai == 1)
                {
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

                                            var tungay = DungChung.Ham.NgayTu(dtNgaySao.DateTime);
                                            var denngay = DungChung.Ham.NgayDen(dtNgaySao.DateTime);
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
                                            dthuocCt.NgayNhap = DateTime.Now;
                                            dataContext.DThuoccts.Add(dthuocCt);
                                            dataContext.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                        if (message != null && message.Count > 0)
                        {
                            string show = "";
                            foreach (var item in message)
                            {
                                var dv = dataContext.DichVus.FirstOrDefault(o => o.MaDV == item);
                                if (dv != null)
                                {
                                    show += dv.TenDV + "(" + item + "), ";
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(show))
                            {
                                MessageBox.Show(string.Format("Thuốc/vật tư: {0} không đủ số lượng để xuất", show), "Thông báo");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Sao đơn thành công!", "Thông báo");
                        }
                    }
                }

                MessageBox.Show(loai == 1 ? "Sao diễn biến thành công!" : "Sao chăm sóc thành công!", "Thông báo");
                if (action != null)
                    action();
                this.Close();
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }
    }
}
