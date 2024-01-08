using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BkeVPBNhanRaVien_30005 : DevExpress.XtraReports.UI.XtraReport
    {
        int id = -1;
        public rep_BkeVPBNhanRaVien_30005(int _id)
        {
            InitializeComponent();
            id = _id;
        }


        internal void BindingData()
        {
            if (id==0)
            celNgay.DataBindings.Add("Text", DataSource, "Ngayra").FormatString = "{0: dd/MM/yyyy}";
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            celDiachi.DataBindings.Add("Text", DataSource, "DChi");           
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celTamUng.DataBindings.Add("Text", DataSource, "TamUng").FormatString = DungChung.Bien.FormatString[1];
            //celTienBN.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            //celTienThua.DataBindings.Add("Text", DataSource, "TraLai").FormatString = DungChung.Bien.FormatString[1];
            //celNopthem.DataBindings.Add("Text", DataSource, "NopThem").FormatString = DungChung.Bien.FormatString[1];

            celTamUngG.DataBindings.Add("Text", DataSource, "TamUng").FormatString = DungChung.Bien.FormatString[1];
            //celTienBNG.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            //celTienThuaG.DataBindings.Add("Text", DataSource, "TraLai").FormatString = DungChung.Bien.FormatString[1];
            //celNopthemG.DataBindings.Add("Text", DataSource, "NopThem").FormatString = DungChung.Bien.FormatString[1];

            celTamUngR.DataBindings.Add("Text", DataSource, "TamUng").FormatString = DungChung.Bien.FormatString[1];
            //celTienBNR.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            //celTienThuaR.DataBindings.Add("Text", DataSource, "TraLai").FormatString = DungChung.Bien.FormatString[1];
            //celNopthemR.DataBindings.Add("Text", DataSource, "NopThem").FormatString = DungChung.Bien.FormatString[1];


            if (id == 0)
            GroupHeader1.GroupFields.Add(new GroupField("Ngayra"));

        }
    }
}
