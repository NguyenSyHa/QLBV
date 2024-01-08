using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_DanhsachKSKLaiXe_54 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_DanhsachKSKLaiXe_54()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        internal void databinding()
        {
            celHoten.DataBindings.Add("Text", DataSource, "TenBNhan");
            celNamsinh.DataBindings.Add("Text", DataSource, "NamSinh");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celNgayKham.DataBindings.Add("Text", DataSource, "Ngay").FormatString = "{0:dd/MM/yyyy}";
            celDuSK.DataBindings.Add("Text", DataSource, "duSK");
            celKDuSK.DataBindings.Add("Text", DataSource, "KduSK");
        }
    }
}
