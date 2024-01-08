using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SieuAmDopplerXuyenSoMau : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SieuAmDopplerXuyenSoMau()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void txtSoYTe_BeforePrint(object sender, CancelEventArgs e)
        {
            txtSoYTe.Text = DungChung.Bien.TenCQCQ;
            txtBenhVien.Text = DungChung.Bien.TenCQ;
            string _macbdt="";
            string _macdth="";
            if(MaCBDT.Value!=null && DungChung.Bien.MaBV != "02005")
            {_macbdt=MaCBDT.Value.ToString();}
            if(MaCBTH.Value!=null)
            {_macdth=MaCBTH.Value.ToString();}
            if (MaCBDT.Value!=null)
            { colTenBSDT.Text = DungChung.Ham._getTenCB(_Data, MaCBDT.Value.ToString()); }
            if (MaCBTH.Value!=null)
            { colTenTKXN.Text = DungChung.Ham._getTenCB(_Data, MaCBTH.Value.ToString()); }

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
        }

    }
}
