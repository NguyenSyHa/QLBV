using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.Phieu.TamThanThaiBinh
{
    public partial class repPhieuKetQua_CDHA_34019_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuKetQua_CDHA_34019_A4()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            colYeuCau.DataBindings.Add("Text", DataSource, "TenDV");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void repPhieuKetQua_CDHA_34019_A4_BeforePrint(object sender, CancelEventArgs e)
        {
            if (CLS.InPhieu.InTiep)
            {
                tb_Info.Visible = false;
                tb_ChiDinh.Visible = false;
                xrTable1.Visible = false;
                if (DungChung.Bien.MaBV == "34019")
                {
                    xrTableCell1.Text = "KẾT QUẢ";

                }
                else xrTableCell1.Text = "KẾT QUẢ CHIẾU/CHỤP";

                tb_KetQua.TopF = tb_KetQua.TopF + xrTable1.HeightF + this.ReportHeader.HeightF + this.Margins.Top + this.Margins.Bottom;
                label1.TopF = label1.TopF + xrTable1.HeightF + this.ReportHeader.HeightF + this.Margins.Top + this.Margins.Bottom;
                label2.TopF = label2.TopF + xrTable1.HeightF + this.ReportHeader.HeightF + this.Margins.Top + this.Margins.Bottom;
                label3.TopF = label3.TopF + xrTable1.HeightF + this.ReportHeader.HeightF + this.Margins.Top + this.Margins.Bottom;
                label4.TopF = label4.TopF + xrTable1.HeightF + this.ReportHeader.HeightF + this.Margins.Top + this.Margins.Bottom;

                tb_KetQua.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
            }
            else
            {
                tb_Info.Visible = DungChung.Bien._Visible_CDHA[0];
                tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
                xrTable1.Visible = DungChung.Bien._Visible_CDHA[0];
                if (DungChung.Bien.MaBV == "34019")
                {
                    xrTableCell1.Text = "KẾT QUẢ";

                }
                else xrTableCell1.Text = "KẾT QUẢ CHIẾU/CHỤP";

                if (!DungChung.Bien._Visible_CDHA[0])
                {
                    tb_KetQua.TopF = tb_KetQua.TopF + xrTable1.HeightF + this.ReportHeader.HeightF + this.Margins.Top + this.Margins.Bottom;
                    label1.TopF = label1.TopF + xrTable1.HeightF + this.ReportHeader.HeightF + this.Margins.Top + this.Margins.Bottom;
                    label2.TopF = label2.TopF + xrTable1.HeightF + this.ReportHeader.HeightF + this.Margins.Top + this.Margins.Bottom;
                    label3.TopF = label3.TopF + xrTable1.HeightF + this.ReportHeader.HeightF + this.Margins.Top + this.Margins.Bottom;
                    label4.TopF = label4.TopF + xrTable1.HeightF + this.ReportHeader.HeightF + this.Margins.Top + this.Margins.Bottom;
                }

                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
                label1.Visible = DungChung.Bien._Visible_CDHA[1];
                label2.Visible = DungChung.Bien._Visible_CDHA[1];
                label3.Visible = DungChung.Bien._Visible_CDHA[1];
                label4.Visible = DungChung.Bien._Visible_CDHA[1];
            }
        }

    }
}
