using DevExpress.XtraEditors.Controls;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frm_ChanDoanKH : Form
    {
        BNKB bnkb;
        public frm_ChanDoanKH(BNKB _bnkb)
        {
            InitializeComponent();
            this.bnkb = _bnkb;
        }

        List<c_ICD> lICD = new List<c_ICD>();
        List<ICD10> _licd10 = new List<ICD10>();
        List<CanBo> _lCanBo = new List<CanBo>();
        private void frm_ChanDoanKH_Load(object sender, EventArgs e)
        {
            QLBVEntities DaTaContext = new QLBVEntities(DungChung.Bien.StrCon);
            bnkb = DaTaContext.BNKBs.FirstOrDefault(o => o.IDKB == bnkb.IDKB);
            _licd10 = DaTaContext.ICD10.ToList();
            txtBenhChinh.Visible = txtBenhKhac2.Visible = txtBenhPhu2.Visible = LupICD2.Visible = false;
            if (DungChung.Bien.MaBV == "14017")
                txtBenhChinh.Visible = true;
            lICD = (from ICD in _licd10.Where(p => p.MaYHCT != null) select new c_ICD { MaICD = ICD.MaYHCT ?? "", TenICD = ICD.TenYHCT + "[" + ICD.TenICD + "]" ?? "" }).OrderBy(p => p.MaICD).ToList();
            _lCanBo = DaTaContext.CanBoes.OrderBy(o => o.TenCB).ToList();
            if ((DungChung.Bien.ChuyenKhoa).Contains("trực"))
                lupNguoiKhamkb.Properties.DataSource = _lCanBo.Where(p => p.Status == 1).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")).ToList();
            else
                lupNguoiKhamkb.Properties.DataSource = _lCanBo.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains((bnkb.MaKPDTKH ?? 0).ToString())).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")).ToList();
            lupChanDoanKb.Properties.DataSource = lICD;
            lupMaICDkb.Properties.DataSource = lICD;
            txtBenhKhac2.Properties.DataSource = lICD;
            lupKhac.Properties.DataSource = lICD.Select(p => p.MaICD).ToList();
            txtBenhKhac1.Properties.DataSource = lICD.Select(p => p.TenICD).ToList();
            LupICD2.Properties.DataSource = lICD;

            
            lupChanDoanKb.EditValue = bnkb.MaICDDTKH;
            lupMaICDkb.EditValue = bnkb.MaICDDTKH;
            LupICD2.EditValue = bnkb.MaICD2DTKH;
            lupKhac.EditValue = bnkb.MaICD2DTKH;
            txtBenhKhac1.EditValue = bnkb.BenhKhacDTKH;
            dtNgayKham.EditValue = bnkb.NgayKhamDTKH ?? DateTime.Now;
            lupNguoiKhamkb.EditValue = bnkb.MaCBDTKH;
            txtBenhChinh.Text = bnkb.ChanDoanDTKH;
        }

        public class c_ICD
        {
            public string TenICD { set; get; }
            public string MaICD { set; get; }
        }

        bool isNoShow = false;
        private void lupChanDoanKb_Closed(object sender, ClosedEventArgs e)
        {
            if (isNoShow)
            {
                isNoShow = false;
                lupChanDoanKb.Properties.DataSource = lICD;
            }
        }

        private void buttonEdit1_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Combo)
            {
                if (txtBenhKhac2.IsPopupOpen)
                    txtBenhKhac2.ClosePopup();
                else
                    txtBenhKhac2.ShowPopup();
            }
        }

        private void lupChanDoanKb_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lupChanDoanKb.Text))
            {
                if (lupChanDoanKb.EditValue.ToString() == "0")
                {
                    lupChanDoanKb.EditValue = "";
                    txtBenhChinh.EditValue = "";
                }
                else
                {
                    lupMaICDkb.EditValue = lICD.Where(p => p.TenICD == lupChanDoanKb.EditValue.ToString()).Select(p => p.MaICD).FirstOrDefault();
                    txtBenhChinh.EditValue = lICD.Where(p => p.TenICD == lupChanDoanKb.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                }

            }
            else
            {
                lupMaICDkb.EditValue = "";
                txtBenhChinh.Text = "";
            }
        }

        private void lupChanDoanKb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD);
                frm.ShowDialog();

            }
        }

        public void getICD(string _maicd)
        {
            lupMaICDkb.EditValue = _maicd;
        }

        private void lupMaICDkb_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lupMaICDkb.Text))
            {
                if (lupMaICDkb.EditValue.ToString() == "0")
                {
                    lupChanDoanKb.EditValue = "";
                    lupMaICDkb.EditValue = "";
                    txtBenhChinh.Text = "";
                }
                else
                {
                    lupChanDoanKb.EditValue = lICD.Where(p => p.MaICD == lupMaICDkb.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                    txtBenhChinh.Text = lICD.Where(p => p.MaICD == lupMaICDkb.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                }


            }
            else
            {
                lupChanDoanKb.EditValue = "";
                txtBenhChinh.Text = "";
            }
        }

        private void lupMaICDkb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD);
                frm.ShowDialog();

            }
        }

        private void txtBenhPhu2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var listIcd = lICD.Where(o => o.TenICD.ToUpper().Contains(txtBenhPhu2.Text.ToUpper())).ToList();
                if (listIcd != null && listIcd.Count > 0)
                    txtBenhKhac2.Properties.DataSource = listIcd;
                else
                    txtBenhKhac2.Properties.DataSource = lICD;
                txtBenhKhac2.ShowPopup();
                isNoShow = true;
            }
        }

        private void txtBenhKhac2_Closed(object sender, ClosedEventArgs e)
        {
            if (isNoShow)
            {
                isNoShow = false;
                txtBenhKhac2.Properties.DataSource = lICD;
            }
        }

        private void txtBenhKhac2_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBenhKhac2.Text))
            {
                if (txtBenhKhac2.EditValue.ToString() == "0")
                {
                    txtBenhKhac2.EditValue = "";
                    LupICD2.EditValue = "";
                    txtBenhPhu2.Text = "";
                }
                else
                {
                    LupICD2.EditValue = lICD.Where(p => p.TenICD == txtBenhKhac2.EditValue.ToString()).Select(p => p.MaICD).FirstOrDefault();
                    txtBenhPhu2.EditValue = lICD.Where(p => p.TenICD == txtBenhKhac2.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                }

            }
            else
            {
                LupICD2.EditValue = "";
                txtBenhPhu2.Text = "";
            }

        }

        private void LupICD2_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LupICD2.Text))
            {
                if (LupICD2.EditValue.ToString() == "0")
                {
                    txtBenhKhac2.EditValue = "";
                    LupICD2.EditValue = "";
                    txtBenhPhu2.Text = "";
                }
                else
                {
                    txtBenhKhac2.EditValue = lICD.Where(p => p.MaICD == LupICD2.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                    txtBenhPhu2.EditValue = lICD.Where(p => p.MaICD == LupICD2.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                }


            }
            else
            {
                txtBenhKhac2.EditValue = "";
                txtBenhPhu2.Text = "";
            }
        }

        private void LupICD2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD2);
                frm.ShowDialog();

            }
        }

        public void getICD2(string _maicd)
        {

            LupICD2.EditValue = _maicd;
            string tenICD = lICD.Where(p => p.MaICD == _maicd).Select(p => p.TenICD).FirstOrDefault();
            if (string.IsNullOrEmpty(txtBenhKhac2.Text) && tenICD != null)
                txtBenhKhac2.Text = tenICD;
        }

        private void lupKhac_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lupKhac.Text))
            {

                List<string> a = lupKhac.Text.Split(';').ToList();
                List<string> b = new List<string>();
                foreach (var item in a)
                {
                    string c = lICD.Where(p => p.MaICD == item.Trim()).Select(p => p.TenICD).FirstOrDefault();
                    b.Add(c);
                }
                txtBenhKhac1.SetEditValue(string.Join(";", b));
            }
            else
            {
                txtBenhKhac1.SetEditValue(null);
            }
        }

        private void lupKhac_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICDKhac);
                frm.ShowDialog();
            }
        }

        public void getICDKhac(string _maicd)
        {
            lupKhac.EditValue = _maicd;
            string tenICD = lICD.Where(p => p.MaICD == _maicd).Select(p => p.TenICD).FirstOrDefault();
            if (string.IsNullOrEmpty(txtBenhKhac1.Text) && tenICD != null)
            {
                txtBenhKhac1.Text = tenICD;
            }
        }

        private void txtBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var listIcd = lICD.Where(o => o.TenICD.ToUpper().Contains(txtBenhChinh.Text.ToUpper())).ToList();
                if (listIcd != null && listIcd.Count > 0)
                    lupChanDoanKb.Properties.DataSource = listIcd;
                else
                    lupChanDoanKb.Properties.DataSource = lICD;
                lupChanDoanKb.ShowPopup();
                isNoShow = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtNgayKham.EditValue == null)
            {
                MessageBox.Show("Ngày khám không được để trống");
                return;
            }
            if (lupChanDoanKb.EditValue == null || lupChanDoanKb.EditValue.ToString() == "" || lupMaICDkb.EditValue == null || lupMaICDkb.EditValue.ToString() == "")
            {
                MessageBox.Show("Bệnh chính không được để trống");
                return;
            }
            if (lupNguoiKhamkb.EditValue == null || lupNguoiKhamkb.EditValue.ToString() == "")
            {
                MessageBox.Show("Bác sĩ điều trị không được để trống");
                return;
            }
            QLBVEntities DaTaContext = new QLBVEntities(DungChung.Bien.StrCon);
            var bnkbUpdate = DaTaContext.BNKBs.FirstOrDefault(o => o.IDKB == bnkb.IDKB);
            if (bnkbUpdate != null)
            {
                bnkbUpdate.NgayKhamDTKH = dtNgayKham.DateTime;
                bnkbUpdate.MaICDDTKH = lupMaICDkb.EditValue.ToString();
                bnkbUpdate.ChanDoanDTKH = txtBenhChinh.Text;
                bnkbUpdate.MaICD2DTKH = lupKhac.EditValue != null ? lupKhac.EditValue.ToString() : "";
                bnkbUpdate.BenhKhacDTKH = txtBenhKhac1.EditValue != null ? txtBenhKhac1.EditValue.ToString() : "";
                bnkbUpdate.MaCBDTKH = lupNguoiKhamkb.EditValue.ToString();
                if (DaTaContext.SaveChanges() > 0)
                {
                    MessageBox.Show("Lưu thành công");
                    this.Close();
                }
            }
        }
    }
}
