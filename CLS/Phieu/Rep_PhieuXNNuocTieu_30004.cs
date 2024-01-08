using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNNuocTieu_30004 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNNuocTieu_30004()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
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
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }

                        }
                        catch
                        {
                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                {
                                    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                {
                                    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                {
                                    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                                //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuTu);
                                //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnuDen);
                                //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                //if (tstu <= kq && kq <= tsden)
                                //    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                                //else
                                //{
                                //    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                                //    this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                //    colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                //}
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                {
                                    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                        }
                    }
                    

                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                {
                                    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                {
                                    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                {
                                    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                {
                                    colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ1.ForeColor = System.Drawing.Color.Red;
                                            colKQ1.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua;
                        }
                    }
                    
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
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                {
                                    colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                {
                                    colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                {
                                    colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                {
                                    colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua;
                        }
                    }
                    

                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                {
                                    colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                {
                                    colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ6.ForeColor = System.Drawing.Color.Red;
                                            colKQ6.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua;
                        }
                    }
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
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ10.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua;
                        }  
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                {
                                    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                {
                                    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ10.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                {
                                    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                                //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuTu);
                                //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnuDen);
                                //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                //if (tstu <= kq && kq <= tsden)
                                //    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua;
                                //else
                                //{
                                //    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua;
                                //    this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                //    colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                //}
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                {
                                    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ10.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua;
                        }
                    }                   

                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                {
                                    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                {
                                    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ10.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                {
                                    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                                //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuTu);
                                //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnuDen);
                                //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                //if (tstu <= kq && kq <= tsden)
                                //    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                                //else
                                //{
                                //    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                                //    this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                //    colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                //}
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                {
                                    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ10.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuDen == null)
                                {
                                    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
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
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnDen == null)
                                {
                                    colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
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
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).First().KetQua.ToString();
                                            this.colKQ10.ForeColor = System.Drawing.Color.Red;
                                            colKQ10.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ10.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                        }
                    }
                    
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
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ16.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua;
                        } 
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                {
                                    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                {
                                    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ16.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua;
                        } 
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                {
                                    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                {
                                    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ16.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua;
                        }
                    }
                    

                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                {
                                    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                {
                                    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                                //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnTu);
                                //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().TSnDen);
                                //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua);
                                //if (tstu <= kq && kq <= tsden)
                                //    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua;
                                //else
                                //{
                                //    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua;
                                //    this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                //    colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                //}
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ16.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                {
                                    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                {
                                    colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ16.ForeColor = System.Drawing.Color.Red;
                                            colKQ16.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ16.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua;
                        }
                    }
                    

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
                                colKQ21.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua;
                        } 
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                {
                                    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                {
                                    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ21.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                {
                                    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                {
                                    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ21.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                {
                                    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                {
                                    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ21.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua;
                        } 
                    }
                    

                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                {
                                    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                {
                                    colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ21.ForeColor = System.Drawing.Color.Red;
                                            colKQ21.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ21.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua;
                        }
                    }
                    

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
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ26.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                {
                                    colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                {
                                    colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ26.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua;
                        } 
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                {
                                    colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                {
                                    colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ26.ForeColor = System.Drawing.Color.Red;
                                            colKQ26.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ26.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua;
                        }
                    }
                    
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
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ29.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua;
                        } 
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                {
                                    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                {
                                    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ29.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua;
                        }
                    
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                {
                                    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnTu);
                                double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().TSnDen);
                                double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua);
                                if (tstu <= kq && kq <= tsden)
                                    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua;
                                else
                                {
                                    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua;
                                    this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                    colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ29.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua;
                        }
                         
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                {
                                    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                {
                                    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ29.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                {
                                    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                {
                                    colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ29.ForeColor = System.Drawing.Color.Red;
                                            colKQ29.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ29.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua;
                        }
                    }
                    
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
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ34.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua;
                        } 
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                {
                                    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                {
                                    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ34.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                {
                                    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                                //double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuTu);
                                //double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnuDen);
                                //double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                //if (tstu <= kq && kq <= tsden)
                                //    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua;
                                //else
                                //{
                                //    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua;
                                //    this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                //    colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                //}
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                {
                                    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ34.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                {
                                    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                {
                                    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ34.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuDen == null)
                                {
                                    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnDen == null)
                                {
                                    colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
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
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua.ToString();
                                            this.colKQ34.ForeColor = System.Drawing.Color.Red;
                                            colKQ34.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ34.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua;
                        }
                    }
                    
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
                                colKQ39.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua;
                        } 
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                {
                                    colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                            colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                            colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                            colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                            colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                {
                                    colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                            colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                            colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                            colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ39.ForeColor = System.Drawing.Color.Red;
                                            colKQ39.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ39.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua;
                        } 
                    }
                    

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
                                colKQ41.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua;
                        } 
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuDen == null)
                                {
                                    colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnDen == null)
                                {
                                    colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
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
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ41.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua;
                        } 
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuDen == null)
                                {
                                    colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnDen == null)
                                {
                                    colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
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
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ41.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua;
                        }
                    }
                    
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        try
                        {
                            if (Gtinh == 0)
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuDen == null)
                                {
                                    colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnuDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnDen == null)
                                {
                                    colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
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
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else if (kq <= tstu)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                        else if (kq >= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnTu != null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnDen == null)//trị số nhỏ nhất
                                    {
                                        double tstu = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnTu);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (tstu <= kq)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                        }
                                    }
                                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnTu == null && qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnDen != null)//giá trị lớn nhất
                                    {
                                        double tsden = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().TSnDen);
                                        double kq = Convert.ToDouble(qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua);
                                        if (kq <= tsden)
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                        }
                                        else
                                        {
                                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua.ToString();
                                            this.colKQ41.ForeColor = System.Drawing.Color.Red;
                                            colKQ41.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                            colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                        }
                                    }
                                }
                            }
                            if (DungChung.Bien.MaBV == "30004")
                            {
                                colKQ41.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                                colKQ41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            }
                        }
                        catch
                        {
                            colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua;
                        } 
                    }
                    
                }
            }
        }

        private void xrTableCell7_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
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
            //if (DungChung.Bien.MaBV == "02005")
            //{
            //    colSo.Visible = false;
            //    colTenTKXN.Visible = false;
            //}
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
            ////////////////In Phôi/////////////
            //int sta = 0;
            //sta = Convert.ToInt32(Status.Value);
            //int sta = DungChung.Bien.InPhoi;

            //if (sta == 1) //in phôi tại phòng khám
            //{
            //    colTKXN.Visible = false;

            //}
            //if (sta == 2) //in phôi tại phòng CLS
            //{
            //    colTenCQCQ.Visible = false; colTenCQ.Visible = false;

            //    txtTenBN.Visible = false; txtTuoi.Visible = false; txtDiaChi.Visible = false;
            //    txtThe1.Visible = false; txtThe2.Visible = false; txtThe3.Visible = false; txtThe4.Visible = false; txtThe5.Visible = false;
            //    txtKhoa.Visible = false; txtBuong.Visible = false; txtGiuong.Visible = false; txtChanDoan.Visible = false;


            //    lab1.Visible = false; lab4.Visible = false; lab5.Visible = false; lab6.Visible = false;
            //    lab7.Visible = false; lab8.Visible = false; lab9.Visible = false; lab10.Visible = false;
            //    lab11.Visible = false; lab12.Visible = false; lab13.Visible = false; lab14.Visible = false; lab15.Visible = false;
            //    lab16.Visible = false; lab17.Visible = false; lab18.Visible = false; lab19.Visible = false; lab20.Visible = false;
            //    lab21.Visible = false; lab22.Visible = false; lab23.Visible = false; lab24.Visible = false; lab25.Visible = false; lab26.Visible = false;
            //    lab27.Visible = false; lab28.Visible = false; lab29.Visible = false; lab30.Visible = false; lab31.Visible = false; lab32.Visible = false;
            //    lab33.Visible = false; lab34.Visible = false; lab35.Visible = false; lab36.Visible = false; lab37.Visible = false; lab38.Visible = false;
            //    lab39.Visible = false; lab40.Visible = false; lab41.Visible = false; lab42.Visible = false; lab43.Visible = false; lab44.Visible = false;
            //    lab45.Visible = false; lab46.Visible = false; lab47.Visible = false; lab48.Visible = false; lab49.Visible = false; lab50.Visible = false;
            //    lab51.Visible = false; lab52.Visible = false; lab53.Visible = false; lab54.Visible = false; lab55.Visible = false; lab56.Visible = false;
            //    lab57.Visible = false; lab58.Visible = false; lab59.Visible = false; lab60.Visible = false; lab61.Visible = false; lab62.Visible = false;
            //    lab63.Visible = false; lab64.Visible = false; lab65.Visible = false; lab66.Visible = false; lab67.Visible = false; lab68.Visible = false;
            //    lab69.Visible = false; lab70.Visible = false; lab71.Visible = false; lab72.Visible = false; lab73.Visible = false; lab74.Visible = false;
            //    lab75.Visible = false; lab76.Visible = false; lab77.Visible = false; lab78.Visible = false; lab79.Visible = false; lab80.Visible = false;
            //    lab81.Visible = false; lab82.Visible = false; lab83.Visible = false; lab84.Visible = false; lab85.Visible = false; lab86.Visible = false;
            //    lab87.Visible = false; lab88.Visible = false; lab89.Visible = false; lab90.Visible = false; lab91.Visible = false; lab92.Visible = false;
            //    lab93.Visible = false;  lab94.Visible = false;  lab95.Visible = false;  lab96.Visible = false;  lab97.Visible = false;  lab98.Visible = false; 
            //    lab99.Visible = false; lab100.Visible = false; lab101.Visible = false; lab102.Visible = false; lab103.Visible = false; lab104.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab105.Visible = false; lab106.Visible = false; lab107.Visible = false;  lab108.Visible = false;  lab109.Visible = false;  lab110.Visible = false; 
            //    lab111.Visible = false;  lab112.Visible = false;  lab113.Visible = false;  


            //    txtThe1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtThe2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtThe3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtThe3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtThe4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtThe5.Borders = DevExpress.XtraPrinting.BorderSide.None;

            //    txtThuong.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtCapCuu.Borders = DevExpress.XtraPrinting.BorderSide.None;

            //    //lab1.Borders = DevExpress.XtraPrinting.BorderSide.None; lab4.Borders = DevExpress.XtraPrinting.BorderSide.None; lab5.Borders = DevExpress.XtraPrinting.BorderSide.None; lab6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    //lab7.Borders = DevExpress.XtraPrinting.BorderSide.None; lab8.Borders = DevExpress.XtraPrinting.BorderSide.None; lab9.Borders = DevExpress.XtraPrinting.BorderSide.None; lab10.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    //lab11.Borders = DevExpress.XtraPrinting.BorderSide.None; lab12.Borders = DevExpress.XtraPrinting.BorderSide.None; lab13.Borders = DevExpress.XtraPrinting.BorderSide.None; lab14.Borders = DevExpress.XtraPrinting.BorderSide.None; lab15.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    //lab16.Borders = DevExpress.XtraPrinting.BorderSide.None; lab17.Borders = DevExpress.XtraPrinting.BorderSide.None; lab18.Borders = DevExpress.XtraPrinting.BorderSide.None; 
            //    lab19.Borders = DevExpress.XtraPrinting.BorderSide.None; lab20.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab21.Borders = DevExpress.XtraPrinting.BorderSide.None; lab22.Borders = DevExpress.XtraPrinting.BorderSide.None; lab23.Borders = DevExpress.XtraPrinting.BorderSide.None; lab24.Borders = DevExpress.XtraPrinting.BorderSide.None; lab25.Borders = DevExpress.XtraPrinting.BorderSide.None; lab26.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab27.Borders = DevExpress.XtraPrinting.BorderSide.None; lab28.Borders = DevExpress.XtraPrinting.BorderSide.None; lab29.Borders = DevExpress.XtraPrinting.BorderSide.None; lab30.Borders = DevExpress.XtraPrinting.BorderSide.None; lab31.Borders = DevExpress.XtraPrinting.BorderSide.None; lab32.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab33.Borders = DevExpress.XtraPrinting.BorderSide.None; lab34.Borders = DevExpress.XtraPrinting.BorderSide.None; lab35.Borders = DevExpress.XtraPrinting.BorderSide.None; lab36.Borders = DevExpress.XtraPrinting.BorderSide.None; lab37.Borders = DevExpress.XtraPrinting.BorderSide.None; lab38.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab39.Borders = DevExpress.XtraPrinting.BorderSide.None; lab40.Borders = DevExpress.XtraPrinting.BorderSide.None; lab41.Borders = DevExpress.XtraPrinting.BorderSide.None; lab42.Borders = DevExpress.XtraPrinting.BorderSide.None; lab43.Borders = DevExpress.XtraPrinting.BorderSide.None; lab44.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab45.Borders = DevExpress.XtraPrinting.BorderSide.None; lab46.Borders = DevExpress.XtraPrinting.BorderSide.None; lab47.Borders = DevExpress.XtraPrinting.BorderSide.None; lab48.Borders = DevExpress.XtraPrinting.BorderSide.None; lab49.Borders = DevExpress.XtraPrinting.BorderSide.None; lab50.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab51.Borders = DevExpress.XtraPrinting.BorderSide.None; lab52.Borders = DevExpress.XtraPrinting.BorderSide.None; lab53.Borders = DevExpress.XtraPrinting.BorderSide.None; lab54.Borders = DevExpress.XtraPrinting.BorderSide.None; lab55.Borders = DevExpress.XtraPrinting.BorderSide.None; lab56.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab57.Borders = DevExpress.XtraPrinting.BorderSide.None; lab58.Borders = DevExpress.XtraPrinting.BorderSide.None; lab59.Borders = DevExpress.XtraPrinting.BorderSide.None; lab60.Borders = DevExpress.XtraPrinting.BorderSide.None; lab61.Borders = DevExpress.XtraPrinting.BorderSide.None; lab62.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab63.Borders = DevExpress.XtraPrinting.BorderSide.None; lab64.Borders = DevExpress.XtraPrinting.BorderSide.None; lab65.Borders = DevExpress.XtraPrinting.BorderSide.None; lab66.Borders = DevExpress.XtraPrinting.BorderSide.None; lab67.Borders = DevExpress.XtraPrinting.BorderSide.None; lab68.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab69.Borders = DevExpress.XtraPrinting.BorderSide.None; lab70.Borders = DevExpress.XtraPrinting.BorderSide.None; lab71.Borders = DevExpress.XtraPrinting.BorderSide.None; lab72.Borders = DevExpress.XtraPrinting.BorderSide.None; lab73.Borders = DevExpress.XtraPrinting.BorderSide.None; lab74.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab75.Borders = DevExpress.XtraPrinting.BorderSide.None; lab76.Borders = DevExpress.XtraPrinting.BorderSide.None; lab77.Borders = DevExpress.XtraPrinting.BorderSide.None; lab78.Borders = DevExpress.XtraPrinting.BorderSide.None; lab79.Borders = DevExpress.XtraPrinting.BorderSide.None; lab80.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab81.Borders = DevExpress.XtraPrinting.BorderSide.None; lab82.Borders = DevExpress.XtraPrinting.BorderSide.None; lab83.Borders = DevExpress.XtraPrinting.BorderSide.None; lab84.Borders = DevExpress.XtraPrinting.BorderSide.None; lab85.Borders = DevExpress.XtraPrinting.BorderSide.None; lab86.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab87.Borders = DevExpress.XtraPrinting.BorderSide.None; lab88.Borders = DevExpress.XtraPrinting.BorderSide.None; lab89.Borders = DevExpress.XtraPrinting.BorderSide.None; lab90.Borders = DevExpress.XtraPrinting.BorderSide.None; lab91.Borders = DevExpress.XtraPrinting.BorderSide.None; lab92.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab93.Borders = DevExpress.XtraPrinting.BorderSide.None; lab94.Borders = DevExpress.XtraPrinting.BorderSide.None; lab95.Borders = DevExpress.XtraPrinting.BorderSide.None; lab96.Borders = DevExpress.XtraPrinting.BorderSide.None; lab97.Borders = DevExpress.XtraPrinting.BorderSide.None; lab98.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab99.Borders = DevExpress.XtraPrinting.BorderSide.None; lab100.Borders = DevExpress.XtraPrinting.BorderSide.None; lab101.Borders = DevExpress.XtraPrinting.BorderSide.None; lab102.Borders = DevExpress.XtraPrinting.BorderSide.None; lab103.Borders = DevExpress.XtraPrinting.BorderSide.None; lab104.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab105.Borders = DevExpress.XtraPrinting.BorderSide.None; lab106.Borders = DevExpress.XtraPrinting.BorderSide.None; lab107.Borders = DevExpress.XtraPrinting.BorderSide.None; lab108.Borders = DevExpress.XtraPrinting.BorderSide.None; lab109.Borders = DevExpress.XtraPrinting.BorderSide.None; lab110.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab111.Borders = DevExpress.XtraPrinting.BorderSide.None; lab112.Borders = DevExpress.XtraPrinting.BorderSide.None; lab113.Borders = DevExpress.XtraPrinting.BorderSide.None; 

            //    colKQ1.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ6.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ10.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ16.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ21.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ1.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ6.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ10.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ16.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ26.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ1.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ6.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ10.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ21.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ26.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ1.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ10.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ16.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ21.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ26.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ1.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ10.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ16.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ21.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ29.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ6.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ10.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ16.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ21.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ29.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ29.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ29.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ29.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ34.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ34.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ34.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ34.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ34.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ39.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ39.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ41.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ41.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ41.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ41.Borders = DevExpress.XtraPrinting.BorderSide.None; 

            //    colBSDT.Visible = false;

            //}  
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
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "04012")
                lab92.Text = "Trưởng khoa cận lâm sàng".ToUpper();
            if (MaCBDT.Value != null)
            {
                colBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (Macb.Value != null)
            {
                colTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                lab89.Visible = false;
                lab90.Visible = false;
                colBSDT.Visible = false;
                colTKXN.Visible = false;
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
    }
}
