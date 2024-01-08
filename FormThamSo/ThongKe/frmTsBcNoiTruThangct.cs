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
    public partial class frmTsBcNoiTruThangct : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBcNoiTruThangct()
        {
            InitializeComponent();
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
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
           else return true;
        }
       
        private void btnInBC_Click(object sender, EventArgs e)
        {

            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBc())
            {
                int _khoa = 0;
                if (lupKhoa.EditValue != null)
                    _khoa = Convert.ToInt32(lupKhoa.EditValue);
               
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);

                frmIn frm = new frmIn();
                BaoCao.repBcNoiTruThangct rep = new BaoCao.repBcNoiTruThangct();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
                //rep.Khoa.Value = _khoa;
                var qtenkhoa = (from kp in data.KPhongs
                                where (kp.PLoai=="Lâm sàng")
                               join kb in data.BNKBs on kp.MaKP equals kb.MaKP
                               where (kb.MaKP == _khoa)
                               select new { kp.TenKP }).ToList();
                if (qtenkhoa.Count > 0)
                {
                    rep.Khoa.Value = qtenkhoa.First().TenKP;
                }

               
                var qnxt = (from bn in data.BenhNhans 
                            join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan 
                            join icd in data.ICD10 on bnkb.MaICD equals icd.MaICD
                            where (bn.NoiTru == 1)
                            where(bnkb.NgayKham >= tungay && bnkb.NgayKham <= denngay)
                            group new { bn, bnkb, icd } by new {bnkb.MaKP, bnkb.MaICD, icd.TenICD } into kq
                            select new
                            {
                                MaKP=kq.Key.MaKP,
                                TenICD = kq.Key.TenICD,
                                MaICD = kq.Key.MaICD,

                                TongSoLK = kq.Select(p => p.bnkb.MaBNhan).Count(),
                                TongSoLKTE=kq.Where(p=>p.bn.Tuoi<=15).Select(p=>p.bnkb.MaBNhan).Count(),
                                TongSoLKNu=kq.Where(p=>p.bn.GTinh == 0).Select(p=>p.bnkb.MaBNhan).Count()
                         
                            }).ToList();

      

                if (_khoa>0)
                {
                    rep.DataSource = qnxt.ToList().Where(p => p.MaKP== (_khoa));
                } else
                    rep.DataSource = qnxt.ToList();
                    //rep.DataSource = qnxt.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
               

            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmTsBcNoiTruThangct_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs.Where(p => p.PLoai== ("Lâm sàng")) select new { TK.TenKP, TK.MaKP };
            lupKhoa.Properties.DataSource = q.ToList();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lupKhoa_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}