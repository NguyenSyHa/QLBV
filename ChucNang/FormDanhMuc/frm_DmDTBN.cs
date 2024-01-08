using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormNhap
{
    public partial class frm_DmDTBN : DevExpress.XtraEditors.XtraForm
    {
        public frm_DmDTBN()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        bool luu;
        public bool kiemtra()
        {
            if (txtdoituong.Text == "" || txtdoituong.Text == null || txtmota.Text == "" || txtmota.Text == null | cbo_Status.Text == "" || cbo_Status.Text == null || txtid.Text == "" || txtid.Text == null)
            {
                MessageBox.Show("Chưa nhập thông tin đầy đủ", "Thông báo");
                return false;

            }
            else
            {
                return true;
            }

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        public void hamloatdulieu()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lDTBN = data.DTBNs.ToList();
            binS_DTBN.DataSource = _lDTBN.OrderBy(p => p.DTBN1);
            danhsachdoituong.DataSource = _lDTBN;
            //  danhsachdoituong.DataSource = dt;
        }
        List<c_HTTT> _lhttt = new List<c_HTTT>();
        List<DTBN> _lDTBN = new List<DTBN>();
        private void frm_nhapdtbt_Load(object sender, EventArgs e)
        {
            _lhttt.Add(new c_HTTT { Ten = "Miễn", _HTTT = 0 });
            _lhttt.Add(new c_HTTT { Ten = "BHYT", _HTTT = 1 });
            _lhttt.Add(new c_HTTT { Ten = "Thanh toán toàn bộ", _HTTT = 2 });
            _lhttt.Add(new c_HTTT { Ten = "Khám sức khỏe", _HTTT = 3});
            lup_HTTT.Properties.DataSource = _lhttt;
            hamloatdulieu();


        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridView1.GetFocusedRowCellValue(idDT) != null)
                {
                    txtid.Text = gridView1.GetFocusedRowCellValue(idDT).ToString();
                    int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue(idDT).ToString());
                    // txtmota.Text = _lDTBN.Where(p => p.IDDTBN == id).First().MoTa;
                    txtdoituong.Text = gridView1.GetFocusedRowCellValue(DT).ToString();
                    txtmota.Text = gridView1.GetFocusedRowCellValue(mota).ToString();

                    if (gridView1.GetFocusedRowCellValue("HTTT") != null)
                    {
                        lup_HTTT.EditValue = Convert.ToByte(gridView1.GetFocusedRowCellValue("HTTT"));
                    }
                    else
                        lup_HTTT.EditValue = -1;
                    if (gridView1.GetFocusedRowCellValue("Status") != null)
                    {
                        cbo_Status.SelectedIndex = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Status"));
                    }
                    else
                    {
                        cbo_Status.SelectedIndex = -1;
                    }
                }
                else {
                    txtdoituong.Text = "";
                    txtmota.Text = "";
                    cbo_Status.SelectedIndex = -1;
                    lup_HTTT.EditValue = -1;
                    txtid.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Lỗi gridView1_FocusedRowChanged ");
            }

        }

        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {

            gridView1_FocusedRowChanged(sender, null);
        
        }
        public void hamsaulu(bool a)
        {

            lup_HTTT.Properties.ReadOnly = a;
            txtid.Properties.ReadOnly = a;
            txtdoituong.Properties.ReadOnly = a;
            txtmota.Properties.ReadOnly = a;
            cbo_Status.Properties.ReadOnly = a;

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (DungChung.Ham.checkQuyen(this.Name)[0])
            {
                luu = true;
                luusua.Enabled = true;
                txtdoituong.Text = "";
                txtmota.Text = "";
                cbo_Status.SelectedIndex = -1;
                lup_HTTT.EditValue = -1;
                txtid.Text = "";
                lup_HTTT.Properties.ReadOnly = false;
                txtid.Properties.ReadOnly = false;
                txtdoituong.Properties.ReadOnly = false;
                txtmota.Properties.ReadOnly = false;
                cbo_Status.Properties.ReadOnly = false;
                sua1111.Enabled = false;
                danhsachdoituong.Enabled = false;
            }
        }
        public class c_HTTT
        {
            private byte httt;
            private string ten;
            public byte _HTTT
            {
                set { httt = value; }
                get { return httt; }
            }
            public string Ten
            {
                set { ten = value; }
                get { return ten; }
            }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (luu == true && kiemtra() == true)
                {

                    DTBN doituong = new DTBN();
                    doituong.MoTa = txtmota.Text.Trim();
                    doituong.DTBN1 = txtdoituong.Text.Trim();
                    doituong.IDDTBN = Convert.ToByte(txtid.Text);
                    doituong.Status = cbo_Status.SelectedIndex;
                    doituong.HTTT = Convert.ToByte(lup_HTTT.EditValue);
                    data.DTBNs.Add(doituong);
                    data.SaveChanges();
                    MessageBox.Show("Thêm Thành công", "thông báo");
                    hamloatdulieu();
                    them.Enabled = true;
                    sua1111.Enabled = true;
                    danhsachdoituong.Enabled = true;
                    hamsaulu(true);
                }
                else if (luu == false && kiemtra() == true)
                {

                    byte a = Convert.ToByte(txtid.Text);
                    var sua = (from h in data.DTBNs.Where(p => p.IDDTBN == a) select h).ToList();
                    foreach (var h in sua)
                    {
                        h.MoTa = txtmota.Text.Trim();
                        h.DTBN1 = txtdoituong.Text.Trim();
                        h.Status = cbo_Status.SelectedIndex;
                        h.HTTT = Convert.ToByte(lup_HTTT.EditValue);
                        data.SaveChanges();
                    }
                    MessageBox.Show("Sửa thành công", "thông báo");
                    hamloatdulieu();
                    them.Enabled = true;
                    sua1111.Enabled = true;
                    danhsachdoituong.Enabled = true;
                    hamsaulu(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu ", "thông báo: " + ex.Message);

            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (DungChung.Ham.checkQuyen(this.Name)[1])
            {
                luusua.Enabled = true;
                luu = false;
                lup_HTTT.Properties.ReadOnly = false;
                txtid.Properties.ReadOnly = true;
                txtdoituong.Properties.ReadOnly = false;
                txtmota.Properties.ReadOnly = false;
                cbo_Status.Properties.ReadOnly = false;
                them.Enabled = false;
                danhsachdoituong.Enabled = false;
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            if (DungChung.Ham.checkQuyen(this.Name)[2])
            {
                byte a = Convert.ToByte(txtid.Text);
                var kiemtra = from k in data.BenhNhans.Where(p => p.IDDTBN == a) select k;
                if (kiemtra.Count() == 0)
                {
                    var xoa = data.DTBNs.Single(p => p.IDDTBN == a);
                    data.DTBNs.Remove(xoa);
                    data.SaveChanges();
                    MessageBox.Show("xóa thành công", "Thông báo");
                    //  var dt = from h in data.DTBNs select new { h.IDDTBN, h.DTBN1, h.Status, h.MoTa };
                    List<DTBN> _dt = new List<DTBN>();
                    _dt = data.DTBNs.ToList();
                    binS_DTBN.DataSource = _dt;
                    danhsachdoituong.DataSource = binS_DTBN.DataSource;
                    gridView1_DataSourceChanged(sender, e);
                }
                else
                {

                    MessageBox.Show("Đối tượng đã sửa dụng không được xóa", "Thông báo");

                }
            }

        }


    }
}