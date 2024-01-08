using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang
{
    public partial class us_DSTamUng_TCTT : DevExpress.XtraEditors.XtraUserControl
    {
        public us_DSTamUng_TCTT()
        {
            InitializeComponent();
        }
        DateTime _dttu = DungChung.Ham.NgayTu(System.DateTime.Now);
        DateTime _dtden = DungChung.Ham.NgayDen(System.DateTime.Now);
        private void Timkiem()
        {
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
                                string _macb = lupNguoiThu.EditValue == null ? "" : lupNguoiThu.EditValue.ToString();
            int _makp = lupBPThu.EditValue == null ? 0 : Convert.ToInt32(lupBPThu.EditValue);
            var quyen = cbo_quyenTU.Text;var so = cbo_soHD_TU.Text;
            int pl = cboPLThu.SelectedIndex;
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
           var _ltamung =(from a in _data.TamUngs.Where(p => p.NgayThu <= _dtden && p.NgayThu >= _dttu)
                                    select new 
                                    {
                                        IDTamUng=a.IDTamUng,
                                        KetLuan=a.KetLuan,
                                        LyDo=a.LyDo,
                                        MaBNhan=a.MaBNhan,
                                        MaCB=a.MaCB==null?"":a.MaCB,
                                        MaKP=a.MaKP==null?0:a.MaKP,
                                        NgayThu=a.NgayThu,
                                        NgoaiGio=a.NgoaiGio,
                                        PhanLoai=a.PhanLoai,
                                        QuyenHD=a.QuyenHD==null?"":a.QuyenHD,
                                        SoHD=a.SoHD==null?"":a.SoHD,
                                        SoTien=a.SoTien,
                                        SoTo=a.SoTo,
                                        Status=a.Status,
                                        TienChenh=a.TienChenh,

                                    }
                                        ).ToList();
           var _ltamung2 = (from tu in _ltamung
                            where (tu.QuyenHD.Contains(quyen)) && (tu.SoHD.Contains(so)) && (_macb == "" ? true : tu.MaCB == _macb) && (_makp == 0 ? true : tu.MaKP == _makp)
                                            && (pl == 5 ? true : tu.PhanLoai == pl)
                                            select tu).OrderByDescending(p=>p.IDTamUng).ToList();
            grcTamUng.DataSource = null;
            grcTamUng.DataSource = _ltamung2;
        }
                            
        private void dtTimTuNgay_TextChanged(object sender, EventArgs e)
        {
            Timkiem();
        }
        class ploai
        {
            string tenPL;

            public string TenPL
            {
                get { return tenPL; }
                set { tenPL = value; }
            }
            int phanLoai;

            public int PhanLoai
            {
                get { return phanLoai; }
                set { phanLoai = value; }
            }

        }
        private void us_DSTamUng_TCTT_Load(object sender, EventArgs e)
        {
         
             QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<CanBo> _lcb=(from cb in _data.CanBoes
                            join kp in _data.KPhongs on cb.MaKP equals kp.MaKP
                            where kp.PLoai == "Kế toán"
                            select cb).OrderBy(p => p.TenCB).ToList();
            _lcb.Add(new CanBo { TenCB = "", MaCB = "" });
            lupNguoiThu.Properties.DataSource = _lcb;
            lup_CBthu_ct.DataSource = _lcb;
            List<KPhong> _lkp=(from  kp in _data.KPhongs 
                                               where kp.PLoai == "Kế toán" || kp.PLoai=="Lâm sàng" || kp.PLoai=="Phòng khám"
                                               select kp).OrderBy(p => p.TenKP).ToList();
            _lkp.Add(new KPhong { TenKP = "", MaKP = 0 });
            lupBPThu.Properties.DataSource = _lkp;
            lup_KPct.DataSource = _lkp;
            List<ploai> _lpl = new List<ploai>();
            _lpl.Add(new ploai{TenPL="Tạm thu",PhanLoai=0});
            _lpl.Add(new ploai{TenPL="Thu TT",PhanLoai=1});
            _lpl.Add(new ploai{TenPL="Chi TT",PhanLoai=2});
            _lpl.Add(new ploai{TenPL="Thu Trực Tiếp",PhanLoai=3});
            _lpl.Add(new ploai { TenPL = "Chi Tạm Thu", PhanLoai = 4 });
            lupPL.DataSource = _lpl;
            dtTimDenNgay.DateTime = System.DateTime.Now;
            dtTimTuNgay.DateTime = System.DateTime.Now;
        }

        private void dtTimDenNgay_Leave(object sender, EventArgs e)
        {
            Timkiem();
        }

        private void cboPLThu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Timkiem();
        }

        private void grvTamUng_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "XemCT") { 
            int id=0;
            if (grvTamUng.GetFocusedRowCellValue(colIDTamUng) != null)
                id = (Int32)grvTamUng.GetFocusedRowCellValue(colIDTamUng);
            frm_DSTamUng_TCTT_ct frm = new frm_DSTamUng_TCTT_ct(id);
            frm.ShowDialog();
            }
        }

        private void cbo_quyenTU_EditValueChanged(object sender, EventArgs e)
        {
            Timkiem();
        }

        private void cbo_soHD_TU_EditValueChanged(object sender, EventArgs e)
        {
            Timkiem();
        }

        private void lupBPThu_EditValueChanged(object sender, EventArgs e)
        {
            Timkiem();
        }

        private void lupNguoiThu_EditValueChanged(object sender, EventArgs e)
        {
            Timkiem();
        }
    }
}
