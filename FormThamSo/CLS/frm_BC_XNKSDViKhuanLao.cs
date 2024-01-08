using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_XNKSDViKhuanLao : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_XNKSDViKhuanLao()
        {
            InitializeComponent();
        }

        private QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void frm_BC_XNKSDViKhuanLao_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool KTtaoBc()
        {

            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            if (DungChung.Ham.NgayDen(dateDenNgay.DateTime) < DungChung.Ham.NgayTu(dateTuNgay.DateTime))
            {
                MessageBox.Show("Chọn lại ngày của Thời gian TH đến.");
                dateDenNgay.Focus();
                return false;
            }
            else return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (KTtaoBc())
            {
                List<Content> _lContent = new List<Content>();
                Content moi = new Content();
                DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);

                var qCLS = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                            join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                            join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join cb in data.CanBoes on cls.MaCBth equals cb.MaCB into kq
                            from kq1 in kq.DefaultIfEmpty()
                            select new
                            {
                                cls.IdCLS,
                                cls.MaBNhan,
                                cls.MaKP,
                                cls.MaKPth,
                                cls.NgayTH,
                                cls.GhiChu,
                                dvct.STT,
                                cls.NgayKQ,
                                clsct.Id,
                                clsct.KetQua,
                                tn.TenRG,
                                dv.SoTT,
                                TenCB = (cls.MaCBth != null || cls.MaCBth != "") ? kq1.TenCB : "",
                                dvct.TenDVct
                            }).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom)
                              .Where(p => p.SoTT == 7 || p.SoTT == 8 || p.SoTT == 9 || p.SoTT == 10).ToList();

                var qBN = (from bn in data.BenhNhans
                           join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                           select new
                           {
                               bn.MaBNhan,
                               bn.TenBNhan,
                               bn.Tuoi,
                               bn.GTinh,
                               bn.DChi,
                               ttbx.ThangTheoDoi,
                               ttbx.ChanDoanLao,
                               ttbx.DTuongLao
                           }).ToList();

                var query = (from a in qCLS
                             join b in qBN on a.MaBNhan equals b.MaBNhan

                             select new
                             {
                                 a.IdCLS,
                                 a.MaBNhan,
                                 a.MaKP,
                                 a.MaKPth,
                                 NgayTH = String.Format("{0:dd/MM/yyyy}", a.NgayTH),
                                 a.SoTT,
                                 a.Id,
                                 a.KetQua,
                                 a.TenCB,
                                 a.GhiChu,
                                 NgayKQ = String.Format("{0:dd/MM/yyyy}", a.NgayKQ),
                                 a.TenDVct,
                                 a.STT,
                                 b.TenBNhan,
                                 b.Tuoi,
                                 b.GTinh,
                                 b.DChi
                             }).OrderBy(p => p.SoTT).ToList();

                foreach (var item in query)
                {
                    moi = new Content();
                    moi.NgayTH = item.NgayTH;
                    moi.SoKSD = item.Id.ToString();
                    moi.TenBNhan = item.TenBNhan;
                    moi.Tuoi = Convert.ToInt32(item.Tuoi);
                    moi.DiaChi = item.DChi;
                    moi.H = (item.KetQua != null && item.TenDVct.Contains("INH")) ? (item.KetQua.Contains("Nhạy")) ? "S" : (item.KetQua.Contains("Kháng") ? "R" : "") : "";
                    moi.R = (item.KetQua != null && item.TenDVct.Contains("RMP")) ? (item.KetQua.Contains("Nhạy")) ? "S" : (item.KetQua.Contains("Kháng") ? "R" : "") : "";
                    moi.S = (item.KetQua != null && item.TenDVct.Contains("SM")) ? (item.KetQua.Contains("Nhạy")) ? "S" : (item.KetQua.Contains("Kháng") ? "R" : "") : "";
                    moi.E = (item.KetQua != null && item.TenDVct.Contains("EMB")) ? (item.KetQua.Contains("Nhạy")) ? "S" : (item.KetQua.Contains("Kháng") ? "R" : "") : "";
                    moi.PZA = (item.KetQua != null && item.TenDVct.Contains("PZA")) ? (item.KetQua.Contains("Nhạy")) ? "S" : (item.KetQua.Contains("Kháng") ? "R" : "") : "";
                    moi.AK = (item.KetQua != null && item.TenDVct.Contains("AK")) ? (item.KetQua.Contains("Nhạy")) ? "S" : (item.KetQua.Contains("Kháng") ? "R" : "") : "";
                    moi.OF = (item.KetQua != null && item.TenDVct.Contains("OF")) ? (item.KetQua.Contains("Nhạy")) ? "S" : (item.KetQua.Contains("Kháng") ? "R" : "") : "";
                    moi.KM = (item.KetQua != null && item.TenDVct.Contains("KM")) ? (item.KetQua.Contains("Nhạy")) ? "S" : (item.KetQua.Contains("Kháng") ? "R" : "") : "";
                    moi.CAP = (item.KetQua != null && item.TenDVct.Contains("CAP")) ? (item.KetQua.Contains("Nhạy")) ? "S" : (item.KetQua.Contains("Kháng") ? "R" : "") : "";
                    moi.NgayKQ = item.NgayKQ;
                    moi.NguoiLamXN = item.TenCB;
                    moi.GhiChu = item.GhiChu;
                    _lContent.Add(moi);
                }
                BaoCao.Rep_BC_XNKSDViKhuanLao rep = new BaoCao.Rep_BC_XNKSDViKhuanLao();
                frmIn frm = new frmIn();
                rep.lblThoiGian.Text = "(Từ ngày " + dateTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + dateDenNgay.DateTime.ToString("dd/MM/yyyy") + ")";
                rep.DataSource = _lContent.OrderBy(p => p.NgayTH);
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        public class Content
        {
            public string NgayTH { get; set; }
            public string SoKSD { get; set; }
            public string SoCay { get; set; }// số cấy
            public string TenBNhan { get; set; }
            public int Tuoi { get; set; }
            public string DiaChi { get; set; }
            public string H { get; set; }//H: Isoniazid
            public string R { get; set; }//R: Rifampicin
            public string S { get; set; }//S: Streptomycin
            public string E { get; set; }//E: Ethambutol
            public string PZA { get; set; }//PZA: Pyrazinamid
            public string AK { get; set; }//AK: Amikacin
            public string OF { get; set; }//OF: Ofloxacin
            public string KM { get; set; }//KM: Kanamycin
            public string CAP { get; set; }//CAP: Capreomycin
            public string NgayKQ { get; set; }
            public string NguoiLamXN { get; set; }
            public string GhiChu { get; set; }
        }
    }
}