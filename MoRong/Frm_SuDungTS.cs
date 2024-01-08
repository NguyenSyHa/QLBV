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
    public partial class Frm_SuDungTS : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SuDungTS()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
         private bool KTLuu()
        {
            if (lupNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày tháng ");
                lupNgay.Focus();
                return false;
            }
            if (lupKhoa.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Khoa phòng");
                lupKhoa.Focus();
                return false;
            }
           if (lupCB.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Cán bộ");
                lupCB.Focus();
                return false;
            }
            return true;
        }
        private void Enablebutton(bool Status)
        {
            btnLuu.Enabled = Status;
          
        }
        private void Frm_SuDungTS_Load(object sender, EventArgs e)
        {

            lupNgay.DateTime = System.DateTime.Now;
            var qkp = from TK in DataContect.KPhongs select new { TK.TenKP, TK.MaKP };
            if (qkp.Count() > 0)
            {
                lupKhoa.Properties.DataSource = qkp.ToList();
            }
            //var qcb = from CB in DataContect.CanBoes select new { CB.TenCB,CB.MaCB};
            //if (qcb.Count() > 0)
            //{

            //    lupCB.EditValue = qcb.ToList();
            //} 
            btnLuu.Enabled = true;
         
        }
        List<SDT> _sdts = new List<SDT>();
        private void btnLuu_Click(object sender, EventArgs e)
        {
        }

        private void lupKhoa_EditValueChanged(object sender, EventArgs e)
        {
            int _makp=0;
            if (lupKhoa.EditValue != null )
            {
                _makp = Convert.ToInt32( lupKhoa.EditValue);
                var q = from cb in DataContect.CanBoes.Where(p => p.MaKP == _makp) select new { cb.TenCB, cb.MaCB };
                lupCB.Properties.DataSource = q;
            }
        }
        

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInPhieu_Click_1(object sender, EventArgs e)
        {
            FormThamSo.Frm_BcSuDungTS frm = new FormThamSo.Frm_BcSuDungTS();
            frm.ShowDialog();
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
               if (KTLuu())
                {
                    SDT nhapsdts = new SDT();
                    //nhapsdts.Ngay = lupNgay.Text;
                    if (lupNgay.EditValue != null)
                    nhapsdts.Ngay = lupNgay.DateTime; 
                    if(lupKhoa.EditValue!=null)
                        nhapsdts.MaKP = lupKhoa.EditValue == null ?0 : Convert.ToInt32 (lupKhoa.EditValue);
                    if (lupCB.EditValue != null)
                        nhapsdts.MaCB = lupCB.EditValue.ToString();
                    if (cmbHT.Text != null && cmbHT.Text != "")
                        nhapsdts.PLoai = cmbHT.Text;
                    nhapsdts.NoiDung = txtNoiDung.Text;
                    DataContect.SDTs.Add(nhapsdts);
                    DataContect.SaveChanges();
                    MessageBox.Show("Lưu thành công!");
                    btnLuu.Enabled = false;
                }
            
        }

       
    }
}