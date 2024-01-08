using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_BieuCongKhaiThuocHangNgay : DevExpress.XtraEditors.XtraForm
    {
        int mau = 0;
        string st1 = "", st2 = "", st3 = "", st4 = "", st5 = "", st6 = "", st7 = "", st8 = "", st9 = "", st10 = "",
              st11 = "", st12 = "", st13 = "", st14 = "", st15 = "", st16 = "", st17 = "", st18 = "", st19 = "", st20 = "",
              st21 = "", st22 = "", st23 = "", st24 = "", st25 = "", st26 = "", st27 = "", st28 = "", st29 = "", st30 = "",
              st31 = "", st32 = "", st33 = "", st34 = "", st35 = "", st36 = "", st37 = "", st38 = "", st39 = "", st40 = "",
              st41 = "", st42 = "", st43 = "", st44 = "", st45 = "", st46 = "", st47 = "", st48 = "", st49 = "", st50 = "",
              st51 = "", st52 = "", st53 = "", st54 = "", st55 = "", st56 = "", st57 = "", st58 = "", st59 = "", st60 = "",
              st61 = "", st62 = "", st63 = "", st64 = "", st65 = "", st66 = "", st67 = "", st68 = "", st69 = "", st70 = "",
              st71 = "", st72 = "", st73 = "", st74 = "", st75 = "", st76 = "", st77 = "", st78 = "", st79 = "", st80 = "",
              st81 = "", st82 = "", st83 = "", st84 = "", st85 = "", st86 = "", st87 = "", st88 = "", st89 = "", st90 = "",
              st91 = "", st92 = "", st93 = "", st94 = "", st95 = "", st96 = "", st97 = "", st98 = "", st99 = "", st100 = "",
              st101 = "", st102 = "", st103 = "", st104 = "", st105 = "", st106 = "", st107 = "", st108 = "", st109 = "", st110 = "",
              st111 = "", st112 = "", st113 = "", st114 = "", st115 = "", st116 = "", st117 = "", st118 = "", st119 = "", st120 = "",
              st121 = "", st122 = "", st123 = "", st124 = "", st125 = "", st126 = "", st127 = "", st128 = "", st129 = "", st130 = "",
              st131 = "", st132 = "", st133 = "", st134 = "", st135 = "", st136 = "", st137 = "", st138 = "", st139 = "", st140 = "",
              st141 = "", st142 = "", st143 = "", st144 = "", st145 = "", st146 = "", st147 = "", st148 = "", st149 = "", st150 = "",
              st151 = "", st152 = "", st153 = "", st154 = "", st155 = "", st156 = "", st157 = "", st158 = "", st159 = "", st160 = "",
              st161 = "", st162 = "", st163 = "", st164 = "", st165 = "", st166 = "", st167 = "", st168 = "", st169 = "", st170 = "";
        public Frm_BieuCongKhaiThuocHangNgay()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_BieuCongKhaiThuocHangNgay_Load(object sender, EventArgs e)
        {
            LupNgayTu.DateTime = System.DateTime.Now;
            LupNgayDen.DateTime = System.DateTime.Now;
            var KP = (from kp1 in _Data.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng")) select new { kp1.TenKP, kp1.MaKP }).ToList();
            if (KP.Count > 0)
            {
                LupKhoaPhong.Properties.DataSource = KP.OrderBy(p => p.TenKP).ToList();
            }
            //radM.SelectedIndex = 1;
            radNT.SelectedIndex = 2;
            chkBoxung.Checked = true;
            chkThuongxuyen.Checked = true;
            chkTrathuoc.Checked = true;

        }

        private void Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Khai báo lớp dữ liệu 
        private class Tamtra
        {
            //Kahi báo cột Đơn vị tính
            private string _DVT1;

            public string DVT1
            {
                get { return _DVT1; }
                set { _DVT1 = value; }
            }


            private string _DVT2;

            public string DVT2
            {
                get { return _DVT2; }
                set { _DVT2 = value; }
            }

            
            private string _DVT3;

            public string DVT3
            {
                get { return _DVT3; }
                set { _DVT3 = value; }
            }
            private string _DVT4;

            public string DVT4
            {
                get { return _DVT4; }
                set { _DVT4 = value; }
            }

           
            private string _DVT5;

            public string DVT5
            {
                get { return _DVT5; }
                set { _DVT5 = value; }
            }
            private string _DVT6;

            public string DVT6
            {
                get { return _DVT6; }
                set { _DVT6 = value; }
            }
            private string _DVT7;

            public string DVT7
            {
                get { return _DVT7; }
                set { _DVT7 = value; }
            }
            private string _DVT8;

            public string DVT8
            {
                get { return _DVT8; }
                set { _DVT8 = value; }
            }
            private string _DVT9;

            public string DVT9
            {
                get { return _DVT9; }
                set { _DVT9 = value; }
            }
            private string _DVT10;

            public string DVT10
            {
                get { return _DVT10; }
                set { _DVT10 = value; }
            }
            private string _DVT11;

            public string DVT11
            {
                get { return _DVT11; }
                set { _DVT11 = value; }
            }
            private string _DVT12;

            public string DVT12
            {
                get { return _DVT12; }
                set { _DVT12 = value; }
            }
            private string _DVT13;

            public string DVT13
            {
                get { return _DVT13; }
                set { _DVT13 = value; }
            }
            private string _DVT14;

            public string DVT14
            {
                get { return _DVT14; }
                set { _DVT14 = value; }
            }
            private string _DVT15;

            public string DVT15
            {
                get { return _DVT15; }
                set { _DVT15 = value; }
            }
            private string _DVT16;

            public string DVT16
            {
                get { return _DVT16; }
                set { _DVT16 = value; }
            }
            private string _DVT17;

            public string DVT17
            {
                get { return _DVT17; }
                set { _DVT17 = value; }
            }
            private string _DVT18;

            public string DVT18
            {
                get { return _DVT18; }
                set { _DVT18 = value; }
            }
            private string _DVT19;

            public string DVT19
            {
                get { return _DVT19; }
                set { _DVT19 = value; }
            }
            private string _DVT20;

            public string DVT20
            {
                get { return _DVT20; }
                set { _DVT20 = value; }
            }
            private string _DVT21;

            public string DVT21
            {
                get { return _DVT21; }
                set { _DVT21 = value; }
            }
            private string _DVT22;

            public string DVT22
            {
                get { return _DVT22; }
                set { _DVT22 = value; }
            }
            private string _DVT23;

            public string DVT23
            {
                get { return _DVT23; }
                set { _DVT23 = value; }
            }
            private string _DVT24;

            public string DVT24
            {
                get { return _DVT24; }
                set { _DVT24 = value; }
            }
            private string _DVT25;

            public string DVT25
            {
                get { return _DVT25; }
                set { _DVT25 = value; }
            }
            private string _DVT26;

            public string DVT26
            {
                get { return _DVT26; }
                set { _DVT26 = value; }
            }
            private string _DVT27;

            public string DVT27
            {
                get { return _DVT27; }
                set { _DVT27 = value; }
            }
            private string _DVT28;

            public string DVT28
            {
                get { return _DVT28; }
                set { _DVT28 = value; }
            }
            private string _DVT29;

            public string DVT29
            {
                get { return _DVT29; }
                set { _DVT29 = value; }
            }
            private string _DVT30;

            public string DVT30
            {
                get { return _DVT30; }
                set { _DVT30 = value; }
            }
            private string _DVT31;

            public string DVT31
            {
                get { return _DVT31; }
                set { _DVT31 = value; }
            }
            private string _DVT32;

            public string DVT32
            {
                get { return _DVT32; }
                set { _DVT32 = value; }
            }
            private string _DVT33;

            public string DVT33
            {
                get { return _DVT33; }
                set { _DVT33 = value; }
            }
            private string _DVT34;

            public string DVT34
            {
                get { return _DVT34; }
                set { _DVT34 = value; }
            }
            private string _DVT35;

            public string DVT35
            {
                get { return _DVT35; }
                set { _DVT35 = value; }
            }
            private string _DVT36;

            public string DVT36
            {
                get { return _DVT36; }
                set { _DVT36 = value; }
            }
            private string _DVT37;

            public string DVT37
            {
                get { return _DVT37; }
                set { _DVT37 = value; }
            }
            private string _DVT38;

            public string DVT38
            {
                get { return _DVT38; }
                set { _DVT38 = value; }
            }
            private string _DVT39;

            public string DVT39
            {
                get { return _DVT39; }
                set { _DVT39 = value; }
            }
            private string _DVT40;

            public string DVT40
            {
                get { return _DVT40; }
                set { _DVT40 = value; }
            }
            private string _DVT41;

            public string DVT41
            {
                get { return _DVT41; }
                set { _DVT41 = value; }
            }
            private string _DVT42;

            public string DVT42
            {
                get { return _DVT42; }
                set { _DVT42 = value; }
            }
            private string _DVT43;

            public string DVT43
            {
                get { return _DVT43; }
                set { _DVT43 = value; }
            }
            private string _DVT44;

            public string DVT44
            {
                get { return _DVT44; }
                set { _DVT44 = value; }
            }
            private string _DVT45;

            public string DVT45
            {
                get { return _DVT45; }
                set { _DVT45 = value; }
            }
            private string _DVT46;

            public string DVT46
            {
                get { return _DVT46; }
                set { _DVT46 = value; }
            }

            private string _DVT47;

            public string DVT47
            {
                get { return _DVT47; }
                set { _DVT47 = value; }
            }
            private string _DVT48;

            public string DVT48
            {
                get { return _DVT48; }
                set { _DVT48 = value; }
            }
            private string _DVT49;

            public string DVT49
            {
                get { return _DVT49; }
                set { _DVT49 = value; }
            }
            private string _DVT50;

            public string DVT50
            {
                get { return _DVT50; }
                set { _DVT50 = value; }
            }
            private string _DVT51;

            public string DVT51
            {
                get { return _DVT51; }
                set { _DVT51 = value; }
            }
            private string _DVT52;

            public string DVT52
            {
                get { return _DVT52; }
                set { _DVT52 = value; }
            }
            private string _DVT53;

            public string DVT53
            {
                get { return _DVT53; }
                set { _DVT53 = value; }
            }
            private string _DVT54;

            public string DVT54
            {
                get { return _DVT54; }
                set { _DVT54 = value; }
            }
            private string _DVT55;

            public string DVT55
            {
                get { return _DVT55; }
                set { _DVT55 = value; }
            }
            private string _DVT56;

            public string DVT56
            {
                get { return _DVT56; }
                set { _DVT56 = value; }
            }
            private string _DVT57;

            public string DVT57
            {
                get { return _DVT57; }
                set { _DVT57 = value; }
            }
            private string _DVT58;

            public string DVT58
            {
                get { return _DVT58; }
                set { _DVT58 = value; }
            }
            private string _DVT59;

            public string DVT59
            {
                get { return _DVT59; }
                set { _DVT59 = value; }
            }
            private string _DVT60;

            public string DVT60
            {
                get { return _DVT60; }
                set { _DVT60 = value; }
            }
            private string _DVT61;

            public string DVT61
            {
                get { return _DVT61; }
                set { _DVT61 = value; }
            }
            private string _DVT62;

            public string DVT62
            {
                get { return _DVT62; }
                set { _DVT62 = value; }
            }
            private string _DVT63;

            public string DVT63
            {
                get { return _DVT63; }
                set { _DVT63 = value; }
            }
            private string _DVT64;

            public string DVT64
            {
                get { return _DVT64; }
                set { _DVT64 = value; }
            }
            private string _DVT65;

            public string DVT65
            {
                get { return _DVT65; }
                set { _DVT65 = value; }
            }
            private string _DVT66;

            public string DVT66
            {
                get { return _DVT66; }
                set { _DVT66 = value; }
            }
            private string _DVT67;

            public string DVT67
            {
                get { return _DVT67; }
                set { _DVT67 = value; }
            }
            private string _DVT68;

            public string DVT68
            {
                get { return _DVT68; }
                set { _DVT68 = value; }
            }
            private string _DVT69;

            public string DVT69
            {
                get { return _DVT69; }
                set { _DVT69 = value; }
            }
            private string _DVT70;

            public string DVT70
            {
                get { return _DVT70; }
                set { _DVT70 = value; }
            }
            private string _DVT71;

            public string DVT71
            {
                get { return _DVT71; }
                set { _DVT71 = value; }
            }
            private string _DVT72;

            public string DVT72
            {
                get { return _DVT72; }
                set { _DVT72 = value; }
            }
            private string _DVT73;

            public string DVT73
            {
                get { return _DVT73; }
                set { _DVT73 = value; }
            }
            private string _DVT74;

            public string DVT74
            {
                get { return _DVT74; }
                set { _DVT74 = value; }
            }
            private string _DVT75;

            public string DVT75
            {
                get { return _DVT75; }
                set { _DVT75 = value; }
            }
            private string _DVT76;

            public string DVT76
            {
                get { return _DVT76; }
                set { _DVT76 = value; }
            }
            private string _DVT77;

            public string DVT77
            {
                get { return _DVT77; }
                set { _DVT77 = value; }
            }
            private string _DVT78;

            public string DVT78
            {
                get { return _DVT78; }
                set { _DVT78 = value; }
            }
            private string _DVT79;

            public string DVT79
            {
                get { return _DVT79; }
                set { _DVT79 = value; }
            }
            private string _DVT80;

            public string DVT80
            {
                get { return _DVT80; }
                set { _DVT80 = value; }
            }
            private string _DVT81;

            public string DVT81
            {
                get { return _DVT81; }
                set { _DVT81 = value; }
            }
            private string _DVT82;

            public string DVT82
            {
                get { return _DVT82; }
                set { _DVT82 = value; }
            }
            private string _DVT83;

            public string DVT83
            {
                get { return _DVT83; }
                set { _DVT83 = value; }
            }
            private string _DVT84;

            public string DVT84
            {
                get { return _DVT84; }
                set { _DVT84 = value; }
            }
            private string _DVT85;

            public string DVT85
            {
                get { return _DVT85; }
                set { _DVT85 = value; }
            }
            private string _DVT86;

            public string DVT86
            {
                get { return _DVT86; }
                set { _DVT86 = value; }
            }
            private string _DVT87;

            public string DVT87
            {
                get { return _DVT87; }
                set { _DVT87 = value; }
            }
            private string _DVT88;

            public string DVT88
            {
                get { return _DVT88; }
                set { _DVT88 = value; }
            }
            private string _DVT89;

            public string DVT89
            {
                get { return _DVT89; }
                set { _DVT89 = value; }
            }
            private string _DVT90;

            public string DVT90
            {
                get { return _DVT90; }
                set { _DVT90 = value; }
            }
            private string _DVT91;

            public string DVT91
            {
                get { return _DVT91; }
                set { _DVT91 = value; }
            }
            private string _DVT92;

            public string DVT92
            {
                get { return _DVT92; }
                set { _DVT92 = value; }
            }
            private string _DVT93;

            public string DVT93
            {
                get { return _DVT93; }
                set { _DVT93 = value; }
            }
            private string _DVT94;

            public string DVT94
            {
                get { return _DVT94; }
                set { _DVT94 = value; }
            }
            private string _DVT95;

            public string DVT95
            {
                get { return _DVT95; }
                set { _DVT95 = value; }
            }
            private string _DVT96;

            public string DVT96
            {
                get { return _DVT96; }
                set { _DVT96 = value; }
            }
            private string _DVT97;

            public string DVT97
            {
                get { return _DVT97; }
                set { _DVT97 = value; }
            }
            private string _DVT98;

            public string DVT98
            {
                get { return _DVT98; }
                set { _DVT98 = value; }
            }
            private string _DVT99;

            public string DVT99
            {
                get { return _DVT99; }
                set { _DVT99 = value; }
            }
            private string _DVT100;

            public string DVT100
            {
                get { return _DVT100; }
                set { _DVT100 = value; }
            }
            private string _DVT101;

            public string DVT101
            {
                get { return _DVT101; }
                set { _DVT101 = value; }
            }
            private string _DVT102;

            public string DVT102
            {
                get { return _DVT102; }
                set { _DVT102 = value; }
            }
            private string _DVT103;

            public string DVT103
            {
                get { return _DVT103; }
                set { _DVT103 = value; }
            }
            private string _DVT104;

            public string DVT104
            {
                get { return _DVT104; }
                set { _DVT104 = value; }
            }
            private string _DVT105;

            public string DVT105
            {
                get { return _DVT105; }
                set { _DVT105 = value; }
            }
            private string _DVT106;

            public string DVT106
            {
                get { return _DVT106; }
                set { _DVT106 = value; }
            }
            private string _DVT107;

            public string DVT107
            {
                get { return _DVT107; }
                set { _DVT107 = value; }
            }
            private string _DVT108;

            public string DVT108
            {
                get { return _DVT108; }
                set { _DVT108 = value; }
            }
            private string _DVT109;

            public string DVT109
            {
                get { return _DVT109; }
                set { _DVT109 = value; }
            }
            private string _DVT110;

            public string DVT110
            {
                get { return _DVT110; }
                set { _DVT110 = value; }
            }
            private string _DVT111;

            public string DVT111
            {
                get { return _DVT111; }
                set { _DVT111 = value; }
            }
            private string _DVT112;

            public string DVT112
            {
                get { return _DVT112; }
                set { _DVT112 = value; }
            }
            private string _DVT113;

            public string DVT113
            {
                get { return _DVT113; }
                set { _DVT113 = value; }
            }
            private string _DVT114;

            public string DVT114
            {
                get { return _DVT114; }
                set { _DVT114 = value; }
            }
            private string _DVT115;

            public string DVT115
            {
                get { return _DVT115; }
                set { _DVT115 = value; }
            }
            private string _DVT116;

            public string DVT116
            {
                get { return _DVT116; }
                set { _DVT116 = value; }
            }
            private string _DVT117;

            public string DVT117
            {
                get { return _DVT117; }
                set { _DVT117 = value; }
            }
            private string _DVT118;

            public string DVT118
            {
                get { return _DVT118; }
                set { _DVT118 = value; }
            }
            private string _DVT119;

            public string DVT119
            {
                get { return _DVT119; }
                set { _DVT119 = value; }
            }
            private string _DVT120;

            public string DVT120
            {
                get { return _DVT120; }
                set { _DVT120 = value; }
            }
            private string _DVT121;

            public string DVT121
            {
                get { return _DVT121; }
                set { _DVT121 = value; }
            }
            private string _DVT122;

            public string DVT122
            {
                get { return _DVT122; }
                set { _DVT122 = value; }
            }
            private string _DVT123;

            public string DVT123
            {
                get { return _DVT123; }
                set { _DVT123 = value; }
            }
            private string _DVT124;

            public string DVT124
            {
                get { return _DVT124; }
                set { _DVT124 = value; }
            }
            private string _DVT125;

            public string DVT125
            {
                get { return _DVT125; }
                set { _DVT125 = value; }
            }
            private string _DVT126;

            public string DVT126
            {
                get { return _DVT126; }
                set { _DVT126 = value; }
            }
            private string _DVT127;

            public string DVT127
            {
                get { return _DVT127; }
                set { _DVT127 = value; }
            }
            private string _DVT128;

            public string DVT128
            {
                get { return _DVT128; }
                set { _DVT128 = value; }
            }
            private string _DVT129;

            public string DVT129
            {
                get { return _DVT129; }
                set { _DVT129 = value; }
            }
            private string _DVT130;

            public string DVT130
            {
                get { return _DVT130; }
                set { _DVT130 = value; }
            }
            private string _DVT131;

            public string DVT131
            {
                get { return _DVT131; }
                set { _DVT131 = value; }
            }
            private string _DVT132;

            public string DVT132
            {
                get { return _DVT132; }
                set { _DVT132 = value; }
            }
            private string _DVT133;

            public string DVT133
            {
                get { return _DVT133; }
                set { _DVT133 = value; }
            }
            private string _DVT134;

            public string DVT134
            {
                get { return _DVT134; }
                set { _DVT134 = value; }
            }
            private string _DVT135;

            public string DVT135
            {
                get { return _DVT135; }
                set { _DVT135 = value; }
            }
            private string _DVT136;

            public string DVT136
            {
                get { return _DVT136; }
                set { _DVT136 = value; }
            }
            private string _DVT137;

            public string DVT137
            {
                get { return _DVT137; }
                set { _DVT137 = value; }
            }
            private string _DVT138;

            public string DVT138
            {
                get { return _DVT138; }
                set { _DVT138 = value; }
            }
            private string _DVT139;

            public string DVT139
            {
                get { return _DVT139; }
                set { _DVT139 = value; }
            }
            private string _DVT140;

            public string DVT140
            {
                get { return _DVT140; }
                set { _DVT140 = value; }
            }
            private string _DVT141;

            public string DVT141
            {
                get { return _DVT141; }
                set { _DVT141 = value; }
            }
            private string _DVT142;

            public string DVT142
            {
                get { return _DVT142; }
                set { _DVT142 = value; }
            }
            private string _DVT143;

            public string DVT143
            {
                get { return _DVT143; }
                set { _DVT143 = value; }
            }
            private string _DVT144;

            public string DVT144
            {
                get { return _DVT144; }
                set { _DVT144 = value; }
            }
            private string _DVT145;

            public string DVT145
            {
                get { return _DVT145; }
                set { _DVT145 = value; }
            }
            private string _DVT146;

            public string DVT146
            {
                get { return _DVT146; }
                set { _DVT146 = value; }
            }
            private string _DVT147;

            public string DVT147
            {
                get { return _DVT147; }
                set { _DVT147 = value; }
            }
            private string _DVT148;

            public string DVT148
            {
                get { return _DVT148; }
                set { _DVT148 = value; }
            }
            private string _DVT149;

            public string DVT149
            {
                get { return _DVT149; }
                set { _DVT149 = value; }
            }
            private string _DVT150;

            public string DVT150
            {
                get { return _DVT150; }
                set { _DVT150 = value; }
            }
            private string _DVT151;

            public string DVT151
            {
                get { return _DVT151; }
                set { _DVT151 = value; }
            }
            private string _DVT152;

            public string DVT152
            {
                get { return _DVT152; }
                set { _DVT152 = value; }
            }
            private string _DVT153;

            public string DVT153
            {
                get { return _DVT153; }
                set { _DVT153 = value; }
            }
            private string _DVT154;

            public string DVT154
            {
                get { return _DVT154; }
                set { _DVT154 = value; }
            }
            private string _DVT155;

            public string DVT155
            {
                get { return _DVT155; }
                set { _DVT155 = value; }
            }
            private string _DVT156;

            public string DVT156
            {
                get { return _DVT156; }
                set { _DVT156 = value; }
            }
            private string _DVT157;

            public string DVT157
            {
                get { return _DVT157; }
                set { _DVT157 = value; }
            }
            private string _DVT158;

            public string DVT158
            {
                get { return _DVT158; }
                set { _DVT158 = value; }
            }
            private string _DVT159;

            public string DVT159
            {
                get { return _DVT159; }
                set { _DVT159 = value; }
            }
            private string _DVT160;

            public string DVT160
            {
                get { return _DVT160; }
                set { _DVT160 = value; }
            }
            private string _DVT161;

            public string DVT161
            {
                get { return _DVT161; }
                set { _DVT161 = value; }
            }
            private string _DVT162;

            public string DVT162
            {
                get { return _DVT162; }
                set { _DVT162 = value; }
            }
            private string _DVT163;

            public string DVT163
            {
                get { return _DVT163; }
                set { _DVT163 = value; }
            }
            private string _DVT164;

            public string DVT164
            {
                get { return _DVT164; }
                set { _DVT164 = value; }
            }
            private string _DVT165;

            public string DVT165
            {
                get { return _DVT165; }
                set { _DVT165 = value; }
            }
            private string _DVT166;

            public string DVT166
            {
                get { return _DVT166; }
                set { _DVT166 = value; }
            }
            private string _DVT167;

            public string DVT167
            {
                get { return _DVT167; }
                set { _DVT167 = value; }
            }
            private string _DVT168;

            public string DVT168
            {
                get { return _DVT168; }
                set { _DVT168 = value; }
            }
            private string _DVT169;

            public string DVT169
            {
                get { return _DVT169; }
                set { _DVT169 = value; }
            }
            private string _DVT170;

            public string DVT170
            {
                get { return _DVT170; }
                set { _DVT170 = value; }
            }
            //private string _DVT171;

            //public string DVT171
            //{
            //    get { return _DVT171; }
            //    set { _DVT171 = value; }
            //}
            //private string _DVT172;

            //public string DVT172
            //{
            //    get { return _DVT172; }
            //    set { _DVT172 = value; }
            //}
            //private string _DVT173;

            //public string DVT173
            //{
            //    get { return _DVT173; }
            //    set { _DVT173 = value; }
            //}
            //private string _DVT174;

            //public string DVT174
            //{
            //    get { return _DVT174; }
            //    set { _DVT174 = value; }
            //}
            //private string _DVT175;

            //public string DVT175
            //{
            //    get { return _DVT175; }
            //    set { _DVT175 = value; }
            //}
            //private string _DVT176;

            //public string DVT176
            //{
            //    get { return _DVT176; }
            //    set { _DVT176 = value; }
            //}
           


            private int tuoi;
            public string Giuong { get; set; }
            private string TenBN;
            private string sl1;
            private string sl2;
            private string sl3;
            private string sl4;
            private string sl5;
            private string sl6;
            private string sl7;
            private string sl8;
            private string sl9;
            private string sl10;
            private string sl11;
            private string sl12;
            private string sl13;
            private string sl14;
            private string sl15;
            private string sl16;
            private string sl17;
            private string sl18;
            private string sl19;
            private string sl20;
            private string sl21;
            private string sl22;
            private string sl23;
            private string sl24;
            private string sl25;
            private string sl26;
            private string sl27;
            private string sl28;
            private string sl29;
            private string sl30;
            private string sl31;
            private string sl32;
            private string sl33;
            private string sl34;
            private string sl35;
            private string sl36;
            private string sl37;
            private string sl38;
            private string sl39;
            private string sl40;
            private string sl41;
            private string sl42;
            private string sl43;
            private string sl44;
            private string sl45;
            private string sl46;
            private string sl47;
            private string sl48;
            private string sl49;
            private string sl50;
            private string sl51;
            private string sl52;
            private string sl53;
            private string sl54;
            private string sl55;
            private string sl56;
            private string sl57;
            private string sl58;
            private string sl59;
            private string sl60;
            private string sl61;
            private string sl62;
            private string sl63;
            private string sl64;
            private string sl65;
            private string sl66;
            private string sl67;
            private string sl68;
            private string sl69;
            private string sl70;
            private string sl71;
            private string sl72;
            private string sl73;
            private string sl74;
            private string sl75;
            private string sl76;
            private string sl77;
            private string sl78;
            private string sl79;
            private string sl80;
            private string sl81;
            private string sl82;
            private string sl83;
            private string sl84;
            private string sl85;
            private string sl86;
            private string sl87;
            private string sl88;
            private string sl89;
            private string sl90;
            private string sl91;
            private string sl92;
            private string sl93;
            private string sl94;
            private string sl95;
            private string sl96;
            private string sl97;
            private string sl98;
            private string sl99;

            private string sl100;
            private string sl101;
            private string sl102;
            private string sl103;
            private string sl104;
            private string sl105;
            private string sl106;
            private string sl107;
            private string sl108;
            private string sl109;
            private string sl110;
            private string sl111;
            private string sl112;
            private string sl113;
            private string sl114;
            private string sl115;
            private string sl116;
            private string sl117;
            private string sl118;
            private string sl119;
            private string sl120;
            private string sl121;
            private string sl122;
            private string sl123;
            private string sl124;
            private string sl125;
            private string sl126;
            private string sl127;
            private string sl128;
            private string sl129;
            private string sl130;
            private string sl131;
            private string sl132;
            private string sl133;
            private string sl134;
            private string sl135;
            private string sl136;
            private string sl137;
            private string sl138;
            private string sl139;
            private string sl140;
            private string sl141;
            private string sl142;
            private string sl143;
            private string sl144;
            private string sl145;
            private string sl146;
            private string sl147;
            private string sl148;
            private string sl149;
            private string sl150;
            private string sl151;
            private string sl152;
            private string sl153;
            private string sl154;
            private string sl155;
            private string sl156;
            private string sl157;
            private string sl158;
            private string sl159;
            private string sl160;
            private string sl161;
            private string sl162;
            private string sl163;
            private string sl164;
            private string sl165;
            private string sl166;
            private string sl167;
            private string sl168;
            private string sl169;
            private string sl170;
            private int MaBN;
            private int SX;
            public int sx
            { set { SX = value; } get { return SX; } }
            public int mabn

            { set { MaBN = value; } get { return MaBN; } }
            public string tenbn
            {
                set { TenBN = value; }
                get { return TenBN; }
            }
            public int Tuoi
            {
                set { tuoi = value; }
                get { return tuoi; }
            }
            public string SL101
            { set { sl1 = value; } get { return sl1; } }
            public string SL201
            { set { sl2 = value; } get { return sl2; } }
            public string SL301
            { set { sl3 = value; } get { return sl3; } }
            public string SL401
            { set { sl4 = value; } get { return sl4; } }
            public string SL501
            { set { sl5 = value; } get { return sl5; } }
            public string SL601
            { set { sl6 = value; } get { return sl6; } }
            public string SL701
            { set { sl7 = value; } get { return sl7; } }
            public string SL801
            { set { sl8 = value; } get { return sl8; } }
            public string SL901
            { set { sl9 = value; } get { return sl9; } }
            public string SL1001
            { set { sl10 = value; } get { return sl10; } }
            public string SL1101
            { set { sl11 = value; } get { return sl11; } }
            public string SL1201
            {
                set { sl12 = value; }
                get { return sl12; }
            }
            public string SL1301
            { set { sl13 = value; } get { return sl13; } }
            public string SL1401
            { set { sl14 = value; } get { return sl14; } }
            public string SL1501
            {
                set { sl15 = value; }
                get { return sl15; }
            }
            public string SL1601
            {
                set { sl16 = value; }
                get { return sl16; }
            }
            public string SL1701
            {
                set { sl17 = value; }
                get { return sl17; }
            }
            public string SL1801
            {
                set { sl18 = value; }
                get { return sl18; }
            }
            public string SL1901
            {
                set { sl19 = value; }
                get { return sl19; }
            }
            public string SL2001
            {
                set { sl20 = value; }
                get { return sl20; }
            }
            public string SL2101
            {
                set { sl21 = value; }
                get { return sl21; }
            }
            public string SL2201
            {
                set { sl22 = value; }
                get { return sl22; }
            }
            public string SL2301
            {
                set { sl23 = value; }
                get { return sl23; }
            }
            public string SL2401
            {
                set { sl24 = value; }
                get { return sl24; }
            }
            public string SL2501
            {
                set { sl25 = value; }
                get { return sl25; }
            }
            public string SL2601
            {
                set { sl26 = value; }
                get { return sl26; }
            }
            public string SL2701
            {
                set { sl27 = value; }
                get { return sl27; }
            }
            public string SL2801
            {
                set { sl28 = value; }
                get { return sl28; }
            }
            public string SL2901
            {
                set { sl29 = value; }
                get { return sl29; }
            }
            public string SL3001
            {
                set { sl30 = value; }
                get { return sl30; }
            }
            public string SL3101
            {
                set { sl31 = value; }
                get { return sl31; }
            }
            public string SL3201
            {
                set { sl32 = value; }
                get { return sl32; }
            }
            public string SL3301
            {
                set { sl33 = value; }
                get { return sl33; }
            }
            public string SL3401
            {
                set { sl34 = value; }
                get { return sl34; }
            }
            public string SL3501
            {
                set { sl35 = value; }
                get { return sl35; }
            }
            public string SL3601
            {
                set { sl36 = value; }
                get { return sl36; }
            }
            public string SL3701
            {
                set { sl37 = value; }
                get { return sl37; }
            }
            public string SL3801
            {
                set { sl38 = value; }
                get { return sl38; }
            }
            public string SL3901
            {
                set { sl39 = value; }
                get { return sl39; }
            }
            public string SL4001
            {
                set { sl40 = value; }
                get { return sl40; }
            }
            public string SL4101
            {
                set { sl41 = value; }
                get { return sl41; }
            }
            public string SL4201
            {
                set { sl42 = value; }
                get { return sl42; }
            }
            public string SL4301
            {
                set { sl43 = value; }
                get { return sl43; }
            }
            public string SL4401
            {
                set { sl44 = value; }
                get { return sl44; }
            }
            public string SL4501
            {
                set { sl45 = value; }
                get { return sl45; }
            }
            public string SL4601
            {
                set { sl46 = value; }
                get { return sl46; }
            }
            public string SL4701
            {
                set { sl47 = value; }
                get { return sl47; }
            }
            public string SL4801
            {
                set { sl48 = value; }
                get { return sl48; }
            }
            public string SL4901
            {
                set { sl49 = value; }
                get { return sl49; }
            }
            public string SL5001
            {
                set { sl50 = value; }
                get { return sl50; }
            }
            public string SL5101
            {
                set { sl51 = value; }
                get { return sl51; }
            }
            public string SL5201
            {
                set { sl52 = value; }
                get { return sl52; }
            }
            public string SL5301
            {
                set { sl53 = value; }
                get { return sl53; }
            }
            public string SL5401
            {
                set { sl54 = value; }
                get { return sl54; }
            }
            public string SL5501
            {
                set { sl55 = value; }
                get { return sl55; }
            }
            public string SL5601
            {
                set { sl56 = value; }
                get { return sl56; }
            }
            public string SL5701
            {
                set { sl57 = value; }
                get { return sl57; }
            }
            public string SL5801
            {
                set { sl58 = value; }
                get { return sl58; }
            }
            public string SL5901
            {
                set { sl59 = value; }
                get { return sl59; }
            }
            public string SL6001
            {
                set { sl60 = value; }
                get { return sl60; }
            }
            public string SL6101
            {
                set { sl61 = value; }
                get { return sl61; }
            }
            public string SL6201
            {
                set { sl62 = value; }
                get { return sl62; }
            }
            public string SL6301
            {
                set { sl63 = value; }
                get { return sl63; }
            }
            public string SL6401
            {
                set { sl64 = value; }
                get { return sl64; }
            }
            public string SL6501
            {
                set { sl65 = value; }
                get { return sl65; }
            }
            public string SL6601
            {
                set { sl66 = value; }
                get { return sl66; }
            }
            public string SL6701
            {
                set { sl67 = value; }
                get { return sl67; }
            }
            public string SL6801
            {
                set { sl68 = value; }
                get { return sl68; }
            }
            public string SL6901
            {
                set { sl69 = value; }
                get { return sl69; }
            }
            public string SL7001
            {
                set { sl70 = value; }
                get { return sl70; }
            }
            public string SL7101
            {
                set { sl71 = value; }
                get { return sl71; }
            }
            public string SL7201
            {
                set { sl72 = value; }
                get { return sl72; }
            }
            public string SL7301
            {
                set { sl73 = value; }
                get { return sl73; }
            }
            public string SL7401
            {
                set { sl74 = value; }
                get { return sl74; }
            }
            public string SL7501
            {
                set { sl75 = value; }
                get { return sl75; }
            }
            public string SL7601
            {
                set { sl76 = value; }
                get { return sl76; }
            }
            public string SL7701
            {
                set { sl77 = value; }
                get { return sl77; }
            }
            public string SL7801
            {
                set { sl78 = value; }
                get { return sl78; }
            }
            public string SL7901
            {
                set { sl79 = value; }
                get { return sl79; }
            }
            public string SL8001
            {
                set { sl80 = value; }
                get { return sl80; }
            }
            public string SL8101
            {
                set { sl81 = value; }
                get { return sl81; }
            }
            public string SL8201
            {
                set { sl82 = value; }
                get { return sl82; }
            }
            public string SL8301
            {
                set { sl83 = value; }
                get { return sl83; }
            }
            public string SL8401
            {
                set { sl84 = value; }
                get { return sl84; }
            }
            public string SL8501
            {
                set { sl85 = value; }
                get { return sl85; }
            }
            public string SL8601
            {
                set { sl86 = value; }
                get { return sl86; }
            }
            public string SL8701
            {
                set { sl87 = value; }
                get { return sl87; }
            }
            public string SL8801
            {
                set { sl88 = value; }
                get { return sl88; }
            }
            public string SL8901
            {
                set { sl89 = value; }
                get { return sl89; }
            }
            public string SL9001
            {
                set { sl90 = value; }
                get { return sl90; }
            }
            public string SL9101
            {
                set { sl91 = value; }
                get { return sl91; }
            }
            public string SL9201
            {
                set { sl92 = value; }
                get { return sl92; }
            }
            public string SL9301
            {
                set { sl93 = value; }
                get { return sl93; }
            }
            public string SL9401
            {
                set { sl94 = value; }
                get { return sl94; }
            }
            public string SL9501
            {
                set { sl95 = value; }
                get { return sl95; }
            }
            public string SL9601
            {
                set { sl96 = value; }
                get { return sl96; }
            }
            public string SL9701
            {
                set { sl97 = value; }
                get { return sl97; }
            }
            public string SL9801
            {
                set { sl98 = value; }
                get { return sl98; }
            }
            public string SL9901
            {
                set { sl99 = value; }
                get { return sl99; }
            }
            public string SL10001
            {
                set { sl100 = value; }
                get { return sl100; }
            }
            public string SL10101
            { set { sl101 = value; } get { return sl101; } }
            public string SL10201
            { set { sl102 = value; } get { return sl102; } }
            public string SL10301
            { set { sl103 = value; } get { return sl103; } }
            public string SL10401
            { set { sl104 = value; } get { return sl104; } }
            public string SL10501
            { set { sl105 = value; } get { return sl105; } }
            public string SL10601
            { set { sl106 = value; } get { return sl106; } }
            public string SL10701
            { set { sl107 = value; } get { return sl107; } }
            public string SL10801
            { set { sl108 = value; } get { return sl108; } }
            public string SL10901
            { set { sl109 = value; } get { return sl109; } }
            public string SL11001
            { set { sl110 = value; } get { return sl110; } }
            public string SL11101
            { set { sl111 = value; } get { return sl111; } }
            public string SL11201
            {
                set { sl112 = value; }
                get { return sl112; }
            }
            public string SL11301
            { set { sl113 = value; } get { return sl113; } }
            public string SL11401
            { set { sl114 = value; } get { return sl114; } }
            public string SL11501
            {
                set { sl115 = value; }
                get { return sl115; }
            }
            public string SL11601
            {
                set { sl116 = value; }
                get { return sl116; }
            }
            public string SL11701
            {
                set { sl117 = value; }
                get { return sl117; }
            }
            public string SL11801
            {
                set { sl118 = value; }
                get { return sl118; }
            }
            public string SL11901
            {
                set { sl119 = value; }
                get { return sl119; }
            }
            public string SL12001
            {
                set { sl120 = value; }
                get { return sl120; }
            }
            public string SL12101
            {
                set { sl121 = value; }
                get { return sl121; }
            }
            public string SL12201
            {
                set { sl122 = value; }
                get { return sl122; }
            }
            public string SL12301
            {
                set { sl123 = value; }
                get { return sl123; }
            }
            public string SL12401
            {
                set { sl124 = value; }
                get { return sl124; }
            }
            public string SL12501
            {
                set { sl125 = value; }
                get { return sl125; }
            }
            public string SL12601
            {
                set { sl126 = value; }
                get { return sl126; }
            }
            public string SL12701
            {
                set { sl127 = value; }
                get { return sl127; }
            }
            public string SL12801
            {
                set { sl128 = value; }
                get { return sl128; }
            }
            public string SL12901
            {
                set { sl129 = value; }
                get { return sl129; }
            }
            public string SL13001
            {
                set { sl130 = value; }
                get { return sl130; }
            }
            public string SL13101
            {
                set { sl131 = value; }
                get { return sl131; }
            }
            public string SL13201
            {
                set { sl132 = value; }
                get { return sl132; }
            }
            public string SL13301
            {
                set { sl133 = value; }
                get { return sl133; }
            }
            public string SL13401
            {
                set { sl134 = value; }
                get { return sl134; }
            }
            public string SL13501
            {
                set { sl135 = value; }
                get { return sl135; }
            }
            public string SL13601
            {
                set { sl136 = value; }
                get { return sl136; }
            }
            public string SL13701
            {
                set { sl137 = value; }
                get { return sl137; }
            }
            public string SL13801
            {
                set { sl138 = value; }
                get { return sl138; }
            }
            public string SL13901
            {
                set { sl139 = value; }
                get { return sl139; }
            }
            public string SL14001
            {
                set { sl140 = value; }
                get { return sl140; }
            }
            public string SL14101
            {
                set { sl141 = value; }
                get { return sl141; }
            }
            public string SL14201
            {
                set { sl142 = value; }
                get { return sl142; }
            }
            public string SL14301
            {
                set { sl143 = value; }
                get { return sl143; }
            }
            public string SL14401
            {
                set { sl144 = value; }
                get { return sl144; }
            }
            public string SL14501
            {
                set { sl145 = value; }
                get { return sl145; }
            }
            public string SL14601
            {
                set { sl146 = value; }
                get { return sl146; }
            }
            public string SL14701
            {
                set { sl147 = value; }
                get { return sl147; }
            }
            public string SL14801
            {
                set { sl148 = value; }
                get { return sl148; }
            }
            public string SL14901
            {
                set { sl149 = value; }
                get { return sl149; }
            }
            public string SL15001
            {
                set { sl150 = value; }
                get { return sl150; }
            }
            public string SL15101
            {
                set { sl151 = value; }
                get { return sl151; }
            }
            public string SL15201
            {
                set { sl152 = value; }
                get { return sl152; }
            }
            public string SL15301
            {
                set { sl153 = value; }
                get { return sl153; }
            }
            public string SL15401
            {
                set { sl154 = value; }
                get { return sl154; }
            }
            public string SL15501
            {
                set { sl155 = value; }
                get { return sl155; }
            }
            public string SL15601
            {
                set { sl156 = value; }
                get { return sl156; }
            }
            public string SL15701
            {
                set { sl157 = value; }
                get { return sl157; }
            }
            public string SL15801
            {
                set { sl158 = value; }
                get { return sl158; }
            }
            public string SL15901
            {
                set { sl159 = value; }
                get { return sl159; }
            }
            public string SL16001
            {
                set { sl160 = value; }
                get { return sl160; }
            }
            public string SL16101
            {
                set { sl161 = value; }
                get { return sl161; }
            }
            public string SL16201
            {
                set { sl162 = value; }
                get { return sl162; }
            }
            public string SL16301
            {
                set { sl163 = value; }
                get { return sl163; }
            }
            public string SL16401
            {
                set { sl164 = value; }
                get { return sl164; }
            }
            public string SL16501
            {
                set { sl165 = value; }
                get { return sl165; }
            }
            public string SL16601
            {
                set { sl166 = value; }
                get { return sl166; }
            }
            public string SL16701
            {
                set { sl167 = value; }
                get { return sl167; }
            }
            public string SL16801
            {
                set { sl168 = value; }
                get { return sl168; }
            }
            public string SL16901
            {
                set { sl169 = value; }
                get { return sl169; }
            }
            public string SL17001
            {
                set { sl170 = value; }
                get { return sl170; }
            }
        }
        #endregion
        class DSDV
        {
            private string TenDV;
            private int MaDV;
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            public int madv
            {
                set { MaDV = value; }
                get { return MaDV; }
            }
            private string _donVi;

            public string DonVi
            {
                get { return _donVi; }
                set { _donVi = value; }
            }
        }
        List<DSDV> _DSDV = new List<DSDV>();
        List<DSDV> _DSDVMoi = new List<DSDV>();
        List<Tamtra> _Tamtra = new List<Tamtra>();
        List<Tamtra> _Tamtramoi = new List<Tamtra>();
        private bool KT()
        {
            if (LupNgayTu.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày tháng");
                LupNgayTu.Focus();
                return false;
            }
            if (LupNgayDen.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày tháng");
                LupNgayDen.Focus();
                return false;
            }
            if (LupKhoaPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng!");
                LupKhoaPhong.Focus();
                return false;
            }
            if (CboThuoc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn trạng thái!");
                CboThuoc.Focus();
                return false;
            }
            return true;
        }
        private void Taoso_Click(object sender, EventArgs e)
        {
            if (KT())
            {
                int _a1 = -1;
                int _a2 = -1;
                int _a3 = -1;
                //int _a4 = -1;
                if (chkBoxung.Checked == true)
                { _a2 = 1; }
                if (chkThuongxuyen.Checked == true)
                { _a1 = 0; }
                if (chkTrathuoc.Checked == true)
                { _a3 = 2; }

                int _Status1 = 0;
                int _Status2 = 0;
                int TT = CboThuoc.SelectedIndex;
                switch (TT)
                {
                    case 0:
                        _Status1 = 1;
                        _Status2 = 1;
                        break;
                    case 1:
                        _Status1 = 0;
                        _Status2 = 0;
                        break;
                    case 2:
                        _Status1 = 1;
                        _Status2 = 0;
                        break;
                }
                int _MaKP = 0;
                _Tamtra.Clear();
                _DSDV.Clear();
                if (LupKhoaPhong.Text != null)
                {
                    _MaKP = Convert.ToInt32(LupKhoaPhong.EditValue);//ok
                }
                int _noitru = radNT.SelectedIndex;
                DateTime Ngaytu = DungChung.Ham.NgayTu(LupNgayTu.DateTime);
                DateTime Ngayden = DungChung.Ham.NgayDen(LupNgayDen.DateTime);
                 var _ldv = (from dv in _Data.DichVus.Where(P => P.PLoai == 1)
                             join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join n in _Data.NhomDVs on tn.IDNhom equals n.IDNhom
                             select new { dv.MaDV, dv.TenDV, n.TenNhomCT, tn.IdTieuNhom, n.IDNhom }).ToList();
                 #region Nếu lấy cả thuốc ngoài BH

                 if (chkThuoc.Checked == true)
                {
                    var q1 = (from bn1 in _Data.BenhNhans.Where(p => _noitru == 2 ? true : (_noitru == 0 ? p.NoiTru == 1 : (p.NoiTru == 0 && p.DTNT == true)))
                              join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                              join DTct in _Data.DThuoccts on DT.IDDon equals DTct.IDDon
                              join vv in _Data.VaoViens on bn1.MaBNhan equals vv.MaBNhan
                              join kp in _Data.BNKBs.Where(p => (radioGroup1.SelectedIndex == 0 || radioGroup1.SelectedIndex == -1) ? true : p.MaKP == _MaKP) on bn1.MaBNhan equals kp.MaBNhan
                              where ((radioGroup1.SelectedIndex == 0 || radioGroup1.SelectedIndex == -1) ? (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden) : (kp.NgayKham >= Ngaytu && kp.NgayKham <= Ngayden))
                              where (DTct.Status == _Status1 || DTct.Status == _Status2)
                              where (DTct.IDKB == kp.IDKB)
                              select new
                              {
                                  DTct.MaDV,
                                  bn1.MaBNhan,
                                  bn1.TenBNhan,
                                  bn1.Tuoi,
                                  DTct.IDDonct,
                                  DTct.DonVi,
                                  DTct.SoLuong,
                                  kp.Giuong,
                                  kp.IDKB
                              }).ToList();
                    var bn2 = (from a in q1
                               join dv in _ldv on a.MaDV equals dv.MaDV
                               group new { a, dv } by new { dv.MaDV, dv.TenDV, dv.TenNhomCT,a.DonVi } into kq
                               select new
                               {
                                   TenDV = kq.Key.TenDV,
                                   MaDV = kq.Key.MaDV,
                                   TenN = kq.Key.TenNhomCT,
                                   Donvi = kq.Key.DonVi,
                                   SL = kq.Select(p => p.a.IDDonct).Count()
                               }).OrderBy(p => p.TenDV).OrderBy(p => p.TenN).ToList();
                    
                    if (bn2.Count > 0)
                    {
                        foreach (var a in bn2)
                        {
                            DSDV themmoi = new DSDV();
                            themmoi.tendv = a.TenDV;
                            themmoi.madv = a.MaDV;
                            themmoi.DonVi = a.Donvi;
                            _DSDV.Add(themmoi);
                        }
                    }
                    var MaBN = (from a in q1
                                join dv in _ldv on a.MaDV equals dv.MaDV
                                group new { a, dv } by new { a.MaBNhan } into kq
                                select new { MaBN = kq.Key.MaBNhan }).ToList();

                    
                    if (MaBN.Count > 0)
                    {
                        foreach (var c in MaBN)
                        {
                            Tamtra themmoi = new Tamtra();
                            var g1 = q1.Where(p => p.MaBNhan == c.MaBN).OrderByDescending(p => p.IDKB).ToList();
                            if (g1.First().Giuong != null)
                            {
                                if (g1.First().Giuong.Contains(";"))
                                {
                                    string[] arrGiuong = g1.First().Giuong.Split(';');
                                    themmoi.Giuong = arrGiuong.LastOrDefault();
                                }
                                else
                                {
                                    themmoi.Giuong = g1.First().Giuong;
                                }
                            }
                            themmoi.mabn = c.MaBN;
                            _Tamtra.Add(themmoi);
                        }
                    }
                    var bn = (from a in q1
                              join dv in _ldv on a.MaDV equals dv.MaDV
                              group new { a, dv } by new { dv.MaDV, a.TenBNhan, a.MaBNhan, a.Tuoi,a.DonVi } into kq
                              select new { kq.Key.Tuoi,
                                           Mabenhnhan = kq.Key.MaBNhan,
                                           MaDV = kq.Key.MaDV,
                                           SoLuong = kq.Sum(p => p.a.SoLuong),
                                           kq.Key.DonVi,
                                           TenBN = kq.Key.TenBNhan,
                              }).ToList();
                    
                    if (bn.Count > 0)
                    {
                        #region tạo code báo cáo
                        foreach (var n in _Tamtra)
                        {
                            foreach (var m in bn)
                            {
                                if (n.mabn == m.Mabenhnhan)
                                {
                                    if (m.SoLuong != null && m.SoLuong != 0)
                                    {
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            if (m.MaDV == _DSDV.Skip(i).First().madv)
                                            {
                                                switch (i)
                                                {
                                                    case 0:
                                                        n.SL101 = m.SoLuong.ToString();
                                                        n.DVT1 = m.DonVi;
                                                        break;
                                                    case 1:
                                                        n.SL201 = m.SoLuong.ToString();
                                                        n.DVT2 = m.DonVi;
                                                        break;
                                                    case 2:
                                                        n.SL301 = m.SoLuong.ToString();
                                                        n.DVT3 = m.DonVi;
                                                        break;
                                                    case 3:
                                                        n.SL401 = m.SoLuong.ToString();
                                                        n.DVT4 = m.DonVi;
                                                        break;
                                                    case 4:
                                                        n.SL501 = m.SoLuong.ToString();
                                                        n.DVT5 = m.DonVi;
                                                        break;
                                                    case 5:
                                                        n.SL601 = m.SoLuong.ToString();
                                                        n.DVT6 = m.DonVi;
                                                        break;
                                                    case 6:
                                                        n.SL701 = m.SoLuong.ToString();
                                                        n.DVT7 = m.DonVi;
                                                        break;
                                                    case 7:
                                                        n.SL801 = m.SoLuong.ToString();n.DVT8 = m.DonVi;
                                                        break;
                                                    case 8:
                                                        n.SL901 = m.SoLuong.ToString();
                                                        n.DVT9 = m.DonVi;
                                                        break;
                                                    case 9:
                                                        n.SL1001 = m.SoLuong.ToString();
                                                        n.DVT10 = m.DonVi;
                                                        break;
                                                    case 10:
                                                        n.SL1101 = m.SoLuong.ToString();
                                                        n.DVT11 = m.DonVi;
                                                        break;
                                                    case 11:
                                                        n.SL1201 = m.SoLuong.ToString();n.DVT12 = m.DonVi;
                                                        break;
                                                    case 12:
                                                        n.SL1301 = m.SoLuong.ToString();n.DVT13 = m.DonVi;
                                                        break;
                                                    case 13:
                                                        n.SL1401 = m.SoLuong.ToString();n.DVT14 = m.DonVi;
                                                        break;
                                                    case 14:
                                                        n.SL1501 = m.SoLuong.ToString();n.DVT15 = m.DonVi;
                                                        break;
                                                    case 15:
                                                        n.SL1601 = m.SoLuong.ToString();n.DVT16 = m.DonVi;
                                                        break;
                                                    case 16:
                                                        n.SL1701 = m.SoLuong.ToString();n.DVT17 = m.DonVi;
                                                        break;
                                                    case 17:
                                                        n.SL1801 = m.SoLuong.ToString();n.DVT18 = m.DonVi;
                                                        break;
                                                    case 18:
                                                        n.SL1901 = m.SoLuong.ToString();n.DVT19 = m.DonVi;
                                                        break;
                                                    case 19:
                                                        n.SL2001 = m.SoLuong.ToString();n.DVT20 = m.DonVi;
                                                        break;
                                                    case 20:
                                                        n.SL2101 = m.SoLuong.ToString();n.DVT21 = m.DonVi;
                                                        break;
                                                    case 21:
                                                        n.SL2201 = m.SoLuong.ToString();n.DVT22 = m.DonVi;
                                                        break;
                                                    case 22:
                                                        n.SL2301 = m.SoLuong.ToString();n.DVT23 = m.DonVi;
                                                        break;
                                                    case 23:
                                                        n.SL2401 = m.SoLuong.ToString();n.DVT24 = m.DonVi;
                                                        break;
                                                    case 24:
                                                        n.SL2501 = m.SoLuong.ToString();n.DVT25 = m.DonVi;
                                                        break;
                                                    case 25:
                                                        n.SL2601 = m.SoLuong.ToString();n.DVT26 = m.DonVi;
                                                        break;
                                                    case 26:
                                                        n.SL2701 = m.SoLuong.ToString();n.DVT27 = m.DonVi;
                                                        break;
                                                    case 27:
                                                        n.SL2801 = m.SoLuong.ToString();n.DVT28 = m.DonVi;
                                                        break;
                                                    case 28:
                                                        n.SL2901 = m.SoLuong.ToString();n.DVT29 = m.DonVi;
                                                        break;
                                                    case 29:
                                                        n.SL3001 = m.SoLuong.ToString();n.DVT30 = m.DonVi;
                                                        break;
                                                    case 30:
                                                        n.SL3101 = m.SoLuong.ToString();n.DVT31 = m.DonVi;
                                                        break;
                                                    case 31:
                                                        n.SL3201 = m.SoLuong.ToString();n.DVT32 = m.DonVi;
                                                        break;
                                                    case 32:
                                                        n.SL3301 = m.SoLuong.ToString();n.DVT33 = m.DonVi;
                                                        break;
                                                    case 33:
                                                        n.SL3401 = m.SoLuong.ToString();n.DVT34 = m.DonVi;
                                                        break;
                                                    case 34:
                                                        n.SL3501 = m.SoLuong.ToString();n.DVT35 = m.DonVi;
                                                        break;
                                                    case 35:
                                                        n.SL3601 = m.SoLuong.ToString();n.DVT36 = m.DonVi;
                                                        break;
                                                    case 36:
                                                        n.SL3701 = m.SoLuong.ToString();n.DVT37 = m.DonVi;
                                                        break;
                                                    case 37:
                                                        n.SL3801 = m.SoLuong.ToString();n.DVT38 = m.DonVi;
                                                        break;
                                                    case 38:
                                                        n.SL3901 = m.SoLuong.ToString();n.DVT39 = m.DonVi;
                                                        break;
                                                    case 39:
                                                        n.SL4001 = m.SoLuong.ToString();n.DVT40 = m.DonVi;
                                                        break;
                                                    case 40:
                                                        n.SL4101 = m.SoLuong.ToString();n.DVT41 = m.DonVi;
                                                        break;
                                                    case 41:
                                                        n.SL4201 = m.SoLuong.ToString();n.DVT42 = m.DonVi;
                                                        break;
                                                    case 42:
                                                        n.SL4301 = m.SoLuong.ToString();n.DVT43 = m.DonVi;
                                                        break;
                                                    case 43:
                                                        n.SL4401 = m.SoLuong.ToString();n.DVT44 = m.DonVi;
                                                        break;
                                                    case 44:
                                                        n.SL4501 = m.SoLuong.ToString();n.DVT45 = m.DonVi;
                                                        break;
                                                    case 45:
                                                        n.SL4601 = m.SoLuong.ToString();n.DVT46 = m.DonVi;
                                                        break;
                                                    case 46:
                                                        n.SL4701 = m.SoLuong.ToString();n.DVT47 = m.DonVi;
                                                        break;
                                                    case 47:
                                                        n.SL4801 = m.SoLuong.ToString();n.DVT48 = m.DonVi;
                                                        break;
                                                    case 48:
                                                        n.SL4901 = m.SoLuong.ToString();n.DVT49 = m.DonVi;
                                                        break;
                                                    case 49:
                                                        n.SL5001 = m.SoLuong.ToString();n.DVT50 = m.DonVi;
                                                        break;
                                                    case 50:
                                                        n.SL5101 = m.SoLuong.ToString();n.DVT51 = m.DonVi;
                                                        break;
                                                    case 51:
                                                        n.SL5201 = m.SoLuong.ToString();n.DVT52 = m.DonVi;
                                                        break;
                                                    case 52:
                                                        n.SL5301 = m.SoLuong.ToString();n.DVT53 = m.DonVi;
                                                        break;
                                                    case 53:
                                                        n.SL5401 = m.SoLuong.ToString();n.DVT54 = m.DonVi;
                                                        break;
                                                    case 54:
                                                        n.SL5501 = m.SoLuong.ToString();n.DVT55 = m.DonVi;
                                                        break;
                                                    case 55:
                                                        n.SL5601 = m.SoLuong.ToString();n.DVT56 = m.DonVi;
                                                        break;
                                                    case 56:
                                                        n.SL5701 = m.SoLuong.ToString();n.DVT57 = m.DonVi;
                                                        break;
                                                    case 57:
                                                        n.SL5801 = m.SoLuong.ToString();n.DVT58 = m.DonVi;
                                                        break;
                                                    case 58:
                                                        n.SL5901 = m.SoLuong.ToString();n.DVT59 = m.DonVi;
                                                        break;
                                                    case 59:
                                                        n.SL6001 = m.SoLuong.ToString();n.DVT60 = m.DonVi;
                                                        break;
                                                    case 60:
                                                        n.SL6101 = m.SoLuong.ToString();n.DVT61 = m.DonVi;
                                                        break;
                                                    case 61:
                                                        n.SL6201 = m.SoLuong.ToString();n.DVT62 = m.DonVi;
                                                        break;
                                                    case 62:
                                                        n.SL6301 = m.SoLuong.ToString();n.DVT63 = m.DonVi;
                                                        break;
                                                    case 63:
                                                        n.SL6401 = m.SoLuong.ToString();n.DVT64 = m.DonVi;
                                                        break;
                                                    case 64:
                                                        n.SL6501 = m.SoLuong.ToString();n.DVT65 = m.DonVi;
                                                        break;
                                                    case 65:
                                                        n.SL6601 = m.SoLuong.ToString();n.DVT66 = m.DonVi;
                                                        break;
                                                    case 66:
                                                        n.SL6701 = m.SoLuong.ToString();n.DVT67 = m.DonVi;
                                                        break;
                                                    case 67:
                                                        n.SL6801 = m.SoLuong.ToString();n.DVT68 = m.DonVi;
                                                        break;
                                                    case 68:
                                                        n.SL6901 = m.SoLuong.ToString();n.DVT69 = m.DonVi;
                                                        break;
                                                    case 69:
                                                        n.SL7001 = m.SoLuong.ToString();n.DVT70 = m.DonVi;
                                                        break;
                                                    case 70:
                                                        n.SL7101 = m.SoLuong.ToString();n.DVT71 = m.DonVi;
                                                        break;
                                                    case 71:
                                                        n.SL7201 = m.SoLuong.ToString();n.DVT72 = m.DonVi;
                                                        break;
                                                    case 72:
                                                        n.SL7301 = m.SoLuong.ToString();n.DVT73 = m.DonVi;
                                                        break;
                                                    case 73:
                                                        n.SL7401 = m.SoLuong.ToString();n.DVT74 = m.DonVi;
                                                        break;
                                                    case 74:
                                                        n.SL7501 = m.SoLuong.ToString();n.DVT75 = m.DonVi;
                                                        break;
                                                    case 75:
                                                        n.SL7601 = m.SoLuong.ToString();n.DVT76 = m.DonVi;
                                                        break;
                                                    case 76:
                                                        n.SL7701 = m.SoLuong.ToString();n.DVT77 = m.DonVi;
                                                        break;
                                                    case 77:
                                                        n.SL7801 = m.SoLuong.ToString();n.DVT78 = m.DonVi;
                                                        break;
                                                    case 78:
                                                        n.SL7901 = m.SoLuong.ToString();n.DVT79 = m.DonVi;
                                                        break;
                                                    case 79:
                                                        n.SL8001 = m.SoLuong.ToString();n.DVT80 = m.DonVi;
                                                        break;
                                                    case 80:
                                                        n.SL8101 = m.SoLuong.ToString();n.DVT81 = m.DonVi;
                                                        break;
                                                    case 81:
                                                        n.SL8201 = m.SoLuong.ToString();n.DVT82 = m.DonVi;
                                                        break;
                                                    case 82:
                                                        n.SL8301 = m.SoLuong.ToString();n.DVT83 = m.DonVi;
                                                        break;
                                                    case 83:
                                                        n.SL8401 = m.SoLuong.ToString();n.DVT84 = m.DonVi;
                                                        break;
                                                    case 84:
                                                        n.SL8501 = m.SoLuong.ToString();n.DVT85 = m.DonVi;
                                                        break;
                                                    case 85:
                                                        n.SL8601 = m.SoLuong.ToString();n.DVT86 = m.DonVi;
                                                        break;
                                                    case 86:
                                                        n.SL8701 = m.SoLuong.ToString();n.DVT87 = m.DonVi;
                                                        break;
                                                    case 87:
                                                        n.SL8801 = m.SoLuong.ToString();n.DVT88 = m.DonVi;
                                                        break;
                                                    case 88:
                                                        n.SL8901 = m.SoLuong.ToString();n.DVT89 = m.DonVi;
                                                        break;
                                                    case 89:
                                                        n.SL9001 = m.SoLuong.ToString();n.DVT90 = m.DonVi;
                                                        break;
                                                    case 90:
                                                        n.SL9101 = m.SoLuong.ToString();n.DVT91 = m.DonVi;
                                                        break;
                                                    case 91:
                                                        n.SL9201 = m.SoLuong.ToString();n.DVT92 = m.DonVi;
                                                        break;
                                                    case 92:
                                                        n.SL9301 = m.SoLuong.ToString();n.DVT93 = m.DonVi;
                                                        break;
                                                    case 93:
                                                        n.SL9401 = m.SoLuong.ToString();n.DVT94 = m.DonVi;
                                                        break;
                                                    case 94:
                                                        n.SL9501 = m.SoLuong.ToString();n.DVT95 = m.DonVi;
                                                        break;
                                                    case 95:
                                                        n.SL9601 = m.SoLuong.ToString();n.DVT96 = m.DonVi;
                                                        break;
                                                    case 96:
                                                        n.SL9701 = m.SoLuong.ToString();n.DVT97 = m.DonVi;
                                                        break;
                                                    case 97:
                                                        n.SL9801 = m.SoLuong.ToString();n.DVT98 = m.DonVi;
                                                        break;
                                                    case 98:
                                                        n.SL9901 = m.SoLuong.ToString();n.DVT99 = m.DonVi;
                                                        break;
                                                    case 99:
                                                        n.SL10001 = m.SoLuong.ToString();n.DVT100 = m.DonVi;
                                                        break;
                                                    case 100:
                                                        n.SL10101 = m.SoLuong.ToString();n.DVT101 = m.DonVi;
                                                        break;
                                                    case 101:
                                                        n.SL10201 = m.SoLuong.ToString();n.DVT102 = m.DonVi;
                                                        break;
                                                    case 102:
                                                        n.SL10301 = m.SoLuong.ToString();n.DVT103 = m.DonVi;
                                                        break;
                                                    case 103:
                                                        n.SL10401 = m.SoLuong.ToString();n.DVT104 = m.DonVi;
                                                        break;
                                                    case 104:
                                                        n.SL10501 = m.SoLuong.ToString();n.DVT105 = m.DonVi;
                                                        break;
                                                    case 105:
                                                        n.SL10601 = m.SoLuong.ToString();n.DVT106 = m.DonVi;
                                                        break;
                                                    case 106:
                                                        n.SL10701 = m.SoLuong.ToString();n.DVT107 = m.DonVi;
                                                        break;
                                                    case 107:
                                                        n.SL10801 = m.SoLuong.ToString();n.DVT108 = m.DonVi;
                                                        break;
                                                    case 108:
                                                        n.SL10901 = m.SoLuong.ToString();n.DVT109 = m.DonVi;
                                                        break;
                                                    case 109:
                                                        n.SL11001 = m.SoLuong.ToString();n.DVT110 = m.DonVi;
                                                        break;
                                                    case 110:
                                                        n.SL11101 = m.SoLuong.ToString();n.DVT111 = m.DonVi;
                                                        break;
                                                    case 111:
                                                        n.SL11201 = m.SoLuong.ToString();n.DVT112 = m.DonVi;
                                                        break;
                                                    case 112:
                                                        n.SL11301 = m.SoLuong.ToString();n.DVT113 = m.DonVi;
                                                        break;
                                                    case 113:
                                                        n.SL11401 = m.SoLuong.ToString();n.DVT114 = m.DonVi;
                                                        break;
                                                    case 114:
                                                        n.SL11501 = m.SoLuong.ToString();n.DVT115 = m.DonVi;
                                                        break;
                                                    case 115:
                                                        n.SL11601 = m.SoLuong.ToString();n.DVT116 = m.DonVi;
                                                        break;
                                                    case 116:
                                                        n.SL11701 = m.SoLuong.ToString();n.DVT117 = m.DonVi;
                                                        break;
                                                    case 117:
                                                        n.SL11801 = m.SoLuong.ToString();n.DVT118 = m.DonVi;
                                                        break;
                                                    case 118:
                                                        n.SL11901 = m.SoLuong.ToString();n.DVT119 = m.DonVi;
                                                        break;
                                                    case 119:
                                                        n.SL12001 = m.SoLuong.ToString();n.DVT120 = m.DonVi;
                                                        break;
                                                    case 120:
                                                        n.SL12101 = m.SoLuong.ToString();n.DVT121 = m.DonVi;
                                                        break;
                                                    case 121:
                                                        n.SL12201 = m.SoLuong.ToString();n.DVT122 = m.DonVi;
                                                        break;
                                                    case 122:
                                                        n.SL12301 = m.SoLuong.ToString();n.DVT123 = m.DonVi;
                                                        break;
                                                    case 123:
                                                        n.SL12401 = m.SoLuong.ToString();n.DVT124 = m.DonVi;
                                                        break;
                                                    case 124:
                                                        n.SL12501 = m.SoLuong.ToString();n.DVT125 = m.DonVi;
                                                        break;
                                                    case 125:
                                                        n.SL12601 = m.SoLuong.ToString();n.DVT126 = m.DonVi;
                                                        break;
                                                    case 126:
                                                        n.SL12701 = m.SoLuong.ToString();n.DVT127 = m.DonVi;
                                                        break;
                                                    case 127:
                                                        n.SL12801 = m.SoLuong.ToString();n.DVT128 = m.DonVi;
                                                        break;
                                                    case 128:
                                                        n.SL12901 = m.SoLuong.ToString();n.DVT129 = m.DonVi;
                                                        break;
                                                    case 129:
                                                        n.SL13001 = m.SoLuong.ToString();n.DVT130 = m.DonVi;
                                                        break;
                                                    case 130:
                                                        n.SL13101 = m.SoLuong.ToString();n.DVT131 = m.DonVi;
                                                        break;
                                                    case 131:
                                                        n.SL13201 = m.SoLuong.ToString();n.DVT132 = m.DonVi;
                                                        break;
                                                    case 132:
                                                        n.SL13301 = m.SoLuong.ToString();n.DVT133 = m.DonVi;
                                                        break;
                                                    case 133:
                                                        n.SL13401 = m.SoLuong.ToString();n.DVT134 = m.DonVi;
                                                        break;
                                                    case 134:
                                                        n.SL13501 = m.SoLuong.ToString();n.DVT135 = m.DonVi;
                                                        break;
                                                    case 135:
                                                        n.SL13601 = m.SoLuong.ToString();n.DVT136 = m.DonVi;
                                                        break;
                                                    case 136:
                                                        n.SL13701 = m.SoLuong.ToString();n.DVT137 = m.DonVi;
                                                        break;
                                                    case 137:
                                                        n.SL13801 = m.SoLuong.ToString();n.DVT138 = m.DonVi;
                                                        break;
                                                    case 138:
                                                        n.SL13901 = m.SoLuong.ToString();n.DVT139 = m.DonVi;
                                                        break;
                                                    case 139:
                                                        n.SL14001 = m.SoLuong.ToString();n.DVT140 = m.DonVi;
                                                        break;
                                                    case 140:
                                                        n.SL14101 = m.SoLuong.ToString();n.DVT141 = m.DonVi;
                                                        break;
                                                    case 141:
                                                        n.SL14201 = m.SoLuong.ToString();n.DVT142 = m.DonVi;
                                                        break;
                                                    case 142:
                                                        n.SL14301 = m.SoLuong.ToString();n.DVT143 = m.DonVi;
                                                        break;
                                                    case 143:
                                                        n.SL14401 = m.SoLuong.ToString();n.DVT144 = m.DonVi;
                                                        break;
                                                    case 144:
                                                        n.SL4501 = m.SoLuong.ToString();n.DVT145 = m.DonVi;
                                                        break;
                                                    case 145:
                                                        n.SL14601 = m.SoLuong.ToString();n.DVT146 = m.DonVi;
                                                        break;
                                                    case 146:
                                                        n.SL14701 = m.SoLuong.ToString();n.DVT147 = m.DonVi;
                                                        break;
                                                    case 147:
                                                        n.SL14801 = m.SoLuong.ToString();n.DVT148 = m.DonVi;
                                                        break;
                                                    case 148:
                                                        n.SL4901 = m.SoLuong.ToString();n.DVT149 = m.DonVi;
                                                        break;
                                                    case 149:
                                                        n.SL15001 = m.SoLuong.ToString();n.DVT150 = m.DonVi;
                                                        break;
                                                    case 150:
                                                        n.SL15101 = m.SoLuong.ToString();n.DVT151 = m.DonVi;
                                                        break;
                                                    case 151:
                                                        n.SL15201 = m.SoLuong.ToString();n.DVT152 = m.DonVi;
                                                        break;
                                                    case 152:
                                                        n.SL15301 = m.SoLuong.ToString();n.DVT153 = m.DonVi;
                                                        break;
                                                    case 153:
                                                        n.SL15401 = m.SoLuong.ToString();n.DVT154 = m.DonVi;
                                                        break;
                                                    case 154:
                                                        n.SL15501 = m.SoLuong.ToString();n.DVT155 = m.DonVi;
                                                        break;
                                                    case 155:
                                                        n.SL15601 = m.SoLuong.ToString();n.DVT156 = m.DonVi;
                                                        break;
                                                    case 156:
                                                        n.SL15701 = m.SoLuong.ToString();n.DVT157 = m.DonVi;
                                                        break;
                                                    case 157:
                                                        n.SL15801 = m.SoLuong.ToString();n.DVT158 = m.DonVi;
                                                        break;
                                                    case 158:
                                                        n.SL15901 = m.SoLuong.ToString();n.DVT159 = m.DonVi;
                                                        break;
                                                    case 159:
                                                        n.SL16001 = m.SoLuong.ToString();n.DVT160 = m.DonVi;
                                                        break;
                                                    case 160:
                                                        n.SL16101 = m.SoLuong.ToString();n.DVT161 = m.DonVi;
                                                        break;
                                                    case 161:
                                                        n.SL16201 = m.SoLuong.ToString();n.DVT162 = m.DonVi;
                                                        break;
                                                    case 162:
                                                        n.SL16301 = m.SoLuong.ToString();n.DVT163 = m.DonVi;
                                                        break;
                                                    case 163:
                                                        n.SL16401 = m.SoLuong.ToString();n.DVT164 = m.DonVi;
                                                        break;
                                                    case 164:
                                                        n.SL16501 = m.SoLuong.ToString();n.DVT165 = m.DonVi;
                                                        break;
                                                    case 165:
                                                        n.SL16601 = m.SoLuong.ToString();n.DVT166 = m.DonVi;
                                                        break;
                                                    case 166:
                                                        n.SL16701 = m.SoLuong.ToString();n.DVT167 = m.DonVi;
                                                        break;
                                                    case 167:
                                                        n.SL16801 = m.SoLuong.ToString();n.DVT168 = m.DonVi;
                                                        break;
                                                    case 168:
                                                        n.SL16901 = m.SoLuong.ToString();n.DVT169 = m.DonVi;
                                                        break;
                                                    case 169:
                                                        n.SL17001 = m.SoLuong.ToString();n.DVT170 = m.DonVi;
                                                        break;
                                                }
                                                n.tenbn = m.TenBN;
                                                n.Tuoi = m.Tuoi.Value;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion tạo code báo cáo
                        if (_DSDV.Count < 153)
                        {
                            if (_DSDV.Count >= 100)
                            {
                                DialogResult _result = MessageBox.Show("Mẫu A4 không đủ để hiển thị, bạn có muốn in mẫu A3 không?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                {
                                    #region tạo báo cáo a3
                                    if (_DSDV.Count > 54)
                                    {
                                        BaoCao.Rep_TamTraThuocA3 rep = new BaoCao.Rep_TamTraThuocA3(mau);
                                        BaoCao.Rep_TamTraThuocA302 rep1 = new BaoCao.Rep_TamTraThuocA302(mau);
                                        BaoCao.Rep_TamTraThuocA303 rep2 = new BaoCao.Rep_TamTraThuocA303(mau);
                                        MessageBox.Show("Mẫu báo cáo in thành 2 phần thoát phần 1 để lấy BC phần 2");
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st1 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT01.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st2 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT02.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st3 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT03.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st4 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT04.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st5 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT05.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st6 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT06.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st7 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT07.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st8 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT08.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st9 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT09.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st10 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st11 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st12 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st13 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st14 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st15 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st16 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st17 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st18 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st19 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st20 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st21 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st22 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st23 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st24 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st25 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st26 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st27 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st28 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st29 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st30 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st31 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st32 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st33 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st34 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st35 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st36 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st37 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st38 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st39 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st40 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st41 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st42 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st43 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st44 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st45 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                        rep.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st46 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 46:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T47.Value = _DSDV.Skip(i).First().tendv;
                                                        st47 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT47.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 47:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T48.Value = _DSDV.Skip(i).First().tendv;
                                                        st48 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT48.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 48:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T49.Value = _DSDV.Skip(i).First().tendv;
                                                        st49 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT49.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 49:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T50.Value = _DSDV.Skip(i).First().tendv;
                                                        st50 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT50.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 50:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T51.Value = _DSDV.Skip(i).First().tendv;
                                                        st51 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT51.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 51:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T52.Value = _DSDV.Skip(i).First().tendv;
                                                        st52 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT52.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 52:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T53.Value = _DSDV.Skip(i).First().tendv;
                                                        st53 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT53.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 53:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T54.Value = _DSDV.Skip(i).First().tendv;
                                                        st54 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT54.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 54:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st55 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT01.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 55:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st56 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT02.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 56:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st57 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT03.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 57:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st58 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT04.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 58:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st59 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT05.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 59:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st60 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT06.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 60:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st61 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT07.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 61:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st62 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT08.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 62:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st63 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT09.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 63:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st64 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 64:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st65 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 65:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st66 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 66:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st67 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 67:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st68 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 68:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st69 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 69:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st70 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 70:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st71 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 71:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st72 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 72:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st73 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 73:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st74 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 74:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st75 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 75:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st76 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 76:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st77 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 77:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st78 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 78:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st79 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 79:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st80 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 80:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st81 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 81:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st82 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 82:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st83 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 83:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st84 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 84:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st85 = _DSDV.Skip(i).First().tendv; 
                                                        rep1.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 85:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st86 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 86:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st87 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 87:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st88 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 88:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st89 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 89:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st90 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 90:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st91 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 91:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st92 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 92:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st93 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 93:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st94 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 94:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st95 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 95:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st96 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 96:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st97 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 97:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st98 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 98:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st99 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 99:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st100 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 100:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T47.Value = _DSDV.Skip(i).First().tendv;
                                                        st101 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT47.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 101:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T48.Value = _DSDV.Skip(i).First().tendv;
                                                        st102 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT48.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 102:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T49.Value = _DSDV.Skip(i).First().tendv;
                                                        st103 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT49.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 103:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T50.Value = _DSDV.Skip(i).First().tendv;
                                                        st104 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT50.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 104:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T51.Value = _DSDV.Skip(i).First().tendv;
                                                        st105 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT51.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 105:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T52.Value = _DSDV.Skip(i).First().tendv;
                                                        st106 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT52.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 106:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T53.Value = _DSDV.Skip(i).First().tendv;
                                                        st107 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT53.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 107:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T54.Value = _DSDV.Skip(i).First().tendv;
                                                        st108 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT54.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 108:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st109 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT01.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 109:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st110 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT02.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 110:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st111 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT03.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 111:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st112 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT04.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 112:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st113 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT05.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 113:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st114 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT06.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 114:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st115 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT07.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 115:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st116 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT08.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 116:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st117 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT09.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 117:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st118 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 118:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st119 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 119:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st120 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 120:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st121 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 121:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st122 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 122:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st123 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 123:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st124 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 124:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st125 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 125:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st126 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 126:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st127 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 127:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st128 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 128:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st129 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 129:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st130 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 130:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st131 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 131:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st132 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 132:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st133 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 133:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st134 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 134:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st135 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 135:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st136 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 136:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st137 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 137:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st138 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 138:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st139 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 139:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st140 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 140:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st141 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 141:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st142 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 142:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st143 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 143:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st144 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 144:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st145 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 145:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st146 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 146:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st147 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 147:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st148 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 148:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st149 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 149:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st150 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 150:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st151 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 151:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st152 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 152:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st153 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 153:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st154 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 155:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T47.Value = _DSDV.Skip(i).First().tendv;
                                                        st156 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT47.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 156:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T48.Value = _DSDV.Skip(i).First().tendv;
                                                        st157 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT48.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 157:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T49.Value = _DSDV.Skip(i).First().tendv;
                                                        st158 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT49.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 158:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T50.Value = _DSDV.Skip(i).First().tendv;
                                                        st159 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT50.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 159:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T51.Value = _DSDV.Skip(i).First().tendv;
                                                        st160 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT51.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 161:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T52.Value = _DSDV.Skip(i).First().tendv;
                                                        st162 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT52.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 162:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T53.Value = _DSDV.Skip(i).First().tendv;
                                                        st163 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT53.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 163:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T54.Value = _DSDV.Skip(i).First().tendv;
                                                        st164 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT54.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                            }
                                        }

                                        #region
                                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                        int[] _arrWidth = new int[] { };
                                        int num = 1;
                                        DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                        string[] _tieude = { "STT", "Họ và tên", "Tuổi",st1,st2,st3,st4,st5,st6,st7,st8,st9,st10,st11,st12,st13,st14,st15,st16,st17,st18,st19,st20,st21,st22,st23,st24,st25
                                                               ,st26,st27,st28,st29,st30,st31,st32,st33,st34,st35,st36,st37,st38,st39,st40,st41,st42,st43,st44,st45,st46,st47,st48,st49,st50,st51,st52,st53,st54, "Ký nhận"};
                                        for (int i = 0; i < _tieude.Length; i++)
                                        {
                                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                        }

                                        ////for (int i = 0; i <= 17; i++) {
                                        ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                        ////}
                                        foreach (var r in _Tamtra.ToList())
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                                            DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.SL101;
                                            DungChung.Bien.MangHaiChieu[num, 4] = r.SL201;
                                            DungChung.Bien.MangHaiChieu[num, 5] = r.SL301;
                                            DungChung.Bien.MangHaiChieu[num, 6] = r.SL401;
                                            DungChung.Bien.MangHaiChieu[num, 7] = r.SL501;
                                            DungChung.Bien.MangHaiChieu[num, 8] = r.SL601;
                                            DungChung.Bien.MangHaiChieu[num, 9] = r.SL701;
                                            DungChung.Bien.MangHaiChieu[num, 10] = r.SL801;
                                            DungChung.Bien.MangHaiChieu[num, 11] = r.SL901;
                                            DungChung.Bien.MangHaiChieu[num, 12] = r.SL1001;
                                            DungChung.Bien.MangHaiChieu[num, 13] = r.SL1101;
                                            DungChung.Bien.MangHaiChieu[num, 14] = r.SL1201;
                                            DungChung.Bien.MangHaiChieu[num, 15] = r.SL1301;
                                            DungChung.Bien.MangHaiChieu[num, 16] = r.SL1401;
                                            DungChung.Bien.MangHaiChieu[num, 17] = r.SL1501;
                                            DungChung.Bien.MangHaiChieu[num, 18] = r.SL1601;
                                            DungChung.Bien.MangHaiChieu[num, 19] = r.SL1701;
                                            DungChung.Bien.MangHaiChieu[num, 20] = r.SL1801;
                                            DungChung.Bien.MangHaiChieu[num, 21] = r.SL1901;
                                            DungChung.Bien.MangHaiChieu[num, 22] = r.SL2001;
                                            DungChung.Bien.MangHaiChieu[num, 23] = r.SL2101;
                                            DungChung.Bien.MangHaiChieu[num, 24] = r.SL2201;
                                            DungChung.Bien.MangHaiChieu[num, 25] = r.SL2301;
                                            DungChung.Bien.MangHaiChieu[num, 26] = r.SL2401;
                                            DungChung.Bien.MangHaiChieu[num, 27] = r.SL2501;
                                            DungChung.Bien.MangHaiChieu[num, 28] = r.SL2601;
                                            DungChung.Bien.MangHaiChieu[num, 29] = r.SL2701;
                                            DungChung.Bien.MangHaiChieu[num, 30] = r.SL2801;
                                            DungChung.Bien.MangHaiChieu[num, 31] = r.SL2901;
                                            DungChung.Bien.MangHaiChieu[num, 32] = r.SL3001;
                                            DungChung.Bien.MangHaiChieu[num, 33] = r.SL3101;
                                            DungChung.Bien.MangHaiChieu[num, 34] = r.SL3201;
                                            DungChung.Bien.MangHaiChieu[num, 35] = r.SL3301;
                                            DungChung.Bien.MangHaiChieu[num, 36] = r.SL3401;
                                            DungChung.Bien.MangHaiChieu[num, 37] = r.SL3501;
                                            DungChung.Bien.MangHaiChieu[num, 38] = r.SL3601;
                                            DungChung.Bien.MangHaiChieu[num, 39] = r.SL3701;
                                            DungChung.Bien.MangHaiChieu[num, 40] = r.SL3801;
                                            DungChung.Bien.MangHaiChieu[num, 41] = r.SL3901;
                                            DungChung.Bien.MangHaiChieu[num, 42] = r.SL4001;
                                            DungChung.Bien.MangHaiChieu[num, 43] = r.SL4101;
                                            DungChung.Bien.MangHaiChieu[num, 44] = r.SL4201;
                                            DungChung.Bien.MangHaiChieu[num, 45] = r.SL4301;
                                            DungChung.Bien.MangHaiChieu[num, 46] = r.SL4401;
                                            DungChung.Bien.MangHaiChieu[num, 47] = r.SL4501;
                                            DungChung.Bien.MangHaiChieu[num, 48] = r.SL4601;
                                            DungChung.Bien.MangHaiChieu[num, 49] = r.SL4701;
                                            DungChung.Bien.MangHaiChieu[num, 50] = r.SL4801;
                                            DungChung.Bien.MangHaiChieu[num, 51] = r.SL4901;
                                            DungChung.Bien.MangHaiChieu[num, 52] = r.SL5001;
                                            DungChung.Bien.MangHaiChieu[num, 53] = r.SL5101;
                                            DungChung.Bien.MangHaiChieu[num, 54] = r.SL5201;
                                            DungChung.Bien.MangHaiChieu[num, 55] = r.SL5301;
                                            DungChung.Bien.MangHaiChieu[num, 56] = r.SL5401;
                                            DungChung.Bien.MangHaiChieu[num, 57] = "";
                                            num++;
                                        }
                                        #endregion

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.KhoaPhong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                        rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep1.KhoaPhong.Value = LupKhoaPhong.Text;
                                        rep1.DataSource = _Tamtra;
                                        rep1.BindingData();
                                        rep1.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                        frm1.ShowDialog();

                                        if (_DSDV.Count > 108)
                                        {
                                            rep2.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                            rep2.KhoaPhong.Value = LupKhoaPhong.Text;
                                            rep2.DataSource = _Tamtra;
                                            rep2.BindingData();
                                            rep2.CreateDocument();
                                            //rep.DataMember = "Table";
                                            frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                            frm2.prcIN.PrintingSystem = rep2.PrintingSystem;
                                            frm2.ShowDialog();
                                        }
                                    }
                                    #endregion tạo báo cáo a3
                                }
                                else
                                {
                                    #region tạo báo cáo A4 1 trang
                                    if (_DSDV.Count <= 46)
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc(mau);
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st1 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT1.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st2 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT2.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st3 = _DSDV.Skip(i).First().tendv; ;
                                                        rep.DVT3.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st4 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT4.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st5 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT5.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st6 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT6.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st7 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT7.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st8 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT8.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st9 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT9.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st10 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st11 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st12 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st13 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st14 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st15 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st16 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st17 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st18 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st19 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st20 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st21 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st22 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st23 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st24 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st25 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st26 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st27 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st28 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st29 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st30 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st31 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st32 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st33 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st34 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st35 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st36 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st37 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st38 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st39 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st40 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st41 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st42 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st43 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st44 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                        st45 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st46 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;

                                            }
                                        }

                                        #region
                                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                        int[] _arrWidth = new int[] { };
                                        int num = 1;
                                        DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                        string[] _tieude = { "STT", "Họ và tên", "Tuổi",st1,st2,st3,st4,st5,st6,st7,st8,st9,st10,st11,st12,st13,st14,st15,st16,st17,st18,st19,st20,st21,st22,st23,st24,st25
                                                               ,st26,st27,st28,st29,st30,st31,st32,st33,st34,st35,st36,st37,st38,st39,st40,st41,st42,st43,st44,st45,st46, "Ký nhận"};
                                        for (int i = 0; i < _tieude.Length; i++)
                                        {
                                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i]; ;
                                        }

                                        ////for (int i = 0; i <= 17; i++) {
                                        ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                        ////}
                                        foreach (var r in _Tamtra.ToList())
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                                            DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.SL101;
                                            DungChung.Bien.MangHaiChieu[num, 4] = r.SL201;
                                            DungChung.Bien.MangHaiChieu[num, 5] = r.SL301;
                                            DungChung.Bien.MangHaiChieu[num, 6] = r.SL401;
                                            DungChung.Bien.MangHaiChieu[num, 7] = r.SL501;
                                            DungChung.Bien.MangHaiChieu[num, 8] = r.SL601;
                                            DungChung.Bien.MangHaiChieu[num, 9] = r.SL701;
                                            DungChung.Bien.MangHaiChieu[num, 10] = r.SL801;
                                            DungChung.Bien.MangHaiChieu[num, 11] = r.SL901;
                                            DungChung.Bien.MangHaiChieu[num, 12] = r.SL1001;
                                            DungChung.Bien.MangHaiChieu[num, 13] = r.SL1101;
                                            DungChung.Bien.MangHaiChieu[num, 14] = r.SL1201;
                                            DungChung.Bien.MangHaiChieu[num, 15] = r.SL1301;
                                            DungChung.Bien.MangHaiChieu[num, 16] = r.SL1401;
                                            DungChung.Bien.MangHaiChieu[num, 17] = r.SL1501;
                                            DungChung.Bien.MangHaiChieu[num, 18] = r.SL1601;
                                            DungChung.Bien.MangHaiChieu[num, 19] = r.SL1701;
                                            DungChung.Bien.MangHaiChieu[num, 20] = r.SL1801;
                                            DungChung.Bien.MangHaiChieu[num, 21] = r.SL1901;
                                            DungChung.Bien.MangHaiChieu[num, 22] = r.SL2001;
                                            DungChung.Bien.MangHaiChieu[num, 23] = r.SL2101;
                                            DungChung.Bien.MangHaiChieu[num, 24] = r.SL2201;
                                            DungChung.Bien.MangHaiChieu[num, 25] = r.SL2301;
                                            DungChung.Bien.MangHaiChieu[num, 26] = r.SL2401;
                                            DungChung.Bien.MangHaiChieu[num, 27] = r.SL2501;
                                            DungChung.Bien.MangHaiChieu[num, 28] = r.SL2601;
                                            DungChung.Bien.MangHaiChieu[num, 29] = r.SL2701;
                                            DungChung.Bien.MangHaiChieu[num, 30] = r.SL2801;
                                            DungChung.Bien.MangHaiChieu[num, 31] = r.SL2901;
                                            DungChung.Bien.MangHaiChieu[num, 32] = r.SL3001;
                                            DungChung.Bien.MangHaiChieu[num, 33] = r.SL3101;
                                            DungChung.Bien.MangHaiChieu[num, 34] = r.SL3201;
                                            DungChung.Bien.MangHaiChieu[num, 35] = r.SL3301;
                                            DungChung.Bien.MangHaiChieu[num, 36] = r.SL3401;
                                            DungChung.Bien.MangHaiChieu[num, 37] = r.SL3501;
                                            DungChung.Bien.MangHaiChieu[num, 38] = r.SL3601;
                                            DungChung.Bien.MangHaiChieu[num, 39] = r.SL3701;
                                            DungChung.Bien.MangHaiChieu[num, 40] = r.SL3801;
                                            DungChung.Bien.MangHaiChieu[num, 41] = r.SL3901;
                                            DungChung.Bien.MangHaiChieu[num, 42] = r.SL4001;
                                            DungChung.Bien.MangHaiChieu[num, 43] = r.SL4101;
                                            DungChung.Bien.MangHaiChieu[num, 44] = r.SL4201;
                                            DungChung.Bien.MangHaiChieu[num, 45] = r.SL4301;
                                            DungChung.Bien.MangHaiChieu[num, 46] = r.SL4401;
                                            DungChung.Bien.MangHaiChieu[num, 47] = r.SL4501;
                                            DungChung.Bien.MangHaiChieu[num, 48] = r.SL4601;
                                            DungChung.Bien.MangHaiChieu[num, 49] = "";
                                            num++;
                                        }
                                        #endregion

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    #endregion 
                                    #region tạo báo cáo A4 2 trang
                                    if (_DSDV.Count > 46)
                                    {

                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc(mau);
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st1 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT1.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st2 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT2.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st3 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT3.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st4 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT4.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st5 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT5.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st6 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT6.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st7 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT7.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st8 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT8.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st9 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT9.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st10 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st11 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st12 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st13 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st14 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st15 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st16 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st17 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st18 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st19 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st20 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st21 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st22 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st23 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st24 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st25 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st26 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st27 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st28 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st29 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st30 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st31 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st32 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st33 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st34 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st35 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st36 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st37 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st38 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st39 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st40 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st41 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st42 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st43 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st44 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st45 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                        rep.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st46 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;

                                            }
                                        }

                                        #region
                                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                        int[] _arrWidth = new int[] { };
                                        int num = 1;
                                        DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                        string[] _tieude = { "STT", "Họ và tên", "Tuổi",st1,st2,st3,st4,st5,st6,st7,st8,st9,st10,st11,st12,st13,st14,st15,st16,st17,st18,st19,st20,st21,st22,st23,st24,st25
                                                               ,st26,st27,st28,st29,st30,st31,st32,st33,st34,st35,st36,st37,st38,st39,st40,st41,st42,st43,st44,st45,st46, "Ký nhận"};
                                        for (int i = 0; i < _tieude.Length; i++)
                                        {
                                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                        }

                                        ////for (int i = 0; i <= 17; i++) {
                                        ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                        ////}
                                        foreach (var r in _Tamtra.ToList())
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                                            DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.SL101;
                                            DungChung.Bien.MangHaiChieu[num, 4] = r.SL201;
                                            DungChung.Bien.MangHaiChieu[num, 5] = r.SL301;
                                            DungChung.Bien.MangHaiChieu[num, 6] = r.SL401;
                                            DungChung.Bien.MangHaiChieu[num, 7] = r.SL501;
                                            DungChung.Bien.MangHaiChieu[num, 8] = r.SL601;
                                            DungChung.Bien.MangHaiChieu[num, 9] = r.SL701;
                                            DungChung.Bien.MangHaiChieu[num, 10] = r.SL801;
                                            DungChung.Bien.MangHaiChieu[num, 11] = r.SL901;
                                            DungChung.Bien.MangHaiChieu[num, 12] = r.SL1001;
                                            DungChung.Bien.MangHaiChieu[num, 13] = r.SL1101;
                                            DungChung.Bien.MangHaiChieu[num, 14] = r.SL1201;
                                            DungChung.Bien.MangHaiChieu[num, 15] = r.SL1301;
                                            DungChung.Bien.MangHaiChieu[num, 16] = r.SL1401;
                                            DungChung.Bien.MangHaiChieu[num, 17] = r.SL1501;
                                            DungChung.Bien.MangHaiChieu[num, 18] = r.SL1601;
                                            DungChung.Bien.MangHaiChieu[num, 19] = r.SL1701;
                                            DungChung.Bien.MangHaiChieu[num, 20] = r.SL1801;
                                            DungChung.Bien.MangHaiChieu[num, 21] = r.SL1901;
                                            DungChung.Bien.MangHaiChieu[num, 22] = r.SL2001;
                                            DungChung.Bien.MangHaiChieu[num, 23] = r.SL2101;
                                            DungChung.Bien.MangHaiChieu[num, 24] = r.SL2201;
                                            DungChung.Bien.MangHaiChieu[num, 25] = r.SL2301;
                                            DungChung.Bien.MangHaiChieu[num, 26] = r.SL2401;
                                            DungChung.Bien.MangHaiChieu[num, 27] = r.SL2501;
                                            DungChung.Bien.MangHaiChieu[num, 28] = r.SL2601;
                                            DungChung.Bien.MangHaiChieu[num, 29] = r.SL2701;
                                            DungChung.Bien.MangHaiChieu[num, 30] = r.SL2801;
                                            DungChung.Bien.MangHaiChieu[num, 31] = r.SL2901;
                                            DungChung.Bien.MangHaiChieu[num, 32] = r.SL3001;
                                            DungChung.Bien.MangHaiChieu[num, 33] = r.SL3101;
                                            DungChung.Bien.MangHaiChieu[num, 34] = r.SL3201;
                                            DungChung.Bien.MangHaiChieu[num, 35] = r.SL3301;
                                            DungChung.Bien.MangHaiChieu[num, 36] = r.SL3401;
                                            DungChung.Bien.MangHaiChieu[num, 37] = r.SL3501;
                                            DungChung.Bien.MangHaiChieu[num, 38] = r.SL3601;
                                            DungChung.Bien.MangHaiChieu[num, 39] = r.SL3701;
                                            DungChung.Bien.MangHaiChieu[num, 40] = r.SL3801;
                                            DungChung.Bien.MangHaiChieu[num, 41] = r.SL3901;
                                            DungChung.Bien.MangHaiChieu[num, 42] = r.SL4001;
                                            DungChung.Bien.MangHaiChieu[num, 43] = r.SL4101;
                                            DungChung.Bien.MangHaiChieu[num, 44] = r.SL4201;
                                            DungChung.Bien.MangHaiChieu[num, 45] = r.SL4301;
                                            DungChung.Bien.MangHaiChieu[num, 46] = r.SL4401;
                                            DungChung.Bien.MangHaiChieu[num, 47] = r.SL4501;
                                            DungChung.Bien.MangHaiChieu[num, 48] = r.SL4601;
                                            DungChung.Bien.MangHaiChieu[num, 49] = "";
                                            num++;
                                        }
                                        #endregion

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();


                                        #region tạo báo cáo thứ 2
                                        BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02(mau);
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += "bổ xung"; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep1.Kieudon.Value = a;
                                        for (int i = 46; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 46:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st47 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT01.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 47:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st48 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT02.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 48:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st49 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT03.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 49:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st50 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT04.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 50:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st51 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT05.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 51:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st52 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT06.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 52:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st53 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT07.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 53:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st54 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT08.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 54:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st55 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT09.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 55:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st56 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 56:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st57 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 57:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st58 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 58:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st59 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 59:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st60 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 60:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st61 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 61:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st62 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 62:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st63 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 63:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st64 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 64:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st65 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 65:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st66 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 66:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st67 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 67:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st68 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 68:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st69 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 69:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st70 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 70:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st71 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 71:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st72 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 72:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st73 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 73:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st74 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 74:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st75 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 75:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st76 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 76:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st77 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 77:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st78 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 78:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st79 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 79:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st80 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 80:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st81 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 81:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st82 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 82:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st83 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 83:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st84 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 84:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st85 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 85:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st86 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 86:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st87 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 87:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st88 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 88:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st89 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 89:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st90 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 90:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st91 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 91:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st92 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 92:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T47.Value = _DSDV.Skip(i).First().tendv;
                                                        st93 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT47.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 93:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T48.Value = _DSDV.Skip(i).First().tendv;
                                                        st94 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT48.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 94:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T49.Value = _DSDV.Skip(i).First().tendv;
                                                        st95 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT49.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 95:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T50.Value = _DSDV.Skip(i).First().tendv;
                                                        st96 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT50.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 96:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T51.Value = _DSDV.Skip(i).First().tendv;
                                                        st97 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT51.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 97:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T52.Value = _DSDV.Skip(i).First().tendv;
                                                        st98 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT52.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 98:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T53.Value = _DSDV.Skip(i).First().tendv;
                                                        st99 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT53.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;

                                            }
                                        }
                                        #endregion

                                        #region
                                        string[] _arr1 = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                        int[] _arrWidth1 = new int[] { };
                                        int num1 = 1;
                                        DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                        string[] _tieude1 = { "STT",st47,st48,st49,st50,st11,st52,st53,st54,st55,st56,st57,st58,st59,st60,st61,st62,st63,st64,st65,st66,st67,st68,st69,st70
                                                                ,st71,st72,st73,st74,st75,st75,st76,st77,st78,st79,st80,st81,st82,st83,st84,st85,st86,st87,st88,st89,st90,st91,st92,st93,st94,st95,st96,st97,st98,st99, "Ký nhận"};
                                        for (int i = 0; i < _tieude1.Length; i++)
                                        {
                                            DungChung.Bien.MangHaiChieu[0, i] = _tieude1[i];
                                        }

                                        ////for (int i = 0; i <= 17; i++) {
                                        ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                        ////}
                                        foreach (var r in _Tamtra.ToList())
                                        {
                                            DungChung.Bien.MangHaiChieu[num1, 0] = num1;
                                            DungChung.Bien.MangHaiChieu[num1, 1] = r.SL101;
                                            DungChung.Bien.MangHaiChieu[num1, 2] = r.SL201;
                                            DungChung.Bien.MangHaiChieu[num1, 3] = r.SL301;
                                            DungChung.Bien.MangHaiChieu[num1, 4] = r.SL401;
                                            DungChung.Bien.MangHaiChieu[num1, 5] = r.SL501;
                                            DungChung.Bien.MangHaiChieu[num1, 6] = r.SL601;
                                            DungChung.Bien.MangHaiChieu[num1, 7] = r.SL701;
                                            DungChung.Bien.MangHaiChieu[num1, 8] = r.SL801;
                                            DungChung.Bien.MangHaiChieu[num1, 9] = r.SL901;
                                            DungChung.Bien.MangHaiChieu[num1, 10] = r.SL1001;
                                            DungChung.Bien.MangHaiChieu[num1, 11] = r.SL1101;
                                            DungChung.Bien.MangHaiChieu[num1, 12] = r.SL1201;
                                            DungChung.Bien.MangHaiChieu[num1, 13] = r.SL1301;
                                            DungChung.Bien.MangHaiChieu[num1, 14] = r.SL1401;
                                            DungChung.Bien.MangHaiChieu[num1, 15] = r.SL1501;
                                            DungChung.Bien.MangHaiChieu[num1, 16] = r.SL1601;
                                            DungChung.Bien.MangHaiChieu[num1, 17] = r.SL1701;
                                            DungChung.Bien.MangHaiChieu[num1, 18] = r.SL1801;
                                            DungChung.Bien.MangHaiChieu[num1, 19] = r.SL1901;
                                            DungChung.Bien.MangHaiChieu[num1, 20] = r.SL2001;
                                            DungChung.Bien.MangHaiChieu[num1, 21] = r.SL2101;
                                            DungChung.Bien.MangHaiChieu[num1, 22] = r.SL2201;
                                            DungChung.Bien.MangHaiChieu[num1, 23] = r.SL2301;
                                            DungChung.Bien.MangHaiChieu[num1, 24] = r.SL2401;
                                            DungChung.Bien.MangHaiChieu[num1, 25] = r.SL2501;
                                            DungChung.Bien.MangHaiChieu[num1, 26] = r.SL2601;
                                            DungChung.Bien.MangHaiChieu[num1, 27] = r.SL2701;
                                            DungChung.Bien.MangHaiChieu[num1, 28] = r.SL2801;
                                            DungChung.Bien.MangHaiChieu[num1, 29] = r.SL2901;
                                            DungChung.Bien.MangHaiChieu[num1, 30] = r.SL3001;
                                            DungChung.Bien.MangHaiChieu[num1, 31] = r.SL3101;
                                            DungChung.Bien.MangHaiChieu[num1, 32] = r.SL3201;
                                            DungChung.Bien.MangHaiChieu[num1, 33] = r.SL3301;
                                            DungChung.Bien.MangHaiChieu[num1, 34] = r.SL3401;
                                            DungChung.Bien.MangHaiChieu[num1, 35] = r.SL3501;
                                            DungChung.Bien.MangHaiChieu[num1, 36] = r.SL3601;
                                            DungChung.Bien.MangHaiChieu[num1, 37] = r.SL3701;
                                            DungChung.Bien.MangHaiChieu[num1, 38] = r.SL3801;
                                            DungChung.Bien.MangHaiChieu[num1, 39] = r.SL3901;
                                            DungChung.Bien.MangHaiChieu[num1, 40] = r.SL4001;
                                            DungChung.Bien.MangHaiChieu[num1, 41] = r.SL4101;
                                            DungChung.Bien.MangHaiChieu[num1, 42] = r.SL4201;
                                            DungChung.Bien.MangHaiChieu[num1, 43] = r.SL4301;
                                            DungChung.Bien.MangHaiChieu[num1, 44] = r.SL4401;
                                            DungChung.Bien.MangHaiChieu[num1, 45] = r.SL4501;
                                            DungChung.Bien.MangHaiChieu[num1, 46] = r.SL4601;
                                            DungChung.Bien.MangHaiChieu[num1, 47] = r.SL4701;
                                            DungChung.Bien.MangHaiChieu[num1, 48] = r.SL4801;
                                            DungChung.Bien.MangHaiChieu[num1, 49] = r.SL4901;
                                            DungChung.Bien.MangHaiChieu[num1, 50] = r.SL5001;
                                            DungChung.Bien.MangHaiChieu[num1, 51] = r.SL5101;
                                            DungChung.Bien.MangHaiChieu[num1, 52] = r.SL5201;
                                            DungChung.Bien.MangHaiChieu[num1, 53] = r.SL5301;
                                            DungChung.Bien.MangHaiChieu[num1, 54] = "";
                                            num1++;
                                        }
                                        #endregion

                                        rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep1.DataSource = _Tamtra;
                                        rep1.BindingData();
                                        rep1.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr1, _arrWidth1, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                        frm1.ShowDialog();
                                    }
                                    #endregion tạo báo cáo A4 2 trang
                                    
                                }
                            }
                            else
                            {
                                #region tạo báo cáo A4 1 trang
                                if (_DSDV.Count <= 46)
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc(mau);
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T1.Value = _DSDV.Skip(i).First().tendv;
                                                    st1 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT1.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T2.Value = _DSDV.Skip(i).First().tendv;
                                                    st2 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT2.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T3.Value = _DSDV.Skip(i).First().tendv;
                                                    st3 = _DSDV.Skip(i).First().tendv; ;
                                                    rep.DVT3.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T4.Value = _DSDV.Skip(i).First().tendv;
                                                    st4 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT4.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T5.Value = _DSDV.Skip(i).First().tendv;
                                                    st5 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT5.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T6.Value = _DSDV.Skip(i).First().tendv;
                                                    st6 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT6.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T7.Value = _DSDV.Skip(i).First().tendv;
                                                    st7 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT7.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T8.Value = _DSDV.Skip(i).First().tendv;
                                                    st8 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT8.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T9.Value = _DSDV.Skip(i).First().tendv;
                                                    st9 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT9.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T10.Value = _DSDV.Skip(i).First().tendv;
                                                    st10 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T11.Value = _DSDV.Skip(i).First().tendv;
                                                    st11 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T12.Value = _DSDV.Skip(i).First().tendv;
                                                    st12 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                    st13 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T14.Value = _DSDV.Skip(i).First().tendv;
                                                    st14 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T15.Value = _DSDV.Skip(i).First().tendv;
                                                    st15 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T16.Value = _DSDV.Skip(i).First().tendv;
                                                    st16 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T17.Value = _DSDV.Skip(i).First().tendv;
                                                    st17 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T18.Value = _DSDV.Skip(i).First().tendv;
                                                    st18 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T19.Value = _DSDV.Skip(i).First().tendv;
                                                    st19 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T20.Value = _DSDV.Skip(i).First().tendv;
                                                    st20 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T21.Value = _DSDV.Skip(i).First().tendv;
                                                    st21 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T22.Value = _DSDV.Skip(i).First().tendv;
                                                    st22 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T23.Value = _DSDV.Skip(i).First().tendv;
                                                    st23 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T24.Value = _DSDV.Skip(i).First().tendv;
                                                    st24 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T25.Value = _DSDV.Skip(i).First().tendv;
                                                    st25 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T26.Value = _DSDV.Skip(i).First().tendv;
                                                    st26 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T27.Value = _DSDV.Skip(i).First().tendv;
                                                    st27 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T28.Value = _DSDV.Skip(i).First().tendv;
                                                    st28 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T29.Value = _DSDV.Skip(i).First().tendv;
                                                    st29 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T30.Value = _DSDV.Skip(i).First().tendv;
                                                    st30 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T31.Value = _DSDV.Skip(i).First().tendv;
                                                    st31 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T32.Value = _DSDV.Skip(i).First().tendv;
                                                    st32 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T33.Value = _DSDV.Skip(i).First().tendv;
                                                    st33 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T34.Value = _DSDV.Skip(i).First().tendv;
                                                    st34 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T35.Value = _DSDV.Skip(i).First().tendv;
                                                    st35 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T36.Value = _DSDV.Skip(i).First().tendv;
                                                    st36 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T37.Value = _DSDV.Skip(i).First().tendv;
                                                    st37 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T38.Value = _DSDV.Skip(i).First().tendv;
                                                    st38 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T39.Value = _DSDV.Skip(i).First().tendv;
                                                    st39 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T40.Value = _DSDV.Skip(i).First().tendv;
                                                    st40 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T41.Value = _DSDV.Skip(i).First().tendv;
                                                    st41 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T42.Value = _DSDV.Skip(i).First().tendv;
                                                    st42 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T43.Value = _DSDV.Skip(i).First().tendv;
                                                    st43 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T44.Value = _DSDV.Skip(i).First().tendv;
                                                    st44 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                               break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T45.Value = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                    st45 = _DSDV.Skip(i).First().tendv;
                                                }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T46.Value = _DSDV.Skip(i).First().tendv;
                                                    st46 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;

                                        }
                                    }

                                    #region
                                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth = new int[] { };
                                    int num = 1;
                                    DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                    string[] _tieude = { "STT", "Họ và tên", "Tuổi",st1,st2,st3,st4,st5,st6,st7,st8,st9,st10,st11,st12,st13,st14,st15,st16,st17,st18,st19,st20,st21,st22,st23,st24,st25
                                                               ,st26,st27,st28,st29,st30,st31,st32,st33,st34,st35,st36,st37,st38,st39,st40,st41,st42,st43,st44,st45,st46, "Ký nhận"};
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i]; ;
                                    }

                                    ////for (int i = 0; i <= 17; i++) {
                                    ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                    ////}
                                    foreach (var r in _Tamtra.ToList())
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.SL101;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.SL201;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.SL301;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.SL401;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.SL501;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.SL601;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.SL701;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.SL801;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.SL901;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.SL1001;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.SL1101;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.SL1201;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.SL1301;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.SL1401;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.SL1501;
                                        DungChung.Bien.MangHaiChieu[num, 18] = r.SL1601;
                                        DungChung.Bien.MangHaiChieu[num, 19] = r.SL1701;
                                        DungChung.Bien.MangHaiChieu[num, 20] = r.SL1801;
                                        DungChung.Bien.MangHaiChieu[num, 21] = r.SL1901;
                                        DungChung.Bien.MangHaiChieu[num, 22] = r.SL2001;
                                        DungChung.Bien.MangHaiChieu[num, 23] = r.SL2101;
                                        DungChung.Bien.MangHaiChieu[num, 24] = r.SL2201;
                                        DungChung.Bien.MangHaiChieu[num, 25] = r.SL2301;
                                        DungChung.Bien.MangHaiChieu[num, 26] = r.SL2401;
                                        DungChung.Bien.MangHaiChieu[num, 27] = r.SL2501;
                                        DungChung.Bien.MangHaiChieu[num, 28] = r.SL2601;
                                        DungChung.Bien.MangHaiChieu[num, 29] = r.SL2701;
                                        DungChung.Bien.MangHaiChieu[num, 30] = r.SL2801;
                                        DungChung.Bien.MangHaiChieu[num, 31] = r.SL2901;
                                        DungChung.Bien.MangHaiChieu[num, 32] = r.SL3001;
                                        DungChung.Bien.MangHaiChieu[num, 33] = r.SL3101;
                                        DungChung.Bien.MangHaiChieu[num, 34] = r.SL3201;
                                        DungChung.Bien.MangHaiChieu[num, 35] = r.SL3301;
                                        DungChung.Bien.MangHaiChieu[num, 36] = r.SL3401;
                                        DungChung.Bien.MangHaiChieu[num, 37] = r.SL3501;
                                        DungChung.Bien.MangHaiChieu[num, 38] = r.SL3601;
                                        DungChung.Bien.MangHaiChieu[num, 39] = r.SL3701;
                                        DungChung.Bien.MangHaiChieu[num, 40] = r.SL3801;
                                        DungChung.Bien.MangHaiChieu[num, 41] = r.SL3901;
                                        DungChung.Bien.MangHaiChieu[num, 42] = r.SL4001;
                                        DungChung.Bien.MangHaiChieu[num, 43] = r.SL4101;
                                        DungChung.Bien.MangHaiChieu[num, 44] = r.SL4201;
                                        DungChung.Bien.MangHaiChieu[num, 45] = r.SL4301;
                                        DungChung.Bien.MangHaiChieu[num, 46] = r.SL4401;
                                        DungChung.Bien.MangHaiChieu[num, 47] = r.SL4501;
                                        DungChung.Bien.MangHaiChieu[num, 48] = r.SL4601;
                                        DungChung.Bien.MangHaiChieu[num, 49] = "";
                                        num++;
                                    }
                                    #endregion

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                }
                                #endregion 
                                #region tạo báo cáo A4 2 trang
                                if (_DSDV.Count > 46)
                                {

                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc(mau);
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T1.Value = _DSDV.Skip(i).First().tendv;
                                                    st1 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT1.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T2.Value = _DSDV.Skip(i).First().tendv;
                                                    st2 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT2.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T3.Value = _DSDV.Skip(i).First().tendv;
                                                    st3 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT3.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T4.Value = _DSDV.Skip(i).First().tendv;
                                                    st4 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT4.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T5.Value = _DSDV.Skip(i).First().tendv;
                                                    st5 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT5.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T6.Value = _DSDV.Skip(i).First().tendv;
                                                    st6 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT6.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T7.Value = _DSDV.Skip(i).First().tendv;
                                                    st7 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT7.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T8.Value = _DSDV.Skip(i).First().tendv;
                                                    st8 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT8.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T9.Value = _DSDV.Skip(i).First().tendv;
                                                    st9 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT9.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T10.Value = _DSDV.Skip(i).First().tendv;
                                                    st10 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T11.Value = _DSDV.Skip(i).First().tendv;
                                                    st11 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T12.Value = _DSDV.Skip(i).First().tendv;
                                                    st12 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                    st13 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T14.Value = _DSDV.Skip(i).First().tendv;
                                                    st14 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T15.Value = _DSDV.Skip(i).First().tendv;
                                                    st15 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T16.Value = _DSDV.Skip(i).First().tendv;
                                                    st16 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T17.Value = _DSDV.Skip(i).First().tendv;
                                                    st17 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T18.Value = _DSDV.Skip(i).First().tendv;
                                                    st18 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T19.Value = _DSDV.Skip(i).First().tendv;
                                                    st19 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T20.Value = _DSDV.Skip(i).First().tendv;
                                                    st20 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T21.Value = _DSDV.Skip(i).First().tendv;
                                                    st21 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T22.Value = _DSDV.Skip(i).First().tendv;
                                                    st22 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T23.Value = _DSDV.Skip(i).First().tendv;
                                                    st23 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T24.Value = _DSDV.Skip(i).First().tendv;
                                                    st24 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T25.Value = _DSDV.Skip(i).First().tendv;
                                                    st25 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T26.Value = _DSDV.Skip(i).First().tendv;
                                                    st26 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T27.Value = _DSDV.Skip(i).First().tendv;
                                                    st27 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T28.Value = _DSDV.Skip(i).First().tendv;
                                                    st28 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T29.Value = _DSDV.Skip(i).First().tendv;
                                                    st29 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T30.Value = _DSDV.Skip(i).First().tendv;
                                                    st30 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T31.Value = _DSDV.Skip(i).First().tendv;
                                                    st31 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T32.Value = _DSDV.Skip(i).First().tendv;
                                                    st32 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T33.Value = _DSDV.Skip(i).First().tendv;
                                                    st33 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T34.Value = _DSDV.Skip(i).First().tendv;
                                                    st34 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T35.Value = _DSDV.Skip(i).First().tendv;
                                                    st35 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T36.Value = _DSDV.Skip(i).First().tendv;
                                                    st36 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T37.Value = _DSDV.Skip(i).First().tendv;
                                                    st37 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T38.Value = _DSDV.Skip(i).First().tendv;
                                                    st38 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T39.Value = _DSDV.Skip(i).First().tendv;
                                                    st39 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T40.Value = _DSDV.Skip(i).First().tendv;
                                                    st40 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T41.Value = _DSDV.Skip(i).First().tendv;
                                                    st41 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T42.Value = _DSDV.Skip(i).First().tendv;
                                                    st42 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T43.Value = _DSDV.Skip(i).First().tendv;
                                                    st43 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T44.Value = _DSDV.Skip(i).First().tendv;
                                                    st44 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T45.Value = _DSDV.Skip(i).First().tendv;
                                                    st45 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                    rep.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T46.Value = _DSDV.Skip(i).First().tendv;
                                                    st46 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;

                                        }
                                    }

                                    #region
                                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth = new int[] { };
                                    int num = 1;
                                    DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                    string[] _tieude = { "STT", "Họ và tên", "Tuổi",st1,st2,st3,st4,st5,st6,st7,st8,st9,st10,st11,st12,st13,st14,st15,st16,st17,st18,st19,st20,st21,st22,st23,st24,st25
                                                               ,st26,st27,st28,st29,st30,st31,st32,st33,st34,st35,st36,st37,st38,st39,st40,st41,st42,st43,st44,st45,st46, "Ký nhận"};
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                    }

                                    ////for (int i = 0; i <= 17; i++) {
                                    ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                    ////}
                                    foreach (var r in _Tamtra.ToList())
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.SL101;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.SL201;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.SL301;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.SL401;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.SL501;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.SL601;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.SL701;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.SL801;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.SL901;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.SL1001;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.SL1101;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.SL1201;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.SL1301;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.SL1401;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.SL1501;
                                        DungChung.Bien.MangHaiChieu[num, 18] = r.SL1601;
                                        DungChung.Bien.MangHaiChieu[num, 19] = r.SL1701;
                                        DungChung.Bien.MangHaiChieu[num, 20] = r.SL1801;
                                        DungChung.Bien.MangHaiChieu[num, 21] = r.SL1901;
                                        DungChung.Bien.MangHaiChieu[num, 22] = r.SL2001;
                                        DungChung.Bien.MangHaiChieu[num, 23] = r.SL2101;
                                        DungChung.Bien.MangHaiChieu[num, 24] = r.SL2201;
                                        DungChung.Bien.MangHaiChieu[num, 25] = r.SL2301;
                                        DungChung.Bien.MangHaiChieu[num, 26] = r.SL2401;
                                        DungChung.Bien.MangHaiChieu[num, 27] = r.SL2501;
                                        DungChung.Bien.MangHaiChieu[num, 28] = r.SL2601;
                                        DungChung.Bien.MangHaiChieu[num, 29] = r.SL2701;
                                        DungChung.Bien.MangHaiChieu[num, 30] = r.SL2801;
                                        DungChung.Bien.MangHaiChieu[num, 31] = r.SL2901;
                                        DungChung.Bien.MangHaiChieu[num, 32] = r.SL3001;
                                        DungChung.Bien.MangHaiChieu[num, 33] = r.SL3101;
                                        DungChung.Bien.MangHaiChieu[num, 34] = r.SL3201;
                                        DungChung.Bien.MangHaiChieu[num, 35] = r.SL3301;
                                        DungChung.Bien.MangHaiChieu[num, 36] = r.SL3401;
                                        DungChung.Bien.MangHaiChieu[num, 37] = r.SL3501;
                                        DungChung.Bien.MangHaiChieu[num, 38] = r.SL3601;
                                        DungChung.Bien.MangHaiChieu[num, 39] = r.SL3701;
                                        DungChung.Bien.MangHaiChieu[num, 40] = r.SL3801;
                                        DungChung.Bien.MangHaiChieu[num, 41] = r.SL3901;
                                        DungChung.Bien.MangHaiChieu[num, 42] = r.SL4001;
                                        DungChung.Bien.MangHaiChieu[num, 43] = r.SL4101;
                                        DungChung.Bien.MangHaiChieu[num, 44] = r.SL4201;
                                        DungChung.Bien.MangHaiChieu[num, 45] = r.SL4301;
                                        DungChung.Bien.MangHaiChieu[num, 46] = r.SL4401;
                                        DungChung.Bien.MangHaiChieu[num, 47] = r.SL4501;
                                        DungChung.Bien.MangHaiChieu[num, 48] = r.SL4601;
                                        DungChung.Bien.MangHaiChieu[num, 49] = "";
                                        num++;
                                    }
                                    #endregion

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                    
                                    #region tạo báo cáo thứ 2
                                    BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02(mau);
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += "bổ xung"; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep1.Kieudon.Value = a;
                                    for (int i = 46; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T1.Value = _DSDV.Skip(i).First().tendv;
                                                    st47 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT01.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T2.Value = _DSDV.Skip(i).First().tendv;
                                                    st48 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT02.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T3.Value = _DSDV.Skip(i).First().tendv;
                                                    st49 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT03.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T4.Value = _DSDV.Skip(i).First().tendv;
                                                    st50 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT04.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T5.Value = _DSDV.Skip(i).First().tendv;
                                                    st51 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT05.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T6.Value = _DSDV.Skip(i).First().tendv;
                                                    st52 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT06.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T7.Value = _DSDV.Skip(i).First().tendv;
                                                    st53 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT07.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T8.Value = _DSDV.Skip(i).First().tendv;
                                                    st54 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT08.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 54:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T9.Value = _DSDV.Skip(i).First().tendv;
                                                    st55 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT09.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 55:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T10.Value = _DSDV.Skip(i).First().tendv;
                                                    st56 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 56:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T11.Value = _DSDV.Skip(i).First().tendv;
                                                    st57 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 57:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T12.Value = _DSDV.Skip(i).First().tendv;
                                                    st58 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 58:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T13.Value = _DSDV.Skip(i).First().tendv;
                                                    st59 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 59:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T14.Value = _DSDV.Skip(i).First().tendv;
                                                    st60 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 60:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T15.Value = _DSDV.Skip(i).First().tendv;
                                                    st61 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 61:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T16.Value = _DSDV.Skip(i).First().tendv;
                                                    st62 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 62:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T17.Value = _DSDV.Skip(i).First().tendv;
                                                    st63 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 63:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T18.Value = _DSDV.Skip(i).First().tendv;
                                                    st64 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 64:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T19.Value = _DSDV.Skip(i).First().tendv;
                                                    st65 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 65:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T20.Value = _DSDV.Skip(i).First().tendv;
                                                    st66 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 66:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T21.Value = _DSDV.Skip(i).First().tendv;
                                                    st67 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 67:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T22.Value = _DSDV.Skip(i).First().tendv;
                                                    st68 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 68:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T23.Value = _DSDV.Skip(i).First().tendv;
                                                    st69 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 69:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T24.Value = _DSDV.Skip(i).First().tendv;
                                                    st70 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 70:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T25.Value = _DSDV.Skip(i).First().tendv;
                                                    st71 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 71:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T26.Value = _DSDV.Skip(i).First().tendv;
                                                    st72 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 72:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T27.Value = _DSDV.Skip(i).First().tendv;
                                                    st73 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 73:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T28.Value = _DSDV.Skip(i).First().tendv;
                                                    st74 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 74:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T29.Value = _DSDV.Skip(i).First().tendv;
                                                    st75 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 75:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T30.Value = _DSDV.Skip(i).First().tendv;
                                                    st76 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 76:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T31.Value = _DSDV.Skip(i).First().tendv;
                                                    st77 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 77:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T32.Value = _DSDV.Skip(i).First().tendv;
                                                    st78 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 78:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T33.Value = _DSDV.Skip(i).First().tendv;
                                                    st79 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 79:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T34.Value = _DSDV.Skip(i).First().tendv;
                                                    st80 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 80:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T35.Value = _DSDV.Skip(i).First().tendv;
                                                    st81 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 81:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T36.Value = _DSDV.Skip(i).First().tendv;
                                                    st82 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 82:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T37.Value = _DSDV.Skip(i).First().tendv;
                                                    st83 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 83:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T38.Value = _DSDV.Skip(i).First().tendv;
                                                    st84 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 84:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T39.Value = _DSDV.Skip(i).First().tendv;
                                                    st85 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 85:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T40.Value = _DSDV.Skip(i).First().tendv;
                                                    st86 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 86:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T41.Value = _DSDV.Skip(i).First().tendv;
                                                    st87 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 87:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T42.Value = _DSDV.Skip(i).First().tendv;
                                                    st88 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 88:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T43.Value = _DSDV.Skip(i).First().tendv;
                                                    st89 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 89:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T44.Value = _DSDV.Skip(i).First().tendv;
                                                    st90 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 90:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T45.Value = _DSDV.Skip(i).First().tendv;
                                                    st91 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 91:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T46.Value = _DSDV.Skip(i).First().tendv;
                                                    st92 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 92:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T47.Value = _DSDV.Skip(i).First().tendv;
                                                    st93 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT47.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 93:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T48.Value = _DSDV.Skip(i).First().tendv;
                                                    st94 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT48.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 94:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T49.Value = _DSDV.Skip(i).First().tendv;
                                                    st95 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT49.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 95:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T50.Value = _DSDV.Skip(i).First().tendv;
                                                    st96 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT50.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 96:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T51.Value = _DSDV.Skip(i).First().tendv;
                                                    st97 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT51.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 97:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T52.Value = _DSDV.Skip(i).First().tendv;
                                                    st98 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT52.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 98:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T53.Value = _DSDV.Skip(i).First().tendv;
                                                    st99 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT53.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;

                                        }
                                    }
                                    #endregion

                                    #region
                                    string[] _arr1 = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth1 = new int[] { };
                                    int num1 = 1;
                                    DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                    string[] _tieude1 = { "STT",st47,st48,st49,st50,st11,st52,st53,st54,st55,st56,st57,st58,st59,st60,st61,st62,st63,st64,st65,st66,st67,st68,st69,st70
                                                                ,st71,st72,st73,st74,st75,st75,st76,st77,st78,st79,st80,st81,st82,st83,st84,st85,st86,st87,st88,st89,st90,st91,st92,st93,st94,st95,st96,st97,st98,st99, "Ký nhận"};
                                    for (int i = 0; i < _tieude1.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude1[i];
                                    }

                                    ////for (int i = 0; i <= 17; i++) {
                                    ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                    ////}
                                    foreach (var r in _Tamtra.ToList())
                                    {
                                        DungChung.Bien.MangHaiChieu[num1, 0] = num1;
                                        DungChung.Bien.MangHaiChieu[num1, 1] = r.SL101;
                                        DungChung.Bien.MangHaiChieu[num1, 2] = r.SL201;
                                        DungChung.Bien.MangHaiChieu[num1, 3] = r.SL301;
                                        DungChung.Bien.MangHaiChieu[num1, 4] = r.SL401;
                                        DungChung.Bien.MangHaiChieu[num1, 5] = r.SL501;
                                        DungChung.Bien.MangHaiChieu[num1, 6] = r.SL601;
                                        DungChung.Bien.MangHaiChieu[num1, 7] = r.SL701;
                                        DungChung.Bien.MangHaiChieu[num1, 8] = r.SL801;
                                        DungChung.Bien.MangHaiChieu[num1, 9] = r.SL901;
                                        DungChung.Bien.MangHaiChieu[num1, 10] = r.SL1001;
                                        DungChung.Bien.MangHaiChieu[num1, 11] = r.SL1101;
                                        DungChung.Bien.MangHaiChieu[num1, 12] = r.SL1201;
                                        DungChung.Bien.MangHaiChieu[num1, 13] = r.SL1301;
                                        DungChung.Bien.MangHaiChieu[num1, 14] = r.SL1401;
                                        DungChung.Bien.MangHaiChieu[num1, 15] = r.SL1501;
                                        DungChung.Bien.MangHaiChieu[num1, 16] = r.SL1601;
                                        DungChung.Bien.MangHaiChieu[num1, 17] = r.SL1701;
                                        DungChung.Bien.MangHaiChieu[num1, 18] = r.SL1801;
                                        DungChung.Bien.MangHaiChieu[num1, 19] = r.SL1901;
                                        DungChung.Bien.MangHaiChieu[num1, 20] = r.SL2001;
                                        DungChung.Bien.MangHaiChieu[num1, 21] = r.SL2101;
                                        DungChung.Bien.MangHaiChieu[num1, 22] = r.SL2201;
                                        DungChung.Bien.MangHaiChieu[num1, 23] = r.SL2301;
                                        DungChung.Bien.MangHaiChieu[num1, 24] = r.SL2401;
                                        DungChung.Bien.MangHaiChieu[num1, 25] = r.SL2501;
                                        DungChung.Bien.MangHaiChieu[num1, 26] = r.SL2601;
                                        DungChung.Bien.MangHaiChieu[num1, 27] = r.SL2701;
                                        DungChung.Bien.MangHaiChieu[num1, 28] = r.SL2801;
                                        DungChung.Bien.MangHaiChieu[num1, 29] = r.SL2901;
                                        DungChung.Bien.MangHaiChieu[num1, 30] = r.SL3001;
                                        DungChung.Bien.MangHaiChieu[num1, 31] = r.SL3101;
                                        DungChung.Bien.MangHaiChieu[num1, 32] = r.SL3201;
                                        DungChung.Bien.MangHaiChieu[num1, 33] = r.SL3301;
                                        DungChung.Bien.MangHaiChieu[num1, 34] = r.SL3401;
                                        DungChung.Bien.MangHaiChieu[num1, 35] = r.SL3501;
                                        DungChung.Bien.MangHaiChieu[num1, 36] = r.SL3601;
                                        DungChung.Bien.MangHaiChieu[num1, 37] = r.SL3701;
                                        DungChung.Bien.MangHaiChieu[num1, 38] = r.SL3801;
                                        DungChung.Bien.MangHaiChieu[num1, 39] = r.SL3901;
                                        DungChung.Bien.MangHaiChieu[num1, 40] = r.SL4001;
                                        DungChung.Bien.MangHaiChieu[num1, 41] = r.SL4101;
                                        DungChung.Bien.MangHaiChieu[num1, 42] = r.SL4201;
                                        DungChung.Bien.MangHaiChieu[num1, 43] = r.SL4301;
                                        DungChung.Bien.MangHaiChieu[num1, 44] = r.SL4401;
                                        DungChung.Bien.MangHaiChieu[num1, 45] = r.SL4501;
                                        DungChung.Bien.MangHaiChieu[num1, 46] = r.SL4601;
                                        DungChung.Bien.MangHaiChieu[num1, 47] = r.SL4701;
                                        DungChung.Bien.MangHaiChieu[num1, 48] = r.SL4801;
                                        DungChung.Bien.MangHaiChieu[num1, 49] = r.SL4901;
                                        DungChung.Bien.MangHaiChieu[num1, 50] = r.SL5001;
                                        DungChung.Bien.MangHaiChieu[num1, 51] = r.SL5101;
                                        DungChung.Bien.MangHaiChieu[num1, 52] = r.SL5201;
                                        DungChung.Bien.MangHaiChieu[num1, 53] = r.SL5301;
                                        DungChung.Bien.MangHaiChieu[num1, 54] = "";
                                        num1++;
                                    }
                                    #endregion

                                    rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep1.DataSource = _Tamtra;
                                    rep1.BindingData();
                                    rep1.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr1, _arrWidth1, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                    frm1.ShowDialog();
                                }
                                #endregion tạo báo cáo A4 2 trang


                            }
                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu quá dài, rút ngắn thời gian lấy báo cáo !");
                        }
                    }
                    else MessageBox.Show("Không có dữ liệu !","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                 #endregion
                 #region Nếu không lấy thuốc ngoài BH
                 else
                {
                    
                    var q1 = (from bn1 in _Data.BenhNhans.Where(p => _noitru == 2 ? true : (_noitru == 0 ? p.NoiTru == 1 : (p.NoiTru == 0 && p.DTNT == true)))
                              join DT in _Data.DThuocs.Where(p => p.KieuDon == _a1 || p.KieuDon == _a2 || p.KieuDon == _a3).Where(p => p.MaKP == _MaKP) on bn1.MaBNhan equals DT.MaBNhan
                              join DTct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on DT.IDDon equals DTct.IDDon
                              join DV in _Data.DichVus.Where(P => P.PLoai == 1) on DTct.MaDV equals DV.MaDV
                              join Ndv in _Data.NhomDVs on DV.IDNhom equals Ndv.IDNhom
                              join vv in _Data.VaoViens on bn1.MaBNhan equals vv.MaBNhan
                              join kp in _Data.BNKBs.Where(p => (radioGroup1.SelectedIndex == 0 || radioGroup1.SelectedIndex == -1) ? true : p.MaKP == _MaKP) on bn1.MaBNhan equals kp.MaBNhan
                              where ((radioGroup1.SelectedIndex == 0 || radioGroup1.SelectedIndex == -1) ? (DT.NgayKe >= Ngaytu && DT.NgayKe <= Ngayden) : (kp.NgayKham >= Ngaytu && kp.NgayKham <= Ngayden))
                              where (DTct.Status != null && (DTct.Status == _Status1 || DTct.Status == _Status2))
                              select new
                              {
                                  DTct.MaDV,
                                  bn1.MaBNhan,
                                  bn1.TenBNhan,
                                  bn1.Tuoi,
                                  DTct.DonVi,
                                  DTct.IDDonct,
                                  DTct.SoLuong,
                                  kp.Giuong,
                                  kp.IDKB
                              }).ToList();
                    var bn2 = (from a in q1
                               join dv in _ldv on a.MaDV equals dv.MaDV
                               group new { a, dv } by new { dv.MaDV, dv.TenDV, dv.TenNhomCT,a.DonVi } into kq
                               select new
                               {
                                   TenDV = kq.Key.TenDV,
                                   MaDV = kq.Key.MaDV,
                                   TenN = kq.Key.TenNhomCT,
                                   Donvi = kq.Key.DonVi,
                                   SL = kq.Select(p => p.a.IDDonct).Count()
                               }).OrderBy(p => p.TenDV).OrderBy(p => p.TenN).ToList();
                    if (bn2.Count > 0)
                    {
                        foreach (var a in bn2)
                        {
                            DSDV themmoi = new DSDV();
                            themmoi.tendv = a.TenDV;
                            themmoi.madv = a.MaDV;
                            themmoi.DonVi = a.Donvi;
                            _DSDV.Add(themmoi);
                        }
                    }

                    var MaBN = (from a in q1
                                join dv in _ldv on a.MaDV equals dv.MaDV
                                group new { a, dv } by new { a.MaBNhan } into kq
                                select new { MaBN = kq.Key.MaBNhan }).ToList();

                    if (MaBN.Count > 0)
                    {
                        foreach (var c in MaBN)
                        {
                            Tamtra themmoi = new Tamtra();
                            var g1 = q1.Where(p => p.MaBNhan == c.MaBN).OrderByDescending(p => p.IDKB).ToList();
                            if (g1.First().Giuong != null)
                            {
                                if (g1.First().Giuong.Contains(";"))
                                {
                                    string[] arrGiuong = g1.First().Giuong.Split(';');
                                    themmoi.Giuong = arrGiuong.LastOrDefault();
                                }
                                else
                                {
                                    themmoi.Giuong = g1.First().Giuong;
                                }
                            }
                            themmoi.mabn = c.MaBN;
                            _Tamtra.Add(themmoi);
                        }
                    }
                    
                    var bn = (from a in q1
                              join dv in _ldv on a.MaDV equals dv.MaDV
                              group new { a, dv } by new { dv.MaDV, a.TenBNhan, a.MaBNhan, a.Tuoi } into kq
                              select new
                              {
                                  kq.Key.Tuoi,
                                  Mabenhnhan = kq.Key.MaBNhan,
                                  MaDV = kq.Key.MaDV,
                                  SoLuong = kq.Sum(p => p.a.SoLuong),
                                  TenBN = kq.Key.TenBNhan,
                              }).ToList();
                    if (bn.Count > 0)
                    {
                        #region tạo code báo cáo
                        foreach (var n in _Tamtra)
                        {
                            foreach (var m in bn)
                            {
                                if (n.mabn == m.Mabenhnhan)
                                {
                                    if (m.SoLuong != null && m.SoLuong != 0)
                                    {
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            if (m.MaDV == _DSDV.Skip(i).First().madv)
                                            {
                                                switch (i)
                                                {
                                                    case 0:
                                                        n.SL101 = m.SoLuong.ToString();
                                                        break;
                                                    case 1:
                                                        n.SL201 = m.SoLuong.ToString();
                                                        break;
                                                    case 2:
                                                        n.SL301 = m.SoLuong.ToString();
                                                        break;
                                                    case 3:
                                                        n.SL401 = m.SoLuong.ToString();
                                                        break;
                                                    case 4:
                                                        n.SL501 = m.SoLuong.ToString();
                                                        break;
                                                    case 5:
                                                        n.SL601 = m.SoLuong.ToString();
                                                        break;
                                                    case 6:
                                                        n.SL701 = m.SoLuong.ToString();
                                                        break;
                                                    case 7:
                                                        n.SL801 = m.SoLuong.ToString();
                                                        break;
                                                    case 8:
                                                        n.SL901 = m.SoLuong.ToString();
                                                        break;
                                                    case 9:
                                                        n.SL1001 = m.SoLuong.ToString();
                                                        break;
                                                    case 10:
                                                        n.SL1101 = m.SoLuong.ToString();
                                                        break;
                                                    case 11:
                                                        n.SL1201 = m.SoLuong.ToString();
                                                        break;
                                                    case 12:
                                                        n.SL1301 = m.SoLuong.ToString();
                                                        break;
                                                    case 13:
                                                        n.SL1401 = m.SoLuong.ToString();
                                                        break;
                                                    case 14:
                                                        n.SL1501 = m.SoLuong.ToString();
                                                        break;
                                                    case 15:
                                                        n.SL1601 = m.SoLuong.ToString();
                                                        break;
                                                    case 16:
                                                        n.SL1701 = m.SoLuong.ToString();
                                                        break;
                                                    case 17:
                                                        n.SL1801 = m.SoLuong.ToString();
                                                        break;
                                                    case 18:
                                                        n.SL1901 = m.SoLuong.ToString();
                                                        break;
                                                    case 19:
                                                        n.SL2001 = m.SoLuong.ToString();
                                                        break;
                                                    case 20:
                                                        n.SL2101 = m.SoLuong.ToString();
                                                        break;
                                                    case 21:
                                                        n.SL2201 = m.SoLuong.ToString();
                                                        break;
                                                    case 22:
                                                        n.SL2301 = m.SoLuong.ToString();
                                                        break;
                                                    case 23:
                                                        n.SL2401 = m.SoLuong.ToString();
                                                        break;
                                                    case 24:
                                                        n.SL2501 = m.SoLuong.ToString();
                                                        break;
                                                    case 25:
                                                        n.SL2601 = m.SoLuong.ToString();
                                                        break;
                                                    case 26:
                                                        n.SL2701 = m.SoLuong.ToString();
                                                        break;
                                                    case 27:
                                                        n.SL2801 = m.SoLuong.ToString();
                                                        break;
                                                    case 28:
                                                        n.SL2901 = m.SoLuong.ToString();
                                                        break;
                                                    case 29:
                                                        n.SL3001 = m.SoLuong.ToString();
                                                        break;
                                                    case 30:
                                                        n.SL3101 = m.SoLuong.ToString();
                                                        break;
                                                    case 31:
                                                        n.SL3201 = m.SoLuong.ToString();
                                                        break;
                                                    case 32:
                                                        n.SL3301 = m.SoLuong.ToString();
                                                        break;
                                                    case 33:
                                                        n.SL3401 = m.SoLuong.ToString();
                                                        break;
                                                    case 34:
                                                        n.SL3501 = m.SoLuong.ToString();
                                                        break;
                                                    case 35:
                                                        n.SL3601 = m.SoLuong.ToString();
                                                        break;
                                                    case 36:
                                                        n.SL3701 = m.SoLuong.ToString();
                                                        break;
                                                    case 37:
                                                        n.SL3801 = m.SoLuong.ToString();
                                                        break;
                                                    case 38:
                                                        n.SL3901 = m.SoLuong.ToString();
                                                        break;
                                                    case 39:
                                                        n.SL4001 = m.SoLuong.ToString();
                                                        break;
                                                    case 40:
                                                        n.SL4101 = m.SoLuong.ToString();
                                                        break;
                                                    case 41:
                                                        n.SL4201 = m.SoLuong.ToString();
                                                        break;
                                                    case 42:
                                                        n.SL4301 = m.SoLuong.ToString();
                                                        break;
                                                    case 43:
                                                        n.SL4401 = m.SoLuong.ToString();
                                                        break;
                                                    case 44:
                                                        n.SL4501 = m.SoLuong.ToString();
                                                        break;
                                                    case 45:
                                                        n.SL4601 = m.SoLuong.ToString();
                                                        break;
                                                    case 46:
                                                        n.SL4701 = m.SoLuong.ToString();
                                                        break;
                                                    case 47:
                                                        n.SL4801 = m.SoLuong.ToString();
                                                        break;
                                                    case 48:
                                                        n.SL4901 = m.SoLuong.ToString();
                                                        break;
                                                    case 49:
                                                        n.SL5001 = m.SoLuong.ToString();
                                                        break;
                                                    case 50:
                                                        n.SL5101 = m.SoLuong.ToString();
                                                        break;
                                                    case 51:
                                                        n.SL5201 = m.SoLuong.ToString();
                                                        break;
                                                    case 52:
                                                        n.SL5301 = m.SoLuong.ToString();
                                                        break;
                                                    case 53:
                                                        n.SL5401 = m.SoLuong.ToString();
                                                        break;
                                                    case 54:
                                                        n.SL5501 = m.SoLuong.ToString();
                                                        break;
                                                    case 55:
                                                        n.SL5601 = m.SoLuong.ToString();
                                                        break;
                                                    case 56:
                                                        n.SL5701 = m.SoLuong.ToString();
                                                        break;
                                                    case 57:
                                                        n.SL5801 = m.SoLuong.ToString();
                                                        break;
                                                    case 58:
                                                        n.SL5901 = m.SoLuong.ToString();
                                                        break;
                                                    case 59:
                                                        n.SL6001 = m.SoLuong.ToString();
                                                        break;
                                                    case 60:
                                                        n.SL6101 = m.SoLuong.ToString();
                                                        break;
                                                    case 61:
                                                        n.SL6201 = m.SoLuong.ToString();
                                                        break;
                                                    case 62:
                                                        n.SL6301 = m.SoLuong.ToString();
                                                        break;
                                                    case 63:
                                                        n.SL6401 = m.SoLuong.ToString();
                                                        break;
                                                    case 64:
                                                        n.SL6501 = m.SoLuong.ToString();
                                                        break;
                                                    case 65:
                                                        n.SL6601 = m.SoLuong.ToString();
                                                        break;
                                                    case 66:
                                                        n.SL6701 = m.SoLuong.ToString();
                                                        break;
                                                    case 67:
                                                        n.SL6801 = m.SoLuong.ToString();
                                                        break;
                                                    case 68:
                                                        n.SL6901 = m.SoLuong.ToString();
                                                        break;
                                                    case 69:
                                                        n.SL7001 = m.SoLuong.ToString();
                                                        break;
                                                    case 70:
                                                        n.SL7101 = m.SoLuong.ToString();
                                                        break;
                                                    case 71:
                                                        n.SL7201 = m.SoLuong.ToString();
                                                        break;
                                                    case 72:
                                                        n.SL7301 = m.SoLuong.ToString();
                                                        break;
                                                    case 73:
                                                        n.SL7401 = m.SoLuong.ToString();
                                                        break;
                                                    case 74:
                                                        n.SL7501 = m.SoLuong.ToString();
                                                        break;
                                                    case 75:
                                                        n.SL7601 = m.SoLuong.ToString();
                                                        break;
                                                    case 76:
                                                        n.SL7701 = m.SoLuong.ToString();
                                                        break;
                                                    case 77:
                                                        n.SL7801 = m.SoLuong.ToString();
                                                        break;
                                                    case 78:
                                                        n.SL7901 = m.SoLuong.ToString();
                                                        break;
                                                    case 79:
                                                        n.SL8001 = m.SoLuong.ToString();
                                                        break;
                                                    case 80:
                                                        n.SL8101 = m.SoLuong.ToString();
                                                        break;
                                                    case 81:
                                                        n.SL8201 = m.SoLuong.ToString();
                                                        break;
                                                    case 82:
                                                        n.SL8301 = m.SoLuong.ToString();
                                                        break;
                                                    case 83:
                                                        n.SL8401 = m.SoLuong.ToString();
                                                        break;
                                                    case 84:
                                                        n.SL8501 = m.SoLuong.ToString();
                                                        break;
                                                    case 85:
                                                        n.SL8601 = m.SoLuong.ToString();
                                                        break;
                                                    case 86:
                                                        n.SL8701 = m.SoLuong.ToString();
                                                        break;
                                                    case 87:
                                                        n.SL8801 = m.SoLuong.ToString();
                                                        break;
                                                    case 88:
                                                        n.SL8901 = m.SoLuong.ToString();
                                                        break;
                                                    case 89:
                                                        n.SL9001 = m.SoLuong.ToString();
                                                        break;
                                                    case 90:
                                                        n.SL9101 = m.SoLuong.ToString();
                                                        break;
                                                    case 91:
                                                        n.SL9201 = m.SoLuong.ToString();
                                                        break;
                                                    case 92:
                                                        n.SL9301 = m.SoLuong.ToString();
                                                        break;
                                                    case 93:
                                                        n.SL9401 = m.SoLuong.ToString();
                                                        break;
                                                    case 94:
                                                        n.SL9501 = m.SoLuong.ToString();
                                                        break;
                                                    case 95:
                                                        n.SL9601 = m.SoLuong.ToString();
                                                        break;
                                                    case 96:
                                                        n.SL9701 = m.SoLuong.ToString();
                                                        break;
                                                    case 97:
                                                        n.SL9801 = m.SoLuong.ToString();
                                                        break;
                                                    case 98:
                                                        n.SL9901 = m.SoLuong.ToString();
                                                        break;
                                                    case 99:
                                                        n.SL10001 = m.SoLuong.ToString();
                                                        break;
                                                    case 100:
                                                        n.SL10101 = m.SoLuong.ToString();
                                                        break;
                                                    case 101:
                                                        n.SL10201 = m.SoLuong.ToString();
                                                        break;
                                                    case 102:
                                                        n.SL10301 = m.SoLuong.ToString();
                                                        break;
                                                    case 103:
                                                        n.SL10401 = m.SoLuong.ToString();
                                                        break;
                                                    case 104:
                                                        n.SL10501 = m.SoLuong.ToString();
                                                        break;
                                                    case 105:
                                                        n.SL10601 = m.SoLuong.ToString();
                                                        break;
                                                    case 106:
                                                        n.SL10701 = m.SoLuong.ToString();
                                                        break;
                                                    case 107:
                                                        n.SL10801 = m.SoLuong.ToString();
                                                        break;
                                                    case 108:
                                                        n.SL10901 = m.SoLuong.ToString();
                                                        break;
                                                    case 109:
                                                        n.SL11001 = m.SoLuong.ToString();
                                                        break;
                                                    case 110:
                                                        n.SL11101 = m.SoLuong.ToString();
                                                        break;
                                                    case 111:
                                                        n.SL11201 = m.SoLuong.ToString();
                                                        break;
                                                    case 112:
                                                        n.SL11301 = m.SoLuong.ToString();
                                                        break;
                                                    case 113:
                                                        n.SL11401 = m.SoLuong.ToString();
                                                        break;
                                                    case 114:
                                                        n.SL11501 = m.SoLuong.ToString();
                                                        break;
                                                    case 115:
                                                        n.SL11601 = m.SoLuong.ToString();
                                                        break;
                                                    case 116:
                                                        n.SL11701 = m.SoLuong.ToString();
                                                        break;
                                                    case 117:
                                                        n.SL11801 = m.SoLuong.ToString();
                                                        break;
                                                    case 118:
                                                        n.SL11901 = m.SoLuong.ToString();
                                                        break;
                                                    case 119:
                                                        n.SL12001 = m.SoLuong.ToString();
                                                        break;
                                                    case 120:
                                                        n.SL12101 = m.SoLuong.ToString();
                                                        break;
                                                    case 121:
                                                        n.SL12201 = m.SoLuong.ToString();
                                                        break;
                                                    case 122:
                                                        n.SL12301 = m.SoLuong.ToString();
                                                        break;
                                                    case 123:
                                                        n.SL12401 = m.SoLuong.ToString();
                                                        break;
                                                    case 124:
                                                        n.SL12501 = m.SoLuong.ToString();
                                                        break;
                                                    case 125:
                                                        n.SL12601 = m.SoLuong.ToString();
                                                        break;
                                                    case 126:
                                                        n.SL12701 = m.SoLuong.ToString();
                                                        break;
                                                    case 127:
                                                        n.SL12801 = m.SoLuong.ToString();
                                                        break;
                                                    case 128:
                                                        n.SL12901 = m.SoLuong.ToString();
                                                        break;
                                                    case 129:
                                                        n.SL13001 = m.SoLuong.ToString();
                                                        break;
                                                    case 130:
                                                        n.SL13101 = m.SoLuong.ToString();
                                                        break;
                                                    case 131:
                                                        n.SL13201 = m.SoLuong.ToString();
                                                        break;
                                                    case 132:
                                                        n.SL13301 = m.SoLuong.ToString();
                                                        break;
                                                    case 133:
                                                        n.SL13401 = m.SoLuong.ToString();
                                                        break;
                                                    case 134:
                                                        n.SL13501 = m.SoLuong.ToString();
                                                        break;
                                                    case 135:
                                                        n.SL13601 = m.SoLuong.ToString();
                                                        break;
                                                    case 136:
                                                        n.SL13701 = m.SoLuong.ToString();
                                                        break;
                                                    case 137:
                                                        n.SL13801 = m.SoLuong.ToString();
                                                        break;
                                                    case 138:
                                                        n.SL13901 = m.SoLuong.ToString();
                                                        break;
                                                    case 139:
                                                        n.SL14001 = m.SoLuong.ToString();
                                                        break;
                                                    case 140:
                                                        n.SL14101 = m.SoLuong.ToString();
                                                        break;
                                                    case 141:
                                                        n.SL14201 = m.SoLuong.ToString();
                                                        break;
                                                    case 142:
                                                        n.SL14301 = m.SoLuong.ToString();
                                                        break;
                                                    case 143:
                                                        n.SL14401 = m.SoLuong.ToString();
                                                        break;
                                                    case 144:
                                                        n.SL4501 = m.SoLuong.ToString();
                                                        break;
                                                    case 145:
                                                        n.SL14601 = m.SoLuong.ToString();
                                                        break;
                                                    case 146:
                                                        n.SL14701 = m.SoLuong.ToString();
                                                        break;
                                                    case 147:
                                                        n.SL14801 = m.SoLuong.ToString();
                                                        break;
                                                    case 148:
                                                        n.SL4901 = m.SoLuong.ToString();
                                                        break;
                                                    case 149:
                                                        n.SL15001 = m.SoLuong.ToString();
                                                        break;
                                                    case 150:
                                                        n.SL15101 = m.SoLuong.ToString();
                                                        break;
                                                    case 151:
                                                        n.SL15201 = m.SoLuong.ToString();
                                                        break;
                                                    case 152:
                                                        n.SL15301 = m.SoLuong.ToString();
                                                        break;
                                                    case 153:
                                                        n.SL15401 = m.SoLuong.ToString();
                                                        break;
                                                    case 154:
                                                        n.SL15501 = m.SoLuong.ToString();
                                                        break;
                                                    case 155:
                                                        n.SL15601 = m.SoLuong.ToString();
                                                        break;
                                                    case 156:
                                                        n.SL15701 = m.SoLuong.ToString();
                                                        break;
                                                    case 157:
                                                        n.SL15801 = m.SoLuong.ToString();
                                                        break;
                                                    case 158:
                                                        n.SL15901 = m.SoLuong.ToString();
                                                        break;
                                                    case 159:
                                                        n.SL16001 = m.SoLuong.ToString();
                                                        break;
                                                    case 160:
                                                        n.SL16101 = m.SoLuong.ToString();
                                                        break;
                                                    case 161:
                                                        n.SL16201 = m.SoLuong.ToString();
                                                        break;
                                                    case 162:
                                                        n.SL16301 = m.SoLuong.ToString();
                                                        break;
                                                    case 163:
                                                        n.SL16401 = m.SoLuong.ToString();
                                                        break;
                                                    case 164:
                                                        n.SL16501 = m.SoLuong.ToString();
                                                        break;
                                                    case 165:
                                                        n.SL16601 = m.SoLuong.ToString();
                                                        break;
                                                    case 166:
                                                        n.SL16701 = m.SoLuong.ToString();
                                                        break;
                                                    case 167:
                                                        n.SL16801 = m.SoLuong.ToString();
                                                        break;
                                                    case 168:
                                                        n.SL16901 = m.SoLuong.ToString();
                                                        break;
                                                    case 169:
                                                        n.SL17001 = m.SoLuong.ToString();
                                                        break;
                                                }
                                                n.tenbn = m.TenBN;
                                                n.Tuoi = m.Tuoi.Value;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion tạo code báo cáo
                        if (_DSDV.Count < 153)
                        {
                            if (_DSDV.Count >= 100)
                            {
                                DialogResult _result = MessageBox.Show("Mẫu A4 không đủ để hiển thị, bạn có muốn in mẫu A3 không?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                {
                                    #region tạo báo cáo a3
                                    if (_DSDV.Count > 54)
                                    {
                                        BaoCao.Rep_TamTraThuocA3 rep = new BaoCao.Rep_TamTraThuocA3(mau);
                                        BaoCao.Rep_TamTraThuocA302 rep1 = new BaoCao.Rep_TamTraThuocA302(mau);
                                        BaoCao.Rep_TamTraThuocA303 rep2 = new BaoCao.Rep_TamTraThuocA303(mau);
                                        MessageBox.Show("Mẫu báo cáo in thành 2 phần thoát phần 1 để lấy BC phần 2");
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st1 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT01.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st2 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT02.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st3 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT03.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st4 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT04.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st5 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT05.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st6 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT06.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st7 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT07.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st8 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT08.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st9 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT09.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st10 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st11 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st12 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st13 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st14 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st15 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st16 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st17 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st18 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st19 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st20 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st21 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st22 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st23 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st24 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st25 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st26 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st27 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st28 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st29 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st30 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st31 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st32 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st33 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st34 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st35 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st36 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st37 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st38 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st39 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st40 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st41 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st42 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st43 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st44 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st45 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                        rep.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st46 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 46:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T47.Value = _DSDV.Skip(i).First().tendv;
                                                        st47 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT47.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 47:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T48.Value = _DSDV.Skip(i).First().tendv;
                                                        st48 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT48.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 48:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T49.Value = _DSDV.Skip(i).First().tendv;
                                                        st49 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT49.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 49:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T50.Value = _DSDV.Skip(i).First().tendv;
                                                        st50 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT50.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 50:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T51.Value = _DSDV.Skip(i).First().tendv;
                                                        st51 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT51.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 51:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T52.Value = _DSDV.Skip(i).First().tendv;
                                                        st52 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT52.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 52:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T53.Value = _DSDV.Skip(i).First().tendv;
                                                        st53 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT53.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 53:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T54.Value = _DSDV.Skip(i).First().tendv;
                                                        st54 = _DSDV.Skip(i).First().tendv;
                                                        rep.DVT54.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 54:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st55 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT01.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 55:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st56 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT02.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 56:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st57 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT03.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 57:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st58 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT04.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 58:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st59 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT05.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 59:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st60 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT06.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 60:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st61 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT07.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 61:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st62 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT08.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 62:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st63 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT09.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 63:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st64 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 64:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st65 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 65:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st66 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 66:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st67 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 67:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st68 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 68:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st69 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 69:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st70 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 70:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st71 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 71:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st72 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 72:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st73 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 73:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st74 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 74:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st75 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 75:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st76 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 76:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st77 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 77:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st78 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 78:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st79 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 79:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st80 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 80:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st81 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 81:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st82 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 82:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st83 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 83:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st84 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 84:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st85 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 85:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st86 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 86:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st87 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 87:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st88 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 88:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st89 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 89:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st90 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 90:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st91 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 91:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st92 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 92:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st93 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 93:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st94 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 94:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st95 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 95:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st96 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 96:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st97 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 97:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st98 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 98:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st99 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 99:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st100 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 100:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T47.Value = _DSDV.Skip(i).First().tendv;
                                                        st101 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT47.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 101:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T48.Value = _DSDV.Skip(i).First().tendv;
                                                        st102 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT48.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 102:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T49.Value = _DSDV.Skip(i).First().tendv;
                                                        st103 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT49.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 103:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T50.Value = _DSDV.Skip(i).First().tendv;
                                                        st104 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT50.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 104:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T51.Value = _DSDV.Skip(i).First().tendv;
                                                        st105 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT51.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 105:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T52.Value = _DSDV.Skip(i).First().tendv;
                                                        st106 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT52.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 106:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T53.Value = _DSDV.Skip(i).First().tendv;
                                                        st107 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT53.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 107:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T54.Value = _DSDV.Skip(i).First().tendv;
                                                        st108 = _DSDV.Skip(i).First().tendv;
                                                        rep1.DVT54.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 108:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st109 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT01.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 109:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st110 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT02.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 110:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st111 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT03.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 111:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st112 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT04.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 112:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st113 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT05.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 113:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st114 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT06.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 114:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st115 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT07.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 115:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st116 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT08.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 116:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st117 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT09.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 117:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st118 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 118:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st119 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 119:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st120 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 120:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st121 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 121:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st122 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 122:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st123 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 123:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st124 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 124:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st125 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 125:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st126 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 126:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st127 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 127:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st128 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 128:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st129 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 129:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st130 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 130:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st131 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 131:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st132 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 132:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st133 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 133:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st134 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 134:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st135 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 135:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st136 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 136:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st137 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 137:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st138 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 138:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st139 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 139:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st140 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 140:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st141 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 141:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st142 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 142:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st143 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 143:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st144 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 144:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st145 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 145:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st146 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 146:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st147 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 147:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st148 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 148:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st149 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 149:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st150 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 150:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st151 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 151:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st152 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 152:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st153 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 153:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st154 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 155:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T47.Value = _DSDV.Skip(i).First().tendv;
                                                        st156 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT47.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 156:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T48.Value = _DSDV.Skip(i).First().tendv;
                                                        st157 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT48.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 157:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T49.Value = _DSDV.Skip(i).First().tendv;
                                                        st158 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT49.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 158:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T50.Value = _DSDV.Skip(i).First().tendv;
                                                        st159 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT50.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 159:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T51.Value = _DSDV.Skip(i).First().tendv;
                                                        st160 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT51.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 161:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T52.Value = _DSDV.Skip(i).First().tendv;
                                                        st162 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT52.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 162:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T53.Value = _DSDV.Skip(i).First().tendv;
                                                        st163 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT53.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                                case 163:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T54.Value = _DSDV.Skip(i).First().tendv;
                                                        st164 = _DSDV.Skip(i).First().tendv;
                                                        rep2.DVT54.Value = _DSDV.Skip(i).First().DonVi;
                                                    }
                                                    break;
                                            }
                                        }

                                        #region
                                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                        int[] _arrWidth = new int[] { };
                                        int num = 1;
                                        DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                        string[] _tieude = { "STT", "Họ và tên", "Tuổi",st1,st2,st3,st4,st5,st6,st7,st8,st9,st10,st11,st12,st13,st14,st15,st16,st17,st18,st19,st20,st21,st22,st23,st24,st25
                                                               ,st26,st27,st28,st29,st30,st31,st32,st33,st34,st35,st36,st37,st38,st39,st40,st41,st42,st43,st44,st45,st46,st47,st48,st49,st50,st51,st52,st53,st54, "Ký nhận"};
                                        for (int i = 0; i < _tieude.Length; i++)
                                        {
                                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                        }

                                        ////for (int i = 0; i <= 17; i++) {
                                        ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                        ////}
                                        foreach (var r in _Tamtra.ToList())
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                                            DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.SL101;
                                            DungChung.Bien.MangHaiChieu[num, 4] = r.SL201;
                                            DungChung.Bien.MangHaiChieu[num, 5] = r.SL301;
                                            DungChung.Bien.MangHaiChieu[num, 6] = r.SL401;
                                            DungChung.Bien.MangHaiChieu[num, 7] = r.SL501;
                                            DungChung.Bien.MangHaiChieu[num, 8] = r.SL601;
                                            DungChung.Bien.MangHaiChieu[num, 9] = r.SL701;
                                            DungChung.Bien.MangHaiChieu[num, 10] = r.SL801;
                                            DungChung.Bien.MangHaiChieu[num, 11] = r.SL901;
                                            DungChung.Bien.MangHaiChieu[num, 12] = r.SL1001;
                                            DungChung.Bien.MangHaiChieu[num, 13] = r.SL1101;
                                            DungChung.Bien.MangHaiChieu[num, 14] = r.SL1201;
                                            DungChung.Bien.MangHaiChieu[num, 15] = r.SL1301;
                                            DungChung.Bien.MangHaiChieu[num, 16] = r.SL1401;
                                            DungChung.Bien.MangHaiChieu[num, 17] = r.SL1501;
                                            DungChung.Bien.MangHaiChieu[num, 18] = r.SL1601;
                                            DungChung.Bien.MangHaiChieu[num, 19] = r.SL1701;
                                            DungChung.Bien.MangHaiChieu[num, 20] = r.SL1801;
                                            DungChung.Bien.MangHaiChieu[num, 21] = r.SL1901;
                                            DungChung.Bien.MangHaiChieu[num, 22] = r.SL2001;
                                            DungChung.Bien.MangHaiChieu[num, 23] = r.SL2101;
                                            DungChung.Bien.MangHaiChieu[num, 24] = r.SL2201;
                                            DungChung.Bien.MangHaiChieu[num, 25] = r.SL2301;
                                            DungChung.Bien.MangHaiChieu[num, 26] = r.SL2401;
                                            DungChung.Bien.MangHaiChieu[num, 27] = r.SL2501;
                                            DungChung.Bien.MangHaiChieu[num, 28] = r.SL2601;
                                            DungChung.Bien.MangHaiChieu[num, 29] = r.SL2701;
                                            DungChung.Bien.MangHaiChieu[num, 30] = r.SL2801;
                                            DungChung.Bien.MangHaiChieu[num, 31] = r.SL2901;
                                            DungChung.Bien.MangHaiChieu[num, 32] = r.SL3001;
                                            DungChung.Bien.MangHaiChieu[num, 33] = r.SL3101;
                                            DungChung.Bien.MangHaiChieu[num, 34] = r.SL3201;
                                            DungChung.Bien.MangHaiChieu[num, 35] = r.SL3301;
                                            DungChung.Bien.MangHaiChieu[num, 36] = r.SL3401;
                                            DungChung.Bien.MangHaiChieu[num, 37] = r.SL3501;
                                            DungChung.Bien.MangHaiChieu[num, 38] = r.SL3601;
                                            DungChung.Bien.MangHaiChieu[num, 39] = r.SL3701;
                                            DungChung.Bien.MangHaiChieu[num, 40] = r.SL3801;
                                            DungChung.Bien.MangHaiChieu[num, 41] = r.SL3901;
                                            DungChung.Bien.MangHaiChieu[num, 42] = r.SL4001;
                                            DungChung.Bien.MangHaiChieu[num, 43] = r.SL4101;
                                            DungChung.Bien.MangHaiChieu[num, 44] = r.SL4201;
                                            DungChung.Bien.MangHaiChieu[num, 45] = r.SL4301;
                                            DungChung.Bien.MangHaiChieu[num, 46] = r.SL4401;
                                            DungChung.Bien.MangHaiChieu[num, 47] = r.SL4501;
                                            DungChung.Bien.MangHaiChieu[num, 48] = r.SL4601;
                                            DungChung.Bien.MangHaiChieu[num, 49] = r.SL4701;
                                            DungChung.Bien.MangHaiChieu[num, 50] = r.SL4801;
                                            DungChung.Bien.MangHaiChieu[num, 51] = r.SL4901;
                                            DungChung.Bien.MangHaiChieu[num, 52] = r.SL5001;
                                            DungChung.Bien.MangHaiChieu[num, 53] = r.SL5101;
                                            DungChung.Bien.MangHaiChieu[num, 54] = r.SL5201;
                                            DungChung.Bien.MangHaiChieu[num, 55] = r.SL5301;
                                            DungChung.Bien.MangHaiChieu[num, 56] = r.SL5401;
                                            DungChung.Bien.MangHaiChieu[num, 57] = "";
                                            num++;
                                        }
                                        #endregion

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.KhoaPhong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                        rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep1.KhoaPhong.Value = LupKhoaPhong.Text;
                                        rep1.DataSource = _Tamtra;
                                        rep1.BindingData();
                                        rep1.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                        frm1.ShowDialog();

                                        if (_DSDV.Count > 108)
                                        {
                                            rep2.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                            rep2.KhoaPhong.Value = LupKhoaPhong.Text;
                                            rep2.DataSource = _Tamtra;
                                            rep2.BindingData();
                                            rep2.CreateDocument();
                                            //rep.DataMember = "Table";
                                            frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                            frm2.prcIN.PrintingSystem = rep2.PrintingSystem;
                                            frm2.ShowDialog();
                                        }
                                    }
                                    #endregion tạo báo cáo a3
                                }
                                else
                                {

                                    #region tạo báo cáo A4 1 trang
                                    if (_DSDV.Count <= 46)
                                    {
                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc(mau);
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st1 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st2 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st3 = _DSDV.Skip(i).First().tendv; ;
                                                    }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st4 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st5 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st6 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st7 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st8 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st9 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st10 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st11 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st12 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st13 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st14 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st15 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st16 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st17 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st18 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st19 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st20 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st21 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st22 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st23 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st24 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st25 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st26 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st27 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st28 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st29 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st30 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st31 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st32 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st33 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st34 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st35 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st36 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st37 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st38 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st39 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st40 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st41 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st42 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st43 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st44 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st45 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st46 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;

                                            }
                                        }

                                        #region
                                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                        int[] _arrWidth = new int[] { };
                                        int num = 1;
                                        DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                        string[] _tieude = { "STT", "Họ và tên", "Tuổi",st1,st2,st3,st4,st5,st6,st7,st8,st9,st10,st11,st12,st13,st14,st15,st16,st17,st18,st19,st20,st21,st22,st23,st24,st25
                                                               ,st26,st27,st28,st29,st30,st31,st32,st33,st34,st35,st36,st37,st38,st39,st40,st41,st42,st43,st44,st45,st46, "Ký nhận"};
                                        for (int i = 0; i < _tieude.Length; i++)
                                        {
                                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i]; ;
                                        }

                                        ////for (int i = 0; i <= 17; i++) {
                                        ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                        ////}
                                        foreach (var r in _Tamtra.ToList())
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                                            DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.SL101;
                                            DungChung.Bien.MangHaiChieu[num, 4] = r.SL201;
                                            DungChung.Bien.MangHaiChieu[num, 5] = r.SL301;
                                            DungChung.Bien.MangHaiChieu[num, 6] = r.SL401;
                                            DungChung.Bien.MangHaiChieu[num, 7] = r.SL501;
                                            DungChung.Bien.MangHaiChieu[num, 8] = r.SL601;
                                            DungChung.Bien.MangHaiChieu[num, 9] = r.SL701;
                                            DungChung.Bien.MangHaiChieu[num, 10] = r.SL801;
                                            DungChung.Bien.MangHaiChieu[num, 11] = r.SL901;
                                            DungChung.Bien.MangHaiChieu[num, 12] = r.SL1001;
                                            DungChung.Bien.MangHaiChieu[num, 13] = r.SL1101;
                                            DungChung.Bien.MangHaiChieu[num, 14] = r.SL1201;
                                            DungChung.Bien.MangHaiChieu[num, 15] = r.SL1301;
                                            DungChung.Bien.MangHaiChieu[num, 16] = r.SL1401;
                                            DungChung.Bien.MangHaiChieu[num, 17] = r.SL1501;
                                            DungChung.Bien.MangHaiChieu[num, 18] = r.SL1601;
                                            DungChung.Bien.MangHaiChieu[num, 19] = r.SL1701;
                                            DungChung.Bien.MangHaiChieu[num, 20] = r.SL1801;
                                            DungChung.Bien.MangHaiChieu[num, 21] = r.SL1901;
                                            DungChung.Bien.MangHaiChieu[num, 22] = r.SL2001;
                                            DungChung.Bien.MangHaiChieu[num, 23] = r.SL2101;
                                            DungChung.Bien.MangHaiChieu[num, 24] = r.SL2201;
                                            DungChung.Bien.MangHaiChieu[num, 25] = r.SL2301;
                                            DungChung.Bien.MangHaiChieu[num, 26] = r.SL2401;
                                            DungChung.Bien.MangHaiChieu[num, 27] = r.SL2501;
                                            DungChung.Bien.MangHaiChieu[num, 28] = r.SL2601;
                                            DungChung.Bien.MangHaiChieu[num, 29] = r.SL2701;
                                            DungChung.Bien.MangHaiChieu[num, 30] = r.SL2801;
                                            DungChung.Bien.MangHaiChieu[num, 31] = r.SL2901;
                                            DungChung.Bien.MangHaiChieu[num, 32] = r.SL3001;
                                            DungChung.Bien.MangHaiChieu[num, 33] = r.SL3101;
                                            DungChung.Bien.MangHaiChieu[num, 34] = r.SL3201;
                                            DungChung.Bien.MangHaiChieu[num, 35] = r.SL3301;
                                            DungChung.Bien.MangHaiChieu[num, 36] = r.SL3401;
                                            DungChung.Bien.MangHaiChieu[num, 37] = r.SL3501;
                                            DungChung.Bien.MangHaiChieu[num, 38] = r.SL3601;
                                            DungChung.Bien.MangHaiChieu[num, 39] = r.SL3701;
                                            DungChung.Bien.MangHaiChieu[num, 40] = r.SL3801;
                                            DungChung.Bien.MangHaiChieu[num, 41] = r.SL3901;
                                            DungChung.Bien.MangHaiChieu[num, 42] = r.SL4001;
                                            DungChung.Bien.MangHaiChieu[num, 43] = r.SL4101;
                                            DungChung.Bien.MangHaiChieu[num, 44] = r.SL4201;
                                            DungChung.Bien.MangHaiChieu[num, 45] = r.SL4301;
                                            DungChung.Bien.MangHaiChieu[num, 46] = r.SL4401;
                                            DungChung.Bien.MangHaiChieu[num, 47] = r.SL4501;
                                            DungChung.Bien.MangHaiChieu[num, 48] = r.SL4601;
                                            DungChung.Bien.MangHaiChieu[num, 49] = "";
                                            num++;
                                        }
                                        #endregion

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                    }
                                    #endregion tạo báo cáo a4
                                    #region tạo báo cáo A4 2 trang
                                    if (_DSDV.Count > 46)
                                    {

                                        BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc(mau);
                                        for (int i = 0; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st1 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 1:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st2 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 2:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st3 = _DSDV.Skip(i).First().tendv; ;
                                                    }
                                                    break;
                                                case 3:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st4 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 4:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st5 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 5:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st6 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 6:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st7 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 7:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st8 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 8:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st9 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 9:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st10 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 10:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st11 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 11:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st12 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 12:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st13 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 13:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st14 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 14:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st15 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 15:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st16 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 16:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st17 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 17:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st18 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 18:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st19 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 19:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st20 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 20:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st21 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 21:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st22 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 22:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st23 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 23:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st24 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 24:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st25 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 25:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st26 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 26:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st27 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 27:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st28 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 28:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st29 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 29:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st30 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 30:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st31 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 31:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st32 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 32:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st33 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 33:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st34 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 34:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st35 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 35:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st36 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 36:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st37 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 37:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st38 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 38:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st39 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 39:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st40 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 40:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st41 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 41:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st42 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 42:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st43 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 43:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st44 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 44:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st45 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 45:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st46 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;

                                            }
                                        }

                                        #region
                                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                        int[] _arrWidth = new int[] { };
                                        int num = 1;
                                        DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                        string[] _tieude = { "STT", "Họ và tên", "Tuổi",st1,st2,st3,st4,st5,st6,st7,st8,st9,st10,st11,st12,st13,st14,st15,st16,st17,st18,st19,st20,st21,st22,st23,st24,st25
                                                               ,st26,st27,st28,st29,st30,st31,st32,st33,st34,st35,st36,st37,st38,st39,st40,st41,st42,st43,st44,st45,st46, "Ký nhận"};
                                        for (int i = 0; i < _tieude.Length; i++)
                                        {
                                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                        }

                                        ////for (int i = 0; i <= 17; i++) {
                                        ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                        ////}
                                        foreach (var r in _Tamtra.ToList())
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                                            DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.SL101;
                                            DungChung.Bien.MangHaiChieu[num, 4] = r.SL201;
                                            DungChung.Bien.MangHaiChieu[num, 5] = r.SL301;
                                            DungChung.Bien.MangHaiChieu[num, 6] = r.SL401;
                                            DungChung.Bien.MangHaiChieu[num, 7] = r.SL501;
                                            DungChung.Bien.MangHaiChieu[num, 8] = r.SL601;
                                            DungChung.Bien.MangHaiChieu[num, 9] = r.SL701;
                                            DungChung.Bien.MangHaiChieu[num, 10] = r.SL801;
                                            DungChung.Bien.MangHaiChieu[num, 11] = r.SL901;
                                            DungChung.Bien.MangHaiChieu[num, 12] = r.SL1001;
                                            DungChung.Bien.MangHaiChieu[num, 13] = r.SL1101;
                                            DungChung.Bien.MangHaiChieu[num, 14] = r.SL1201;
                                            DungChung.Bien.MangHaiChieu[num, 15] = r.SL1301;
                                            DungChung.Bien.MangHaiChieu[num, 16] = r.SL1401;
                                            DungChung.Bien.MangHaiChieu[num, 17] = r.SL1501;
                                            DungChung.Bien.MangHaiChieu[num, 18] = r.SL1601;
                                            DungChung.Bien.MangHaiChieu[num, 19] = r.SL1701;
                                            DungChung.Bien.MangHaiChieu[num, 20] = r.SL1801;
                                            DungChung.Bien.MangHaiChieu[num, 21] = r.SL1901;
                                            DungChung.Bien.MangHaiChieu[num, 22] = r.SL2001;
                                            DungChung.Bien.MangHaiChieu[num, 23] = r.SL2101;
                                            DungChung.Bien.MangHaiChieu[num, 24] = r.SL2201;
                                            DungChung.Bien.MangHaiChieu[num, 25] = r.SL2301;
                                            DungChung.Bien.MangHaiChieu[num, 26] = r.SL2401;
                                            DungChung.Bien.MangHaiChieu[num, 27] = r.SL2501;
                                            DungChung.Bien.MangHaiChieu[num, 28] = r.SL2601;
                                            DungChung.Bien.MangHaiChieu[num, 29] = r.SL2701;
                                            DungChung.Bien.MangHaiChieu[num, 30] = r.SL2801;
                                            DungChung.Bien.MangHaiChieu[num, 31] = r.SL2901;
                                            DungChung.Bien.MangHaiChieu[num, 32] = r.SL3001;
                                            DungChung.Bien.MangHaiChieu[num, 33] = r.SL3101;
                                            DungChung.Bien.MangHaiChieu[num, 34] = r.SL3201;
                                            DungChung.Bien.MangHaiChieu[num, 35] = r.SL3301;
                                            DungChung.Bien.MangHaiChieu[num, 36] = r.SL3401;
                                            DungChung.Bien.MangHaiChieu[num, 37] = r.SL3501;
                                            DungChung.Bien.MangHaiChieu[num, 38] = r.SL3601;
                                            DungChung.Bien.MangHaiChieu[num, 39] = r.SL3701;
                                            DungChung.Bien.MangHaiChieu[num, 40] = r.SL3801;
                                            DungChung.Bien.MangHaiChieu[num, 41] = r.SL3901;
                                            DungChung.Bien.MangHaiChieu[num, 42] = r.SL4001;
                                            DungChung.Bien.MangHaiChieu[num, 43] = r.SL4101;
                                            DungChung.Bien.MangHaiChieu[num, 44] = r.SL4201;
                                            DungChung.Bien.MangHaiChieu[num, 45] = r.SL4301;
                                            DungChung.Bien.MangHaiChieu[num, 46] = r.SL4401;
                                            DungChung.Bien.MangHaiChieu[num, 47] = r.SL4501;
                                            DungChung.Bien.MangHaiChieu[num, 48] = r.SL4601;
                                            DungChung.Bien.MangHaiChieu[num, 49] = "";
                                            num++;
                                        }
                                        #endregion

                                        rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep.DataSource = _Tamtra;
                                        rep.BindingData();
                                        rep.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                        frm.ShowDialog();

                                        // tạo báo cáo thứ 2

                                        BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02(mau);
                                        string a = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a += "bổ xung"; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " thường xuyên ";
                                            }
                                            else
                                            { a = a + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a += " trả thuốc ";
                                            }
                                            else
                                            { a = a + " - " + " trả thuốc "; }
                                        }
                                        rep1.Kieudon.Value = a;
                                        for (int i = 46; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 46:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st47 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 47:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st48 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 48:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st49 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 49:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st50 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 50:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st51 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 51:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st52 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 52:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st53 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 53:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st54 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 54:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st55 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 55:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st56 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 56:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st57 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 57:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st58 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 58:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st59 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 59:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st60 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 60:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st61 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 61:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st62 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 62:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st63 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 63:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st64 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 64:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st65 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 65:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st66 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 66:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st67 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 67:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st68 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 68:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st69 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 69:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st70 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 70:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st71 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 71:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st72 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 72:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st73 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 73:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st74 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 74:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st75 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 75:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st76 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 76:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st77 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 77:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st78 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 78:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st79 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 79:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st80 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 80:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st81 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 81:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st82 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 82:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st83 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 83:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st84 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 84:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st85 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 85:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st86 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 86:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st87 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 87:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st88 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 88:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st89 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 89:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st90 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 90:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st91 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 91:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st92 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 92:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T47.Value = _DSDV.Skip(i).First().tendv;
                                                        st93 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 93:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T48.Value = _DSDV.Skip(i).First().tendv;
                                                        st94 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 94:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T49.Value = _DSDV.Skip(i).First().tendv;
                                                        st95 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 95:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T50.Value = _DSDV.Skip(i).First().tendv;
                                                        st96 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 96:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T51.Value = _DSDV.Skip(i).First().tendv;
                                                        st97 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 97:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T52.Value = _DSDV.Skip(i).First().tendv;
                                                        st98 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 98:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep1.T53.Value = _DSDV.Skip(i).First().tendv;
                                                        st99 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;

                                            }
                                        }

                                        #region
                                        string[] _arr1 = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                        int[] _arrWidth1 = new int[] { };
                                        int num1 = 1;
                                        DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                        string[] _tieude1 = { "STT",st47,st48,st49,st50,st11,st52,st53,st54,st55,st56,st57,st58,st59,st60,st61,st62,st63,st64,st65,st66,st67,st68,st69,st70
                                                                ,st71,st72,st73,st74,st75,st75,st76,st77,st78,st79,st80,st81,st82,st83,st84,st85,st86,st87,st88,st89,st90,st91,st92,st93,st94,st95,st96,st97,st98,st99, "Ký nhận"};
                                        for (int i = 0; i < _tieude1.Length; i++)
                                        {
                                            DungChung.Bien.MangHaiChieu[0, i] = _tieude1[i];
                                        }

                                        ////for (int i = 0; i <= 17; i++) {
                                        ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                        ////}
                                        foreach (var r in _Tamtra.ToList())
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                                            DungChung.Bien.MangHaiChieu[num, 1] = r.SL101;
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.SL201;
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.SL301;
                                            DungChung.Bien.MangHaiChieu[num, 4] = r.SL401;
                                            DungChung.Bien.MangHaiChieu[num, 5] = r.SL501;
                                            DungChung.Bien.MangHaiChieu[num, 6] = r.SL601;
                                            DungChung.Bien.MangHaiChieu[num, 7] = r.SL701;
                                            DungChung.Bien.MangHaiChieu[num, 8] = r.SL801;
                                            DungChung.Bien.MangHaiChieu[num, 9] = r.SL901;
                                            DungChung.Bien.MangHaiChieu[num, 10] = r.SL1001;
                                            DungChung.Bien.MangHaiChieu[num, 11] = r.SL1101;
                                            DungChung.Bien.MangHaiChieu[num, 12] = r.SL1201;
                                            DungChung.Bien.MangHaiChieu[num, 13] = r.SL1301;
                                            DungChung.Bien.MangHaiChieu[num, 14] = r.SL1401;
                                            DungChung.Bien.MangHaiChieu[num, 15] = r.SL1501;
                                            DungChung.Bien.MangHaiChieu[num, 16] = r.SL1601;
                                            DungChung.Bien.MangHaiChieu[num, 17] = r.SL1701;
                                            DungChung.Bien.MangHaiChieu[num, 18] = r.SL1801;
                                            DungChung.Bien.MangHaiChieu[num, 19] = r.SL1901;
                                            DungChung.Bien.MangHaiChieu[num, 20] = r.SL2001;
                                            DungChung.Bien.MangHaiChieu[num, 21] = r.SL2101;
                                            DungChung.Bien.MangHaiChieu[num, 22] = r.SL2201;
                                            DungChung.Bien.MangHaiChieu[num, 23] = r.SL2301;
                                            DungChung.Bien.MangHaiChieu[num, 24] = r.SL2401;
                                            DungChung.Bien.MangHaiChieu[num, 25] = r.SL2501;
                                            DungChung.Bien.MangHaiChieu[num, 26] = r.SL2601;
                                            DungChung.Bien.MangHaiChieu[num, 27] = r.SL2701;
                                            DungChung.Bien.MangHaiChieu[num, 28] = r.SL2801;
                                            DungChung.Bien.MangHaiChieu[num, 29] = r.SL2901;
                                            DungChung.Bien.MangHaiChieu[num, 30] = r.SL3001;
                                            DungChung.Bien.MangHaiChieu[num, 31] = r.SL3101;
                                            DungChung.Bien.MangHaiChieu[num, 32] = r.SL3201;
                                            DungChung.Bien.MangHaiChieu[num, 33] = r.SL3301;
                                            DungChung.Bien.MangHaiChieu[num, 34] = r.SL3401;
                                            DungChung.Bien.MangHaiChieu[num, 35] = r.SL3501;
                                            DungChung.Bien.MangHaiChieu[num, 36] = r.SL3601;
                                            DungChung.Bien.MangHaiChieu[num, 37] = r.SL3701;
                                            DungChung.Bien.MangHaiChieu[num, 38] = r.SL3801;
                                            DungChung.Bien.MangHaiChieu[num, 39] = r.SL3901;
                                            DungChung.Bien.MangHaiChieu[num, 40] = r.SL4001;
                                            DungChung.Bien.MangHaiChieu[num, 41] = r.SL4101;
                                            DungChung.Bien.MangHaiChieu[num, 42] = r.SL4201;
                                            DungChung.Bien.MangHaiChieu[num, 43] = r.SL4301;
                                            DungChung.Bien.MangHaiChieu[num, 44] = r.SL4401;
                                            DungChung.Bien.MangHaiChieu[num, 45] = r.SL4501;
                                            DungChung.Bien.MangHaiChieu[num, 46] = r.SL4601;
                                            DungChung.Bien.MangHaiChieu[num, 47] = r.SL4701;
                                            DungChung.Bien.MangHaiChieu[num, 48] = r.SL4801;
                                            DungChung.Bien.MangHaiChieu[num, 49] = r.SL4901;
                                            DungChung.Bien.MangHaiChieu[num, 50] = r.SL5001;
                                            DungChung.Bien.MangHaiChieu[num, 51] = r.SL5101;
                                            DungChung.Bien.MangHaiChieu[num, 52] = r.SL5201;
                                            DungChung.Bien.MangHaiChieu[num, 53] = r.SL5301;
                                            DungChung.Bien.MangHaiChieu[num, 54] = "";
                                            num++;
                                        }
                                        #endregion

                                        rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep1.DataSource = _Tamtra;
                                        rep1.BindingData();
                                        rep1.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr1, _arrWidth1, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                        frm1.ShowDialog();
                                        //trang 3
                                        BaoCao.Rep_Tamtrathuoc02 rep2 = new BaoCao.Rep_Tamtrathuoc02(mau);
                                        string a2 = "Kiểu đơn: ";
                                        if (chkBoxung.Checked == true)
                                        { a2 += "bổ xung"; }
                                        if (chkThuongxuyen.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a2))
                                            {
                                                a2 += " thường xuyên ";
                                            }
                                            else
                                            { a2 = a2 + " - " + " thường xuyên "; }
                                        }
                                        if (chkTrathuoc.Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(a))
                                            {
                                                a2 += " trả thuốc ";
                                            }
                                            else
                                            { a2 = a2 + " - " + " trả thuốc "; }
                                        }
                                        rep2.Kieudon.Value = a2;
                                        for (int i = 99; i < _DSDV.Count; i++)
                                        {
                                            switch (i)
                                            {
                                                case 99:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T1.Value = _DSDV.Skip(i).First().tendv;
                                                        st100 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 100:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T2.Value = _DSDV.Skip(i).First().tendv;
                                                        st101 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 101:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T3.Value = _DSDV.Skip(i).First().tendv;
                                                        st102 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 102:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T4.Value = _DSDV.Skip(i).First().tendv;
                                                        st103 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 103:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T5.Value = _DSDV.Skip(i).First().tendv;
                                                        st104 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 104:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T6.Value = _DSDV.Skip(i).First().tendv;
                                                        st105 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 105:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T7.Value = _DSDV.Skip(i).First().tendv;
                                                        st106 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 106:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T8.Value = _DSDV.Skip(i).First().tendv;
                                                        st107 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 107:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T9.Value = _DSDV.Skip(i).First().tendv;
                                                        st108 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 108:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T10.Value = _DSDV.Skip(i).First().tendv;
                                                        st109 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 109:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T11.Value = _DSDV.Skip(i).First().tendv;
                                                        st110 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 110:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T12.Value = _DSDV.Skip(i).First().tendv;
                                                        st111 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 111:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T13.Value = _DSDV.Skip(i).First().tendv;
                                                        st112 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 112:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T14.Value = _DSDV.Skip(i).First().tendv;
                                                        st113 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 113:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T15.Value = _DSDV.Skip(i).First().tendv;
                                                        st114 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 114:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T16.Value = _DSDV.Skip(i).First().tendv;
                                                        st115 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 115:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T17.Value = _DSDV.Skip(i).First().tendv;
                                                        st116 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 116:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T18.Value = _DSDV.Skip(i).First().tendv;
                                                        st117 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 117:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T19.Value = _DSDV.Skip(i).First().tendv;
                                                        st118 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 118:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T20.Value = _DSDV.Skip(i).First().tendv;
                                                        st119 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 119:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T21.Value = _DSDV.Skip(i).First().tendv;
                                                        st120 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 120:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T22.Value = _DSDV.Skip(i).First().tendv;
                                                        st121 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 121:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T23.Value = _DSDV.Skip(i).First().tendv;
                                                        st122 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 122:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T24.Value = _DSDV.Skip(i).First().tendv;
                                                        st123 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 123:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T25.Value = _DSDV.Skip(i).First().tendv;
                                                        st124 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 124:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T26.Value = _DSDV.Skip(i).First().tendv;
                                                        st125 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 125:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T27.Value = _DSDV.Skip(i).First().tendv;
                                                        st126 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 126:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T28.Value = _DSDV.Skip(i).First().tendv;
                                                        st127 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 127:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T29.Value = _DSDV.Skip(i).First().tendv;
                                                        st128 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 128:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T30.Value = _DSDV.Skip(i).First().tendv;
                                                        st129 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 129:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T31.Value = _DSDV.Skip(i).First().tendv;
                                                        st130 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 130:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T32.Value = _DSDV.Skip(i).First().tendv;
                                                        st131 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 131:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T33.Value = _DSDV.Skip(i).First().tendv;
                                                        st132 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 132:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T34.Value = _DSDV.Skip(i).First().tendv;
                                                        st133 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 133:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T35.Value = _DSDV.Skip(i).First().tendv;
                                                        st134 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 134:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T36.Value = _DSDV.Skip(i).First().tendv;
                                                        st135 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 135:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T37.Value = _DSDV.Skip(i).First().tendv;
                                                        st136 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 136:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T38.Value = _DSDV.Skip(i).First().tendv;
                                                        st137 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 137:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T39.Value = _DSDV.Skip(i).First().tendv;
                                                        st138 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 138:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T40.Value = _DSDV.Skip(i).First().tendv;
                                                        st139 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 139:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T41.Value = _DSDV.Skip(i).First().tendv;
                                                        st140 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 140:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T42.Value = _DSDV.Skip(i).First().tendv;
                                                        st141 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 141:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T43.Value = _DSDV.Skip(i).First().tendv;
                                                        st142 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 142:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T44.Value = _DSDV.Skip(i).First().tendv;
                                                        st143 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 143:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T45.Value = _DSDV.Skip(i).First().tendv;
                                                        st144 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 144:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T46.Value = _DSDV.Skip(i).First().tendv;
                                                        st145 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 145:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T47.Value = _DSDV.Skip(i).First().tendv;
                                                        st146 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 146:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T48.Value = _DSDV.Skip(i).First().tendv;
                                                        st147 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 147:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T49.Value = _DSDV.Skip(i).First().tendv;
                                                        st148 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 148:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T50.Value = _DSDV.Skip(i).First().tendv;
                                                        st149 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 149:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T51.Value = _DSDV.Skip(i).First().tendv;
                                                        st150 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 150:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T52.Value = _DSDV.Skip(i).First().tendv;
                                                        st151 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;
                                                case 151:
                                                    if (_DSDV.Skip(i).First().tendv != null)
                                                    {
                                                        rep2.T53.Value = _DSDV.Skip(i).First().tendv;
                                                        st152 = _DSDV.Skip(i).First().tendv;
                                                    }
                                                    break;

                                            }
                                        }

                                        #region
                                        string[] _arr2 = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                        int[] _arrWidth2 = new int[] { };
                                        int num2 = 1;
                                        DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                        string[] _tieude2 = { "STT",st100,st101,st102,st103,st104,st105,st106,st107,st108,st109,st110,st111,st112,st113,st114,st115,st116,st117,st118,st119,st120,st121,st122,st123
                                                                ,st124,st125,st126,st127,st128,st129,st130,st131,st132,st133,st134,st135,st136,st137,st138,st139,st140,st141,st142,st143,st144,st145,st146,st147,st148,st149,st151,st152,st153,st154, "Ký nhận"};
                                        for (int i = 0; i < _tieude1.Length; i++)
                                        {
                                            DungChung.Bien.MangHaiChieu[0, i] = _tieude2[i];
                                        }

                                        ////for (int i = 0; i <= 17; i++) {
                                        ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                        ////}
                                        foreach (var r in _Tamtra.ToList())
                                        {
                                            DungChung.Bien.MangHaiChieu[num2, 0] = num2;
                                            DungChung.Bien.MangHaiChieu[num2, 1] = r.SL101;
                                            DungChung.Bien.MangHaiChieu[num2, 2] = r.SL201;
                                            DungChung.Bien.MangHaiChieu[num2, 3] = r.SL301;
                                            DungChung.Bien.MangHaiChieu[num2, 4] = r.SL401;
                                            DungChung.Bien.MangHaiChieu[num2, 5] = r.SL501;
                                            DungChung.Bien.MangHaiChieu[num2, 6] = r.SL601;
                                            DungChung.Bien.MangHaiChieu[num2, 7] = r.SL701;
                                            DungChung.Bien.MangHaiChieu[num2, 8] = r.SL801;
                                            DungChung.Bien.MangHaiChieu[num2, 9] = r.SL901;
                                            DungChung.Bien.MangHaiChieu[num2, 10] = r.SL1001;
                                            DungChung.Bien.MangHaiChieu[num2, 11] = r.SL1101;
                                            DungChung.Bien.MangHaiChieu[num2, 12] = r.SL1201;
                                            DungChung.Bien.MangHaiChieu[num2, 13] = r.SL1301;
                                            DungChung.Bien.MangHaiChieu[num2, 14] = r.SL1401;
                                            DungChung.Bien.MangHaiChieu[num2, 15] = r.SL1501;
                                            DungChung.Bien.MangHaiChieu[num2, 16] = r.SL1601;
                                            DungChung.Bien.MangHaiChieu[num2, 17] = r.SL1701;
                                            DungChung.Bien.MangHaiChieu[num2, 18] = r.SL1801;
                                            DungChung.Bien.MangHaiChieu[num2, 19] = r.SL1901;
                                            DungChung.Bien.MangHaiChieu[num2, 20] = r.SL2001;
                                            DungChung.Bien.MangHaiChieu[num2, 21] = r.SL2101;
                                            DungChung.Bien.MangHaiChieu[num2, 22] = r.SL2201;
                                            DungChung.Bien.MangHaiChieu[num2, 23] = r.SL2301;
                                            DungChung.Bien.MangHaiChieu[num2, 24] = r.SL2401;
                                            DungChung.Bien.MangHaiChieu[num2, 25] = r.SL2501;
                                            DungChung.Bien.MangHaiChieu[num2, 26] = r.SL2601;
                                            DungChung.Bien.MangHaiChieu[num2, 27] = r.SL2701;
                                            DungChung.Bien.MangHaiChieu[num2, 28] = r.SL2801;
                                            DungChung.Bien.MangHaiChieu[num2, 29] = r.SL2901;
                                            DungChung.Bien.MangHaiChieu[num2, 30] = r.SL3001;
                                            DungChung.Bien.MangHaiChieu[num2, 31] = r.SL3101;
                                            DungChung.Bien.MangHaiChieu[num2, 32] = r.SL3201;
                                            DungChung.Bien.MangHaiChieu[num2, 33] = r.SL3301;
                                            DungChung.Bien.MangHaiChieu[num2, 34] = r.SL3401;
                                            DungChung.Bien.MangHaiChieu[num2, 35] = r.SL3501;
                                            DungChung.Bien.MangHaiChieu[num2, 36] = r.SL3601;
                                            DungChung.Bien.MangHaiChieu[num2, 37] = r.SL3701;
                                            DungChung.Bien.MangHaiChieu[num2, 38] = r.SL3801;
                                            DungChung.Bien.MangHaiChieu[num2, 39] = r.SL3901;
                                            DungChung.Bien.MangHaiChieu[num2, 40] = r.SL4001;
                                            DungChung.Bien.MangHaiChieu[num2, 41] = r.SL4101;
                                            DungChung.Bien.MangHaiChieu[num2, 42] = r.SL4201;
                                            DungChung.Bien.MangHaiChieu[num2, 43] = r.SL4301;
                                            DungChung.Bien.MangHaiChieu[num2, 44] = r.SL4401;
                                            DungChung.Bien.MangHaiChieu[num2, 45] = r.SL4501;
                                            DungChung.Bien.MangHaiChieu[num2, 46] = r.SL4601;
                                            DungChung.Bien.MangHaiChieu[num2, 47] = r.SL4701;
                                            DungChung.Bien.MangHaiChieu[num2, 48] = r.SL4801;
                                            DungChung.Bien.MangHaiChieu[num2, 49] = r.SL4901;
                                            DungChung.Bien.MangHaiChieu[num2, 50] = r.SL5001;
                                            DungChung.Bien.MangHaiChieu[num2, 51] = r.SL5101;
                                            DungChung.Bien.MangHaiChieu[num2, 52] = r.SL5201;
                                            DungChung.Bien.MangHaiChieu[num2, 53] = r.SL5301;
                                            DungChung.Bien.MangHaiChieu[num2, 54] = "";
                                            num2++;
                                        }
                                        #endregion

                                        rep2.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                        rep2.Khoaphong.Value = LupKhoaPhong.Text;
                                        rep2.DataSource = _Tamtra;
                                        rep2.BindingData2();
                                        rep2.CreateDocument();
                                        //rep.DataMember = "Table";
                                        frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr2, _arrWidth2, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                        frm2.prcIN.PrintingSystem = rep2.PrintingSystem;
                                        frm2.ShowDialog();
                                    }
                                    #endregion tạo báo cáo A4 2 trang
                                }
                            }
                            else
                            {

                                #region tạo báo cáo A4 1 trang
                                if (_DSDV.Count <= 46)
                                {
                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc(mau);
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T1.Value = _DSDV.Skip(i).First().tendv;
                                                    st1 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT1.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T2.Value = _DSDV.Skip(i).First().tendv;
                                                    st2 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT2.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T3.Value = _DSDV.Skip(i).First().tendv;
                                                    st3 = _DSDV.Skip(i).First().tendv; ;
                                                    rep.DVT3.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T4.Value = _DSDV.Skip(i).First().tendv;
                                                    st4 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT4.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T5.Value = _DSDV.Skip(i).First().tendv;
                                                    st5 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT5.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T6.Value = _DSDV.Skip(i).First().tendv;
                                                    st6 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT6.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T7.Value = _DSDV.Skip(i).First().tendv;
                                                    st7 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT7.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T8.Value = _DSDV.Skip(i).First().tendv;
                                                    st8 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT8.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T9.Value = _DSDV.Skip(i).First().tendv;
                                                    st9 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT9.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T10.Value = _DSDV.Skip(i).First().tendv;
                                                    st10 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T11.Value = _DSDV.Skip(i).First().tendv;
                                                    st11 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T12.Value = _DSDV.Skip(i).First().tendv;
                                                    st12 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                    st13 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T14.Value = _DSDV.Skip(i).First().tendv;
                                                    st14 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T15.Value = _DSDV.Skip(i).First().tendv;
                                                    st15 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T16.Value = _DSDV.Skip(i).First().tendv;
                                                    st16 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T17.Value = _DSDV.Skip(i).First().tendv;
                                                    st17 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T18.Value = _DSDV.Skip(i).First().tendv;
                                                    st18 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T19.Value = _DSDV.Skip(i).First().tendv;
                                                    st19 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T20.Value = _DSDV.Skip(i).First().tendv;
                                                    st20 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T21.Value = _DSDV.Skip(i).First().tendv;
                                                    st21 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T22.Value = _DSDV.Skip(i).First().tendv;
                                                    st22 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T23.Value = _DSDV.Skip(i).First().tendv;
                                                    st23 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T24.Value = _DSDV.Skip(i).First().tendv;
                                                    st24 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T25.Value = _DSDV.Skip(i).First().tendv;
                                                    st25 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T26.Value = _DSDV.Skip(i).First().tendv;
                                                    st26 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T27.Value = _DSDV.Skip(i).First().tendv;
                                                    st27 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T28.Value = _DSDV.Skip(i).First().tendv;
                                                    st28 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T29.Value = _DSDV.Skip(i).First().tendv;
                                                    st29 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T30.Value = _DSDV.Skip(i).First().tendv;
                                                    st30 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T31.Value = _DSDV.Skip(i).First().tendv;
                                                    st31 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T32.Value = _DSDV.Skip(i).First().tendv;
                                                    st32 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T33.Value = _DSDV.Skip(i).First().tendv;
                                                    st33 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T34.Value = _DSDV.Skip(i).First().tendv;
                                                    st34 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T35.Value = _DSDV.Skip(i).First().tendv;
                                                    st35 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T36.Value = _DSDV.Skip(i).First().tendv;
                                                    st36 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T37.Value = _DSDV.Skip(i).First().tendv;
                                                    st37 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T38.Value = _DSDV.Skip(i).First().tendv;
                                                    st38 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T39.Value = _DSDV.Skip(i).First().tendv;
                                                    st39 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T40.Value = _DSDV.Skip(i).First().tendv;
                                                    st40 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T41.Value = _DSDV.Skip(i).First().tendv;
                                                    st41 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T42.Value = _DSDV.Skip(i).First().tendv;
                                                    st42 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T43.Value = _DSDV.Skip(i).First().tendv;
                                                    st43 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T44.Value = _DSDV.Skip(i).First().tendv;
                                                    st44 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T45.Value = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                    st45 = _DSDV.Skip(i).First().tendv;
                                                }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T46.Value = _DSDV.Skip(i).First().tendv;
                                                    st46 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;

                                        }
                                    }

                                    #region
                                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth = new int[] { };
                                    int num = 1;
                                    DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                    string[] _tieude = { "STT", "Họ và tên", "Tuổi",st1,st2,st3,st4,st5,st6,st7,st8,st9,st10,st11,st12,st13,st14,st15,st16,st17,st18,st19,st20,st21,st22,st23,st24,st25
                                                               ,st26,st27,st28,st29,st30,st31,st32,st33,st34,st35,st36,st37,st38,st39,st40,st41,st42,st43,st44,st45,st46, "Ký nhận"};
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i]; ;
                                    }

                                    ////for (int i = 0; i <= 17; i++) {
                                    ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                    ////}
                                    foreach (var r in _Tamtra.ToList())
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.SL101;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.SL201;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.SL301;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.SL401;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.SL501;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.SL601;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.SL701;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.SL801;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.SL901;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.SL1001;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.SL1101;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.SL1201;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.SL1301;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.SL1401;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.SL1501;
                                        DungChung.Bien.MangHaiChieu[num, 18] = r.SL1601;
                                        DungChung.Bien.MangHaiChieu[num, 19] = r.SL1701;
                                        DungChung.Bien.MangHaiChieu[num, 20] = r.SL1801;
                                        DungChung.Bien.MangHaiChieu[num, 21] = r.SL1901;
                                        DungChung.Bien.MangHaiChieu[num, 22] = r.SL2001;
                                        DungChung.Bien.MangHaiChieu[num, 23] = r.SL2101;
                                        DungChung.Bien.MangHaiChieu[num, 24] = r.SL2201;
                                        DungChung.Bien.MangHaiChieu[num, 25] = r.SL2301;
                                        DungChung.Bien.MangHaiChieu[num, 26] = r.SL2401;
                                        DungChung.Bien.MangHaiChieu[num, 27] = r.SL2501;
                                        DungChung.Bien.MangHaiChieu[num, 28] = r.SL2601;
                                        DungChung.Bien.MangHaiChieu[num, 29] = r.SL2701;
                                        DungChung.Bien.MangHaiChieu[num, 30] = r.SL2801;
                                        DungChung.Bien.MangHaiChieu[num, 31] = r.SL2901;
                                        DungChung.Bien.MangHaiChieu[num, 32] = r.SL3001;
                                        DungChung.Bien.MangHaiChieu[num, 33] = r.SL3101;
                                        DungChung.Bien.MangHaiChieu[num, 34] = r.SL3201;
                                        DungChung.Bien.MangHaiChieu[num, 35] = r.SL3301;
                                        DungChung.Bien.MangHaiChieu[num, 36] = r.SL3401;
                                        DungChung.Bien.MangHaiChieu[num, 37] = r.SL3501;
                                        DungChung.Bien.MangHaiChieu[num, 38] = r.SL3601;
                                        DungChung.Bien.MangHaiChieu[num, 39] = r.SL3701;
                                        DungChung.Bien.MangHaiChieu[num, 40] = r.SL3801;
                                        DungChung.Bien.MangHaiChieu[num, 41] = r.SL3901;
                                        DungChung.Bien.MangHaiChieu[num, 42] = r.SL4001;
                                        DungChung.Bien.MangHaiChieu[num, 43] = r.SL4101;
                                        DungChung.Bien.MangHaiChieu[num, 44] = r.SL4201;
                                        DungChung.Bien.MangHaiChieu[num, 45] = r.SL4301;
                                        DungChung.Bien.MangHaiChieu[num, 46] = r.SL4401;
                                        DungChung.Bien.MangHaiChieu[num, 47] = r.SL4501;
                                        DungChung.Bien.MangHaiChieu[num, 48] = r.SL4601;
                                        DungChung.Bien.MangHaiChieu[num, 49] = "";
                                        num++;
                                    }
                                    #endregion

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();

                                }
                                #endregion

                                #region tạo báo cáo A4 2 trang
                                if (_DSDV.Count > 46)
                                {

                                    BaoCao.Rep_TamTraThuoc rep = new BaoCao.Rep_TamTraThuoc(mau);
                                    for (int i = 0; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T1.Value = _DSDV.Skip(i).First().tendv;
                                                    st1 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT1.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 1:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T2.Value = _DSDV.Skip(i).First().tendv;
                                                    st2 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT2.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 2:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T3.Value = _DSDV.Skip(i).First().tendv;
                                                    st3 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT3.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 3:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T4.Value = _DSDV.Skip(i).First().tendv;
                                                    st4 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT4.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 4:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T5.Value = _DSDV.Skip(i).First().tendv;
                                                    st5 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT5.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 5:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T6.Value = _DSDV.Skip(i).First().tendv;
                                                    st6 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT6.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 6:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T7.Value = _DSDV.Skip(i).First().tendv;
                                                    st7 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT7.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 7:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T8.Value = _DSDV.Skip(i).First().tendv;
                                                    st8 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT8.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 8:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T9.Value = _DSDV.Skip(i).First().tendv;
                                                    st9 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT9.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 9:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T10.Value = _DSDV.Skip(i).First().tendv;
                                                    st10 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 10:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T11.Value = _DSDV.Skip(i).First().tendv;
                                                    st11 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 11:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T12.Value = _DSDV.Skip(i).First().tendv;
                                                    st12 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 12:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T13.Value = _DSDV.Skip(i).First().tendv;
                                                    st13 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 13:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T14.Value = _DSDV.Skip(i).First().tendv;
                                                    st14 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 14:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T15.Value = _DSDV.Skip(i).First().tendv;
                                                    st15 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 15:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T16.Value = _DSDV.Skip(i).First().tendv;
                                                    st16 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 16:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T17.Value = _DSDV.Skip(i).First().tendv;
                                                    st17 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 17:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T18.Value = _DSDV.Skip(i).First().tendv;
                                                    st18 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 18:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T19.Value = _DSDV.Skip(i).First().tendv;
                                                    st19 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 19:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T20.Value = _DSDV.Skip(i).First().tendv;
                                                    st20 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 20:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T21.Value = _DSDV.Skip(i).First().tendv;
                                                    st21 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 21:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T22.Value = _DSDV.Skip(i).First().tendv;
                                                    st22 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 22:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T23.Value = _DSDV.Skip(i).First().tendv;
                                                    st23 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 23:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T24.Value = _DSDV.Skip(i).First().tendv;
                                                    st24 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 24:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T25.Value = _DSDV.Skip(i).First().tendv;
                                                    st25 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 25:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T26.Value = _DSDV.Skip(i).First().tendv;
                                                    st26 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 26:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T27.Value = _DSDV.Skip(i).First().tendv;
                                                    st27 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 27:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T28.Value = _DSDV.Skip(i).First().tendv;
                                                    st28 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 28:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T29.Value = _DSDV.Skip(i).First().tendv;
                                                    st29 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 29:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T30.Value = _DSDV.Skip(i).First().tendv;
                                                    st30 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 30:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T31.Value = _DSDV.Skip(i).First().tendv;
                                                    st31 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 31:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T32.Value = _DSDV.Skip(i).First().tendv;
                                                    st32 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 32:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T33.Value = _DSDV.Skip(i).First().tendv;
                                                    st33 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 33:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T34.Value = _DSDV.Skip(i).First().tendv;
                                                    st34 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 34:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T35.Value = _DSDV.Skip(i).First().tendv;
                                                    st35 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 35:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T36.Value = _DSDV.Skip(i).First().tendv;
                                                    st36 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 36:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T37.Value = _DSDV.Skip(i).First().tendv;
                                                    st37 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 37:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T38.Value = _DSDV.Skip(i).First().tendv;
                                                    st38 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 38:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T39.Value = _DSDV.Skip(i).First().tendv;
                                                    st39 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 39:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T40.Value = _DSDV.Skip(i).First().tendv;
                                                    st40 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 40:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T41.Value = _DSDV.Skip(i).First().tendv;
                                                    st41 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 41:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T42.Value = _DSDV.Skip(i).First().tendv;
                                                    st42 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 42:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T43.Value = _DSDV.Skip(i).First().tendv;
                                                    st43 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 43:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T44.Value = _DSDV.Skip(i).First().tendv;
                                                    st44 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 44:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T45.Value = _DSDV.Skip(i).First().tendv;
                                                    st45 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                    rep.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 45:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep.T46.Value = _DSDV.Skip(i).First().tendv;
                                                    st46 = _DSDV.Skip(i).First().tendv;
                                                    rep.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;

                                        }
                                    }

                                    #region
                                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth = new int[] { };
                                    int num = 1;
                                    DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                    string[] _tieude = { "STT", "Họ và tên", "Tuổi",st1,st2,st3,st4,st5,st6,st7,st8,st9,st10,st11,st12,st13,st14,st15,st16,st17,st18,st19,st20,st21,st22,st23,st24,st25
                                                               ,st26,st27,st28,st29,st30,st31,st32,st33,st34,st35,st36,st37,st38,st39,st40,st41,st42,st43,st44,st45,st46, "Ký nhận"};
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                    }

                                    ////for (int i = 0; i <= 17; i++) {
                                    ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                    ////}
                                    foreach (var r in _Tamtra.ToList())
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.SL101;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.SL201;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.SL301;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.SL401;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.SL501;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.SL601;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.SL701;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.SL801;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.SL901;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.SL1001;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.SL1101;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.SL1201;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.SL1301;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.SL1401;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.SL1501;
                                        DungChung.Bien.MangHaiChieu[num, 18] = r.SL1601;
                                        DungChung.Bien.MangHaiChieu[num, 19] = r.SL1701;
                                        DungChung.Bien.MangHaiChieu[num, 20] = r.SL1801;
                                        DungChung.Bien.MangHaiChieu[num, 21] = r.SL1901;
                                        DungChung.Bien.MangHaiChieu[num, 22] = r.SL2001;
                                        DungChung.Bien.MangHaiChieu[num, 23] = r.SL2101;
                                        DungChung.Bien.MangHaiChieu[num, 24] = r.SL2201;
                                        DungChung.Bien.MangHaiChieu[num, 25] = r.SL2301;
                                        DungChung.Bien.MangHaiChieu[num, 26] = r.SL2401;
                                        DungChung.Bien.MangHaiChieu[num, 27] = r.SL2501;
                                        DungChung.Bien.MangHaiChieu[num, 28] = r.SL2601;
                                        DungChung.Bien.MangHaiChieu[num, 29] = r.SL2701;
                                        DungChung.Bien.MangHaiChieu[num, 30] = r.SL2801;
                                        DungChung.Bien.MangHaiChieu[num, 31] = r.SL2901;
                                        DungChung.Bien.MangHaiChieu[num, 32] = r.SL3001;
                                        DungChung.Bien.MangHaiChieu[num, 33] = r.SL3101;
                                        DungChung.Bien.MangHaiChieu[num, 34] = r.SL3201;
                                        DungChung.Bien.MangHaiChieu[num, 35] = r.SL3301;
                                        DungChung.Bien.MangHaiChieu[num, 36] = r.SL3401;
                                        DungChung.Bien.MangHaiChieu[num, 37] = r.SL3501;
                                        DungChung.Bien.MangHaiChieu[num, 38] = r.SL3601;
                                        DungChung.Bien.MangHaiChieu[num, 39] = r.SL3701;
                                        DungChung.Bien.MangHaiChieu[num, 40] = r.SL3801;
                                        DungChung.Bien.MangHaiChieu[num, 41] = r.SL3901;
                                        DungChung.Bien.MangHaiChieu[num, 42] = r.SL4001;
                                        DungChung.Bien.MangHaiChieu[num, 43] = r.SL4101;
                                        DungChung.Bien.MangHaiChieu[num, 44] = r.SL4201;
                                        DungChung.Bien.MangHaiChieu[num, 45] = r.SL4301;
                                        DungChung.Bien.MangHaiChieu[num, 46] = r.SL4401;
                                        DungChung.Bien.MangHaiChieu[num, 47] = r.SL4501;
                                        DungChung.Bien.MangHaiChieu[num, 48] = r.SL4601;
                                        DungChung.Bien.MangHaiChieu[num, 49] = "";
                                        num++;
                                    }
                                    #endregion

                                    rep.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep.DataSource = _Tamtra;
                                    rep.BindingData();
                                    rep.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();


                                    #region tạo báo cáo thứ 2
                                    BaoCao.Rep_Tamtrathuoc02 rep1 = new BaoCao.Rep_Tamtrathuoc02(mau);
                                    string a = "Kiểu đơn: ";
                                    if (chkBoxung.Checked == true)
                                    { a += "bổ xung"; }
                                    if (chkThuongxuyen.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " thường xuyên ";
                                        }
                                        else
                                        { a = a + " - " + " thường xuyên "; }
                                    }
                                    if (chkTrathuoc.Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(a))
                                        {
                                            a += " trả thuốc ";
                                        }
                                        else
                                        { a = a + " - " + " trả thuốc "; }
                                    }
                                    rep1.Kieudon.Value = a;
                                    for (int i = 46; i < _DSDV.Count; i++)
                                    {
                                        switch (i)
                                        {
                                            case 46:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T1.Value = _DSDV.Skip(i).First().tendv;
                                                    st47 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT01.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 47:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T2.Value = _DSDV.Skip(i).First().tendv;
                                                    st48 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT02.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 48:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T3.Value = _DSDV.Skip(i).First().tendv;
                                                    st49 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT03.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 49:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T4.Value = _DSDV.Skip(i).First().tendv;
                                                    st50 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT04.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 50:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T5.Value = _DSDV.Skip(i).First().tendv;
                                                    st51 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT05.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 51:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T6.Value = _DSDV.Skip(i).First().tendv;
                                                    st52 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT06.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 52:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T7.Value = _DSDV.Skip(i).First().tendv;
                                                    st53 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT07.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 53:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T8.Value = _DSDV.Skip(i).First().tendv;
                                                    st54 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT08.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 54:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T9.Value = _DSDV.Skip(i).First().tendv;
                                                    st55 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT09.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 55:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T10.Value = _DSDV.Skip(i).First().tendv;
                                                    st56 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT10.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 56:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T11.Value = _DSDV.Skip(i).First().tendv;
                                                    st57 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT11.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 57:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T12.Value = _DSDV.Skip(i).First().tendv;
                                                    st58 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT12.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 58:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T13.Value = _DSDV.Skip(i).First().tendv;
                                                    st59 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT13.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 59:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T14.Value = _DSDV.Skip(i).First().tendv;
                                                    st60 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT14.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 60:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T15.Value = _DSDV.Skip(i).First().tendv;
                                                    st61 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT15.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 61:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T16.Value = _DSDV.Skip(i).First().tendv;
                                                    st62 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT16.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 62:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T17.Value = _DSDV.Skip(i).First().tendv;
                                                    st63 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT17.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 63:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T18.Value = _DSDV.Skip(i).First().tendv;
                                                    st64 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT18.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 64:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T19.Value = _DSDV.Skip(i).First().tendv;
                                                    st65 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT19.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 65:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T20.Value = _DSDV.Skip(i).First().tendv;
                                                    st66 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT20.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 66:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T21.Value = _DSDV.Skip(i).First().tendv;
                                                    st67 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT21.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 67:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T22.Value = _DSDV.Skip(i).First().tendv;
                                                    st68 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT22.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 68:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T23.Value = _DSDV.Skip(i).First().tendv;
                                                    st69 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT23.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 69:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T24.Value = _DSDV.Skip(i).First().tendv;
                                                    st70 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT24.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 70:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T25.Value = _DSDV.Skip(i).First().tendv;
                                                    st71 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT25.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 71:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T26.Value = _DSDV.Skip(i).First().tendv;
                                                    st72 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT26.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 72:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T27.Value = _DSDV.Skip(i).First().tendv;
                                                    st73 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT27.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 73:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T28.Value = _DSDV.Skip(i).First().tendv;
                                                    st74 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT28.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 74:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T29.Value = _DSDV.Skip(i).First().tendv;
                                                    st75 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT29.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 75:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T30.Value = _DSDV.Skip(i).First().tendv;
                                                    st76 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT30.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 76:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T31.Value = _DSDV.Skip(i).First().tendv;
                                                    st77 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT31.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 77:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T32.Value = _DSDV.Skip(i).First().tendv;
                                                    st78 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT32.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 78:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T33.Value = _DSDV.Skip(i).First().tendv;
                                                    st79 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT33.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 79:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T34.Value = _DSDV.Skip(i).First().tendv;
                                                    st80 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT34.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 80:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T35.Value = _DSDV.Skip(i).First().tendv;
                                                    st81 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT35.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 81:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T36.Value = _DSDV.Skip(i).First().tendv;
                                                    st82 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT36.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 82:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T37.Value = _DSDV.Skip(i).First().tendv;
                                                    st83 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT37.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 83:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T38.Value = _DSDV.Skip(i).First().tendv;
                                                    st84 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT38.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 84:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T39.Value = _DSDV.Skip(i).First().tendv;
                                                    st85 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT39.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 85:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T40.Value = _DSDV.Skip(i).First().tendv;
                                                    st86 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT40.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 86:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T41.Value = _DSDV.Skip(i).First().tendv;
                                                    st87 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT41.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 87:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T42.Value = _DSDV.Skip(i).First().tendv;
                                                    st88 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT42.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 88:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T43.Value = _DSDV.Skip(i).First().tendv;
                                                    st89 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT43.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 89:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T44.Value = _DSDV.Skip(i).First().tendv;
                                                    st90 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT44.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 90:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T45.Value = _DSDV.Skip(i).First().tendv;
                                                    st91 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT45.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 91:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T46.Value = _DSDV.Skip(i).First().tendv;
                                                    st92 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT46.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 92:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T47.Value = _DSDV.Skip(i).First().tendv;
                                                    st93 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT47.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 93:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T48.Value = _DSDV.Skip(i).First().tendv;
                                                    st94 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT48.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 94:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T49.Value = _DSDV.Skip(i).First().tendv;
                                                    st95 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT49.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 95:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T50.Value = _DSDV.Skip(i).First().tendv;
                                                    st96 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT50.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 96:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T51.Value = _DSDV.Skip(i).First().tendv;
                                                    st97 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT51.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 97:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T52.Value = _DSDV.Skip(i).First().tendv;
                                                    st98 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT52.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;
                                            case 98:
                                                if (_DSDV.Skip(i).First().tendv != null)
                                                {
                                                    rep1.T53.Value = _DSDV.Skip(i).First().tendv;
                                                    st99 = _DSDV.Skip(i).First().tendv;
                                                    rep1.DVT53.Value = _DSDV.Skip(i).First().DonVi;
                                                }
                                                break;

                                        }
                                    }
                                    #endregion

                                    #region
                                    string[] _arr1 = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth1 = new int[] { };
                                    int num1 = 1;
                                    DungChung.Bien.MangHaiChieu = new Object[_Tamtra.ToList().Count + 1, 60];
                                    string[] _tieude1 = { "STT",st47,st48,st49,st50,st11,st52,st53,st54,st55,st56,st57,st58,st59,st60,st61,st62,st63,st64,st65,st66,st67,st68,st69,st70
                                                                ,st71,st72,st73,st74,st75,st75,st76,st77,st78,st79,st80,st81,st82,st83,st84,st85,st86,st87,st88,st89,st90,st91,st92,st93,st94,st95,st96,st97,st98,st99, "Ký nhận"};
                                    for (int i = 0; i < _tieude1.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude1[i];
                                    }

                                    ////for (int i = 0; i <= 17; i++) {
                                    ////    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                                    ////}
                                    foreach (var r in _Tamtra.ToList())
                                    {
                                        DungChung.Bien.MangHaiChieu[num1, 0] = num1;
                                        DungChung.Bien.MangHaiChieu[num1, 1] = r.SL101;
                                        DungChung.Bien.MangHaiChieu[num1, 2] = r.SL201;
                                        DungChung.Bien.MangHaiChieu[num1, 3] = r.SL301;
                                        DungChung.Bien.MangHaiChieu[num1, 4] = r.SL401;
                                        DungChung.Bien.MangHaiChieu[num1, 5] = r.SL501;
                                        DungChung.Bien.MangHaiChieu[num1, 6] = r.SL601;
                                        DungChung.Bien.MangHaiChieu[num1, 7] = r.SL701;
                                        DungChung.Bien.MangHaiChieu[num1, 8] = r.SL801;
                                        DungChung.Bien.MangHaiChieu[num1, 9] = r.SL901;
                                        DungChung.Bien.MangHaiChieu[num1, 10] = r.SL1001;
                                        DungChung.Bien.MangHaiChieu[num1, 11] = r.SL1101;
                                        DungChung.Bien.MangHaiChieu[num1, 12] = r.SL1201;
                                        DungChung.Bien.MangHaiChieu[num1, 13] = r.SL1301;
                                        DungChung.Bien.MangHaiChieu[num1, 14] = r.SL1401;
                                        DungChung.Bien.MangHaiChieu[num1, 15] = r.SL1501;
                                        DungChung.Bien.MangHaiChieu[num1, 16] = r.SL1601;
                                        DungChung.Bien.MangHaiChieu[num1, 17] = r.SL1701;
                                        DungChung.Bien.MangHaiChieu[num1, 18] = r.SL1801;
                                        DungChung.Bien.MangHaiChieu[num1, 19] = r.SL1901;
                                        DungChung.Bien.MangHaiChieu[num1, 20] = r.SL2001;
                                        DungChung.Bien.MangHaiChieu[num1, 21] = r.SL2101;
                                        DungChung.Bien.MangHaiChieu[num1, 22] = r.SL2201;
                                        DungChung.Bien.MangHaiChieu[num1, 23] = r.SL2301;
                                        DungChung.Bien.MangHaiChieu[num1, 24] = r.SL2401;
                                        DungChung.Bien.MangHaiChieu[num1, 25] = r.SL2501;
                                        DungChung.Bien.MangHaiChieu[num1, 26] = r.SL2601;
                                        DungChung.Bien.MangHaiChieu[num1, 27] = r.SL2701;
                                        DungChung.Bien.MangHaiChieu[num1, 28] = r.SL2801;
                                        DungChung.Bien.MangHaiChieu[num1, 29] = r.SL2901;
                                        DungChung.Bien.MangHaiChieu[num1, 30] = r.SL3001;
                                        DungChung.Bien.MangHaiChieu[num1, 31] = r.SL3101;
                                        DungChung.Bien.MangHaiChieu[num1, 32] = r.SL3201;
                                        DungChung.Bien.MangHaiChieu[num1, 33] = r.SL3301;
                                        DungChung.Bien.MangHaiChieu[num1, 34] = r.SL3401;
                                        DungChung.Bien.MangHaiChieu[num1, 35] = r.SL3501;
                                        DungChung.Bien.MangHaiChieu[num1, 36] = r.SL3601;
                                        DungChung.Bien.MangHaiChieu[num1, 37] = r.SL3701;
                                        DungChung.Bien.MangHaiChieu[num1, 38] = r.SL3801;
                                        DungChung.Bien.MangHaiChieu[num1, 39] = r.SL3901;
                                        DungChung.Bien.MangHaiChieu[num1, 40] = r.SL4001;
                                        DungChung.Bien.MangHaiChieu[num1, 41] = r.SL4101;
                                        DungChung.Bien.MangHaiChieu[num1, 42] = r.SL4201;
                                        DungChung.Bien.MangHaiChieu[num1, 43] = r.SL4301;
                                        DungChung.Bien.MangHaiChieu[num1, 44] = r.SL4401;
                                        DungChung.Bien.MangHaiChieu[num1, 45] = r.SL4501;
                                        DungChung.Bien.MangHaiChieu[num1, 46] = r.SL4601;
                                        DungChung.Bien.MangHaiChieu[num1, 47] = r.SL4701;
                                        DungChung.Bien.MangHaiChieu[num1, 48] = r.SL4801;
                                        DungChung.Bien.MangHaiChieu[num1, 49] = r.SL4901;
                                        DungChung.Bien.MangHaiChieu[num1, 50] = r.SL5001;
                                        DungChung.Bien.MangHaiChieu[num1, 51] = r.SL5101;
                                        DungChung.Bien.MangHaiChieu[num1, 52] = r.SL5201;
                                        DungChung.Bien.MangHaiChieu[num1, 53] = r.SL5301;
                                        DungChung.Bien.MangHaiChieu[num1, 54] = "";
                                        num1++;
                                    }
                                    #endregion

                                    rep1.Ngaythang.Value = " Từ ngày: " + LupNgayTu.Text.Substring(0, 10) + " đến ngày: " + LupNgayDen.Text.Substring(0, 10);
                                    rep1.Khoaphong.Value = LupKhoaPhong.Text;
                                    rep1.DataSource = _Tamtra;
                                    rep1.BindingData();
                                    rep1.CreateDocument();
                                    //rep.DataMember = "Table";
                                    frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr1, _arrWidth1, radioGroup2.SelectedIndex == 1 ? "Biểu Công Khai Thuốc Hàng Ngày" : "Sổ Tổng Hợp Thuốc Hàng Ngày", radioGroup2.SelectedIndex == 1 ? "C:\\BieuCongKhaiThuocHangNgay.xls" : "C:\\SoTongHopThuocHangNgay.xls", true, this.Name);
                                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                                    frm1.ShowDialog();
                                }
                                #endregion tạo báo cáo A4 2 trang
                            }
                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu quá dài, rút ngắn thời gian lấy báo cáo !");
                        }
                    }
                    else MessageBox.Show("Không có dữ liệu","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                 #endregion
                //}
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int a = radNT.SelectedIndex;
            MessageBox.Show(a.ToString());
        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup2.SelectedIndex == 0)
            {
                radioGroup1.SelectedIndex = 0;
                radioGroup1.Visible = false;
                mau = 0;
            }
            else
            {
                radioGroup1.Visible = true;
                mau = 1;
            }
        }

        private void radNT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

