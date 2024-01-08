using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq;
using System.Data.SqlClient;
namespace QLBV.FormNhap
{
    public partial class frm_xoadulieugiac : DevExpress.XtraEditors.XtraForm
    {
        public frm_xoadulieugiac()
        {
            InitializeComponent();
        }
        public static string doingay(DateTime h) 
        {
            string ngay = h.Day.ToString();
            string thang = h.Month.ToString();
            string nam = h.Year.ToString();
            string den = nam +"-"+ thang + "-"+ ngay;
            return den;
        
        }
        static string kt = @"Data Source="+DungChung.Bien.TenServer+";Initial Catalog="+DungChung.Bien.TenCSDL+";User ID=sa;Password="+DungChung.Bien.passql+"";
        SqlCommand xoa;
        SqlCommand xoa1;
        SqlCommand xoa2;
        SqlConnection ketnoi = new SqlConnection(kt);
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
                if (radiodonthuoc.Checked == true)
                {
                    DataTable dulieuxoa = new DataTable();
                    DataTable dulieuxoa1 = new DataTable();
                    string lenhxoadthuoc = @"delete DThuoc where DThuoc.MaBNhan not in (select BenhNhan.MaBNhan from BenhNhan)";
                    string lenhxoadthuocct = @"delete DThuocct where DThuocct.IDDon not in (select DThuoc.IDDon from DThuoc)";
                    ketnoi.Open();
                    xoa1 = new SqlCommand("select IDDon from dthuoc where DThuoc.MaBNhan not in (select BenhNhan.MaBNhan from BenhNhan)", ketnoi);
                    dulieuxoa.Load(xoa1.ExecuteReader());
                    xoa = new SqlCommand(lenhxoadthuoc, ketnoi);
                    xoa.ExecuteNonQuery();
                    xoa2 = new SqlCommand("select IDDonct from dthuocct where DThuocct.IDDon not in (select DThuoc.IDDon from DThuoc)", ketnoi);
                    dulieuxoa1.Load(xoa2.ExecuteReader());
                    xoa = new SqlCommand(lenhxoadthuocct, ketnoi);
                    xoa.ExecuteNonQuery();
                    XtraMessageBox.Show("có " + dulieuxoa.Rows.Count + " bản ghi trong đơn thuốc đã xóa : có " + dulieuxoa1.Rows.Count + " bản ghi đã xóa trong đơn thuốc chi tiết ", "Thông báo");
                    ketnoi.Close();
                }
                else
                {
                    if (denngay.EditValue == null)
                    {
                        string a = doingay(denngay.DateTime);
                        ketnoi.Open();
                        xoa = new SqlCommand(@"delete BenhNhan where BenhNhan.MaBNhan in ( select BenhNhan.MaBNhan from BenhNhan where BenhNhan.NoiTru=1 and convert(date,BenhNhan.NNhap,0)<='" + a + "' and  BenhNhan.MaBNhan not in  (select VienPhi.MaBNhan from VienPhi) and BenhNhan.MaBNhan not in (select RaVien.MaBNhan from RaVien))", ketnoi);
                        xoa.ExecuteNonQuery();
                        xoa = new SqlCommand(@"delete VaoVien where VaoVien.MaBNhan in (select  vaoVien.MaBNhan from VaoVien where vaoVien.MaBNhan not in (select BenhNhan.MaBNhan from BenhNhan) and VaoVien.MaBNhan not in (select VienPhi.MaBNhan from VienPhi) and VaoVien.MaBNhan not in (select RaVien.MaBNhan from RaVien))", ketnoi);
                        xoa.ExecuteNonQuery();
                        xoa = new SqlCommand(@"delete BNKB where BNKB.MaBNhan in (select  BNKB.MaBNhan from BNKB where BNKB.MaBNhan not in (select BenhNhan.MaBNhan from BenhNhan) and BNKB.MaBNhan not in (select VienPhi.MaBNhan from VienPhi) and BNKB.MaBNhan not in (select RaVien.MaBNhan from RaVien))", ketnoi);
                        xoa.ExecuteNonQuery();
                        xoa = new SqlCommand(@"delete CLS where CLS.MaBNhan in ( select  CLS.MaBNhan from CLS where CLS.MaBNhan not in (select BenhNhan.MaBNhan from BenhNhan) and CLS.MaBNhan not in (select VienPhi.MaBNhan from VienPhi) and CLS.MaBNhan not in (select RaVien.MaBNhan from RaVien))", ketnoi);
                        xoa.ExecuteNonQuery();
                        xoa = new SqlCommand(@"delete CLSct where CLSct.IdCLS not in (select CLS.IdCLS from CLS)", ketnoi);
                        xoa.ExecuteNonQuery();
                        xoa = new SqlCommand(@"delete ChiDinh where ChiDinh.IdCLS not in (select CLS.IdCLS from CLS)", ketnoi);
                        xoa.ExecuteNonQuery();
                        ketnoi.Close();
                        XtraMessageBox.Show("xóa thành công ", "Thông báo");
                        simpleButton2_Click(sender, e);
                    }
                    else {

                        XtraMessageBox.Show("Bạn chưa chọn ngày", "Thông Báo");
                    
                    }
                  
                }
            }
       
          
        

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) 
            {
                denngay.Enabled = true;
                benhnhan.Enabled = true;
                Xem.Enabled = true;
                benhnhan.Show();
                this.Size = new System.Drawing.Size(379, 324);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {   
            string a = doingay(denngay.DateTime);
            string lenhhienthibenhnhan = @"select BenhNhan.MaBNhan,BenhNhan.TenBNhan from BenhNhan where BenhNhan.NoiTru=1 and convert(date,BenhNhan.NNhap,0)<='"+a+"' and  BenhNhan.MaBNhan not in  (select VienPhi.MaBNhan from VienPhi) and BenhNhan.MaBNhan not in (select RaVien.MaBNhan from RaVien)";
            SqlDataAdapter bb = new SqlDataAdapter(lenhhienthibenhnhan,ketnoi);
            DataTable bang = new DataTable();
            bb.Fill(bang);
            benhnhan.DataSource = bang;
            sobenhnhan.Text = "có:" + bang.Rows.Count.ToString() + " bênh nhân"; 
           
          
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false)
            {
                denngay.Enabled = false;
                benhnhan.Enabled = false;
                Xem.Enabled = false;
                benhnhan.Hide();
                this.Size = new System.Drawing.Size(379, 120);
            }
        }

        private void frm_xoadulieugia_Load(object sender, EventArgs e)
        {
            radioButton2_Click(sender,e);
        }
    }
}