using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SoThuThuat_12121_2 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_SoThuThuat_12121_2()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celNgayThang.DataBindings.Add("Text", DataSource, "NgayThang").FormatString = "{0:dd/MM/yyyy}";

            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            cel22.DataBindings.Add("Text", DataSource, "ThuyTriLieu");
            cel23.DataBindings.Add("Text", DataSource, "SongXungKich");
            cel24.DataBindings.Add("Text", DataSource, "SongNgan");
            cel25.DataBindings.Add("Text", DataSource, "HongNgoai");
            cel26.DataBindings.Add("Text", DataSource, "DienTuTruong");
            cel27.DataBindings.Add("Text", DataSource, "KeoGianCotSong");
            cel28.DataBindings.Add("Text", DataSource, "TapXeDap");
            cel29.DataBindings.Add("Text", DataSource, "TapRongRoc");
            cel30.DataBindings.Add("Text", DataSource, "Xoabopcucbo");
            cel31.DataBindings.Add("Text", DataSource, "DienViDongGiamDau");
            cel32.DataBindings.Add("Text", DataSource, "CayChi");

            cel22T.DataBindings.Add("Text", DataSource, "ThuyTriLieu");
            cel23T.DataBindings.Add("Text", DataSource, "SongXungKich");
            cel24T.DataBindings.Add("Text", DataSource, "SongNgan");
            cel25T.DataBindings.Add("Text", DataSource, "HongNgoai");
            cel26T.DataBindings.Add("Text", DataSource, "DienTuTruong");
            cel27T.DataBindings.Add("Text", DataSource, "KeoGianCotSong");
            cel28T.DataBindings.Add("Text", DataSource, "TapXeDap");
            cel29T.DataBindings.Add("Text", DataSource, "TapRongRoc");
            cel30T.DataBindings.Add("Text", DataSource, "Xoabopcucbo");
            cel31T.DataBindings.Add("Text", DataSource, "DienViDongGiamDau");
            cel32T.DataBindings.Add("Text", DataSource, "CayChi");

            GroupHeader1.GroupFields.Add(new GroupField("NgayThang"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lbCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }
    }
}
