﻿using System;using QLBV_Database;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QLBV.DungChung
{
    public class vCommon
    {

        public string convertSpecialCharacter(string xmlData)
        {
            return "<![CDATA[" + xmlData + "]]>";
        }

        private string[] ChuSo = new string[10] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bẩy", " tám", " chín" };
        private string[] Tien = new string[6] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
        // Hàm đọc số thành chữ
        public string DocSoTien(long SoTien, string strTail)
        {
            int lan, i;
            long so;
            string KetQua = "", tmp = "";
            int[] ViTri = new int[6];
            if (SoTien < 0) return "Số tiền âm !";
            if (SoTien == 0) return "Không đồng !";
            if (SoTien > 0)
            {
                so = SoTien;
            }
            else
            {
                so = -SoTien;
            }
            //Kiểm tra số quá lớn
            if (SoTien > 8999999999999999)
            {
                SoTien = 0;
                return "";
            }
            ViTri[5] = (int)(so / 1000000000000000);
            so = so - long.Parse(ViTri[5].ToString()) * 1000000000000000;
            ViTri[4] = (int)(so / 1000000000000);
            so = so - long.Parse(ViTri[4].ToString()) * +1000000000000;
            ViTri[3] = (int)(so / 1000000000);
            so = so - long.Parse(ViTri[3].ToString()) * 1000000000;
            ViTri[2] = (int)(so / 1000000);
            ViTri[1] = (int)((so % 1000000) / 1000);
            ViTri[0] = (int)(so % 1000);
            if (ViTri[5] > 0)
            {
                lan = 5;
            }
            else if (ViTri[4] > 0)
            {
                lan = 4;
            }
            else if (ViTri[3] > 0)
            {
                lan = 3;
            }
            else if (ViTri[2] > 0)
            {
                lan = 2;
            }
            else if (ViTri[1] > 0)
            {
                lan = 1;
            }
            else
            {
                lan = 0;
            }
            for (i = lan; i >= 0; i--)
            {
                bool isDoc = false;
                if (ViTri[i].ToString().Length < 3 && i < lan)
                {
                    string testing = ViTri[i].ToString();
                    isDoc = true;
                    //ViTri[i] = AddZero(ViTri[i]);
                }
                tmp = DocSo3ChuSo(ViTri[i], isDoc);
                isDoc = false;
                KetQua += tmp;
                if (ViTri[i] != 0) KetQua += Tien[i];
                if ((i > 0) && (!string.IsNullOrEmpty(tmp))) KetQua += ",";//&& (!string.IsNullOrEmpty(tmp))
            }
            if (KetQua.Substring(KetQua.Length - 1, 1) == ",") KetQua = KetQua.Substring(0, KetQua.Length - 1);
            KetQua = KetQua.Trim() + strTail;
            return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1) + " đồng";
        }

        private string DocSo3ChuSo(int baso, bool isDoc0)
        {
            int tram, chuc, donvi;
            string KetQua = "";
            tram = (int)(baso / 100);
            chuc = (int)((baso % 100) / 10);
            donvi = baso % 10;
            if ((tram == 0) && (chuc == 0) && (donvi == 0)) return "";
            if (tram != 0 || isDoc0)
            {
                KetQua += ChuSo[tram] + " trăm";
                if ((chuc == 0) && (donvi != 0)) KetQua += " linh";
            }
            if ((chuc != 0) && (chuc != 1))
            {
                KetQua += ChuSo[chuc] + " mươi";
                if ((chuc == 0) && (donvi != 0)) KetQua = KetQua + " linh";
            }
            if (chuc == 1) KetQua += " mười";
            switch (donvi)
            {
                case 1:
                    if ((chuc != 0) && (chuc != 1))
                    {
                        KetQua += " mốt";
                    }
                    else
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
                case 5:
                    if (chuc == 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    else
                    {
                        KetQua += " lăm";
                    }
                    break;
                default:
                    if (donvi != 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
            }
            return KetQua;
        }

        public String UpdateAppSettings(string _KeyName, string _KeyValue)
        {
            XmlDocument XmlDoc = new XmlDocument();
            try
            {
                XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                foreach (XmlElement xElement in XmlDoc.DocumentElement)
                {
                    if (xElement.Name == "appSettings")
                    {
                        foreach (XmlNode xNode in xElement.ChildNodes)
                        {

                            if (xNode.Attributes[0].Value == _KeyName)
                            {

                                xNode.Attributes[1].Value = _KeyValue;

                            }

                        }

                    }

                }
                XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                // Set Session para
                System.Configuration.ConfigurationSettings.AppSettings[_KeyName] = _KeyValue;

                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }



        }
    }
}
