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
    public partial class frm_UpdateStatus : DevExpress.XtraEditors.XtraForm
    {
       
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_UpdateStatus()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ServiceReference_stt.Vssoft_serviceSoapClient s = new ServiceReference_stt.Vssoft_serviceSoapClient();
            DataSet ds=new DataSet();
           ds= s.LayDanhSachBN(@"manhcuong-pc\sqlexpress","QLBV_12121","sa","123456","8",10);
           DataTable dt = new DataTable();
           dt = ds.Tables[0];
            foreach(DataRow dr in dt.Rows)
           MessageBox.Show(dr[0].ToString());
        }

        private void frm_UpdateStatus_Load(object sender, EventArgs e)
        {
            memoEdit1.Text = "Đối với giao diện từ ngày 10/05/2015 đã thay đổi chức năng tìm kiếm trên form Khám Bệnh và Form Điều Trị nên cần phải update lại chức năng tìm kiếm mới đúng \n Mục đích của việc update là thay đổi giá trị status trong bảng BNhan thành 3 đối với những Bệnh Nhân đã thanh toán"+
            "cách 1: thao tác trên form: click'Update' \n cách 2: thao tác trên sql:"+
            "\n UPDATE    BenhNhan SET  Status = 3 FROM BenhNhan INNER JOIN  VienPhi ON BenhNhan.MaBNhan = VienPhi.MaBNhan WHERE (BenhNhan.Status <> 3)"+
            "\n \n Update BenhNhan SET Status = 2 from (	select distinct RaVien.MaBNhan from RaVien	LEFT JOIN VienPhi ON RaVien.MaBNhan = VienPhi.MaBNhan	WHERE VienPhi.MaBNhan is null) c where BenhNhan.MaBNhan = c.MaBNhan AND BenhNhan.Status <> 2" ;
        }
    }
}