using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.ChucNang
{
    public partial class Update_MaDV : DevExpress.XtraEditors.XtraForm
    {
        public Update_MaDV()
        {
            InitializeComponent();
        }
        int _madv_cu=0, _madv_moi=0;
        public Update_MaDV(int madv)
        {
            InitializeComponent();
            _madv_cu = madv;
        }
        private void Update_MaDV_Load(object sender, EventArgs e)
        {
            txtMaCu.Text = _madv_cu.ToString();
        }

        private void btnDoiMa_Click(object sender, EventArgs e)
        {
            int ot;

            if (Int32.TryParse(txtMaMoi.Text, out ot))
                _madv_moi = Convert.ToInt32(txtMaMoi.Text);

          
            if(_madv_moi>0){
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                  
            }
         
    
        }
    }
}