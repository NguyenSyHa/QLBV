﻿using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoPhauThuat : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoPhauThuat()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colKhoa.DataBindings.Add("Text", DataSource, "NoiGui");
            colTenBNhan.DataBindings.Add("Text",DataSource,"TenBNhan");
            txtMaBNhan.DataBindings.Add("Text",DataSource,"MaBNhan");
            txtNam.DataBindings.Add("Text",DataSource,"gtinh");
            //txtNu.DataBindings.Add("Text",DataSource,"GTinh");
            txtBHYT.DataBindings.Add("Text",DataSource,"DTuong");
            colDiaChi.DataBindings.Add("Text",DataSource,"DiaChi");
            colCDTruocPT.DataBindings.Add("Text", DataSource, "ChanDoan");
            colSauPT.DataBindings.Add("Text", DataSource, "chandoansau");
            colNgayPT.DataBindings.Add("Text", DataSource, "NgayTH");
            colPPPT.DataBindings.Add("Text", DataSource, "YeuCau");
            txtBSPT.DataBindings.Add("Text", DataSource, "bsth");
            GroupHeader1.GroupFields.Add(new GroupField("NoiGui"));
 
        }

        private void colNam_BeforePrint(object sender, CancelEventArgs e)
        {
            int _gt = 0;
            int _tuoi = 0;
            if (GetCurrentColumnValue("tuoi") != null)
            {
                _tuoi = int.Parse(this.GetCurrentColumnValue("tuoi").ToString());
                if (GetCurrentColumnValue("gtinh") != null)
                {
                    _gt = int.Parse(this.GetCurrentColumnValue("gtinh").ToString());
                    if (_gt == 1)
                    {
                        colNam.Text = _tuoi.ToString();
                        colNu.Text = "";
                    }
                    else { colNam.Text = " "; colNu.Text = _tuoi.ToString(); }
                }
            }
        }

       
        private void colBHYT_BeforePrint(object sender, CancelEventArgs e)
        {
            string dt = "";
            if (GetCurrentColumnValue("dtuong") != null)
            {
                dt = GetCurrentColumnValue("dtuong").ToString();
                if (dt == "BHYT")
                {
                    colBHYT.Text = "X";
                }
                else colBHYT.Text = " ";
            }
        }

        private void colBSPT_BeforePrint(object sender, CancelEventArgs e)
        {
            string _bsth = "";
            if (this.GetCurrentColumnValue("bsth") != null && this.GetCurrentColumnValue("bsth") != "")
            {
                _bsth = GetCurrentColumnValue("bsth").ToString();
                var qbsth = from cb in data.CanBoes.Where(p => p.MaCB == _bsth) select new { cb.TenCB };
                if (qbsth.Count() > 0)
                {
                    colBSPT.Text = qbsth.First().TenCB;
                }
                else colBSPT.Text = "";
            }
        }

        private void colCDTruocPT_BeforePrint(object sender, CancelEventArgs e)
        {
            //int  _mabn = 0;
            //if (this.GetCurrentColumnValue("MaBNhan") != null)
            //{
            //    _mabn = GetCurrentColumnValue("MaBNhan").ToString();
            //    var q = (from bnkb in data.BNKBs.Where(p => p.MaBNhan == _mabn)
            //             join kp in data.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng")) on bnkb.MaKP equals kp.MaKP
            //             select new { bnkb.ChanDoan }).ToList();
            //    if (q.Count > 0)
            //    {
            //        colCDTruocPT.Text = q.First().ChanDoan;
            //    }
            //    else colCDTruocPT.Text = "";
            //}
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text=DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            
        }
    }
}
