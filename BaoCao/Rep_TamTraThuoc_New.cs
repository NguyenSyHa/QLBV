﻿using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class Rep_TamTraThuoc_New : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TamTraThuoc_New()
        {
            InitializeComponent();
        }
        List<FormThamSo.Frm_TamTraThuoc_New.c_DSThuoc> _lDichVu = new List<FormThamSo.Frm_TamTraThuoc_New.c_DSThuoc>();
        List<FormThamSo.Frm_TamTraThuoc_New.c_BN_Thuoc> _lSoLuong = new List<FormThamSo.Frm_TamTraThuoc_New.c_BN_Thuoc>();
        public Rep_TamTraThuoc_New(List<FormThamSo.Frm_TamTraThuoc_New.c_DSThuoc> lDichVu, List<FormThamSo.Frm_TamTraThuoc_New.c_BN_Thuoc> lSoLuong)
        {
            InitializeComponent();
            this._lDichVu = lDichVu;
            this._lSoLuong = lSoLuong;
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
         

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtSoty.Text = txtSoty2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtBV.Text = txtBV2.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30009")
            {
                colYTHC.Text = "Điều dưỡng hành chính";
            }
            // dữ liệu
            for (int i = 0; i < _lDichVu.Count; i++)
            {
                foreach (XRTableCell _tableCell in xrTableRow3)
                {
                    //System.Windows.Forms.MessageBox.Show(_tableCell.Name);

                    if (_tableCell.Name == "colT" + (i + 1).ToString())
                    {
                        int _madv = 0;
                        if (_lDichVu.Skip(i).ToList().Count > 0)
                            _madv = _lDichVu.Skip(i).First().MaDV;
                        double _soluong = 0;
                        _soluong = _lSoLuong.Where(p => p.MaDV == _madv).Sum(p => p.SoLuong);
                        if (_soluong != 0)
                            _tableCell.Text = _soluong.ToString();
                    }
                }

            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < _lDichVu.Count; i++)
            {
                foreach (XRTableCell _tableCell in xrTableRow1)
                {
                    if (_tableCell.Name == "TEN" + (i + 1).ToString())
                    {
                        string _tendv = _lDichVu.Skip(i).First().TenDV;
                        string _donvi = _lDichVu.Skip(i).First().DonVi;
                        if(DungChung.Bien.MaBV=="20001")
                        _tableCell.Angle = 90;
                        _tableCell.Text = DungChung.Bien.MaBV == "30003" ? (_tendv + "_" + _donvi) : _tendv;
                        if (_tendv.Length > 40)
                        {
                            _tableCell.Font = new Font("Times New Roman", 7, System.Drawing.FontStyle.Bold);
                        }
                    }
                }

            }
        }
        int sott = 1;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
          
                xrTableCell4.Text = sott.ToString();
                sott++;
        

            for (int i = 0; i < _lDichVu.Count; i++)
            {
                foreach (XRTableCell _tableCell in xrTableRow2)
                {
                    //System.Windows.Forms.MessageBox.Show(_tableCell.Name);

                    if (_tableCell.Name == "colSL" + (i + 1).ToString())
                    {
                        int _madv = 0;
                        int _mabn = 0;
                        if (_lDichVu.Skip(i).ToList().Count > 0)
                            _madv = _lDichVu.Skip(i).First().MaDV;
                        if (this.GetCurrentColumnValue("MaBNhan") != null)
                            _mabn = Convert.ToInt32(this.GetCurrentColumnValue("MaBNhan"));
                        string _buong = "";
                        if (this.GetCurrentColumnValue("Buong") != null)
                            _buong = this.GetCurrentColumnValue("Buong").ToString();
                        double _soluong = 0;
                        _soluong = _lSoLuong.Where(p => p.MaDV == _madv && p.MaBNhan == _mabn && p.Buong == _buong).Sum(p => p.SoLuong);
                        if (_soluong != 0)
                            _tableCell.Text = _soluong.ToString();
                        else
                            _tableCell.Text = "";
                    }
                }

            }
        }

    }
}
