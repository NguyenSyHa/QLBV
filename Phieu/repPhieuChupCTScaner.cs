using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Nam_0994
{
    public partial class repPhieuChupCTScaner : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChupCTScaner()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            //tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            //tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "24009")
            {
                this.mt.Font = new System.Drawing.Font("Times New Roman", 13F);
                this.colYCCC.Font = new System.Drawing.Font("Times New Roman", 13F);
            }
            if (DungChung.Bien.MaBV == "30009")
                colSo.Visible = false;
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
            if (DungChung.Bien.MaBV == "02005")
            {
                rowBSDieuTri.Visible = false;
            }
        }


    }
}
