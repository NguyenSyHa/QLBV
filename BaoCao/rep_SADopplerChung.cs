using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace QLBV.BaoCao
{
    public partial class rep_SADopplerChung : DevExpress.XtraReports.UI.XtraReport
    {
        private int idcls;
     

        public rep_SADopplerChung()
        {
            InitializeComponent();
        }

        public rep_SADopplerChung(int idcls)
        {
            InitializeComponent();
            this.idcls = idcls;
          
        }
       
        internal void BindingData()
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qcls = (from cls in data.CLS.Where(p => p.IdCLS == idcls)
                        join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                        join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                        join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                        join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                        select new
                        {
                            bn.TenBNhan,
                            bn.DChi,
                            bn.Tuoi,
                            cls.MaCBth,
                           // cls.ChanDoan,
                            cd.KetLuan,
                            cd.LoiDan,
                            cd.MaDV,
                            clsct.KetQua,
                            dvct.STT_R,
                            clsct.DuongDan,
                            clsct.DuongDan2
                        }).FirstOrDefault();

            if(qcls != null)
            {
                
                celHoTen.Text = qcls.TenBNhan.ToUpper();
                celDiaChi.Text = qcls.DChi;
                celTuoi.Text = qcls.Tuoi == null ? "" : qcls.Tuoi.ToString();
                int madv = 0;
                if (qcls.MaDV != null)
                    madv = qcls.MaDV.Value;
                var qdv = data.DichVus.Where(p => p.MaDV == madv).Select(p => p.TenDV).FirstOrDefault();
                if(qdv != null)
                celLyDoKham.Text = qdv;
                celKL.Text = qcls.KetLuan;
                celLoiDan.Text = qcls.LoiDan;
                
                var qcb = data.CanBoes.Where(parameters => parameters.MaCB == qcls.MaCBth).FirstOrDefault();
                if (qcb != null)
                    celBS.Text = "BS. " + qcb.TenCB;
                clTenBS.Text = "BS. " + qcb.TenCB;
                if (qcls.KetQua != null)
                {
                    colKetQua.Text = qcls.KetQua;

                }
                celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
                if (qcls.DuongDan !=null && !String.IsNullOrEmpty(qcls.DuongDan))
                {
                    xrPictureBox1.Image = Image.FromFile(qcls.DuongDan);
                   
                }
                else
                {
                    xrPictureBox1.Image = null;
                   
                }
                if (qcls.DuongDan2 != null && !String.IsNullOrEmpty(qcls.DuongDan2))
                {
                    xrPictureBox2.Image = Image.FromFile(qcls.DuongDan2);
                }
                else
                {
                    xrPictureBox2.Image = null;
                    this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
                }

            }
           
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();

        }
    }
}
