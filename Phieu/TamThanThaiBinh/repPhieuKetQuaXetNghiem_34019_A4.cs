using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.Phieu.TamThanThaiBinh
{
    public partial class repPhieuKetQuaXetNghiem_34019_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public int GT = 0;
        public repPhieuKetQuaXetNghiem_34019_A4()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            _ldvct = DataContect.DichVucts.ToList();
            colHeaderTenXetNghiem.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colKetQua.DataBindings.Add("Text", DataSource, "KetQua");
            colChiSoBinhThuong.DataBindings.Add("Text", DataSource, "TSBT");
            colTenXetNghiem.DataBindings.Add("Text", DataSource, "TenDVct");
            GroupHeader1.GroupFields.Add(new GroupField("TenDV"));
            colGroupNumber.DataBindings.Add("Text", DataSource, "TenDV");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DichVuct> _ldvct = new List<DichVuct>();

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            lblLeft.Text = "";
            lblRight.Text = "";
            lblMid.Text = "";
            if (this.GetCurrentColumnValue("MaDVct") != null && this.GetCurrentColumnValue("KetQua") != null)
            {
                string a = this.GetCurrentColumnValue("MaDVct").ToString();
                var dvct = _ldvct.Where(p => p.MaDVct == a).FirstOrDefault();
                if (dvct != null)
                {
                    double? kq = null;
                    double refKq = 0;
                    if (double.TryParse((this.GetCurrentColumnValue("KetQua")).ToString(), out refKq))
                    {
                        kq = refKq;
                    }
                    if (kq == null)
                        return;
                    double? tstu = dvct.TSnuTu;
                    double? tsden = dvct.TSnuDen;
                    try
                    {
                        if (GT == 0)
                        {
                            if (dvct.TSnuTu != null && dvct.TSnuDen != null)
                            {
                                if (kq < tstu)
                                {
                                    lblLeft.Text = "L";
                                    lblRight.Text = kq.ToString();
                                }
                                else if (kq > tsden)
                                {
                                    lblLeft.Text = "H";
                                    lblRight.Text = kq.ToString();
                                }
                                else
                                {
                                    lblMid.Text = kq.ToString();
                                }
                            }
                            else if (dvct.TSnuTu != null && dvct.TSnuDen == null)//trị số nhỏ nhất
                            {
                                if (tstu > kq)
                                {
                                    lblLeft.Text = "L";
                                    lblRight.Text = kq.ToString();
                                }
                                else
                                {
                                    lblMid.Text = kq.ToString();
                                }
                            }
                            else if (dvct.TSnuTu == null && dvct.TSnuDen != null)//giá trị lớn nhất
                            {
                                if (kq > tsden)
                                {
                                    lblLeft.Text = "H";
                                    lblRight.Text = kq.ToString();
                                }
                                else
                                {
                                    lblMid.Text = kq.ToString();
                                }

                            }
                            else
                            {
                                lblMid.Text = kq.ToString();
                            }
                        }
                        else
                        {
                            if (dvct.TSnTu != null && dvct.TSnDen != null)
                            {
                                if (kq < tstu)
                                {
                                    lblLeft.Text = "L";
                                    lblRight.Text = kq.ToString();
                                }
                                else if (kq > tsden)
                                {
                                    lblLeft.Text = "H";
                                    lblRight.Text = kq.ToString();
                                }
                                else
                                {
                                    lblMid.Text = kq.ToString();
                                }
                            }
                            else if (dvct.TSnTu != null && dvct.TSnDen == null)//trị số nhỏ nhất
                            {
                                if (tstu > kq)
                                {
                                    lblLeft.Text = "L";
                                    lblRight.Text = kq.ToString();
                                }
                                else
                                {
                                    lblMid.Text = kq.ToString();
                                }
                            }
                            else if (dvct.TSnTu == null && dvct.TSnDen != null)//giá trị lớn nhất
                            {
                                if (kq >= tsden)
                                {
                                    lblLeft.Text = "H";
                                    lblRight.Text = kq.ToString();
                                }
                                else
                                {
                                    lblMid.Text = kq.ToString();
                                }

                            }
                            else
                            {
                                lblMid.Text = kq.ToString();
                            }
                        }
                    }
                    catch
                    {
                        lblMid.Text = kq.ToString();
                    }

                }

            }
        }

        int groupCount = 0;
        private void colGroupNumber_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            groupCount++;
            colGroupNumber.Text = groupCount.ToString() + ".";
        }

    }
}
