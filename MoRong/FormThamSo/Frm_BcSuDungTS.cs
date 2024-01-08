using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_BcSuDungTS : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcSuDungTS()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_BcSuDungTS_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            var q = from TK in _Data.KPhongs select new { TK.TenKP, TK.MaKP };
            lupKhoa.Properties.DataSource = q.ToList();
        }
        private bool KTtaoBc()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupNgayden.Focus();
                return false;
            }
           
            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime Ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime Ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            frmIn frm = new frmIn();
            BaoCao.Rep_BcSuDungTS rep = new BaoCao.Rep_BcSuDungTS();

            if (KTtaoBc())
            {
                var qts = (from sd in _Data.SDTs.Where(p => p.Ngay >= Ngaytu).Where(p => p.Ngay <= Ngayden)
                           join kp in _Data.KPhongs on sd.MaKP equals kp.MaKP
                           join cb in _Data.CanBoes on sd.MaCB equals cb.MaCB
                           select new

                           {TenKP=kp.TenKP,
                               MaKP=kp.MaKP,
                               Ngay=sd.Ngay,
                               CanBo=cb.TenCB,
                               HTTS=sd.PLoai,
                               NoiDung=sd.NoiDung,
                           }).ToList();
                if (qts.Count() > 0)
                {
                    int i = 0;
                    if (chkTatCa.Checked == true)
                    {
                        rep.DataSource = qts.ToList();
                        rep.BindingData();
                        rep.In.Value = 0;
           
                    }
                    else
                    {
                        int _MaKP = 0;
                        if (lupKhoa.Text != null)
                        {
                            _MaKP = lupKhoa.EditValue == null ? 0 : Convert.ToInt32( lupKhoa.EditValue);
                            rep.DataSource = qts.Where(p => p.MaKP == _MaKP).ToList();
                            var k = from kp in _Data.KPhongs.Where(p => p.MaKP == _MaKP) select new { kp.TenKP };
                            if (k.Count() > 0)
                            {
                                rep.Khoa.Value = k.First().TenKP;
                            }
                            rep.BindingData();
                        }

                    }

                }
                rep.TuNgayDenNgay.Value = "Từ ngày " + Ngaytu.ToString().Substring(0, 10) + " đến ngày " + Ngayden.ToString().Substring(0, 10);
                rep.CreateDocument();
                //rep.BindingData();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}