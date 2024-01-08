using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCThuPhanUngThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCThuPhanUngThuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities context = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            col_BatDauThu.DataBindings.Add("Text", DataSource, "BatDau").FormatString = "{0:dd/MM HH:mm }";
            col_TenThuoc.DataBindings.Add("Text", DataSource, "TenThuoc");
            col_PhuongPhapThu.DataBindings.Add("Text", DataSource, "PhuongPhap");
            col_BacSiChiDinh.DataBindings.Add("Text", DataSource, "BacSiChiDinh");
            col_NguoiThu.DataBindings.Add("Text", DataSource, "NguoiThu");
            col_BacSiDocKiemTra.DataBindings.Add("Text", DataSource, "BacSiDoc");
            col_KQ.DataBindings.Add("Text", DataSource, "KetQua");
            col_GioPhutDoc.DataBindings.Add("Text", DataSource, "KetThuc").FormatString = "{0:dd/MM HH:mm }";
            if (GioiTinh.Value.ToString().Equals("1"))
            {
                lab_Nu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else
            {
                lab_Nam.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lab_tencqcq.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_tencq.Text = DungChung.Bien.TenCQ.ToUpper();
            //lab_Maso.Text=DungChung.Bien.
            
        }

    }
}
