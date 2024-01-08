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
    public partial class frm_BCXN_TheoBoPhan_01071 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCXN_TheoBoPhan_01071()
        {
            InitializeComponent();
        }
        List<TieuNhomDV> _ltn = new List<TieuNhomDV>();
        private void frm_BCXN_TheoBoPhan_01071_Load(object sender, EventArgs e)
        {
            dttungay.DateTime = System.DateTime.Today;
            DateTime tungay = dttungay.DateTime.AddHours(23).AddMinutes(59);
            dtdenngay.DateTime = tungay;
            _ltn = _data.TieuNhomDVs.ToList();
            rdNhom.SelectedIndex = 1;
        }
        class CTieuNhom
        {
            int idtieunhom;
            string tentn;
            bool chon;
            public int IDTieuNhom
            {
                get { return idtieunhom; }
                set { idtieunhom = value; }
            }
            public string TenTN
            {
                get { return tentn; }
                set { tentn = value; }
            }
            public bool Chon
            {
                get { return chon; }
                set { chon = value; }
            }
        }
        List<CTieuNhom> _lTieuNhom = new List<CTieuNhom>();
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void rdNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdNhom.SelectedIndex == 0)
            {
                _lTieuNhom.Clear();
                _lTieuNhom = (from n in _ltn.Where(p => p.IDNhom == 2)
                              select new CTieuNhom { IDTieuNhom = n.IdTieuNhom, TenTN = n.TenTN, Chon = false }).OrderBy(p => p.IDTieuNhom).ToList();
                ckcTieuNhom.DataSource = null;
                ckcTieuNhom.DataSource = _lTieuNhom;
                ckcTieuNhom.CheckAll();
            }
            else if (rdNhom.SelectedIndex == 1)
            {
                _lTieuNhom.Clear();
                _lTieuNhom = (from n in _ltn.Where(p => p.IDNhom == 1)
                              select new CTieuNhom { IDTieuNhom = n.IdTieuNhom, TenTN = n.TenTN, Chon = false }).OrderBy(p => p.IDTieuNhom).ToList();
                ckcTieuNhom.DataSource = null;
                ckcTieuNhom.DataSource = _lTieuNhom;
                ckcTieuNhom.CheckAll();
            }
            else if (rdNhom.SelectedIndex == 2)
            {
                _lTieuNhom.Clear();
                _lTieuNhom = (from n in _ltn.Where(p => p.IDNhom == 8)
                              select new CTieuNhom { IDTieuNhom = n.IdTieuNhom, TenTN = n.TenTN, Chon = false }).OrderBy(p => p.IDTieuNhom).ToList();
                ckcTieuNhom.DataSource = null;
                ckcTieuNhom.DataSource = _lTieuNhom;
                ckcTieuNhom.CheckAll();
            }
        }

        private void hyp_Chon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            ckcTieuNhom.CheckAll();
        }

        private void hyp_HuyChon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            ckcTieuNhom.UnCheckAll();
        }

        private void btnTaoBC_Click(object sender, EventArgs e)
        {
            DateTime _NgayTu = dttungay.DateTime;
            DateTime _NgayDen = dtdenngay.DateTime;
            var _lDichVu = (from n in _data.NhomDVs
                            join tn in _data.TieuNhomDVs on n.IDNhom equals tn.IDNhom
                            join dv in _data.DichVus.Where(p => p.PLoai == 2) on tn.IdTieuNhom equals dv.IdTieuNhom
                            select new { tn.IdTieuNhom, dv.MaDV, dv.TenDV, tn.TenTN }).ToList();
            foreach (var item in _lTieuNhom)
            {
                item.Chon = false;
            }
            for (int i = 0; i < ckcTieuNhom.ItemCount; i++)
            {
                if (ckcTieuNhom.GetItemChecked(i))
                {
                    int idtieunhom = Convert.ToInt32(ckcTieuNhom.GetItemValue(i));
                    foreach (var item in _lTieuNhom)
                    {
                        if (item.IDTieuNhom == idtieunhom)
                        {
                            item.Chon = true;
                            break;
                        }
                    }
                }
            }
            var _ldthuocct = (from bn in _data.BenhNhans.Where(p => rdDoiTuong.SelectedIndex != 0 ? (rdDoiTuong.SelectedIndex == 1 ? p.DTuong == "BHYT" : p.DTuong == "Dịch vụ") : true)
                              join dt in _data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                              join dtct in _data.DThuoccts.Where(p => p.NgayNhap >= _NgayTu && p.NgayNhap <= _NgayDen) on dt.IDDon equals dtct.IDDon
                              select new { dtct.IDDonct, dtct.MaDV, dtct.SoLuong }).ToList();
            var _lkq = (from dv in _lDichVu
                        join dt in _ldthuocct on dv.MaDV equals dt.MaDV
                        join tn in _lTieuNhom.Where(p => p.Chon == true) on dv.IdTieuNhom equals tn.IDTieuNhom
                        group new { dv, dt } by new { dv.TenTN, dv.IdTieuNhom, dv.MaDV, dv.TenDV } into kq
                        select new
                        {
                            kq.Key.IdTieuNhom,
                            kq.Key.TenTN,
                            kq.Key.TenDV,
                            kq.Key.MaDV,
                            SoLuong = kq.Sum(p => p.dt.SoLuong)
                        }).ToList();
            frmIn frm = new frmIn();
            BaoCao.Rep_BCXNTheoBoPhan_01071 rep = new BaoCao.Rep_BCXNTheoBoPhan_01071();
            rep.TIEUDE.Value = rdNhom.SelectedIndex == 0 ? "BÁO CÁO CHẨN ĐOÁN HÌNH ẢNH THEO BỘ PHẬN" : (rdNhom.SelectedIndex == 1 ? "BÁO CÁO XÉT NGHIỆM THEO BỘ PHẬN" : "BÁO CÁO THỦ THUẬT - PHẪU THUẬT THEO BỘ PHẬN");
            rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
            rep.TENCQ.Value = DungChung.Bien.TenCQ.ToUpper();
            rep.NGAYTHANG.Value = "Từ ngày " + _NgayTu.ToShortDateString() + " đến ngày " + _NgayDen.ToShortDateString();
            rep.DTUONG.Value = rdDoiTuong.SelectedIndex != 0 ? (rdDoiTuong.SelectedIndex == 1 ? "Đối tượng BHYT" : "Đối tượng dịch vụ") : "";
            rep.NGAYLAP.Value = DungChung.Bien.DiaDanh + ", Ngày " + System.DateTime.Today.Day + " tháng " + System.DateTime.Today.Month + " năm " + System.DateTime.Today.Year;
            rep.NGUOILAP.Value = DungChung.Bien.NguoiLapBieu;
            rep.DataSource = _lkq.OrderBy(p => p.TenDV).ToList();
            rep.Bindingdata();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}