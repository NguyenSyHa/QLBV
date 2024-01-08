using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Windows.Forms;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNNuocTieu : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNNuocTieu()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            

            if (DungChung.Bien.MaBV != "14018")
            {
                
                SubBand1.Visible = true;
            }
            else
            {
                SubBand2.Visible = true;
            }
            if (DungChung.Bien.MaBV == "14018")
            {
                colTenXN.DataBindings.Add("Text", DataSource, "TenDVct");
                colKetqua.DataBindings.Add("Text", DataSource, "KetQua");
                colBinhThuong.DataBindings.Add("Text", DataSource, "TSBT");
            }
            #region BV khac14018
            else
            {
                int sophieu = int.Parse(SoPhieu.Value.ToString());

                var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                           join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                           join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                           join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                           join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                           select new { clsct.KetQua, clsct.STTHT, dvct.TenDVct, dvct.TSnuTu, dvct.TSnuDen, dvct.TSnTu, dvct.TSnDen, cls.MaBNhan }).ToList();
                if (qhh.Count > 0)
                {
                    int Gtinh = 0;
                    int _mabn = qhh.First().MaBNhan ?? 0;
                    var ttbn = DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                    if (ttbn != null)
                        Gtinh = ttbn.GTinh ?? 0;
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("Glu")).First().KetQua != null)
                    {
                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("Glu")).First().KetQua != null)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuDen == null)
                                    {
                                        colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                                colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                                colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                                colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                                colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnDen == null)
                                    {
                                        colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                                colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                                colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                                colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                                colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }

                            }
                            catch
                            {
                                colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua;
                            }
                            //colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua;

                        }
                        else colKQ1.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                    {
                                        colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ2.ForeColor = System.Drawing.Color.Red;
                                                colKQ2.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ2.ForeColor = System.Drawing.Color.Red;
                                                colKQ2.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ2.ForeColor = System.Drawing.Color.Red;
                                                colKQ2.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ2.ForeColor = System.Drawing.Color.Red;
                                                colKQ2.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ2.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ2.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                    {
                                        colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ2.ForeColor = System.Drawing.Color.Red;
                                                colKQ2.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ2.ForeColor = System.Drawing.Color.Red;
                                                colKQ2.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ2.ForeColor = System.Drawing.Color.Red;
                                                colKQ2.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ2.ForeColor = System.Drawing.Color.Red;
                                                colKQ2.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ2.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ2.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua;
                            }
                            //colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua;
                        }
                        else colKQ2.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                    {
                                        colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ3.ForeColor = System.Drawing.Color.Red;
                                                colKQ3.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ3.ForeColor = System.Drawing.Color.Red;
                                                colKQ3.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ3.ForeColor = System.Drawing.Color.Red;
                                                colKQ3.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ3.ForeColor = System.Drawing.Color.Red;
                                                colKQ3.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ3.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ3.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                    {
                                        colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ3.ForeColor = System.Drawing.Color.Red;
                                                colKQ3.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ3.ForeColor = System.Drawing.Color.Red;
                                                colKQ3.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ3.ForeColor = System.Drawing.Color.Red;
                                                colKQ3.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ3.ForeColor = System.Drawing.Color.Red;
                                                colKQ3.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ3.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ3.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                            }
                            //colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                        }
                        else colKQ3.Text = " ";

                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                    {
                                        colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ4.ForeColor = System.Drawing.Color.Red;
                                                colKQ4.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ4.ForeColor = System.Drawing.Color.Red;
                                                colKQ4.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ4.ForeColor = System.Drawing.Color.Red;
                                                colKQ4.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ4.ForeColor = System.Drawing.Color.Red;
                                                colKQ4.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ4.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ4.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                    {
                                        colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ4.ForeColor = System.Drawing.Color.Red;
                                                colKQ4.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ4.ForeColor = System.Drawing.Color.Red;
                                                colKQ4.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ4.ForeColor = System.Drawing.Color.Red;
                                                colKQ4.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ4.ForeColor = System.Drawing.Color.Red;
                                                colKQ4.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ4.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ4.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua;
                            }
                            //colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua;
                        }
                        else colKQ4.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                    {
                                        colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ5.ForeColor = System.Drawing.Color.Red;
                                                colKQ5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ5.ForeColor = System.Drawing.Color.Red;
                                                colKQ5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ5.ForeColor = System.Drawing.Color.Red;
                                                colKQ5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ5.ForeColor = System.Drawing.Color.Red;
                                                colKQ5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ5.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                    {
                                        colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ5.ForeColor = System.Drawing.Color.Red;
                                                colKQ5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ5.ForeColor = System.Drawing.Color.Red;
                                                colKQ5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ5.ForeColor = System.Drawing.Color.Red;
                                                colKQ5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ5.ForeColor = System.Drawing.Color.Red;
                                                colKQ5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ5.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua;
                            }
                            //colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua;
                        }
                        else colKQ5.Text = " ";
                    }
                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("BIL")).First().KetQua != null)
                    {
                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuDen == null)
                                    {
                                        colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                                colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                                colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                                colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                                colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnDen == null)
                                    {
                                        colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                                colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                                colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                                colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                                colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua;
                            }
                            //colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua;
                        }
                        else colKQ6.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                    {
                                        colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ7.ForeColor = System.Drawing.Color.Red;
                                                colKQ7.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ7.ForeColor = System.Drawing.Color.Red;
                                                colKQ7.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ7.ForeColor = System.Drawing.Color.Red;
                                                colKQ7.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ7.ForeColor = System.Drawing.Color.Red;
                                                colKQ7.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ7.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ7.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                    {
                                        colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ7.ForeColor = System.Drawing.Color.Red;
                                                colKQ7.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ7.ForeColor = System.Drawing.Color.Red;
                                                colKQ7.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ7.ForeColor = System.Drawing.Color.Red;
                                                colKQ7.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ7.ForeColor = System.Drawing.Color.Red;
                                                colKQ7.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ7.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ7.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua;
                            }
                            //colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua;
                        }
                        else colKQ7.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                    {
                                        colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ8.ForeColor = System.Drawing.Color.Red;
                                                colKQ8.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ8.ForeColor = System.Drawing.Color.Red;
                                                colKQ8.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ8.ForeColor = System.Drawing.Color.Red;
                                                colKQ8.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ8.ForeColor = System.Drawing.Color.Red;
                                                colKQ8.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ8.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ8.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                    {
                                        colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ8.ForeColor = System.Drawing.Color.Red;
                                                colKQ8.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ8.ForeColor = System.Drawing.Color.Red;
                                                colKQ8.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ8.ForeColor = System.Drawing.Color.Red;
                                                colKQ8.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ8.ForeColor = System.Drawing.Color.Red;
                                                colKQ8.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ8.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ8.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua;
                            }
                            //colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua;
                        }
                        else colKQ8.Text = " ";

                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                    {
                                        colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ9.ForeColor = System.Drawing.Color.Red;
                                                colKQ9.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ9.ForeColor = System.Drawing.Color.Red;
                                                colKQ9.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ9.ForeColor = System.Drawing.Color.Red;
                                                colKQ9.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ9.ForeColor = System.Drawing.Color.Red;
                                                colKQ9.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ9.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ9.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                    {
                                        colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ9.ForeColor = System.Drawing.Color.Red;
                                                colKQ9.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ9.ForeColor = System.Drawing.Color.Red;
                                                colKQ9.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ9.ForeColor = System.Drawing.Color.Red;
                                                colKQ9.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ9.ForeColor = System.Drawing.Color.Red;
                                                colKQ9.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ9.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ9.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ9.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua;
                            }
                            //colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua;
                        }
                        else colKQ9.Text = " ";

                    }
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("KET")).First().KetQua != null)
                    {
                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuDen == null)
                                    {
                                        colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                                colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                                colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                                colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                                colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnDen == null)
                                    {
                                        colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                                colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                                colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                                colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                                colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ10.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua;
                            }
                            //colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua; 
                        }
                        else colKQ10.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                    {
                                        colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ11.ForeColor = System.Drawing.Color.Red;
                                                colKQ11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ11.ForeColor = System.Drawing.Color.Red;
                                                colKQ11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ11.ForeColor = System.Drawing.Color.Red;
                                                colKQ11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ11.ForeColor = System.Drawing.Color.Red;
                                                colKQ11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ11.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                    {
                                        colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ11.ForeColor = System.Drawing.Color.Red;
                                                colKQ11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ11.ForeColor = System.Drawing.Color.Red;
                                                colKQ11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ11.ForeColor = System.Drawing.Color.Red;
                                                colKQ11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ11.ForeColor = System.Drawing.Color.Red;
                                                colKQ11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ11.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ11.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua;
                            }
                            //colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua; 
                        }
                        else colKQ11.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                    {
                                        colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ12.ForeColor = System.Drawing.Color.Red;
                                                colKQ12.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ12.ForeColor = System.Drawing.Color.Red;
                                                colKQ12.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ12.ForeColor = System.Drawing.Color.Red;
                                                colKQ12.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ12.ForeColor = System.Drawing.Color.Red;
                                                colKQ12.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ12.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ12.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                    {
                                        colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ12.ForeColor = System.Drawing.Color.Red;
                                                colKQ12.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ12.ForeColor = System.Drawing.Color.Red;
                                                colKQ12.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ12.ForeColor = System.Drawing.Color.Red;
                                                colKQ12.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ12.ForeColor = System.Drawing.Color.Red;
                                                colKQ12.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ12.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ12.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ12.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua;
                            }
                            //colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua; 
                        }
                        else colKQ12.Text = " ";

                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                    {
                                        colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ13.ForeColor = System.Drawing.Color.Red;
                                                colKQ13.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ13.ForeColor = System.Drawing.Color.Red;
                                                colKQ13.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ13.ForeColor = System.Drawing.Color.Red;
                                                colKQ13.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ13.ForeColor = System.Drawing.Color.Red;
                                                colKQ13.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ13.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ13.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                    {
                                        colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ13.ForeColor = System.Drawing.Color.Red;
                                                colKQ13.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ13.ForeColor = System.Drawing.Color.Red;
                                                colKQ13.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ13.ForeColor = System.Drawing.Color.Red;
                                                colKQ13.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ13.ForeColor = System.Drawing.Color.Red;
                                                colKQ13.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ13.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ13.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ13.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua;
                            }
                            //colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua; 
                        }
                        else colKQ13.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                    {
                                        colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ14.ForeColor = System.Drawing.Color.Red;
                                                colKQ14.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ14.ForeColor = System.Drawing.Color.Red;
                                                colKQ14.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ14.ForeColor = System.Drawing.Color.Red;
                                                colKQ14.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ14.ForeColor = System.Drawing.Color.Red;
                                                colKQ14.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ14.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ14.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                    {
                                        colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ14.ForeColor = System.Drawing.Color.Red;
                                                colKQ14.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ14.ForeColor = System.Drawing.Color.Red;
                                                colKQ14.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ14.ForeColor = System.Drawing.Color.Red;
                                                colKQ14.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ14.ForeColor = System.Drawing.Color.Red;
                                                colKQ14.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ14.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ14.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ14.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                            }
                            //colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua; 
                        }
                        else colKQ14.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuDen == null)
                                    {
                                        colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                                this.colKQ15.ForeColor = System.Drawing.Color.Red;
                                                colKQ15.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                                this.colKQ15.ForeColor = System.Drawing.Color.Red;
                                                colKQ15.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                                this.colKQ15.ForeColor = System.Drawing.Color.Red;
                                                colKQ15.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                                this.colKQ15.ForeColor = System.Drawing.Color.Red;
                                                colKQ15.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua;
                                    //    this.colKQ15.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ15.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnDen == null)
                                    {
                                        colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                                this.colKQ15.ForeColor = System.Drawing.Color.Red;
                                                colKQ15.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                                this.colKQ15.ForeColor = System.Drawing.Color.Red;
                                                colKQ15.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                                this.colKQ15.ForeColor = System.Drawing.Color.Red;
                                                colKQ15.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                                this.colKQ15.ForeColor = System.Drawing.Color.Red;
                                                colKQ15.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua;
                                    //    this.colKQ15.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ15.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ15.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                            }
                            //colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua; 
                        }
                        else colKQ15.Text = " ";
                    }
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("SG")).First().KetQua != null)
                    {
                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuDen == null)
                                    {
                                        colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                                colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                                colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                                colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                                colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnDen == null)
                                    {
                                        colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                                colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                                colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                                colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                                colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ16.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua;
                            }
                            //colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua; 
                        }
                        else colKQ16.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                    {
                                        colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ17.ForeColor = System.Drawing.Color.Red;
                                                colKQ17.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ17.ForeColor = System.Drawing.Color.Red;
                                                colKQ17.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ17.ForeColor = System.Drawing.Color.Red;
                                                colKQ17.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ17.ForeColor = System.Drawing.Color.Red;
                                                colKQ17.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ17.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ17.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                    {
                                        colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ17.ForeColor = System.Drawing.Color.Red;
                                                colKQ17.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ17.ForeColor = System.Drawing.Color.Red;
                                                colKQ17.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ17.ForeColor = System.Drawing.Color.Red;
                                                colKQ17.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ17.ForeColor = System.Drawing.Color.Red;
                                                colKQ17.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ17.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ17.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ17.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua;
                            }
                            //colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua; 
                        }
                        else colKQ17.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                    {
                                        colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ18.ForeColor = System.Drawing.Color.Red;
                                                colKQ18.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ18.ForeColor = System.Drawing.Color.Red;
                                                colKQ18.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ18.ForeColor = System.Drawing.Color.Red;
                                                colKQ18.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ18.ForeColor = System.Drawing.Color.Red;
                                                colKQ18.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ18.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ18.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                    {
                                        colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ18.ForeColor = System.Drawing.Color.Red;
                                                colKQ18.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ18.ForeColor = System.Drawing.Color.Red;
                                                colKQ18.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ18.ForeColor = System.Drawing.Color.Red;
                                                colKQ18.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ18.ForeColor = System.Drawing.Color.Red;
                                                colKQ18.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ18.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ18.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ18.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua;
                            }
                            //colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua; 
                        }
                        else colKQ18.Text = " ";

                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                    {
                                        colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ19.ForeColor = System.Drawing.Color.Red;
                                                colKQ19.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ19.ForeColor = System.Drawing.Color.Red;
                                                colKQ19.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ19.ForeColor = System.Drawing.Color.Red;
                                                colKQ19.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ19.ForeColor = System.Drawing.Color.Red;
                                                colKQ19.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ19.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ19.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                    {
                                        colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ19.ForeColor = System.Drawing.Color.Red;
                                                colKQ19.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ19.ForeColor = System.Drawing.Color.Red;
                                                colKQ19.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ19.ForeColor = System.Drawing.Color.Red;
                                                colKQ19.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ19.ForeColor = System.Drawing.Color.Red;
                                                colKQ19.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ19.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ19.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ19.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua;
                            }
                            //colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua; 
                        }
                        else colKQ19.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                    {
                                        colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ20.ForeColor = System.Drawing.Color.Red;
                                                colKQ20.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ20.ForeColor = System.Drawing.Color.Red;
                                                colKQ20.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ20.ForeColor = System.Drawing.Color.Red;
                                                colKQ20.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ20.ForeColor = System.Drawing.Color.Red;
                                                colKQ20.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ20.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ20.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                    {
                                        colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ20.ForeColor = System.Drawing.Color.Red;
                                                colKQ20.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ20.ForeColor = System.Drawing.Color.Red;
                                                colKQ20.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ20.ForeColor = System.Drawing.Color.Red;
                                                colKQ20.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ20.ForeColor = System.Drawing.Color.Red;
                                                colKQ20.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ20.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ20.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ20.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua;
                            }
                            //colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua; 
                        }
                        else colKQ20.Text = " ";

                    }
                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("BLO")).First().KetQua != null)
                    {
                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuDen == null)
                                    {
                                        colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                                colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                                colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                                colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                                colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnDen == null)
                                    {
                                        colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                                colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                                colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                                colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                                colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ21.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua;
                            }
                            //colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua; 
                        }
                        else colKQ21.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                    {
                                        colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ22.ForeColor = System.Drawing.Color.Red;
                                                colKQ22.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ22.ForeColor = System.Drawing.Color.Red;
                                                colKQ22.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ22.ForeColor = System.Drawing.Color.Red;
                                                colKQ22.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ22.ForeColor = System.Drawing.Color.Red;
                                                colKQ22.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ22.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ22.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                    {
                                        colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ22.ForeColor = System.Drawing.Color.Red;
                                                colKQ22.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ22.ForeColor = System.Drawing.Color.Red;
                                                colKQ22.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ22.ForeColor = System.Drawing.Color.Red;
                                                colKQ22.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ22.ForeColor = System.Drawing.Color.Red;
                                                colKQ22.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ22.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ22.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ22.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua;
                            }
                            //colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua; 
                        }
                        else colKQ22.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                    {
                                        colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ23.ForeColor = System.Drawing.Color.Red;
                                                colKQ23.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ23.ForeColor = System.Drawing.Color.Red;
                                                colKQ23.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ23.ForeColor = System.Drawing.Color.Red;
                                                colKQ23.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ23.ForeColor = System.Drawing.Color.Red;
                                                colKQ23.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ23.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ23.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                    {
                                        colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ23.ForeColor = System.Drawing.Color.Red;
                                                colKQ23.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ23.ForeColor = System.Drawing.Color.Red;
                                                colKQ23.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ23.ForeColor = System.Drawing.Color.Red;
                                                colKQ23.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ23.ForeColor = System.Drawing.Color.Red;
                                                colKQ23.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ23.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ23.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ23.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua;
                            }
                            //colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua; 
                        }
                        else colKQ23.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                    {
                                        colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ24.ForeColor = System.Drawing.Color.Red;
                                                colKQ24.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ24.ForeColor = System.Drawing.Color.Red;
                                                colKQ24.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ24.ForeColor = System.Drawing.Color.Red;
                                                colKQ24.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ24.ForeColor = System.Drawing.Color.Red;
                                                colKQ24.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ24.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ24.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                    {
                                        colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ24.ForeColor = System.Drawing.Color.Red;
                                                colKQ24.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ24.ForeColor = System.Drawing.Color.Red;
                                                colKQ24.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ24.ForeColor = System.Drawing.Color.Red;
                                                colKQ24.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ24.ForeColor = System.Drawing.Color.Red;
                                                colKQ24.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ24.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ24.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ24.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua;
                            }
                            //colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua; 
                        }
                        else colKQ24.Text = " ";

                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                    {
                                        colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ25.ForeColor = System.Drawing.Color.Red;
                                                colKQ25.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ25.ForeColor = System.Drawing.Color.Red;
                                                colKQ25.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ25.ForeColor = System.Drawing.Color.Red;
                                                colKQ25.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ25.ForeColor = System.Drawing.Color.Red;
                                                colKQ25.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ25.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ25.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                    {
                                        colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ25.ForeColor = System.Drawing.Color.Red;
                                                colKQ25.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ25.ForeColor = System.Drawing.Color.Red;
                                                colKQ25.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ25.ForeColor = System.Drawing.Color.Red;
                                                colKQ25.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ25.ForeColor = System.Drawing.Color.Red;
                                                colKQ25.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ25.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ25.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ25.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua;
                            }
                            //colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua; 
                        }
                        else colKQ25.Text = " ";

                    }
                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("PH")).First().KetQua != null)
                    {
                        if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuDen == null)
                                    {
                                        colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                                colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                                colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                                colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                                colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnDen == null)
                                    {
                                        colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                                colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                                colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                                colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                                colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ26.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua;
                            }
                            //colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua; 
                        }
                        else colKQ26.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                    {
                                        colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ27.ForeColor = System.Drawing.Color.Red;
                                                colKQ27.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ27.ForeColor = System.Drawing.Color.Red;
                                                colKQ27.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ27.ForeColor = System.Drawing.Color.Red;
                                                colKQ27.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ27.ForeColor = System.Drawing.Color.Red;
                                                colKQ27.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ27.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ27.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                    {
                                        colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ27.ForeColor = System.Drawing.Color.Red;
                                                colKQ27.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ27.ForeColor = System.Drawing.Color.Red;
                                                colKQ27.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ27.ForeColor = System.Drawing.Color.Red;
                                                colKQ27.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ27.ForeColor = System.Drawing.Color.Red;
                                                colKQ27.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ27.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ27.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ27.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua;
                            }
                            //colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua; 
                        }
                        else colKQ27.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                    {
                                        colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ28.ForeColor = System.Drawing.Color.Red;
                                                colKQ28.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ28.ForeColor = System.Drawing.Color.Red;
                                                colKQ28.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ28.ForeColor = System.Drawing.Color.Red;
                                                colKQ28.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ28.ForeColor = System.Drawing.Color.Red;
                                                colKQ28.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ28.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ28.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                    {
                                        colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ28.ForeColor = System.Drawing.Color.Red;
                                                colKQ28.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ28.ForeColor = System.Drawing.Color.Red;
                                                colKQ28.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ28.ForeColor = System.Drawing.Color.Red;
                                                colKQ28.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ28.ForeColor = System.Drawing.Color.Red;
                                                colKQ28.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ28.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ28.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ28.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua;
                            }
                            //colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua; 
                        }
                        else colKQ28.Text = " ";
                    }
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("PRO")).First().KetQua != null)
                    {
                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuDen == null)
                                    {
                                        colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                                colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                                colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                                colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                                colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnDen == null)
                                    {
                                        colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                                colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                                colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                                colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                                colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ29.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua;
                            }
                            //colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua; 
                        }
                        else colKQ29.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                    {
                                        colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ30.ForeColor = System.Drawing.Color.Red;
                                                colKQ30.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ30.ForeColor = System.Drawing.Color.Red;
                                                colKQ30.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ30.ForeColor = System.Drawing.Color.Red;
                                                colKQ30.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ30.ForeColor = System.Drawing.Color.Red;
                                                colKQ30.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ30.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ30.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                    {
                                        colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ30.ForeColor = System.Drawing.Color.Red;
                                                colKQ30.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ30.ForeColor = System.Drawing.Color.Red;
                                                colKQ30.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ30.ForeColor = System.Drawing.Color.Red;
                                                colKQ30.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ30.ForeColor = System.Drawing.Color.Red;
                                                colKQ30.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ30.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ30.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ30.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua;
                            }
                            //colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua; 
                        }
                        else colKQ30.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                    {
                                        colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ31.ForeColor = System.Drawing.Color.Red;
                                                colKQ31.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ31.ForeColor = System.Drawing.Color.Red;
                                                colKQ31.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ31.ForeColor = System.Drawing.Color.Red;
                                                colKQ31.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ31.ForeColor = System.Drawing.Color.Red;
                                                colKQ31.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ31.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ31.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnTu);
                                    double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnDen);
                                    double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua);
                                    if (tstu <= kq && kq <= tsden)
                                        colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua;
                                    else
                                    {
                                        colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua;
                                        this.colKQ31.ForeColor = System.Drawing.Color.Red;
                                        colKQ31.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ31.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua;
                            }
                            //colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua; 
                        }
                        else colKQ31.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                    {
                                        colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ32.ForeColor = System.Drawing.Color.Red;
                                                colKQ32.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ32.ForeColor = System.Drawing.Color.Red;
                                                colKQ32.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ32.ForeColor = System.Drawing.Color.Red;
                                                colKQ32.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ32.ForeColor = System.Drawing.Color.Red;
                                                colKQ32.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ32.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ32.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                    {
                                        colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ32.ForeColor = System.Drawing.Color.Red;
                                                colKQ32.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ32.ForeColor = System.Drawing.Color.Red;
                                                colKQ32.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ32.ForeColor = System.Drawing.Color.Red;
                                                colKQ32.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ32.ForeColor = System.Drawing.Color.Red;
                                                colKQ32.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ32.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ32.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ32.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua;
                            }
                            //colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua; 
                        }
                        else colKQ32.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                    {
                                        colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ33.ForeColor = System.Drawing.Color.Red;
                                                colKQ33.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ33.ForeColor = System.Drawing.Color.Red;
                                                colKQ33.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ33.ForeColor = System.Drawing.Color.Red;
                                                colKQ33.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ33.ForeColor = System.Drawing.Color.Red;
                                                colKQ33.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ33.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ33.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                    {
                                        colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ33.ForeColor = System.Drawing.Color.Red;
                                                colKQ33.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ33.ForeColor = System.Drawing.Color.Red;
                                                colKQ33.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ33.ForeColor = System.Drawing.Color.Red;
                                                colKQ33.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ33.ForeColor = System.Drawing.Color.Red;
                                                colKQ33.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ33.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ33.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ33.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua;
                            }
                            //colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua; 
                        }
                        else colKQ33.Text = " ";
                    }
                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("URO")).First().KetQua != null)
                    {
                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuDen == null)
                                    {
                                        colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                                colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                                colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                                colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                                colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnDen == null)
                                    {
                                        colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                                colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                                colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                                colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                                colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua;
                                    //    this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ34.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua;
                            }
                            //colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua; 
                        }
                        else colKQ34.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                    {
                                        colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ35.ForeColor = System.Drawing.Color.Red;
                                                colKQ35.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ35.ForeColor = System.Drawing.Color.Red;
                                                colKQ35.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ35.ForeColor = System.Drawing.Color.Red;
                                                colKQ35.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ35.ForeColor = System.Drawing.Color.Red;
                                                colKQ35.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ35.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ35.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                    {
                                        colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ35.ForeColor = System.Drawing.Color.Red;
                                                colKQ35.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ35.ForeColor = System.Drawing.Color.Red;
                                                colKQ35.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ35.ForeColor = System.Drawing.Color.Red;
                                                colKQ35.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ35.ForeColor = System.Drawing.Color.Red;
                                                colKQ35.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua;
                                    //    this.colKQ35.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ35.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ35.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua;
                            }
                            //colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua; 
                        }
                        else colKQ35.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                    {
                                        colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ36.ForeColor = System.Drawing.Color.Red;
                                                colKQ36.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ36.ForeColor = System.Drawing.Color.Red;
                                                colKQ36.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ36.ForeColor = System.Drawing.Color.Red;
                                                colKQ36.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ36.ForeColor = System.Drawing.Color.Red;
                                                colKQ36.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ36.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ36.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                    {
                                        colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ36.ForeColor = System.Drawing.Color.Red;
                                                colKQ36.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ36.ForeColor = System.Drawing.Color.Red;
                                                colKQ36.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ36.ForeColor = System.Drawing.Color.Red;
                                                colKQ36.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ36.ForeColor = System.Drawing.Color.Red;
                                                colKQ36.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua;
                                    //    this.colKQ36.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ36.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ36.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua;
                            }
                            //colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua; 
                        }
                        else colKQ36.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                    {
                                        colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ37.ForeColor = System.Drawing.Color.Red;
                                                colKQ37.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ37.ForeColor = System.Drawing.Color.Red;
                                                colKQ37.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ37.ForeColor = System.Drawing.Color.Red;
                                                colKQ37.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ37.ForeColor = System.Drawing.Color.Red;
                                                colKQ37.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ37.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ37.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                    {
                                        colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ37.ForeColor = System.Drawing.Color.Red;
                                                colKQ37.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ37.ForeColor = System.Drawing.Color.Red;
                                                colKQ37.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ37.ForeColor = System.Drawing.Color.Red;
                                                colKQ37.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ37.ForeColor = System.Drawing.Color.Red;
                                                colKQ37.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua;
                                    //    this.colKQ37.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ37.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ37.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua;
                            }
                            //colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua; 
                        }
                        else colKQ37.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                    {
                                        colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ38.ForeColor = System.Drawing.Color.Red;
                                                colKQ38.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ38.ForeColor = System.Drawing.Color.Red;
                                                colKQ38.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ38.ForeColor = System.Drawing.Color.Red;
                                                colKQ38.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ38.ForeColor = System.Drawing.Color.Red;
                                                colKQ38.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ38.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ38.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                    {
                                        colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ38.ForeColor = System.Drawing.Color.Red;
                                                colKQ38.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ38.ForeColor = System.Drawing.Color.Red;
                                                colKQ38.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ38.ForeColor = System.Drawing.Color.Red;
                                                colKQ38.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                                this.colKQ38.ForeColor = System.Drawing.Color.Red;
                                                colKQ38.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                    //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnTu);
                                    //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnDen);
                                    //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                    //if (tstu <= kq && kq <= tsden)
                                    //    colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //else
                                    //{
                                    //    colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua;
                                    //    this.colKQ38.ForeColor = System.Drawing.Color.Red;
                                    //    colKQ38.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    //}
                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ38.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua;
                            }
                            //colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua; 
                        }
                        else colKQ38.Text = " ";
                    }
                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("NIT")).First().KetQua != null)
                    {
                        if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuDen == null)
                                    {
                                        colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                                colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                                colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                                colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                                colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnDen == null)
                                    {
                                        colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                                colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                                colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                                colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                                colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }

                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ39.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua;
                            }
                            //colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua; 
                        }
                        else colKQ39.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                    {
                                        colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ40.ForeColor = System.Drawing.Color.Red;
                                                colKQ40.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ40.ForeColor = System.Drawing.Color.Red;
                                                colKQ40.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ40.ForeColor = System.Drawing.Color.Red;
                                                colKQ40.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ40.ForeColor = System.Drawing.Color.Red;
                                                colKQ40.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                    {
                                        colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ40.ForeColor = System.Drawing.Color.Red;
                                                colKQ40.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ40.ForeColor = System.Drawing.Color.Red;
                                                colKQ40.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ40.ForeColor = System.Drawing.Color.Red;
                                                colKQ40.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ40.ForeColor = System.Drawing.Color.Red;
                                                colKQ40.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }

                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ40.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua;
                            }
                            //colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua; 
                        }
                        else colKQ40.Text = " ";

                    }
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("LEU")).First().KetQua != null)
                    {
                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuDen == null)
                                    {
                                        colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                                colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                                colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                                colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                                colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnDen == null)
                                    {
                                        colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                                colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                                colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                                colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua.ToString();
                                                this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                                colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }

                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ41.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua;
                            }
                            //colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua; 
                        }
                        else colKQ41.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                    {
                                        colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ42.ForeColor = System.Drawing.Color.Red;
                                                colKQ42.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ42.ForeColor = System.Drawing.Color.Red;
                                                colKQ42.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ42.ForeColor = System.Drawing.Color.Red;
                                                colKQ42.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ42.ForeColor = System.Drawing.Color.Red;
                                                colKQ42.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                    {
                                        colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ42.ForeColor = System.Drawing.Color.Red;
                                                colKQ42.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ42.ForeColor = System.Drawing.Color.Red;
                                                colKQ42.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ42.ForeColor = System.Drawing.Color.Red;
                                                colKQ42.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                                this.colKQ42.ForeColor = System.Drawing.Color.Red;
                                                colKQ42.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }

                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ42.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua;
                            }

                        }
                        else colKQ42.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                    {
                                        colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ43.ForeColor = System.Drawing.Color.Red;
                                                colKQ43.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ43.ForeColor = System.Drawing.Color.Red;
                                                colKQ43.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ43.ForeColor = System.Drawing.Color.Red;
                                                colKQ43.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ43.ForeColor = System.Drawing.Color.Red;
                                                colKQ43.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                    {
                                        colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ43.ForeColor = System.Drawing.Color.Red;
                                                colKQ43.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ43.ForeColor = System.Drawing.Color.Red;
                                                colKQ43.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ43.ForeColor = System.Drawing.Color.Red;
                                                colKQ43.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                                this.colKQ43.ForeColor = System.Drawing.Color.Red;
                                                colKQ43.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }

                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ43.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua;
                            }
                            //colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua; 
                        }
                        else colKQ43.Text = " ";
                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).Count() > 0)
                        {
                            try
                            {
                                if (Gtinh == 0)
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                    {
                                        colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ44.ForeColor = System.Drawing.Color.Red;
                                                colKQ44.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ44.ForeColor = System.Drawing.Color.Red;
                                                colKQ44.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ44.ForeColor = System.Drawing.Color.Red;
                                                colKQ44.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ44.ForeColor = System.Drawing.Color.Red;
                                                colKQ44.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                    {
                                        colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                    }
                                    else
                                    {
                                        if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnDen != null)
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq && kq <= tsden)
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else if (kq <= tstu)
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ44.ForeColor = System.Drawing.Color.Red;
                                                colKQ44.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                            else if (kq >= tsden)
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ44.ForeColor = System.Drawing.Color.Red;
                                                colKQ44.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                        {
                                            double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnTu);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (tstu <= kq)
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ44.ForeColor = System.Drawing.Color.Red;
                                                colKQ44.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                            }
                                        }
                                        else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                        {
                                            double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnDen);
                                            double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua);
                                            if (kq <= tsden)
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            }
                                            else
                                            {
                                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                                this.colKQ44.ForeColor = System.Drawing.Color.Red;
                                                colKQ44.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                                colKQ44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                            }
                                        }
                                    }

                                }
                                if (DungChung.Bien.MaBV == "30004")
                                {
                                    colKQ44.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                    colKQ44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            catch
                            {
                                colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua;
                            }
                            //colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua; 
                        }
                        else colKQ44.Text = " ";
                    }
                }
            }
#endregion
        }

        private void xrTableCell7_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "12345"&& DungChung.Bien.MaBV != "24297")
            {
                xrLabel10.Visible = false;
                xrLabel11.Visible = false;
            }
            if (this.ChanDoan.Value != null)
            {
                //  System.Windows.Forms.MessageBox.Show(this.ChanDoan.Value.ToString().Length.ToString());
                if (this.ChanDoan.Value.ToString().Length > 80 && this.ChanDoan.Value.ToString().Length < 100)

                    txtChanDoan.Font = new Font("Times New Roman", 10);
                else
                    if (this.ChanDoan.Value.ToString().Length >= 100 && this.ChanDoan.Value.ToString().Length < 110)
                        txtChanDoan.Font = new Font("Times New Roman", 9);
                    else
                        if (this.ChanDoan.Value.ToString().Length >= 110)
                            txtChanDoan.Font = new Font("Times New Roman", 8);

            }
            if(DungChung.Bien.MaBV=="26007")
            {
                xrBarCode1.Visible = true;
            }
            
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "30003") // Chí Linh
            {
                pnNamNu.Visible = false;
                pnNamNu_CL.Visible = true;
              
                if (this.Nam.Value != null && this.Nam.Value.ToString().Length > 0)
                {
                    this.Nam.Value = "".ToUpper();
                    this.Nu.Value = "X".ToUpper();
                }
                else
                {
                    this.Nam.Value = "X".ToUpper();
                    this.Nu.Value = "".ToUpper();
                }
            }

            if (DungChung.Bien.MaBV == "27777")
            {
                xrPictureBox1.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
                colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
            }

            if (DungChung.Bien.MaBV == "30372")
            {
                SubBand3.Visible = false;
                SubBand4.Visible = false;
                SubBand5.Visible = true;
            }

            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand4.Visible = true;
                xrPictureBox2.Visible = true;
                xrPictureBox2.Image = DungChung.Ham.GetLogo();
            }
            
            if (DungChung.Bien.MaBV == "27183")
            {
                xrPictureBox3.Visible = true;
                colTenCQCQ.Visible = false;
                colTenCQ.Visible = false;
                xrLabel1.Visible = false;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "01049")
            {
                xrLabel1.Visible = false;
            }
            if (DungChung.Bien.MaBV == "20001")
            {
                lab92.Text = "TL. TRƯỞNG KHOA XÉT NGHIỆM";
                xrLine1.Visible = true;
            }
            if (DungChung.Bien.MaBV == "14018")
            {
                GroupHeader1.Visible = true;
                SubBand3.Visible = true;
                xrLabel49.Text = "PHIẾU TRẢ KẾT QUẢ XÉT NGHIỆM NƯỚC TIỂU";
                xrLabel43.Text = "Họ và tên:";
            }
            else SubBand4.Visible = true;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "04012")
                lab92.Text = "Trưởng khoa cận lâm sàng".ToUpper();
            if (DungChung.Bien.MaBV == "30005")
                lab92.Text = "TRƯỞNG KHOA XÉT NGHIỆM";
            if(DungChung.Bien.MaBV=="14018")
            {
                lab90.Text = "BÁC SỸ ĐIỀU TRỊ";
                    lab92.Text = "KHOA XÉT NGHIỆM";
            }
                
            if (MaCBDT.Value != null)
            {
                colBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (Macb.Value != null)
            {
                if (DungChung.Bien.MaBV == "26007")
                {
                    var tencb = DataContect.CanBoes.Where(p => p.MaCB == Macb.Value).Select(p => p.TenCB).FirstOrDefault();
                    var chucVu = DataContect.CanBoes.Where(p => p.MaCB == Macb.Value).Select(p => p.CapBac).FirstOrDefault();
                    if (tencb != null && tencb != "")
                    {
                        colTKXN.Text = chucVu+ ": " + tencb; ;
                    }
                }
                    
                else
                    colTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());

            }
            if (DungChung.Bien.MaBV == "30004")
            {
                lab89.Visible = false;
                lab90.Visible = false;
                colBSDT.Visible = false;
                colTKXN.Visible = false;
            }
            if (DungChung.Bien.MaBV == "26007")
            {
                colBSDT.Visible = false;
                lab90.Text = "BÁC SĨ ĐIỀU TRỊ";
                lab92.Text = "PHÒNG XÉT NGHIỆM";    
            }
        }

        private void lab92_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30007")
            {
                lab92.Text = "Bác Sỹ Cận Lâm Sàng".ToUpper();
            }
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_PhieuXN_Sub repsub = (rep_PhieuXN_Sub)xrSubreport1.ReportSource;
            repsub.NGAYKIXN.Value = NgayTH.Value.ToString();
            repsub.NGAYKYDT.Value = NgayCD.Value.ToString();
            repsub.BSDT.Value = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            repsub.TKXN.Value = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            repsub.tb12128.ForeColor = System.Drawing.Color.Black;
        }

        private void Rep_PhieuXNNuocTieu_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12128")
            {
                GroupFooter1.Visible = true;
                ReportFooter.Visible = false;
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV=="14018")
            {
                GroupHeader1.Visible = true;
                celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            }
        }
    }
}
