using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV_Database;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNTongHop24012 : DevExpress.XtraReports.UI.XtraReport
    {
        QLBVEntities DataContect = new QLBVEntities(DungChung.Bien.StrCon);
        List<DichVuct> _ldvct = new List<DichVuct>();
        int GT = 0;
        public Rep_PhieuXNTongHop24012(int gtinh)
        {
            InitializeComponent();
            GT = gtinh;
        }

        internal void DataBinding()
        {
            celKQ24012.DataBindings.Add("Text", DataSource, "KetQua");
            celTenXN24012.DataBindings.Add("Text", DataSource, "TenDVct");
            celDonVi24012.DataBindings.Add("Text", DataSource, "DonVi");
            celMaQuyTrinh24012.DataBindings.Add("Text", DataSource, "MaQuyTrinh");
            celMaMay24012.DataBindings.Add("Text", DataSource, "MaMay");
            celTSBT24012.DataBindings.Add("Text", DataSource, "TSBT");
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            _ldvct = DataContect.DichVucts.ToList();
            if (this.GetCurrentColumnValue("MaDVct") != null && this.GetCurrentColumnValue("KetQua") != null)
            {
                string a = this.GetCurrentColumnValue("MaDVct").ToString();
                var dvct = _ldvct.Where(p => p.MaDVct == a).FirstOrDefault();
                if (dvct != null)
                {
                    try
                    {
                        if (GT == 0)
                        {
                            if (dvct.TSnuTu != null && dvct.TSnuDen != null)
                            {
                                double tstu = Convert.ToDouble(dvct.TSnuTu);
                                double tsden = Convert.ToDouble(dvct.TSnuDen);
                                if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                {
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                }
                                else
                                {
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                }


                                double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                if (kq < tstu)
                                {

                                    this.celKQ24012.ForeColor = System.Drawing.Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                    }
                                }
                                else if (kq > tsden)
                                {
                                    this.celKQ24012.ForeColor = Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                    }
                                }
                                else
                                {
                                    this.celKQ24012.ForeColor = Color.Black;
                                    celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                                    if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                    }
                                }
                            }
                            else if (dvct.TSnuTu != null && dvct.TSnuDen == null)//trị số nhỏ nhất
                            {
                                double tstu = Convert.ToDouble(dvct.TSnuTu);
                                double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                if (tstu > kq)
                                {
                                    this.celKQ24012.ForeColor = Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                    }
                                }
                                else
                                {
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Black;
                                    celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                                    if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                    }
                                }
                            }
                            else if (dvct.TSnuTu == null && dvct.TSnuDen != null)//giá trị lớn nhất
                            {
                                double tsden = Convert.ToDouble(dvct.TSnuDen);
                                double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                if (kq > tsden)
                                {
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                    }
                                }
                                else
                                {
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Black;
                                    celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                                    if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                    }
                                }

                            }
                            else
                            {
                                this.celKQ24012.ForeColor = System.Drawing.Color.Black;
                                celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                                if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                {
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                }
                                else
                                {
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                }
                            }
                        }
                        else
                        {
                            if (dvct.TSnTu != null && dvct.TSnDen != null)
                            {
                                double tstu = Convert.ToDouble(dvct.TSnTu);
                                double tsden = Convert.ToDouble(dvct.TSnDen);
                                double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                if (kq < tstu)
                                {
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    this.celKQ24012.ForeColor = Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);

                                }
                                else if (kq > tsden)
                                {
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    this.celKQ24012.ForeColor = Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                }
                                else
                                {
                                    this.celKQ24012.ForeColor = Color.Black;
                                    celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                                    this.celKQ24012.ForeColor = Color.Black;
                                    celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                                }
                                if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                {
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                }
                                else
                                {
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                }
                            }
                            else if (dvct.TSnTu != null && dvct.TSnDen == null)//trị số nhỏ nhất
                            {
                                double tstu = Convert.ToDouble(dvct.TSnTu);
                                double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                if (tstu > kq)
                                {
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                }
                                else
                                {
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Black;
                                    celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Black;
                                    celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                                }
                                if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                {
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                }
                                else
                                {
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                }
                            }
                            else if (dvct.TSnTu == null && dvct.TSnDen != null)//giá trị lớn nhất
                            {
                                double tsden = Convert.ToDouble(dvct.TSnDen);
                                double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));

                                if (kq >= tsden)
                                {
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Red;
                                    celKQ24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                }
                                else
                                {
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Black;
                                    celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                                    this.celKQ24012.ForeColor = System.Drawing.Color.Black;
                                    celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                                }
                                if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                {
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                }
                                else
                                {
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                    celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                }
                            }
                            else
                            {

                                this.celKQ24012.ForeColor = System.Drawing.Color.Black;
                                celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                        celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        this.celKQ24012.ForeColor = System.Drawing.Color.Black;
                        celKQ24012.Font = new Font("Times New Roman", 11, FontStyle.Regular);
                        if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                        {
                            celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                            celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                        }
                        else
                        {
                            celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                            celTenXN24012.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                        }
                    }

                }

            }
        }
    }
}
