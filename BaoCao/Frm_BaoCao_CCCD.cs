using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.BaoCao
{
    public partial class Frm_BaoCao_CCCD : Form
    {
        public Frm_BaoCao_CCCD()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_BaoCao_CCCD_Load(object sender, EventArgs e)
        {
            cbxLoaiThe.SelectedIndex = 0;
            lupNgaytu.DateTime = System.DateTime.Now.Date;
            lupNgayden.DateTime = System.DateTime.Now.Date;
        }
        private class BNDichVu
        {
            public string TenBN { get; set; }
            public int MaBN { get; set; }
            public string SoTheBHYT { get; set; }
            public string BHYT { get; set; }
            public string CCCD { get; set; }
        }
        List<BNDichVu> _BN = new List<BNDichVu>();
        private void butTaoBC_Click(object sender, EventArgs e)
        {
            _BN.Clear();
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            #region Tất cả
            if (cbxLoaiThe.SelectedIndex == 0)
            {
                var dichvu = (from bn in data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden) 
                              where bn.DTuong == "BHYT"
                              select new
                              {
                                  bn.TenBNhan,
                                  bn.MaBNhan,
                                  bn.IsCCCD,
                                  bn.SThe,
                              }).ToList();
                if (dichvu.Count() > 0)
                {
                    foreach (var bn in dichvu)
                    {
                        BNDichVu them = new BNDichVu();
                        them.MaBN = bn.MaBNhan;
                        them.TenBN = bn.TenBNhan;
                        them.SoTheBHYT = bn.SThe;
                        if (bn.IsCCCD == false)
                        {
                            them.BHYT = "X";
                        }
                        else if (bn.IsCCCD == true)
                        {
                            them.CCCD = "X";
                        }
                        _BN.Add(them);
                    }
                }
                if (_BN.Count() > 0)
                {
                    Rep_BC_CCCD rep = new Rep_BC_CCCD();
                    frmIn frm = new frmIn();
                    rep.Parameters["Ngay"].Value = "Từ ngày " + ngaytu.ToString("dd/MM/yyyy") + " Đến ngày " + ngayden.ToString("dd/MM/yyyy");
                    rep.Parameters["CQ"].Value = DungChung.Bien.TenCQ;
                    rep.DataSource = _BN;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Không có dữ liệu");
            }
            #endregion
            #region BHYT
            if (cbxLoaiThe.SelectedIndex == 1)
            {
                var dichvu = (from  bn in data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden) 
                              where bn.IsCCCD == false && bn.DTuong == "BHYT"
                              select new
                              {
                                  bn.TenBNhan,
                                  bn.MaBNhan,
                                  bn.IsCCCD,
                                  bn.SThe,
                              }).ToList();
                if (dichvu.Count() > 0)
                {
                    foreach (var bn in dichvu)
                    {
                        BNDichVu them = new BNDichVu();
                        them.MaBN = bn.MaBNhan;
                        them.TenBN = bn.TenBNhan;
                        them.SoTheBHYT = bn.SThe;
                        if (bn.IsCCCD == false)
                        {
                            them.BHYT = "X";
                        }
                        else if (bn.IsCCCD == true)
                        {
                            them.CCCD = "X";
                        }
                        _BN.Add(them);
                    }
                }
                if (_BN.Count() > 0)
                {
                    Rep_BC_CCCD rep = new Rep_BC_CCCD();
                    frmIn frm = new frmIn();
                    rep.Parameters["Ngay"].Value = "Từ ngày " + ngaytu.ToString("dd/MM/yyyy") + " Đến ngày " + ngayden.ToString("dd/MM/yyyy");
                    rep.Parameters["CQ"].Value = DungChung.Bien.TenCQ;
                    rep.DataSource = _BN;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Không có dữ liệu");
            }
            #endregion
            #region CCCD
            if(cbxLoaiThe.SelectedIndex == 2)
            {
                var dichvu = (from bn in data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden) 
                              where bn.IsCCCD == true && bn.DTuong == "BHYT"
                              select new
                              {
                                  bn.TenBNhan,
                                  bn.MaBNhan,
                                  bn.IsCCCD,
                                  bn.SThe,
                              }).ToList();
                if (dichvu.Count() > 0)
                {
                    foreach (var bn in dichvu)
                    {
                        BNDichVu them = new BNDichVu();
                        them.MaBN = bn.MaBNhan;
                        them.TenBN = bn.TenBNhan;
                        them.SoTheBHYT = bn.SThe;
                        if (bn.IsCCCD == false)
                        {
                            them.BHYT = "X";
                        }
                        else if (bn.IsCCCD == true)
                        {
                            them.CCCD = "X";
                        }
                        _BN.Add(them);
                    }
                }
                if (_BN.Count() > 0)
                {
                    Rep_BC_CCCD rep = new Rep_BC_CCCD();
                    frmIn frm = new frmIn();
                    rep.Parameters["Ngay"].Value = "Từ ngày " + ngaytu.ToString("dd/MM/yyyy") + " Đến ngày " + ngayden.ToString("dd/MM/yyyy");
                    rep.Parameters["CQ"].Value = DungChung.Bien.TenCQ;
                    rep.DataSource = _BN;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Không có dữ liệu");
            }    
            #endregion
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
