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

namespace QLBV.Forms.Security
{
    public partial class FrmSecurity : XtraForm
    {
        public FrmSecurity()
        {
            InitializeComponent(); 
        }

        public string Decrypt 
        {
            get => txtDecrypt.Text;
            set => txtDecrypt.Text = value;
        }

        public string Encrypt
        {
            get => txtEncrypt.Text;
            set => txtEncrypt.Text = value;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            Encrypt = QLBV.Utilities.Commons.Security.Encrypt(Decrypt);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            Encrypt = QLBV.Utilities.Commons.Security.Decrypt(Decrypt);
        }
    }
}
