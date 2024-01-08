using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.ChucNang
{
    public partial class frm_DS_HSSK_Error : Form
    {
        public frm_DS_HSSK_Error(QLBV.ChucNang.frm_GuiHSSK.BenhNhanADO ado)
        {
            InitializeComponent();
            txtError.Text = ado.Error;
            groupControlError.Text = "Bệnh nhân: " + ado.TenBNhan;
        }
        public frm_DS_HSSK_Error(QLBV.ChucNang.frm_GuiChungTu_BHYT.BenhNhanADO ado)
        {
            InitializeComponent();
            txtError.Text = ado.Error;
            groupControlError.Text = "Bệnh nhân: " + ado.TenBNhan;
        }
    }
}
