using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BkXuatDuoc_VY001 : DevExpress.XtraReports.UI.XtraReport
    {
        List<QLBV.FormThamSo.Frm_BkXuatDuoc_VY01.DSKP> _DSKP = new List<QLBV.FormThamSo.Frm_BkXuatDuoc_VY01.DSKP>();
        private int num;
        public Rep_BkXuatDuoc_VY001()
        {
            //InitializeComponent();
        }
        public Rep_BkXuatDuoc_VY001(List<QLBV.FormThamSo.Frm_BkXuatDuoc_VY01.DSKP> _lKP)
        {
            InitializeComponent();
            _DSKP = _lKP;
        }

        public Rep_BkXuatDuoc_VY001(List<FormThamSo.Frm_BkXuatDuoc_VY01.DSKP> _DSKP, int num)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this._DSKP = _DSKP;
            this.num = num;
        }
        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "30003")
            {
                xrTable3.Visible = false;
                xrTable7.Visible = true;
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                colMaTam.DataBindings.Add("Text", DataSource, "MaTam");
                columnTen.DataBindings.Add("Text", DataSource, "TenDV");
                //colDG.DataBindings.Add("Text", DataSource, "DG");
                columnDV.DataBindings.Add("Text", DataSource, "DonVi");
                //colSL1.DataBindings.Add
                colNhomDVG1.DataBindings.Add("Text", DataSource, "TenNhomCT");
                colTieuNhomDichVu.DataBindings.Add("Text", DataSource, "TenTNDV");
                //xrTableCell88.DataBindings.Add("Text", DataSource, "TenDV");
                //colDVT.DataBindings.Add("Text", DataSource, "DonVi");
                if (Convert.ToInt32(DG.Value) == 1)
                {
                    columnDG.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                }
                else
                {
                    columnDG.DataBindings.Add("Text", DataSource, "NuocSX");
                }
                columnSLTong.DataBindings.Add("Text", DataSource, "TC").FormatString = DungChung.Bien.FormatString[1];
                if (num == 1)
                {
                    columnSL1.DataBindings.Add("Text", DataSource, "SL1").FormatString = DungChung.Bien.FormatString[0];
                    columnSL2.DataBindings.Add("Text", DataSource, "SL2").FormatString = DungChung.Bien.FormatString[0];
                    columnSL3.DataBindings.Add("Text", DataSource, "SL3").FormatString = DungChung.Bien.FormatString[0];
                    columnSL4.DataBindings.Add("Text", DataSource, "SL4").FormatString = DungChung.Bien.FormatString[0];
                    columnSL5.DataBindings.Add("Text", DataSource, "SL5").FormatString = DungChung.Bien.FormatString[0];
                    columnSL6.DataBindings.Add("Text", DataSource, "SL6").FormatString = DungChung.Bien.FormatString[0];
                    columnSL7.DataBindings.Add("Text", DataSource, "SL7").FormatString = DungChung.Bien.FormatString[0];
                    columnSL8.DataBindings.Add("Text", DataSource, "SL8").FormatString = DungChung.Bien.FormatString[0];
                    columnSL9.DataBindings.Add("Text", DataSource, "SL9").FormatString = DungChung.Bien.FormatString[0];
                    columnSL10.DataBindings.Add("Text", DataSource, "SL10").FormatString = DungChung.Bien.FormatString[0];
                    columnSL11.DataBindings.Add("Text", DataSource, "SL11").FormatString = DungChung.Bien.FormatString[0];
                    columnSL12.DataBindings.Add("Text", DataSource, "Sl12").FormatString = DungChung.Bien.FormatString[0];
                    columnSL13.DataBindings.Add("Text", DataSource, "SL13").FormatString = DungChung.Bien.FormatString[0];
                    columnSL14.DataBindings.Add("Text", DataSource, "SL14").FormatString = DungChung.Bien.FormatString[0];
                    columnSL15.DataBindings.Add("Text", DataSource, "SL15").FormatString = DungChung.Bien.FormatString[0];
                    columnSL16.DataBindings.Add("Text", DataSource, "SL16").FormatString = DungChung.Bien.FormatString[0];
                    columnSL17.DataBindings.Add("Text", DataSource, "SL17").FormatString = DungChung.Bien.FormatString[0];
                    columnSL18.DataBindings.Add("Text", DataSource, "SL18").FormatString = DungChung.Bien.FormatString[0];
                    columnSL19.DataBindings.Add("Text", DataSource, "SL19").FormatString = DungChung.Bien.FormatString[0];
                    columnSL20.DataBindings.Add("Text", DataSource, "SL20").FormatString = DungChung.Bien.FormatString[0];
                    columnSL21.DataBindings.Add("Text", DataSource, "SL21").FormatString = DungChung.Bien.FormatString[0];
                    columnSL22.DataBindings.Add("Text", DataSource, "SL22").FormatString = DungChung.Bien.FormatString[0];
                    columnSL23.DataBindings.Add("Text", DataSource, "SL23").FormatString = DungChung.Bien.FormatString[0];
                    columnSL24.DataBindings.Add("Text", DataSource, "SL24").FormatString = DungChung.Bien.FormatString[0];
                    columnSL25.DataBindings.Add("Text", DataSource, "SL25").FormatString = DungChung.Bien.FormatString[0];
                    columnSL26.DataBindings.Add("Text", DataSource, "SL26").FormatString = DungChung.Bien.FormatString[0];
                    columnSL27.DataBindings.Add("Text", DataSource, "SL27").FormatString = DungChung.Bien.FormatString[0];
                    columnSL28.DataBindings.Add("Text", DataSource, "SL28").FormatString = DungChung.Bien.FormatString[0];
                    columnSL29.DataBindings.Add("Text", DataSource, "SL29").FormatString = DungChung.Bien.FormatString[0];
                    columnSL30.DataBindings.Add("Text", DataSource, "SL30").FormatString = DungChung.Bien.FormatString[0];
                    columnSL31.DataBindings.Add("Text", DataSource, "SL31").FormatString = DungChung.Bien.FormatString[0];
                    columnSL32.DataBindings.Add("Text", DataSource, "SL32").FormatString = DungChung.Bien.FormatString[0];
                    columnSL33.DataBindings.Add("Text", DataSource, "SL33").FormatString = DungChung.Bien.FormatString[0];
                    columnSL34.DataBindings.Add("Text", DataSource, "SL34").FormatString = DungChung.Bien.FormatString[0];
                }
                else if (num == 2)
                {
                    columnSL1.DataBindings.Add("Text", DataSource, "SL35").FormatString = DungChung.Bien.FormatString[0];
                    columnSL2.DataBindings.Add("Text", DataSource, "SL36").FormatString = DungChung.Bien.FormatString[0];
                    columnSL3.DataBindings.Add("Text", DataSource, "SL37").FormatString = DungChung.Bien.FormatString[0];
                    columnSL4.DataBindings.Add("Text", DataSource, "SL38").FormatString = DungChung.Bien.FormatString[0];
                    columnSL5.DataBindings.Add("Text", DataSource, "SL39").FormatString = DungChung.Bien.FormatString[0];
                    columnSL6.DataBindings.Add("Text", DataSource, "SL40").FormatString = DungChung.Bien.FormatString[0];
                    columnSL7.DataBindings.Add("Text", DataSource, "SL41").FormatString = DungChung.Bien.FormatString[0];
                    columnSL8.DataBindings.Add("Text", DataSource, "SL42").FormatString = DungChung.Bien.FormatString[0];
                    columnSL9.DataBindings.Add("Text", DataSource, "SL43").FormatString = DungChung.Bien.FormatString[0];
                    columnSL10.DataBindings.Add("Text", DataSource, "SL44").FormatString = DungChung.Bien.FormatString[0];
                    columnSL11.DataBindings.Add("Text", DataSource, "SL45").FormatString = DungChung.Bien.FormatString[0];
                    columnSL12.DataBindings.Add("Text", DataSource, "Sl46").FormatString = DungChung.Bien.FormatString[0];
                    columnSL13.DataBindings.Add("Text", DataSource, "SL47").FormatString = DungChung.Bien.FormatString[0];
                    columnSL14.DataBindings.Add("Text", DataSource, "SL48").FormatString = DungChung.Bien.FormatString[0];
                    columnSL15.DataBindings.Add("Text", DataSource, "SL49").FormatString = DungChung.Bien.FormatString[0];
                    columnSL16.DataBindings.Add("Text", DataSource, "SL50").FormatString = DungChung.Bien.FormatString[0];
                    columnSL17.DataBindings.Add("Text", DataSource, "SL51").FormatString = DungChung.Bien.FormatString[0];
                    columnSL18.DataBindings.Add("Text", DataSource, "SL52").FormatString = DungChung.Bien.FormatString[0];
                    columnSL19.DataBindings.Add("Text", DataSource, "SL53").FormatString = DungChung.Bien.FormatString[0];
                    columnSL20.DataBindings.Add("Text", DataSource, "SL54").FormatString = DungChung.Bien.FormatString[0];
                    columnSL21.DataBindings.Add("Text", DataSource, "SL55").FormatString = DungChung.Bien.FormatString[0];
                    columnSL22.DataBindings.Add("Text", DataSource, "SL56").FormatString = DungChung.Bien.FormatString[0];
                    columnSL23.DataBindings.Add("Text", DataSource, "SL57").FormatString = DungChung.Bien.FormatString[0];
                    columnSL24.DataBindings.Add("Text", DataSource, "SL58").FormatString = DungChung.Bien.FormatString[0];
                    columnSL25.DataBindings.Add("Text", DataSource, "SL59").FormatString = DungChung.Bien.FormatString[0];
                    columnSL26.DataBindings.Add("Text", DataSource, "SL60").FormatString = DungChung.Bien.FormatString[0];
                    columnSL27.DataBindings.Add("Text", DataSource, "SL61").FormatString = DungChung.Bien.FormatString[0];
                    columnSL28.DataBindings.Add("Text", DataSource, "SL62").FormatString = DungChung.Bien.FormatString[0];
                    columnSL29.DataBindings.Add("Text", DataSource, "SL63").FormatString = DungChung.Bien.FormatString[0];
                    columnSL30.DataBindings.Add("Text", DataSource, "SL64").FormatString = DungChung.Bien.FormatString[0];
                    columnSL31.DataBindings.Add("Text", DataSource, "SL65").FormatString = DungChung.Bien.FormatString[0];
                    columnSL32.DataBindings.Add("Text", DataSource, "SL66").FormatString = DungChung.Bien.FormatString[0];
                    columnSL33.DataBindings.Add("Text", DataSource, "SL67").FormatString = DungChung.Bien.FormatString[0];
                    columnSL34.DataBindings.Add("Text", DataSource, "SL68").FormatString = DungChung.Bien.FormatString[0];
                }
                else if (num == 3)
                {
                    columnSL1.DataBindings.Add("Text", DataSource, "SL69").FormatString = DungChung.Bien.FormatString[0];
                    columnSL2.DataBindings.Add("Text", DataSource, "SL70").FormatString = DungChung.Bien.FormatString[0];
                    columnSL3.DataBindings.Add("Text", DataSource, "SL71").FormatString = DungChung.Bien.FormatString[0];
                    columnSL4.DataBindings.Add("Text", DataSource, "SL72").FormatString = DungChung.Bien.FormatString[0];
                    columnSL5.DataBindings.Add("Text", DataSource, "SL73").FormatString = DungChung.Bien.FormatString[0];
                    columnSL6.DataBindings.Add("Text", DataSource, "SL74").FormatString = DungChung.Bien.FormatString[0];
                    columnSL7.DataBindings.Add("Text", DataSource, "SL75").FormatString = DungChung.Bien.FormatString[0];
                    columnSL8.DataBindings.Add("Text", DataSource, "SL76").FormatString = DungChung.Bien.FormatString[0];
                    columnSL9.DataBindings.Add("Text", DataSource, "SL77").FormatString = DungChung.Bien.FormatString[0];
                    columnSL10.DataBindings.Add("Text", DataSource, "SL78").FormatString = DungChung.Bien.FormatString[0];
                    columnSL11.DataBindings.Add("Text", DataSource, "SL79").FormatString = DungChung.Bien.FormatString[0];
                    columnSL12.DataBindings.Add("Text", DataSource, "Sl80").FormatString = DungChung.Bien.FormatString[0];
                    columnSL13.DataBindings.Add("Text", DataSource, "SL81").FormatString = DungChung.Bien.FormatString[0];
                    columnSL14.DataBindings.Add("Text", DataSource, "SL82").FormatString = DungChung.Bien.FormatString[0];
                    columnSL15.DataBindings.Add("Text", DataSource, "SL83").FormatString = DungChung.Bien.FormatString[0];
                    columnSL16.DataBindings.Add("Text", DataSource, "SL84").FormatString = DungChung.Bien.FormatString[0];
                    columnSL17.DataBindings.Add("Text", DataSource, "SL85").FormatString = DungChung.Bien.FormatString[0];
                    columnSL18.DataBindings.Add("Text", DataSource, "SL86").FormatString = DungChung.Bien.FormatString[0];
                    columnSL19.DataBindings.Add("Text", DataSource, "SL87").FormatString = DungChung.Bien.FormatString[0];
                    columnSL20.DataBindings.Add("Text", DataSource, "SL88").FormatString = DungChung.Bien.FormatString[0];
                    columnSL21.DataBindings.Add("Text", DataSource, "SL89").FormatString = DungChung.Bien.FormatString[0];
                    columnSL22.DataBindings.Add("Text", DataSource, "SL90").FormatString = DungChung.Bien.FormatString[0];
                    columnSL23.DataBindings.Add("Text", DataSource, "SL91").FormatString = DungChung.Bien.FormatString[0];
                    columnSL24.DataBindings.Add("Text", DataSource, "SL92").FormatString = DungChung.Bien.FormatString[0];
                    columnSL25.DataBindings.Add("Text", DataSource, "SL93").FormatString = DungChung.Bien.FormatString[0];
                    columnSL26.DataBindings.Add("Text", DataSource, "SL94").FormatString = DungChung.Bien.FormatString[0];
                    columnSL27.DataBindings.Add("Text", DataSource, "SL95").FormatString = DungChung.Bien.FormatString[0];
                    columnSL28.DataBindings.Add("Text", DataSource, "SL96").FormatString = DungChung.Bien.FormatString[0];
                    columnSL29.DataBindings.Add("Text", DataSource, "SL97").FormatString = DungChung.Bien.FormatString[0];
                    columnSL30.DataBindings.Add("Text", DataSource, "SL98").FormatString = DungChung.Bien.FormatString[0];
                    columnSL31.DataBindings.Add("Text", DataSource, "SL99").FormatString = DungChung.Bien.FormatString[0];
                    columnSL32.DataBindings.Add("Text", DataSource, "SL100").FormatString = DungChung.Bien.FormatString[0];
                    columnSL33.DataBindings.Add("Text", DataSource, "SL101").FormatString = DungChung.Bien.FormatString[0];
                    columnSL34.DataBindings.Add("Text", DataSource, "SL102").FormatString = DungChung.Bien.FormatString[0];
                }
                //if (DungChung.Bien.MaBV == "26007")
                //    colXuatKhac.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
                GroupHeader1.GroupFields.Add(new GroupField("TenTNDV"));
                GroupHeader2.GroupFields.Add(new GroupField("TenNhomCT"));
            }
            else
            {
                xrTable7.Visible = false;
                xrTable3.Visible = true;
                SubBand2.Visible = false;
                SubBand1.Visible = true;
                colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
                //colDG.DataBindings.Add("Text", DataSource, "DG");
                colDV.DataBindings.Add("Text", DataSource, "DonVi");
                //colSL1.DataBindings.Add
                colNhomDVG1.DataBindings.Add("Text", DataSource, "TenNhomCT");
                colTieuNhomDichVu.DataBindings.Add("Text", DataSource, "TenTNDV");
                colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
                //colDVT.DataBindings.Add("Text", DataSource, "DonVi");
                if (Convert.ToInt32(DG.Value) == 1)
                {
                    colDG.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                }
                else
                {
                    colDG.DataBindings.Add("Text", DataSource, "NuocSX");
                }
                colKPTong.DataBindings.Add("Text", DataSource, "TC").FormatString = DungChung.Bien.FormatString[1];
                if (num == 1)
                {
                    colSL1.DataBindings.Add("Text", DataSource, "SL1").FormatString = DungChung.Bien.FormatString[0];
                    colSL2.DataBindings.Add("Text", DataSource, "SL2").FormatString = DungChung.Bien.FormatString[0];
                    colSL3.DataBindings.Add("Text", DataSource, "SL3").FormatString = DungChung.Bien.FormatString[0];
                    colSL4.DataBindings.Add("Text", DataSource, "SL4").FormatString = DungChung.Bien.FormatString[0];
                    colSL5.DataBindings.Add("Text", DataSource, "SL5").FormatString = DungChung.Bien.FormatString[0];
                    colSL6.DataBindings.Add("Text", DataSource, "SL6").FormatString = DungChung.Bien.FormatString[0];
                    colSL7.DataBindings.Add("Text", DataSource, "SL7").FormatString = DungChung.Bien.FormatString[0];
                    colSL8.DataBindings.Add("Text", DataSource, "SL8").FormatString = DungChung.Bien.FormatString[0];
                    colSL9.DataBindings.Add("Text", DataSource, "SL9").FormatString = DungChung.Bien.FormatString[0];
                    colSL10.DataBindings.Add("Text", DataSource, "SL10").FormatString = DungChung.Bien.FormatString[0];
                    colSL11.DataBindings.Add("Text", DataSource, "SL11").FormatString = DungChung.Bien.FormatString[0];
                    colSL12.DataBindings.Add("Text", DataSource, "Sl12").FormatString = DungChung.Bien.FormatString[0];
                    colSL13.DataBindings.Add("Text", DataSource, "SL13").FormatString = DungChung.Bien.FormatString[0];
                    colSL14.DataBindings.Add("Text", DataSource, "SL14").FormatString = DungChung.Bien.FormatString[0];
                    colSL15.DataBindings.Add("Text", DataSource, "SL15").FormatString = DungChung.Bien.FormatString[0];
                    colSL16.DataBindings.Add("Text", DataSource, "SL16").FormatString = DungChung.Bien.FormatString[0];
                    colSL17.DataBindings.Add("Text", DataSource, "SL17").FormatString = DungChung.Bien.FormatString[0];
                    colSL18.DataBindings.Add("Text", DataSource, "SL18").FormatString = DungChung.Bien.FormatString[0];
                    colSL19.DataBindings.Add("Text", DataSource, "SL19").FormatString = DungChung.Bien.FormatString[0];
                    colSL20.DataBindings.Add("Text", DataSource, "SL20").FormatString = DungChung.Bien.FormatString[0];
                    colSL21.DataBindings.Add("Text", DataSource, "SL21").FormatString = DungChung.Bien.FormatString[0];
                    colSL22.DataBindings.Add("Text", DataSource, "SL22").FormatString = DungChung.Bien.FormatString[0];
                    colSL23.DataBindings.Add("Text", DataSource, "SL23").FormatString = DungChung.Bien.FormatString[0];
                    colSl24.DataBindings.Add("Text", DataSource, "SL24").FormatString = DungChung.Bien.FormatString[0];
                    colSL25.DataBindings.Add("Text", DataSource, "SL25").FormatString = DungChung.Bien.FormatString[0];
                    colSL26.DataBindings.Add("Text", DataSource, "SL26").FormatString = DungChung.Bien.FormatString[0];
                    colSL27.DataBindings.Add("Text", DataSource, "SL27").FormatString = DungChung.Bien.FormatString[0];
                    colSL28.DataBindings.Add("Text", DataSource, "SL28").FormatString = DungChung.Bien.FormatString[0];
                    colSL29.DataBindings.Add("Text", DataSource, "SL29").FormatString = DungChung.Bien.FormatString[0];
                    colSL30.DataBindings.Add("Text", DataSource, "SL30").FormatString = DungChung.Bien.FormatString[0];
                    colSL31.DataBindings.Add("Text", DataSource, "SL31").FormatString = DungChung.Bien.FormatString[0];
                    colSL32.DataBindings.Add("Text", DataSource, "SL32").FormatString = DungChung.Bien.FormatString[0];
                    colSL33.DataBindings.Add("Text", DataSource, "SL33").FormatString = DungChung.Bien.FormatString[0];
                    colSL34.DataBindings.Add("Text", DataSource, "SL34").FormatString = DungChung.Bien.FormatString[0];
                }
                else if (num == 2)
                {
                    colSL1.DataBindings.Add("Text", DataSource, "SL35").FormatString = DungChung.Bien.FormatString[0];
                    colSL2.DataBindings.Add("Text", DataSource, "SL36").FormatString = DungChung.Bien.FormatString[0];
                    colSL3.DataBindings.Add("Text", DataSource, "SL37").FormatString = DungChung.Bien.FormatString[0];
                    colSL4.DataBindings.Add("Text", DataSource, "SL38").FormatString = DungChung.Bien.FormatString[0];
                    colSL5.DataBindings.Add("Text", DataSource, "SL39").FormatString = DungChung.Bien.FormatString[0];
                    colSL6.DataBindings.Add("Text", DataSource, "SL40").FormatString = DungChung.Bien.FormatString[0];
                    colSL7.DataBindings.Add("Text", DataSource, "SL41").FormatString = DungChung.Bien.FormatString[0];
                    colSL8.DataBindings.Add("Text", DataSource, "SL42").FormatString = DungChung.Bien.FormatString[0];
                    colSL9.DataBindings.Add("Text", DataSource, "SL43").FormatString = DungChung.Bien.FormatString[0];
                    colSL10.DataBindings.Add("Text", DataSource, "SL44").FormatString = DungChung.Bien.FormatString[0];
                    colSL11.DataBindings.Add("Text", DataSource, "SL45").FormatString = DungChung.Bien.FormatString[0];
                    colSL12.DataBindings.Add("Text", DataSource, "Sl56").FormatString = DungChung.Bien.FormatString[0];
                    colSL13.DataBindings.Add("Text", DataSource, "SL47").FormatString = DungChung.Bien.FormatString[0];
                    colSL14.DataBindings.Add("Text", DataSource, "SL48").FormatString = DungChung.Bien.FormatString[0];
                    colSL15.DataBindings.Add("Text", DataSource, "SL49").FormatString = DungChung.Bien.FormatString[0];
                    colSL16.DataBindings.Add("Text", DataSource, "SL50").FormatString = DungChung.Bien.FormatString[0];
                    colSL17.DataBindings.Add("Text", DataSource, "SL51").FormatString = DungChung.Bien.FormatString[0];
                    colSL18.DataBindings.Add("Text", DataSource, "SL52").FormatString = DungChung.Bien.FormatString[0];
                    colSL19.DataBindings.Add("Text", DataSource, "SL53").FormatString = DungChung.Bien.FormatString[0];
                    colSL20.DataBindings.Add("Text", DataSource, "SL54").FormatString = DungChung.Bien.FormatString[0];
                    colSL21.DataBindings.Add("Text", DataSource, "SL55").FormatString = DungChung.Bien.FormatString[0];
                    colSL22.DataBindings.Add("Text", DataSource, "SL56").FormatString = DungChung.Bien.FormatString[0];
                    colSL23.DataBindings.Add("Text", DataSource, "SL57").FormatString = DungChung.Bien.FormatString[0];
                    colSl24.DataBindings.Add("Text", DataSource, "SL58").FormatString = DungChung.Bien.FormatString[0];
                    colSL25.DataBindings.Add("Text", DataSource, "SL59").FormatString = DungChung.Bien.FormatString[0];
                    colSL26.DataBindings.Add("Text", DataSource, "SL60").FormatString = DungChung.Bien.FormatString[0];
                    colSL27.DataBindings.Add("Text", DataSource, "SL61").FormatString = DungChung.Bien.FormatString[0];
                    colSL28.DataBindings.Add("Text", DataSource, "SL62").FormatString = DungChung.Bien.FormatString[0];
                    colSL29.DataBindings.Add("Text", DataSource, "SL63").FormatString = DungChung.Bien.FormatString[0];
                    colSL30.DataBindings.Add("Text", DataSource, "SL64").FormatString = DungChung.Bien.FormatString[0];
                    colSL31.DataBindings.Add("Text", DataSource, "SL65").FormatString = DungChung.Bien.FormatString[0];
                    colSL32.DataBindings.Add("Text", DataSource, "SL66").FormatString = DungChung.Bien.FormatString[0];
                    colSL33.DataBindings.Add("Text", DataSource, "SL67").FormatString = DungChung.Bien.FormatString[0];
                    colSL34.DataBindings.Add("Text", DataSource, "SL68").FormatString = DungChung.Bien.FormatString[0];
                }
                else if (num == 3)
                {
                    colSL1.DataBindings.Add("Text", DataSource, "SL69").FormatString = DungChung.Bien.FormatString[0];
                    colSL2.DataBindings.Add("Text", DataSource, "SL70").FormatString = DungChung.Bien.FormatString[0];
                    colSL3.DataBindings.Add("Text", DataSource, "SL71").FormatString = DungChung.Bien.FormatString[0];
                    colSL4.DataBindings.Add("Text", DataSource, "SL72").FormatString = DungChung.Bien.FormatString[0];
                    colSL5.DataBindings.Add("Text", DataSource, "SL73").FormatString = DungChung.Bien.FormatString[0];
                    colSL6.DataBindings.Add("Text", DataSource, "SL74").FormatString = DungChung.Bien.FormatString[0];
                    colSL7.DataBindings.Add("Text", DataSource, "SL75").FormatString = DungChung.Bien.FormatString[0];
                    colSL8.DataBindings.Add("Text", DataSource, "SL76").FormatString = DungChung.Bien.FormatString[0];
                    colSL9.DataBindings.Add("Text", DataSource, "SL77").FormatString = DungChung.Bien.FormatString[0];
                    colSL10.DataBindings.Add("Text", DataSource, "SL78").FormatString = DungChung.Bien.FormatString[0];
                    colSL11.DataBindings.Add("Text", DataSource, "SL79").FormatString = DungChung.Bien.FormatString[0];
                    colSL12.DataBindings.Add("Text", DataSource, "Sl80").FormatString = DungChung.Bien.FormatString[0];
                    colSL13.DataBindings.Add("Text", DataSource, "SL81").FormatString = DungChung.Bien.FormatString[0];
                    colSL14.DataBindings.Add("Text", DataSource, "SL82").FormatString = DungChung.Bien.FormatString[0];
                    colSL15.DataBindings.Add("Text", DataSource, "SL83").FormatString = DungChung.Bien.FormatString[0];
                    colSL16.DataBindings.Add("Text", DataSource, "SL84").FormatString = DungChung.Bien.FormatString[0];
                    colSL17.DataBindings.Add("Text", DataSource, "SL85").FormatString = DungChung.Bien.FormatString[0];
                    colSL18.DataBindings.Add("Text", DataSource, "SL86").FormatString = DungChung.Bien.FormatString[0];
                    colSL19.DataBindings.Add("Text", DataSource, "SL87").FormatString = DungChung.Bien.FormatString[0];
                    colSL20.DataBindings.Add("Text", DataSource, "SL88").FormatString = DungChung.Bien.FormatString[0];
                    colSL21.DataBindings.Add("Text", DataSource, "SL89").FormatString = DungChung.Bien.FormatString[0];
                    colSL22.DataBindings.Add("Text", DataSource, "SL90").FormatString = DungChung.Bien.FormatString[0];
                    colSL23.DataBindings.Add("Text", DataSource, "SL91").FormatString = DungChung.Bien.FormatString[0];
                    colSl24.DataBindings.Add("Text", DataSource, "SL92").FormatString = DungChung.Bien.FormatString[0];
                    colSL25.DataBindings.Add("Text", DataSource, "SL93").FormatString = DungChung.Bien.FormatString[0];
                    colSL26.DataBindings.Add("Text", DataSource, "SL94").FormatString = DungChung.Bien.FormatString[0];
                    colSL27.DataBindings.Add("Text", DataSource, "SL95").FormatString = DungChung.Bien.FormatString[0];
                    colSL28.DataBindings.Add("Text", DataSource, "SL96").FormatString = DungChung.Bien.FormatString[0];
                    colSL29.DataBindings.Add("Text", DataSource, "SL97").FormatString = DungChung.Bien.FormatString[0];
                    colSL30.DataBindings.Add("Text", DataSource, "SL98").FormatString = DungChung.Bien.FormatString[0];
                    colSL31.DataBindings.Add("Text", DataSource, "SL99").FormatString = DungChung.Bien.FormatString[0];
                    colSL32.DataBindings.Add("Text", DataSource, "SL100").FormatString = DungChung.Bien.FormatString[0];
                    colSL33.DataBindings.Add("Text", DataSource, "SL101").FormatString = DungChung.Bien.FormatString[0];
                    colSL34.DataBindings.Add("Text", DataSource, "SL102").FormatString = DungChung.Bien.FormatString[0];
                }
                //if (DungChung.Bien.MaBV == "26007")
                //    colXuatKhac.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
                GroupHeader1.GroupFields.Add(new GroupField("TenTNDV"));
                GroupHeader2.GroupFields.Add(new GroupField("TenNhomCT"));
            }
            
        }
   

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if(num == 1)
            {
                #region
                for (int i = 0; i < _DSKP.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T1.Value = _DSKP.Skip(i).First().tenkp;
                            }
                            break;
                        case 1:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T2.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 2:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T3.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 3:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T4.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 4:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T5.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 5:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T6.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 6:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T7.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 7:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T8.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 8:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T9.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 9:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T10.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 10:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T11.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 11:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T12.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 12:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T13.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 13:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T14.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 14:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T15.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 15:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T16.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 16:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T17.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 17:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T18.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 18:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T19.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 19:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T20.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 20:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T21.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 21:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T22.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 22:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T23.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 23:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T24.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 24:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T25.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 25:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T26.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 26:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T27.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 27:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T28.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 28:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T29.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 29:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T30.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 30:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T31.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 31:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T32.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 32:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T33.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 33:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T34.Value = _DSKP.Skip(i).First().tenkp;

                            }

                            break;

                    }
                }
                #endregion
            }
           else if (num == 2)
            {
                #region
                for (int i = 0; i < _DSKP.Count; i++)
                {
                    switch (i)
                    {
                        case 34:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T1.Value = _DSKP.Skip(i).First().tenkp;
                            }
                            break;
                        case 35:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T2.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 36:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T3.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 37:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T4.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 38:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T5.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 39:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T6.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 40:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T7.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 41:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T8.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 42:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T9.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 43:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T10.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 44:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T11.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 45:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T12.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 46:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T13.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 47:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T14.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 48:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T15.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 49:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T16.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 50:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T17.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 51:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T18.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 52:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T19.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 53:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T20.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 54:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T21.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 55:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T22.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 56:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T23.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 57:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T24.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 58:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T25.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 59:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T26.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 60:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T27.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 61:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T28.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 62:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T29.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 63:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T30.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 64:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T31.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 65:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T32.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 66:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T33.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 67:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T34.Value = _DSKP.Skip(i).First().tenkp;

                            }

                            break;

                    }
                }
                #endregion
            }
            else if (num == 3)
            {
                #region
                for (int i = 0; i < _DSKP.Count; i++)
                {
                    switch (i)
                    {
                        case 68:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T1.Value = _DSKP.Skip(i).First().tenkp;
                            }
                            break;
                        case 69:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T2.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 70:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T3.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 71:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T4.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 72:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T5.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 73:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T6.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 74:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T7.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 75:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T8.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 76:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T9.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 77:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T10.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 78:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T11.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 79:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T12.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 80:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T13.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 81:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T14.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 82:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T15.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 83:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T16.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 84:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T17.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 85:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T18.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 86:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T19.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 87:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T20.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 88:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T21.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 89:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T22.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 90:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T23.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 91:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T24.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 92:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T25.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 93:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T26.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 94:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T27.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 95:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T28.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 96:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T29.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 97:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T30.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 98:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T31.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 99:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T32.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 100:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T33.Value = _DSKP.Skip(i).First().tenkp;

                            }
                            break;
                        case 101:
                            if (_DSKP.Skip(i).First().tenkp != null && _DSKP.Skip(i).First().tenkp != "")
                            {
                                T34.Value = _DSKP.Skip(i).First().tenkp;

                            }

                            break;

                    }
                }
                #endregion
            }
            if (Convert.ToInt32(DG.Value) == 1)
            {
                xrTableCell22.Text = "Đơn giá";
                xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

            }
            else
            {
                xrTableCell22.Text = "Nước sản xuất";
            }
            TenBV.Text = DungChung.Bien.TenCQ;
            colTenCQ.Text = DungChung.Bien.TenCQCQ;
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "26007")
            //    colT34.Text = "Xuất khác";
            //else
            //    colT34.Text = "";
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
            }
        }    
}
