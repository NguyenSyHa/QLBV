using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frm_TTTruyenDich : DevExpress.XtraEditors.XtraForm
    {
        int _IdDonct = 0, _mabn = 0;
        public frm_TTTruyenDich(int IdDonct,int mabn)
        {
            InitializeComponent();
            _IdDonct = IdDonct;
            _mabn = _mabn;
        }
        QLBV_Database.QLBVEntities data;
        DateTime _ngayke = DateTime.Now;
        private void frm_TTTruyenDich_Load(object sender, EventArgs e)
        {
            data=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = (from dt in data.DThuoccts.Where(p => p.IDDonct == _IdDonct)
                     join dv in data.DichVus on dt.MaDV equals dv.MaDV
                     select new { dv.TenDV, dv.HamLuong, dt.NgayNhap }).ToList();
            if (q.Count > 0)
            {
                labelControl6.Text = "Thông tin truyền: " + q.First().TenDV;
                _ngayke = q.First().NgayNhap.Value;
            }
            var cb = data.CanBoes.Where(p => p.Status == 1).OrderBy(p => p.TenCB).ToList();
            lupCanBo.Properties.DataSource = cb;
            var kt = data.TTTruyenDiches.Where(p => p.IdDonct == _IdDonct).FirstOrDefault();
            if (kt != null)
            {
                lupCanBo.EditValue = kt.MaCBTH;
                deBatDau.DateTime = kt.BatDau.Value;
                deKetThuc.DateTime = kt.KetThuc.Value;
                if (kt.TocDo != null)
                    lupTocDo.Text = kt.TocDo.ToString();
                if (kt.DienBien != null)
                    mmDienBien.Text = kt.DienBien;
                if (kt.SLTruyen != null)
                    txtSLTruyen.Text = kt.SLTruyen.ToString();
                cboDonVi.Text = kt.DonViTruyen;
                btnxoa.Visible = true;
            }
            else
            {
                lupCanBo.EditValue = DungChung.Bien.MaCB;
                deBatDau.DateTime = DateTime.Now;
                deKetThuc.DateTime = DateTime.Now;
                lupTocDo.Text = "";
                mmDienBien.Text = "";
                btnxoa.Visible = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool kt = true;
            if (deKetThuc.DateTime < deBatDau.DateTime)
            {
                MessageBox.Show("Thời gian kết thúc không được nhỏ hơn thời gian bắt đầu");
                deKetThuc.Focus();
                kt = false;
            }
            if (deBatDau.DateTime < _ngayke)
            {
                MessageBox.Show("Thời gian bắt đầu không được nhỏ hơn thời gian kê đơn");
                deBatDau.Focus();
                kt = false;
            }
            if (lupCanBo.EditValue == null)
            {
                MessageBox.Show("Chưa nhập cán bộ thực hiện truyền dịch");
                lupCanBo.Focus();
                kt = false;
            }
            if (kt)
            {
                var q = data.TTTruyenDiches.Where(p => p.IdDonct == _IdDonct).FirstOrDefault();
                if (q != null)
                {
                    q.BatDau = deBatDau.DateTime;
                    q.KetThuc = deKetThuc.DateTime;
                    if (!string.IsNullOrEmpty(lupTocDo.Text))
                        q.TocDo = Convert.ToInt32(lupTocDo.Text);
                    q.MaCBTH = lupCanBo.EditValue.ToString();
                    q.DienBien = mmDienBien.Text;
                    if (!string.IsNullOrEmpty(txtSLTruyen.Text))
                        q.SLTruyen = Convert.ToDouble(txtSLTruyen.Text);
                    q.DonViTruyen = cboDonVi.Text;
                    data.SaveChanges();
                    MessageBox.Show("Sửa thành công");
                    this.Close();
                }
                else
                {
                    TTTruyenDich moi = new TTTruyenDich();
                    moi.IdDonct = _IdDonct;
                    moi.BatDau = deBatDau.DateTime;
                    moi.KetThuc = deKetThuc.DateTime;
                    if (!string.IsNullOrEmpty(lupTocDo.Text))
                        moi.TocDo = Convert.ToInt32(lupTocDo.Text);
                    moi.MaCBTH = lupCanBo.EditValue.ToString();
                    moi.DienBien = mmDienBien.Text;
                    if (!string.IsNullOrEmpty(txtSLTruyen.Text))
                        moi.SLTruyen = Convert.ToDouble(txtSLTruyen.Text);
                    moi.DonViTruyen = cboDonVi.Text;
                    data.TTTruyenDiches.Add(moi);
                    data.SaveChanges();
                    MessageBox.Show("Thêm mới thành công");
                    this.Close();
                }
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Bạn muốn xóa TT truyền dịch ?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(Result==DialogResult.Yes)
            {
                var q = data.TTTruyenDiches.Where(p => p.IdDonct == _IdDonct).FirstOrDefault();
                if(q!=null)
                {
                    data.TTTruyenDiches.Remove(q);
                    data.SaveChanges();
                    MessageBox.Show("Xóa thành công");
                    this.Close();
                }
            }
        }
    }
}