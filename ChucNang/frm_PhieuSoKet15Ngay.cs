using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang
{
    public partial class frm_PhieuSoKet15Ngay : DevExpress.XtraEditors.XtraForm
    {
        //public delegate void PassData(string dienbien, string xetnghiem, string qtrinhDT, string ketqua, string huongDtri);
        //public PassData passData;
        public frm_PhieuSoKet15Ngay()
        {
            InitializeComponent();
        }
        int IdDienBien = 0, maBNhan = 0;
        int soNgayDT;
        string _BSDT;
        public frm_PhieuSoKet15Ngay(int IdDienBien)
        {
            InitializeComponent();
            this.IdDienBien = IdDienBien;
        }
        public frm_PhieuSoKet15Ngay(int IdDienBien, int maBNhan, int SoNgayDT, string BSDT)
        {
            InitializeComponent();
            this.IdDienBien = IdDienBien;
            this.maBNhan = maBNhan;
            this.Text = "Phiếu sơ kết " + SoNgayDT + " ngày điều trị";
            _BSDT = BSDT;
            soNgayDT = SoNgayDT;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            //if (passData != null)
            //{
            //    this.Close();
            //    passData(txtDienBien.Text, txtXNCLS.Text, txtQuaTrinhDtri.Text, txtKetQua.Text, txtHuongDtri.Text);
            //}
            DateTime NgayThang = DateTime.Now;
            if(!string.IsNullOrEmpty(dedenngay.Text))
                NgayThang = dedenngay.DateTime;
            DungChung.Ham.PhieuSoKet(maBNhan, txtDienBien.Text, txtXNCLS.Text, txtQuaTrinhDtri.Text, txtKetQua.Text, txtHuongDtri.Text, _BSDT, soNgayDT, NgayThang);
        }

        /// <summary>
        /// trạng thái form : 0: Mặc định; 1: Thêm mới diễn biến; 2: sửa diễn biến
        /// </summary>
        int trangthai = 0;

        void laykqxn()
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime _tungay = DungChung.Ham.NgayTu(detungay.DateTime).AddDays(-1);
            DateTime _denngay = DungChung.Ham.NgayDen(dedenngay.DateTime);

            if (DungChung.Bien.MaBV == "20001")
            {
                string kq = "";
                var xetnghiem = (from cls in data.CLS.Where(p => p.MaBNhan == maBNhan).Where(p => p.NgayThang >= _tungay && p.NgayThang <= _denngay)
                                 join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                 join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                 join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                                 join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 1) on dv.IdTieuNhom equals tn.IdTieuNhom //p.TenRG.Contains("XN")
                                 select new
                                 {
                                     dv.TenDV,
                                     dv.MaDV,
                                     dvct.TenDVct,
                                     TenDVctkq = (dvct.TenDVct == null ? "" : dvct.TenDVct) + ": " + (clsct.KetQua == null ? "" : clsct.KetQua) + " " + (dvct.DonVi == null ? "" : dvct.DonVi),
                                     cls.IdCLS
                                 }).OrderBy(p => p.IdCLS).ToList();
                var cdha = (from cls in data.CLS.Where(p => p.MaBNhan == maBNhan).Where(p => p.NgayThang >= _tungay && p.NgayThang <= _denngay)
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                            join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                            join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                            join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 2) on dv.IdTieuNhom equals tn.IdTieuNhom //p.TenRG.Contains("XN")
                            select new
                            {
                                dv.TenDV,
                                dv.MaDV,
                                dvct.TenDVct,
                                TenDVctkq = (clsct.KetQua == null ? "" : clsct.KetQua) + " " + (dvct.DonVi == null ? "" : dvct.DonVi),
                                cls.IdCLS
                            }).OrderBy(p => p.IdCLS).ToList();
                if (xetnghiem.Count > 0)
                {
                    var qxn = (from xn in xetnghiem group xn by new { xn.MaDV, xn.TenDV } into kq1 select new { TenDV = "  - " + kq1.Key.TenDV + ": " + string.Join("; ", kq1.Where(p => p.TenDVctkq != "").Select(p => p.TenDVctkq).ToArray()) }).ToList();
                    // string[] arr = xetnghiem.Select(p => p.TenDVctkq).ToArray();
                    string kqxetnghiem = string.Join(Environment.NewLine, qxn.Select(p => p.TenDV).ToArray());
                    kq += "+ Xét nghiệm: " + Environment.NewLine + kqxetnghiem + Environment.NewLine;
                }
                else
                {
                    kq = "+ Xét nghiệm:" + Environment.NewLine;
                }
                if (cdha.Count > 0)
                {
                    var qcdha = (from cd in cdha group cd by new { cd.MaDV, cd.TenDV } into kq1 select new { TenDV = "  - " + kq1.Key.TenDV + ": " + string.Join("; ", kq1.Where(p => p.TenDVctkq != "").Select(p => p.TenDVctkq).ToArray()) }).ToList();
                    //  string[] arr = cdha.Select(p => p.TenDVctkq).ToArray();
                    string kqcdha = string.Join(Environment.NewLine, qcdha.Select(p => p.TenDV).ToArray());//string.Join(";", arr);
                    kq += "+ Chẩn đoán hình ảnh: " + Environment.NewLine + kqcdha;
                }
                else
                    kq += "+ Chẩn đoán hình ảnh:";
                txtXNCLS.Text = kq;

            }
            else
            {
                var xetnghiem = (from cls in data.CLS.Where(p => p.MaBNhan == maBNhan).Where(p => p.NgayThang >= _tungay && p.NgayThang <= _denngay).Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay).Where(p => p.Status == 1)
                                 join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                 join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                 join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                                 join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 1) on dv.IdTieuNhom equals tn.IdTieuNhom
                                 select new
                                 {
                                     dv.TenDV,
                                     dv.MaDV,
                                     dvct.TenDVct,
                                     TenDVctkq = (dvct.TenDVct == null ? "" : dvct.TenDVct) + ": " + (clsct.KetQua == null ? "" : clsct.KetQua) + " " + (dvct.DonVi == null ? "" : dvct.DonVi),
                                     cls.IdCLS,
                                     cls.Status
                                 }).OrderBy(p => p.IdCLS).ToList();


                if (xetnghiem.Count > 0)
                {
                    string[] arr = xetnghiem.Select(p => p.TenDVctkq).ToArray();
                    string kqxetnghiem = string.Join(";\n", arr);
                    txtXNCLS.Text = kqxetnghiem;


                }
                else
                {
                    txtXNCLS.Text = "";
                }
            }


        }
        private void frm_PhieuSoKet15Ngay_Load(object sender, EventArgs e)
        {
            detungay.DateTime = DateTime.Now;
            dedenngay.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (IdDienBien > 0)
            {

                var qdb = data.DienBiens.Where(p => p.ID == IdDienBien).FirstOrDefault();
                if (qdb != null)
                {
                    txtDienBien.Text = qdb.DienBien1;
                    txtXNCLS.Text = qdb.YLenh;
                    txtQuaTrinhDtri.Text = qdb.ThucHienYL;
                    txtKetQua.Text = qdb.KetQua;
                    txtHuongDtri.Text = qdb.HuongDTri;

                }
                trangthai = 2;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
            }
            else
            {
                laykqxn();
                EnableControl(false);
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
            }

            if (DungChung.Bien.MaBV == "24012") // viện 24012 chỉ nhập các trường rồi in lên BC luôn, ko lưu DB
            {
                btnLuu.Visible = false;
                btnSua.Visible = false;
                btnNew_Click(null, null);

            }
        }

        private void EnableControl(bool enable)
        {
            txtDienBien.Enabled = enable;
            txtXNCLS.Enabled = enable;
            txtQuaTrinhDtri.Enabled = enable;
            txtKetQua.Enabled = enable;
            txtHuongDtri.Enabled = enable;
            detungay.Visible = enable;
            dedenngay.Visible = enable;
            labelControl6.Visible = enable;
            labelControl7.Visible = enable;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (trangthai == 1)
            {
                DienBien moi = new DienBien();
                moi.DienBien1 = txtDienBien.Text;
                moi.YLenh = txtXNCLS.Text;
                moi.ThucHienYL = txtQuaTrinhDtri.Text;
                moi.KetQua = txtKetQua.Text;
                moi.HuongDTri = txtHuongDtri.Text;
                moi.NgayNhap = DateTime.Now; //ngày tháng ????
                moi.MaCB = DungChung.Bien.MaCB;
                moi.Ploai = 2;
                moi.MaBNhan = maBNhan;
                data.DienBiens.Add(moi);
                data.SaveChanges();
                IdDienBien = moi.ID;
            }
            else if (trangthai == 2)
            {
                var dienbien = data.DienBiens.Where(p => p.ID == IdDienBien).FirstOrDefault();
                if (dienbien != null)
                {
                    dienbien.DienBien1 = txtDienBien.Text;
                    dienbien.YLenh = txtXNCLS.Text;
                    dienbien.ThucHienYL = txtQuaTrinhDtri.Text;
                    dienbien.KetQua = txtKetQua.Text;
                    dienbien.HuongDTri = txtHuongDtri.Text;
                    dienbien.Ploai = 2;
                    data.SaveChanges();
                }
            }
            EnableControl(false);
            btnLuu.Enabled = false;
            if (IdDienBien > 0)
                btnSua.Enabled = true;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            EnableControl(true);
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            txtDienBien.Text = "";
            txtXNCLS.Text = "";
            txtQuaTrinhDtri.Text = "";
            txtKetQua.Text = "";
            txtHuongDtri.Text = "";
            IdDienBien = 0;
            trangthai = 1;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            trangthai = 2;
            EnableControl(true);
            btnLuu.Enabled = true;
        }

        private void detungay_EditValueChanged(object sender, EventArgs e)
        {
            laykqxn();

        }

        private void txtXNCLS_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dedenngay_EditValueChanged(object sender, EventArgs e)
        {
            laykqxn();
        }
    }
}