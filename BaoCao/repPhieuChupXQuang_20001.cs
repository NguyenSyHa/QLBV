using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuChupXQuang_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChupXQuang_20001()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
            {
                xrTable5.Visible = true;
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
            }
            else if(DungChung.Bien.MaBV == "24009")
            {
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
            }
            if (DungChung.Bien.MaBV == "20001")
                colTenCQCQ.Font = new Font("Times New Roman", 10f, FontStyle.Regular);
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "24009") {
                this.colKQCC.Font = new System.Drawing.Font("Times New Roman", 13F);
                this.colYCCC.Font = new System.Drawing.Font("Times New Roman", 13F);
            }
            if (DungChung.Bien.MaBV == "30009")
                colSo.Visible = false;
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
            if (MaCBDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "02005")
            {
                rowBSDieuTri.Visible = false;
            }

            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;
        }
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
                lab55.Text = "BÁC SỸ ĐỌC KẾT QUẢ";
        }
    }
}
