using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.CLS
{
    public partial class frm_ChiDinhGoiDV : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0;
        public frm_ChiDinhGoiDV(int mabn)
        {
            InitializeComponent();
            _mabn = mabn;
        }
        public delegate void _DvChon(List<QLBV.FormThamSo.FRM_chidinh_Moi.C_DichVu> a, DateTime NgayCD, int MaKPCD, string MaCBCD);
        public _DvChon ChonGoi;
        QLBV_Database.QLBVEntities _data;
        List<QLBV.FormThamSo.FRM_chidinh_Moi.C_DichVu> _lkq = new List<FormThamSo.FRM_chidinh_Moi.C_DichVu>();
        public class DichVuChon
        {
            public int MaDV { get; set; }
            public int IdGoi { get; set; }
            public string TenDV { get; set; }
            public double DonGia { get; set; }
            public int idTieuNhom { get; set; }
            public int IDNhom { get; set; }
            public string Ylenh { get; set; }
            public int TrongBH { get; set; }
            public bool Chon { get; set; }
            public string MaQD { get; set; }

        }
        string DTBN = "";
        private void frm_ChiDinhGoiDV_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kp = _data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám" || ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") ? p.PLoai == "Hành chính" : true)).Distinct().OrderBy(p => p.TenKP).ToList();
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupKPhong.Properties.DataSource = kp;
            }
            else
            {
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                {
                    var kpkhamdt = (from b in kp
                                    join c in DungChung.Bien.listKPHoatDong on b.MaKP equals c
                                    select b).ToList();
                    lupKPhong.Properties.DataSource = kpkhamdt;
                }
                else
                {
                    var kpkhamdt = (from b in kp
                                    join c in DungChung.Bien.listKPHoatDong on b.MaKP equals c
                                    select b).ToList();
                    lupKPhong.Properties.DataSource = kpkhamdt;
                }
            }
            deNgayCD.DateTime = DateTime.Now;
            var CB = _data.CanBoes.Where(p => (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") ? (p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")) : true).Distinct().OrderBy(p => p.TenCB).ToList();
            //if (DungChung.Bien.CapDo == 9 || DungChung.Bien.CapDo == 8)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                lupCanBo.Properties.DataSource = CB;
            else
                lupCanBo.Properties.DataSource = CB.Where(p => p.MaKP == DungChung.Bien.MaKP).ToList();
            BenhNhan ttbn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            if(ttbn!=null)
            {
                DTBN = ";" + ttbn.IDDTBN + ";";
            }
            var _lGoiDV = _data.DmGoiDVs.Where(p => p.DSDTBN != null && p.DSDTBN.Contains(DTBN)).ToList();
            grcGoiDV.DataSource = null;
            grcGoiDV.DataSource = _lGoiDV;
        }
        void _loadgoidv()
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int IDGoi = 0;
            string TenGoi = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                try
                {
                    IDGoi = Convert.ToInt32(txtTimKiem.Text);
                }
                catch
                {
                    TenGoi = txtTimKiem.Text.ToLower();
                }
            }
            var _lGoiDV = _data.DmGoiDVs.Where(p => IDGoi > 0 ? p.IDGoi == IDGoi : (TenGoi != "" ? p.TenGoi.ToLower().Contains(TenGoi) : true)).Where(p => p.DSDTBN != null && p.DSDTBN.Contains(DTBN)).ToList();
            grcGoiDV.DataSource = null;
            grcGoiDV.DataSource = _lGoiDV;
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            _loadgoidv();
        }
        List<DichVuChon> _ldvchon = new List<DichVuChon>();
        private void grvGoiDV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvGoiDV.GetFocusedRowCellValue(colIdGoi) != null)
            {
                _ldvchon.Clear();
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int TrongDM = 0;
                TrongDM = Convert.ToInt32(grvGoiDV.GetFocusedRowCellValue(colTrongDM));
                int IdGoi = Convert.ToInt32(grvGoiDV.GetFocusedRowCellValue(colIdGoi));
                string _idgoi = ";" + IdGoi + ";";
                var _ldvct = (from dv in _data.DichVus.Where(p => p.IDGoi != null && p.IDGoi.Contains(_idgoi)).Where(p => p.Status == 1)
                              join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                              select new { dv.MaDV, dv.TenDV, dv.IdTieuNhom, dv.DonGia, dv.DonGia2, dv.DonGiaBHYT, dv.DonGiaDV2, dv.DonVi, dv.MaQD, tn.IDNhom }).ToList();
                int GiaCu = DungChung.Ham.GiaCu(_mabn, TrongDM);
                foreach (var item in _ldvct)
                {
                    DichVuChon moi = new DichVuChon();
                    moi.IdGoi = IdGoi;
                    moi.MaDV = item.MaDV;
                    moi.TenDV = item.TenDV;
                    if (TrongDM == 0)
                        moi.DonGia = GiaCu == 0 ? item.DonGia2 : item.DonGiaDV2;
                    else
                        //moi.DonGia = GiaCu == true ? item.DonGia : item.DonGiaBHYT;
                        moi.DonGia = GiaCu == 0 ? item.DonGia : item.DonGiaBHYT;
                    moi.IDNhom = item.IDNhom.Value;
                    moi.idTieuNhom = item.IdTieuNhom.Value;
                    moi.TrongBH = TrongDM;
                    moi.MaQD = item.MaQD;
                    moi.Chon = true;
                    _ldvchon.Add(moi);
                }
                grcChiTietGoi.DataSource = null;
                grcChiTietGoi.DataSource = _ldvchon.ToList();
            }
        }

        private void lupKPhong_EditValueChanged(object sender, EventArgs e)
        {
            int makp = 0; string _makp = "";
            if (lupKPhong.EditValue != null)
            {
                makp = Convert.ToInt32(lupKPhong.EditValue);
                _makp = ";" + makp.ToString() + ";";
            }
            var _lCanBo = _data.CanBoes.Where(p => p.MaKPsd.Contains(_makp)).Where(p => p.Status == 1).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")).ToList();
            if (_lCanBo.Count > 0)
                lupCanBo.Properties.DataSource = _lCanBo;
        }

        private void btnok_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}