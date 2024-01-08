using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBV.BaoCao;

namespace QLBV
{
    public partial class frmBCKeThuocTheoBSi : Form
    {
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frmBCKeThuocTheoBSi()
        {
            InitializeComponent();
        }

        

        private void btnLayBC_Click(object sender, EventArgs e)
        {
            //DateTime tuNgay1 = new DateTime(deTuNgay.DateTime.Year, deTuNgay.DateTime.Month, deTuNgay.DateTime.Day, 0, 0, 0);
            //DateTime tuNgay = DungChung.Ham.NgayTu(dtpkTuNgay.Value);
            //DateTime denNgay = DungChung.Ham.NgayDen(dtpkDenNgay.Value);
            DateTime tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
            DateTime denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
            
            if(gluCanBo.Enabled==false)
            {
               var ds = (from cb in _dataContext.CanBoes
                      join dt in _dataContext.DThuocs on cb.MaCB equals dt.MaCB
                      join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                      join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                      where dt.NgayKe >= tuNgay && dt.NgayKe <= denNgay && cb.TenCB != null && cb.TenCB != "" && dv.PLoai == 1
                      group new { cb, dt, dtct, dv } by new { cb.TenCB, dv.MaTam, dv.TenDV, dtct.DonVi, dtct.DonGia } into gr
                      select new
                      {
                          gr.Key.TenCB,
                          gr.Key.MaTam,
                          gr.Key.TenDV,
                          gr.Key.DonVi,
                          gr.Key.DonGia,
                          SoLuong = gr.Sum(p => p.dtct.SoLuong),
                          ThanhTien = gr.Sum(p => p.dtct.ThanhTien)
                      }).ToList();
                Rep_BCKeThuocTheoBSi rep = new Rep_BCKeThuocTheoBSi();
                rep.xlbTuNgay.Text = dtpkTuNgay.Value.Day + "/" + dtpkTuNgay.Value.Month + "/" + dtpkTuNgay.Value.Year;
                rep.xlbDenNgay.Text = dtpkDenNgay.Value.Day + "/" + dtpkDenNgay.Value.Month + "/" + dtpkDenNgay.Value.Year;
                rep.DataSource = ds.OrderBy(p => p.TenCB).ThenBy(p => p.TenDV);
                rep.Binding();
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                if(gluCanBo.Text==null||gluCanBo.Text=="")
                {
                    MessageBox.Show("Chưa chọn cán bộ!");
                    return;
                }
                string maCB = gluCanBo.EditValue.ToString();
                var ds = (from cb in _dataContext.CanBoes
                          join dt in _dataContext.DThuocs on cb.MaCB equals dt.MaCB
                          join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                          join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                          where dt.NgayKe >= tuNgay && dt.NgayKe <= denNgay && cb.MaCB==maCB && dv.PLoai == 1
                          group new { cb, dt, dtct, dv } by new { cb.TenCB, dv.MaTam, dv.TenDV, dtct.DonVi, dtct.DonGia } into gr
                          select new
                          {
                              gr.Key.TenCB,
                              gr.Key.MaTam,
                              gr.Key.TenDV,
                              gr.Key.DonVi,
                              gr.Key.DonGia,
                              SoLuong = gr.Sum(p => p.dtct.SoLuong),
                              ThanhTien = gr.Sum(p => p.dtct.ThanhTien)
                          }).ToList();
                Rep_BCKeThuocTheoBSi rep = new Rep_BCKeThuocTheoBSi();
                rep.xlbTuNgay.Text = dtpkTuNgay.Value.Day + "/" + dtpkTuNgay.Value.Month + "/" + dtpkTuNgay.Value.Year;
                rep.xlbDenNgay.Text = dtpkDenNgay.Value.Day + "/" + dtpkDenNgay.Value.Month + "/" + dtpkDenNgay.Value.Year;
                rep.DataSource = ds.OrderBy(p => p.TenCB).ThenBy(p => p.TenDV);
                rep.Binding();
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            
            
        }

        private void frmBCKeThuocTheoBSi_Load(object sender, EventArgs e)
        {
            var listCB = (from cb in _dataContext.CanBoes.Where(p => p.Status == 1)
                          where cb.ChucVu == "BS" || cb.CapBac == "BS"
                          select new
                          {
                              cb.MaCB,
                              cb.TenCB
                          }).ToList();
            gluCanBo.Properties.DataSource = listCB;
            gluCanBo.Properties.ValueMember = "MaCB";
            gluCanBo.Properties.DisplayMember = "TenCB";


        }

        private void gluCanBo_EditValueChanged(object sender, EventArgs e)
        {
            string maCB = gluCanBo.EditValue.ToString();
        }

        private void cbChonTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (gluCanBo.Enabled == true)
            {
                gluCanBo.Text = "";
                gluCanBo.Enabled = false;

            }
                
            else gluCanBo.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
