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
    public partial class Frm_ThCPKCBTEKoThe_HL02 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ThCPKCBTEKoThe_HL02()
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
                        quy = " QÚY I NĂM 2014";
                        break;
                    case 2:
                        quy = " QÚY II NĂM 2014 ";
                        break;
                    case 3:
                        quy = " QÚY III NĂM 2014";
                        break;
                    case 4:
                        quy = " QÚY IV NĂM 2014";
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
        private void Frm_ThCPKCBTEKoThe_HL02_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
        
        }
        List<TTboXung> _ttbx =new List<TTboXung>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            if (KTtaoBc())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                frmIn frm = new frmIn();
                BaoCao.Rep_ThCPKCBTEKoThe_HL02 rep = new BaoCao.Rep_ThCPKCBTEKoThe_HL02();
                rep.TuNgay.Value = lupTuNgay.Text;
                rep.DenNgay.Value = lupDenNgay.Text;
                if (ckQuy.Checked == true)
                {
                    rep.TG.Value = theoquy();
                }
                else rep.TG.Value = theoquy();

                var q = (from 
                          bn in data.BenhNhans.Where(p => p.SThe.Contains("TE") && p.SThe.Substring(7, 3) == "000")
                         join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                         join vp in data.VienPhis.Where(p=>p.NgayTT>=ngaytu&&p.NgayTT<=ngayden) on bn.MaBNhan equals vp.MaBNhan
                         join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                         where (radBN.SelectedIndex==2?true: bn.NoiTru==radBN.SelectedIndex)
                         group new{vp,bn,rv,vpct} by new{ bn.MaBNhan, bn.TenBNhan, bn.NoiTru, bn.NamSinh, bn.GTinh, bn.SThe, bn.HanBHTu, bn.HanBHDen, bn.DChi, bn.MaCS, bn.NNhap, rv.NgayRa  } into kq
                         select new {
                             kq.Key.NNhap,kq.Key.DChi, kq.Key.GTinh,kq.Key.HanBHDen,kq.Key.HanBHTu,kq.Key.MaBNhan,kq.Key.MaCS,kq.Key.NamSinh,kq.Key.NoiTru,
                             kq.Key.NgayRa,kq.Key.SThe,kq.Key.TenBNhan,
                             ThanhTien=kq.Sum(p=>p.vpct.ThanhTien),
                                     }).OrderBy(p=>p.NNhap).ThenBy(p=>p.TenBNhan).ToList();
                if (q.Count > 0)
                {
                    rep.DataSource = q.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Không có dữ liệu");
            }
                
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}