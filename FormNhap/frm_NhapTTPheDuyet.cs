using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frm_NhapTTPheDuyet : Form
    {
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int idBNKB;
        int actionType = 0; //0: Mới  1: Sửa
        TTPheDuyet currentData;
        public frm_NhapTTPheDuyet(int _idKB)
        {
            InitializeComponent();
            this.idBNKB = _idKB;
        }

        private void frm_NhapTTPheDuyet_Load(object sender, EventArgs e)
        {
            RefreshData();
            btnSave.Enabled = false;
           
        }

        private void RefreshData()
        {
            this.currentData = null;
            this.actionType = 0;
            LoadDataToGridView();
            EnableControl(actionType);
            ResetControl();
        }

        private void Chandoan()
        {
            var cdoan = (from cd in dataContext.BNKBs.Where(o => o.IDKB == idBNKB) select new { cd.MaBNhan }).ToList();
            string[] _MaICDarr = DungChung.Ham.getMaICDarr_SL(dataContext, idBNKB, DungChung.Bien.GetICD, 0);
            string[] icd2 = _MaICDarr[0].Split(';');
            string[] tenicd2 = _MaICDarr[1].Split(';');
            string tenbenh = "";
            if (icd2.Length <= tenicd2.Length)
            {
                for (int i = 0; i < icd2.Length; i++)
                {
                    tenbenh += icd2[i]=="" ? tenicd2[i] + "; ":  "[" + icd2[i] + "] " + tenicd2[i] + "; ";


                }
                if (icd2.Length < tenicd2.Length)
                {
                    int cut1 = tenicd2.Length - icd2.Length;
                    int cut = tenicd2.Length - cut1;
                    string mab1k = DungChung.Ham.FreshString(string.Join(";", tenicd2.Skip(cut)));
                    tenbenh += " " + mab1k + "; ";
                }
            }
            else
            {
                for (int i = 0; i < tenicd2.Length; i++)
                {
                    tenbenh += icd2[i] == ""? tenicd2[i] + "; " : "[" + icd2[i] + "] " + tenicd2[i] + "; ";
                }
            }
            txtChandoan.Text = tenbenh;

        }
        private void ResetControl()
        {
            textBox1.ResetText();
            txtChandoan.ResetText();
            dtNgayPheDuyet.EditValue = DateTime.Now;
        }

        private void LoadDataToGridView()
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var data = dataContext.TTPheDuyets.Where(o => o.BNKB_ID == idBNKB).OrderByDescending(o => o.NgayPheDuyet).ToList();
            gridControlTTPheDuyet.BeginUpdate();
            gridControlTTPheDuyet.DataSource = data;
            gridControlTTPheDuyet.EndUpdate();
        }

        private void EnableControl(int action)
        {
            btnNew.Enabled = true;
            btnSave.Enabled = true;
            btnPrint.Enabled = action == 1;
        }

        private void gridViewTTPheDuyet_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (TTPheDuyet)gridViewTTPheDuyet.GetRow(gridViewTTPheDuyet.GetRowHandle(e.ListSourceRowIndex));
            if (row != null && e.IsGetData && e.Column.UnboundType != DevExpress.Data.UnboundColumnType.Bound)
            {
                if (e.Column.FieldName == "STT")
                    e.Value = e.ListSourceRowIndex + 1;
                else if (e.Column.FieldName == "NgayPheDuyet_Str")
                    e.Value = row.NgayPheDuyet.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }

        private void gridViewTTPheDuyet_Click(object sender, EventArgs e)
        {
            currentData = (TTPheDuyet)gridViewTTPheDuyet.GetFocusedRow();
            if (currentData != null)
            {
                actionType = 1;
                EnableControl(actionType);
                FillDataToControl(currentData);
            }
        }

        private void FillDataToControl(TTPheDuyet data)
        {
            dtNgayPheDuyet.DateTime = data.NgayPheDuyet;
            txtChandoan.Text = data.ChanDoanTTPD;
            textBox1.Text = Convert.ToString(data.SoNgayThem);
        }

        private void gridViewTTPheDuyet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            RefreshData();
            Chandoan();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            bool success = false;
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (Convert.ToInt32(textBox1.Text) >50)
                {
                    MessageBox.Show("Nhập số ngày điều trị duyệt thêm quá lớn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Convert.ToInt32(textBox1.Text) == 0)
                {
                    MessageBox.Show("Số Ngày duyệt thêm phải lớn hơn 0 hoặc để trống!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (dtNgayPheDuyet.EditValue == null)
            {
                MessageBox.Show("Số ngày duyệt không được để trống!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtChandoan.Text))
            {
                MessageBox.Show("Chẩn đoán không được để trống!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtChandoan.Text.Length > 1000)
            {
                MessageBox.Show("Chẩn đoán quá dài!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            switch (actionType)
            {
                case 0:
                    {
                        TTPheDuyet insertData = new TTPheDuyet();
                        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        insertData.BNKB_ID = idBNKB;
                        insertData.NgayPheDuyet = dtNgayPheDuyet.DateTime;
                        insertData.SoNgayThem = textBox1.Text == "" ? null : insertData.SoNgayThem = Convert.ToInt32(textBox1.Text);
                        insertData.ChanDoanTTPD = txtChandoan.Text;
                        dataContext.TTPheDuyets.Add(insertData);
                        dataContext.SaveChanges();
                        success = true;
                        RefreshData();

                        break;
                    }
                case 1:
                    {
                        if (currentData != null)
                        {
                            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                            var updateData = dataContext.TTPheDuyets.FirstOrDefault(o => o.ID == currentData.ID);
                            if (updateData != null)
                            {
                                updateData.BNKB_ID = updateData.BNKB_ID;
                                updateData.NgayPheDuyet = dtNgayPheDuyet.DateTime;
                                updateData.SoNgayThem = textBox1.Text == "" ? null : updateData.SoNgayThem = Convert.ToInt32(textBox1.Text);
                                updateData.ChanDoanTTPD = txtChandoan.Text;
                                dataContext.SaveChanges();
                                success = true;
                                LoadDataToGridView();
                                currentData = updateData;
                            }
                        }
                        break;
                    }
            }

            if (success)
                MessageBox.Show("Lưu thành công!");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (currentData == null)
            {
                MessageBox.Show("Chưa chọn thông tin phê duyệt");
                return;
            }
            var rs = (from bn in dataContext.BenhNhans
                      join bnkb in dataContext.BNKBs.Where(o => o.IDKB == idBNKB) on bn.MaBNhan equals bnkb.MaBNhan
                      join kp in dataContext.KPhongs on bn.MaKP equals kp.MaKP
                      join ttpd in dataContext.TTPheDuyets.Where(o => o.ID == currentData.ID) on bnkb.IDKB equals ttpd.BNKB_ID
                      // join vv in dataContext.VaoViens on bn.MaBNhan equals vv.MaBNhan
                      select new
                      {
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.NgaySinh,
                          bn.ThangSinh,
                          bn.NamSinh,
                          bn.SThe,
                          bn.DChi,
                          bnkb.ChanDoan,
                          bnkb.MaICD,
                          kp.TenKP,
                          ttpd.CoNang,
                          ttpd.NgayPheDuyet,
                          ttpd.Mach,
                          ttpd.NhietDo,
                          ttpd.HuyetAp,
                          ttpd.NhipTho,
                          ttpd.ThucThe,
                          ttpd.HuongDTTuNgay,
                          ttpd.HuongDTDenNgay,
                          ttpd.ChanDoanTTPD,
                          ttpd.ToanThan,
                          ttpd.SoNgayThem,
                      }
                                 ).ToList();
            Dictionary<string, object> dicParam = new Dictionary<string, object>();

            if (rs != null && rs.Count > 0)
            {
                dicParam["TenBNhan"] = rs.First().TenBNhan;
                dicParam["SThe"] = DungChung.Bien.MaBV == "14018" ? rs.First().SThe + " - " + rs.First().MaBNhan : rs.First().SThe;
                dicParam["DChi"] = rs.First().DChi;
                dicParam["NgayPheDuyet"] = "Ngày " + rs.First().NgayPheDuyet.Day + " tháng " + rs.First().NgayPheDuyet.Month + " năm " + rs.First().NgayPheDuyet.Year;
                dicParam["NgayThangNamSinh"] = DungChung.Ham.GhepNgaySinh("/", rs.First().NamSinh, rs.First().ThangSinh, rs.First().NgaySinh);
                dicParam["NgayThang"] = DungChung.Ham.NgaySangChu(System.DateTime.Now, 7);         
                dicParam["ChanDoanTTPD"] = "Chẩn đoán: " + rs.First().ChanDoanTTPD;
                dicParam["TenKP"] = rs.First().TenKP;
                dicParam["SoNgayThem"] = rs.First().SoNgayThem == null? ".....": dicParam["SoNgayThem"] = rs.First().SoNgayThem;
                if (DungChung.Bien.MaBV=="14018")
                dicParam["Ngaythangnam"] = "Ngày....tháng....năm.....";
            }

            DungChung.Ham.Print(DungChung.PrintConfig.rep_GiayPheDuyet_14018, null, dicParam, false);
        }

        private void dPTungay_Validating(object sender, CancelEventArgs e)
        {
            
           /* if (string.IsNullOrEmpty(dPTungay.Text)||string.IsNullOrWhiteSpace(dPTungay.Text))
            {
                e.Cancel = true;
                dPTungay.Focus();
                errorProvider1.SetError(dPTungay, "Bạn chưa điền thông tin!");
            }
            else
            {
                DateTime tungay = Convert.ToDateTime(dPTungay.Text);
                DateTime denngay = Convert.ToDateTime(dPDenngay.Text);
                if(tungay > denngay)
                {
                    dPTungay.Focus();
                    errorProvider1.SetError(dPTungay, "Hướng điều trị từ ngày lớn hơn hướng điều trị đến ngày. Vui lòng nhập lại!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(dPTungay, null);
                }
            }*/
        }
        
        private void dPDenngay_Validating(object sender, CancelEventArgs e)
        {
           /* if (string.IsNullOrEmpty(dPDenngay.Text) || string.IsNullOrWhiteSpace(dPDenngay.Text))
            {
                e.Cancel = true;
                dPDenngay.Focus();
                errorProvider2.SetError(dPDenngay, "Bạn chưa điền thông tin!");
            }
            else
            {
                DateTime tungay = Convert.ToDateTime(dPTungay.Text);
                DateTime denngay = Convert.ToDateTime(dPDenngay.Text);
                if (tungay > denngay)
                {
                    dPDenngay.Focus();
                    errorProvider2.SetError(dPDenngay, "Hướng điều trị đến ngày nhỏ hơn hướng điều trị từ ngày. Vui lòng nhập lại!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider2.SetError(dPDenngay, null);
                }
            }*/
        }

        private void groupControlPheDuyet_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dtNgayPheDuyet_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl10_Click(object sender, EventArgs e)
        {

        }

        private void labelControl12_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
           
        }
    }
}
