using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;
using System.Data.OleDb;

namespace QLBV.FormDanhMuc
{
    public partial class frm_UpdateGiaDV : DevExpress.XtraEditors.XtraForm
    {
        public frm_UpdateGiaDV()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        List<DichVu> _ldv = new List<DichVu>();
        void TimKiem(string tendv) {
           
            _ldv = _data.DichVus.Where(p => p.PLoai == 2).Where(p=>p.TenDV.Contains(tendv)).OrderBy(p => p.TenDV).ToList();
            grcDichVu.DataSource = _ldv;
            colDonGia.DisplayFormat.FormatString = "n2";
            colDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            colDonGia2.DisplayFormat.FormatString = "n2";
            colDonGia2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
        }
        private void frm_UpdateGiaDV_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        TimKiem("");
            
        }

        private void txtTimkiem_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem(txtTimkiem.Text); 
        }

        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    _data.SaveChanges();
        //}
        public static DataTable OpenFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Không tìm thấy tập tin.");
                return null;
            }
            var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", filePath);
            var adapter = new OleDbDataAdapter("select * from [Sheet1$]", connectionString);
            var ds = new DataSet();
            string tableName = "VSSOFT";
            adapter.Fill(ds, tableName);
            DataTable data = ds.Tables[tableName];
            return data;
        }
       
        private void btnChonFile_Click(object sender, EventArgs e)
        {
            string fileName ="";
            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = false;
            op.Filter = "XLS (*.xls)|*.xls|(*.xlsx)|*.xlsx";
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                fileName = op.FileName;
            }
            DataTable data = OpenFile(fileName);
            List<DonGiaDV> _lDonGia = new List<DonGiaDV>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (i > 1)
                {
                    DonGiaDV dg = new DonGiaDV();
                   // dg.Check = false;
                    dg.MaQD = data.Rows[i][1].ToString();
                    dg.DonGia = data.Rows[i][5] ==DBNull.Value ?0:  Convert.ToDouble(data.Rows[i][5]);
                    dg.DonGia2 = data.Rows[i][4] == DBNull.Value ? 0 : Convert.ToDouble(data.Rows[i][4]);
                    dg.TenDV = data.Rows[i][2].ToString();
                    if (dg.DonGia == 0 || dg.DonGia == dg.DonGia2)
                        dg.Check = false;
                    else
                        dg.Check = true;
                    _lDonGia.Add(dg);
                }
            }
            grcExCell.DataSource = _lDonGia;
            gridColumn7.DisplayFormat.FormatString = "n2" ;
            gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridColumn8.DisplayFormat.FormatString = "n2";
            gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //data.Columns.Add("Check", typeof(bool));
            //foreach (DataRow row in data.Rows)
            //{
            //    row["Check"] = true;
            //}

            //    grcExCell.DataSource = data;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
                   
            _data.SaveChanges();
            btnLuu.Enabled = false;
        }
        private class DonGiaDV
        {
            private bool check;

            public bool Check
            {
                get { return check; }
                set { check = value; }
            }

            private int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            private string maQD;

            public string MaQD
            {
                get { return maQD; }
                set { maQD = value; }
            }
            private string soDK;

            public string SoDK
            {
                get { return soDK; }
                set { soDK = value; }
            }
            private string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            private double donGia;

            public double DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            private double donGia2;

            public double DonGia2
            {
                get { return donGia2; }
                set { donGia2 = value; }
            }

        }

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAll.Checked)
            {
                for (int i = 0; i < grvExCell.RowCount; i++)
                {
                    //if (grvExCell.GetRowCellValue(i, gridColumn7) != null ) // cột giá TT phải có 
                    //{
                    //    if (grvExCell.GetRowCellValue(i, gridColumn8) == null || Convert.ToDouble(grvExCell.GetRowCellValue(i, gridColumn8)) ==0 )// giá dịch vụ =0 hoặc null
                    //        grvExCell.SetRowCellValue(i, colCheck, true);
                    //    else if( Convert.ToDouble(grvExCell.GetRowCellValue(i, gridColumn7)) !=  Convert.ToDouble(grvExCell.GetRowCellValue(i, gridColumn8))) // giá dịch vụ bằng giá BH
                    //    { 
                    //       grvExCell.SetRowCellValue(i, colCheck, true);
                    //    }
                    //}
                    grvExCell.SetRowCellValue(i, colCheck, true);
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {

            List<DonGiaDV> _lmaDV = new List<DonGiaDV>();
            int count = 0;
            for (int i = 0; i < grvExCell.RowCount; i++)
            {
                if (grvExCell.GetRowCellValue(i, colCheck) != null && Convert.ToBoolean(grvExCell.GetRowCellValue(i, colCheck)) == true)
                {
                    if ((grvExCell.GetRowCellValue(i, gridColumn4) != null) && (grvExCell.GetRowCellValue(i, gridColumn7) != null))
                    {
                        DonGiaDV dg = new DonGiaDV();
                        dg.MaQD = grvExCell.GetRowCellValue(i, gridColumn4).ToString();
                        dg.DonGia = Convert.ToDouble(grvExCell.GetRowCellValue(i, gridColumn7));
                        var qdv = _ldv.Where(p => p.MaQD == dg.MaQD).ToList();
                        _lmaDV.Add(dg);
                    }
                }
            }
            if (_lmaDV.Count > 0)
            {
                foreach (var dg in _lmaDV)
                {
                    foreach (var item in _ldv)
                    {
                        if (item.MaDV == dg.MaDV)
                        {
                            item.DonGia = dg.DonGia;
                            count++;
                        }
                    }
                }
            }
            grcDichVu.DataSource = _ldv;
            if(count>0)
              btnLuu.Enabled = true;
            else
                MessageBox.Show("Không tìm thấy dịch vụ tương ứng với các dịch vụ đã chọn");
            ////_data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            ////for (int i = 0; i < grvExCell.RowCount; i++)
            ////{
            ////    if (grvExCell.GetRowCellValue(i, colCheck) != null && Convert.ToBoolean(grvExCell.GetRowCellValue(i, colCheck)) == true)
            ////    {
            ////        if ((grvExCell.GetRowCellValue(i, colMaDV) != null) && (grvExCell.GetRowCellValue(i, colDonGia) != null))
            ////        {
                       
            ////            int maDV= Convert.ToInt32(grvExCell.GetRowCellValue(i, colMaDV));
            ////            double dongia = Convert.ToDouble(grvExCell.GetRowCellValue(i, colDonGia));
            ////            var qdv = _data.DichVus.Where(p => p.MaDV ==maDV).ToList();
            ////            if (qdv.Count > 0)
            ////                qdv.First().DonGia = dongia;
            ////        }
            ////    }
            ////}
           
           
           
        }

        private void frm_UpdateGiaDV_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnLuu.Enabled)
            {
                DialogResult dr = MessageBox.Show("Bạn chưa lưu giá mới, bạn có muốn lưu không.", "Thông báo", MessageBoxButtons.YesNo,
         MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    btnLuu_Click(null, null);
                }
            }
        }

        private void cboFomat_TextChanged(object sender, EventArgs e)
        {
            colDonGia.DisplayFormat.FormatString = "n2";// cboFomat.Text;
            colDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            colDonGia2.DisplayFormat.FormatString = "0;0,0.000;-0,0.000;#";// cboFomat.Text;
            colDonGia2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridColumn7.DisplayFormat.FormatString = cboFomat.Text;
            gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridColumn8.DisplayFormat.FormatString = cboFomat.Text;
            gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
        }
    }
}