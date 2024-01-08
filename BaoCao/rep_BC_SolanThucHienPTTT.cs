using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_BC_SolanThucHienPTTT : DevExpress.XtraReports.UI.XtraReport
    {
        List<QLBV.FormThamSo.frm_BC_SolanThucHienPTTT.DSKP> _DSKP = new List<QLBV.FormThamSo.frm_BC_SolanThucHienPTTT.DSKP>();
        public rep_BC_SolanThucHienPTTT(List<QLBV.FormThamSo.frm_BC_SolanThucHienPTTT.DSKP> _lKP)
        {
            InitializeComponent();
            _DSKP = _lKP;
        }
        public int j = 0, f = 0;
        public void BindingData(int i, int k)
        {
            this.j = i;
            this.f = k;
            string ND = "nd";
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");

            i++;
            ND = "nd" + i.ToString();
            nd1.DataBindings.Add("Text", DataSource, ND);
            tongnd1.DataBindings.Add("Text", DataSource, ND);

            i++;
            ND = "nd" + i.ToString();
            nd2.DataBindings.Add("Text", DataSource, ND);
            tongnd2.DataBindings.Add("Text", DataSource, ND);

            i++;
            ND = "nd" + i.ToString();
            nd3.DataBindings.Add("Text", DataSource, ND);
            tongnd3.DataBindings.Add("Text", DataSource, ND);

            i++;
            ND = "nd" + i.ToString();
            nd4.DataBindings.Add("Text", DataSource, ND);
            tongnd4.DataBindings.Add("Text", DataSource, ND);

            i++;
            ND = "nd" + i.ToString();
            nd5.DataBindings.Add("Text", DataSource, ND);
            tongnd5.DataBindings.Add("Text", DataSource, ND);

            i++;
            ND = "nd" + i.ToString();
            nd6.DataBindings.Add("Text", DataSource, ND);
            tongnd6.DataBindings.Add("Text", DataSource, ND);

            i++;
            ND = "nd" + i.ToString();
            if (j + 7 < k)
            {
                nd7.DataBindings.Add("Text", DataSource, ND);
                tongnd7.DataBindings.Add("Text", DataSource, ND);
            }
            else
            {
                nd7.DataBindings.Add("Text", DataSource, "tc");
                tongnd7.DataBindings.Add("Text", DataSource, "tc");
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {

            for (int i = j; i < _DSKP.Count; i++)
            {
                switch (i - j)
                {
                    case 0:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            kp1.Value = _DSKP.Skip(i).First().tenkp;
                        }
                        break;
                    case 1:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            kp2.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 2:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            kp3.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 3:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            kp4.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 4:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            kp5.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 5:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            kp6.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                    case 6:
                        if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                        {
                            kp7.Value = _DSKP.Skip(i).First().tenkp;

                        }
                        break;
                }

            }
            if (j + 7 >= f)
                kp7.Value = "Toàn viện";
        }
    }
}