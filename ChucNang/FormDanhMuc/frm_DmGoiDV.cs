using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormDanhMuc
{
    public partial class frm_DmGoiDV : DevExpress.XtraEditors.XtraForm
    {
        public frm_DmGoiDV()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        List<DichVu> _ldv = new List<DichVu>();
        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }
        public class DMGOIDV
        {
            public int ID { get; set; }
            public string TrangThai { get; set; }
            public string TenGoi { get; set; }
            public string DTBNSD { get; set; }
            public double DonGia { get; set; }
        }
        List<DMGOIDV> dmgoi = new List<DMGOIDV>();
        List<TieuNhomDV> _ltn = new List<TieuNhomDV>();
        private void frm_DmGoiDV_Load(object sender, EventArgs e)
        {
            dmgoi.Clear();
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _ldv = _data.DichVus.Where(p => p.PLoai == 2).ToList();
            _ltn = _data.TieuNhomDVs.ToList();
            var _lgoidv = _data.DmGoiDVs.ToList();
            foreach (var item in _lgoidv)
            {
                DMGOIDV moi = new DMGOIDV();
                moi.ID = item.IDGoi;
                moi.TrangThai = item.Status == 0 ? "không sử dụng" : "Sử dụng";
                moi.TenGoi = item.TenGoi;
                moi.DTBNSD = item.DSDTBN;
                moi.DonGia = item.DonGia;
                dmgoi.Add(moi);
            }
            grcDSGoi.DataSource = null;
            grcDSGoi.DataSource = dmgoi.ToList();
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            QLBV.FormNhap.frm_EditGoiDV frm = new QLBV.FormNhap.frm_EditGoiDV(0, 0);
            frm.reloaddata = new QLBV.FormNhap.frm_EditGoiDV.ReLoad(LoadLaiForm);
            frm.ShowDialog();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (grvDSGoi.GetFocusedRowCellValue(colIDGoi) != null)
            {
                int idgoi = Convert.ToInt32(grvDSGoi.GetFocusedRowCellValue(colIDGoi));
                QLBV.FormNhap.frm_EditGoiDV frm = new QLBV.FormNhap.frm_EditGoiDV(idgoi, 1);
                frm.reloaddata = new QLBV.FormNhap.frm_EditGoiDV.ReLoad(LoadLaiForm);
                frm.ShowDialog();
            }
        }
        void LoadLaiForm()
        {
            frm_DmGoiDV_Load(null, null);
        }

        private void grvDSGoi_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDSGoi.GetFocusedRowCellValue(colIDGoi) != null)
            {
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int idgoi = Convert.ToInt32(grvDSGoi.GetFocusedRowCellValue(colIDGoi));
                string _idgoi = ";" + idgoi + ";";
                var _ldvgoi = (from dv in _ldv.Where(p => p.IDGoi != null && p.IDGoi.Contains(_idgoi))
                               join tn in _ltn on dv.IdTieuNhom equals tn.IdTieuNhom
                               select new { dv.TenDV, dv.MaDV, dv.DonGia, tn.TenTN }).ToList();
                grcChiTiet.DataSource = _ldvgoi.ToList();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (grvDSGoi.GetFocusedRowCellValue(colIDGoi) != null)
            {

                string tengoi = Convert.ToString(grvDSGoi.GetFocusedRowCellValue(colTenGoi));
                DialogResult Res = MessageBox.Show("Bạn muốn xóa gói dịch vụ: " + tengoi, "Hỏi xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(Res==DialogResult.OK)
                {
                      int idgoi = Convert.ToInt32(grvDSGoi.GetFocusedRowCellValue(colIDGoi));
                    _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    DmGoiDV xoa = _data.DmGoiDVs.Where(p => p.IDGoi == idgoi).FirstOrDefault();
                    if(xoa!=null)
                    {
                        _data.DmGoiDVs.Remove(xoa);
                        int a = _data.SaveChanges();
                        if (a >= 0)
                        {
                            string goi = ";" + "idgoi" + ";";
                            List<DichVu> ldv = _data.DichVus.Where(p => p.IDGoi != null && p.IDGoi.Contains(goi)).ToList();
                            foreach (var item in ldv)
                            {
                                item.IDGoi = item.IDGoi.Replace(goi, ";");
                            }
                            _data.SaveChanges();
                        }
                        frm_DmGoiDV_Load(null, null);
                    }
                }
            }
        }
    }
}