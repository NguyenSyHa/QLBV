using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_SADopplerDongTinhMachChiDuoi : DevExpress.XtraReports.UI.XtraReport
    {
        private int idcls;
        private int mau = 1;// mẫu: 1: Doppler tĩnh mạch bệnh lý; 2: Doppler tĩnh mạch _BT

        public rep_SADopplerDongTinhMachChiDuoi()
        {
            InitializeComponent();
        }

        public rep_SADopplerDongTinhMachChiDuoi(int idcls, int mau)
        {
            InitializeComponent();
            this.idcls = idcls;
            this.mau = mau;
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
                            bn.NamSinh,
                            cls.MaCBth,
                            cls.ChanDoan,
                            cd.KetLuan,
                            cd.LoiDan,
                            clsct.KetQua,
                            dvct.STT_R
                        }).FirstOrDefault();

            if(qcls != null)
            {
                
                celHoTen.Text = qcls.TenBNhan.ToUpper();
                celDiaChi.Text = qcls.DChi;
                celTuoi.Text = qcls.NamSinh;
                celLyDoKham.Text = qcls.ChanDoan;
                celKL.Text = qcls.KetLuan;
                celLoiDan.Text = qcls.LoiDan;
                
                var qcb = data.CanBoes.Where(parameters => parameters.MaCB == qcls.MaCBth).FirstOrDefault();
                if (qcb != null)
                    celBS.Text = "BS. " + qcb.TenCB;
                if(qcls.KetQua != null)
                {
                    List<string> lkq = qcls.KetQua.Split('|').ToList();
                    if (lkq.Count > 0)
                        celKQ1.Text = lkq.First();
                    if (lkq.Count > 1)
                        celKQ2.Text = lkq.Skip(1).First();

                }
                celNgayThang.Text = DungChung.Bien.DiaDanh + ", Ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
            }
           
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (mau == 1)
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }
            else if(mau == 2)
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }

        }
    }
}
