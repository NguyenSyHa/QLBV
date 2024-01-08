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
    public partial class Frm_BcTKSoLuotBNKNgT_HL03 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcTKSoLuotBNKNgT_HL03()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
        private string theoquy()
        {
            string quy = "";

            if (ckQuy.Checked == true)
            {
                switch (timquy(lupTuNgay.DateTime.Month))
                {
                    case 1:
                        quy = " Qúy I năm " + lupTuNgay.DateTime.ToString().Substring(6, 4);
                        break;
                    case 2:
                        quy = " Qúy II năm " + lupTuNgay.DateTime.ToString().Substring(6, 4);
                        break;
                    case 3:
                        quy = " Qúy III năm " + lupTuNgay.DateTime.ToString().Substring(6, 4);
                        break;
                    case 4:
                        quy = " Qúy IV năm " + lupTuNgay.DateTime.ToString().Substring(6, 4);
                        break;
                }

            }
            else
            {
                quy = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
            }
            return quy;
        }

        private int timquy(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return (1);
            }
            if (month > 3 && month <= 6)
            {
                return (2);
            }
            if (month > 6 && month <= 9)
            {
                return (3);
            }
            else { return (4); }
        }
        private void Frm_BcTKSoLuotBNKNgT_HL03_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;

            if (KTtaoBc())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                frmIn frm = new frmIn();
                BaoCao.Rep_BcTKSoLuotBNKNgT_HL03 rep = new BaoCao.Rep_BcTKSoLuotBNKNgT_HL03();
                rep.TuNgay.Value = lupTuNgay.Text;
                rep.DenNgay.Value = lupDenNgay.Text;
                if (ckQuy.Checked == true)
                {
                    rep.TG.Value = theoquy();
                }
                else rep.TG.Value = theoquy();

                var q = (from bn in data.BenhNhans.Where(p=>p.NoiTru==0).Where(p=>p.DTuong=="BHYT")
                         join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                         join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                         where(vp.NgayTT>=ngaytu && vp.NgayTT<=ngayden)
                         group new { bn, vp,vpct } by new { bn.MaBNhan, bn.TenBNhan, bn.SThe, vp.NgayTT } into kq
                         select new 
                                    {
                                        MaBNhan=kq.Key.MaBNhan,
                                        TenBNhan=kq.Key.TenBNhan,
                                        SThe=kq.Key.SThe,
                                        NgayTT=kq.Key.NgayTT,
                                        TienBH=kq.Sum(p=>p.vpct.TienBH),
                                        TienBN=kq.Sum(p=>p.vpct.TienBN),
                                        ThanhTien=kq.Sum(p=>p.vpct.ThanhTien)
                                    }).ToList();
                if (q.Count() > 0)
                {
                    rep.DataSource = q.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else MessageBox.Show("Không có dữ liệu");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}