using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.FormDanhMuc
{
    public partial class frm_DM5084 : DevExpress.XtraEditors.XtraForm
    {
        public frm_DM5084()
        {
            InitializeComponent();
        }
        bool _duoc = true;
        public frm_DM5084(bool d)
        {
            InitializeComponent();
            _duoc = d;
        }
        public delegate void _setValue(string tendv, string maqd, int sott, string tenhc, string sodk, string nuocsx, string duongdung, int trongdm, string hamluong, string baoche, string qcpc, string tchuan, string tuoitho, string nhasx, string nguongoc);
        public _setValue Getdata;
        List<C5084> _l5084 = new List<C5084>();
        private void frm_DM5084_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _l5084 = _data.C5084.Where(p => p.Bang != 5).OrderBy(p => p.TenDV).ToList();
            grc_CV.DataSource = _l5084;
        }

        private void btn_Chon_Click(object sender, EventArgs e)
        {
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<DonVi> _ldonvisx=_data.DonVis.ToList();
            string SoDangKy = "";
            if (grv_CV.GetFocusedRowCellValue("MaDV") != null)
                SoDangKy = grv_CV.GetFocusedRowCellValue("MaDV").ToString();
            if (!string.IsNullOrEmpty(SoDangKy))
            {
                _l5084 = _l5084.Where(p => p.MaDV == SoDangKy).ToList();
                int manhasx=  _l5084.First().MaCtySX==null?0:_l5084.First().MaCtySX.Value ;
                string nhasx=_ldonvisx.Where(p=>p.MaDonVi== manhasx).Select(p=>p.TenDonVi).ToList().Count>0?_ldonvisx.Where(p=>p.MaDonVi== manhasx).Select(p=>p.TenDonVi).First():"";
                Getdata(_l5084.First().TenDV, _l5084.First().MaDV, _l5084.First().STT_ThongTu == null ? 0 : _l5084.First().STT_ThongTu.Value, _l5084.First().TenDV, _l5084.First().SoDangKy, _l5084.First().MaCtySX == null ? "" : _l5084.First().MaCtySX.ToString(), _l5084.First().MaDuongDung, _l5084.First().TrongDM == null ? 1 : _l5084.First().TrongDM ,
                    _l5084.First().HamLuong,       _l5084.First().BaoChe,  _l5084.First().DongGoi,  _l5084.First().TieuChuan,  _l5084.First().TuoiTho,  nhasx,_l5084.First().NguonGoc
                    );
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn dịch vụ");
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                grc_CV.DataSource = _l5084.Where(p => p.TenDV.Contains(txtTimKiem.Text) || (p.HoatChat!=null && p.HoatChat.Contains(txtTimKiem.Text)) || (p.SoDangKy != null && p.SoDangKy.Contains(txtTimKiem.Text)));
            }
            else grc_CV.DataSource = _l5084;

        }
    }
}