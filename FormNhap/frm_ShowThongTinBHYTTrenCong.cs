using DevExpress.DataAccess.Native.Sql.MasterDetail;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frm_ShowThongTinBHYTTrenCong : DevExpress.XtraEditors.XtraForm
    {
        public frm_ShowThongTinBHYTTrenCong(string Messshow, int status)
        {
            InitializeComponent();

            _Messshow = Messshow;
            _status = status;
        }

        string _Messshow = "";
        int _status = 0;

        private void frm_ShowThongTinBHYTTrenCong_Load(object sender, EventArgs e)
        {
            if (_status == 0)
            {
                picWarming.Visible = true;
                this.mmMesshow.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            }
            else
            {
                picWarming.Visible = true;
                this.mmMesshow.Appearance.ForeColor = System.Drawing.Color.Red;
            }
            mmMesshow.Text = _Messshow;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picWarming_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}