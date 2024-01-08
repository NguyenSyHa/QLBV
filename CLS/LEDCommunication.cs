using System;using QLBV_Database;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_LED
{
    public class LEDCommunication
    {
        private byte[] Data;
        private const int TA = 0;
        private const int TB = 1;
        private const int TC = 2;
        private const int TD = 3;
        private const int TE = 4;
        private const int TF = 5;
        private const int TG = 6;
        private SerialPort portCOM { get; set; }
        public string Text { get; set; }

        public LEDCommunication(string portName)
        {
            this.portCOM = new SerialPort();
            this.portCOM.PortName = portName;
            this.portCOM.ReadTimeout = 0x7d0;
            this.portCOM.DiscardNull = true;
        }

        private void DichSo(char so, byte thutu)
        {
            int num2;
            byte num = (byte)Math.Round(Math.Pow(2.0, (double)thutu));
            switch (((char)(so - '0')))
            {
                case '\0':
                    num2 = 0;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 1;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 2;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 3;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 4;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 5;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    break;

                case '\x0001':
                    num2 = 1;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 2;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    break;

                case '\x0002':
                    num2 = 0;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 1;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 3;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 4;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 6;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    break;

                case '\x0003':
                    num2 = 0;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 1;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 2;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 3;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 6;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    break;

                case '\x0004':
                    num2 = 1;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 2;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 5;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 6;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    break;

                case '\x0005':
                    num2 = 0;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 2;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 3;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 5;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 6;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    break;

                case '\x0006':
                    num2 = 0;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 2;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 3;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 4;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 5;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 6;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    break;

                case '\a':
                    num2 = 0;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 1;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 2;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    break;

                case '\b':
                    num2 = 0;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 1;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 2;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 3;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 4;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 5;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 6;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    break;

                case '\t':
                    num2 = 0;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 1;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 2;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 3;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 5;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    num2 = 6;
                    this.Data[num2] = (byte)(this.Data[num2] + num);
                    break;
            }
        }

        private void TinhduLieu()
        {
            byte num9;
            string so1 = this.Text;//, so2 = "3453";
            char[] chArray2 = so1.ToCharArray();
            //char[] chArray3 = so2.ToCharArray();
            char[] chArray = new char[8];
            int index = 3;
            //if (chArray3.Length == 0)
            //{
            //    byte num2 = 0;
            //    do
            //    {
            //        chArray[index] = '\0';
            //        index++;
            //        num2 = (byte)(num2 + 1);
            //        num9 = 3;
            //    }
            //    while (num2 <= num9);
            //}
            //else
            //{
            //    byte num7 = (byte)(chArray3.Length - 1);
            //    for (byte i = 0; i <= num7; i = (byte)(i + 1))
            //    {
            //        chArray[index] = chArray3[(chArray3.Length - 1) - i];
            //        index--;
            //    }
            //}
            index = 7;
            if (chArray2.Length == 0)
            {
                byte num4 = 0;
                do
                {
                    chArray[index] = '\0';
                    index++;
                    num4 = (byte)(num4 + 1);
                    num9 = 3;
                }
                while (num4 <= num9);
            }
            else
            {
                byte num8 = (byte)(chArray2.Length - 1);
                for (byte j = 0; j <= num8; j = (byte)(j + 1))
                {
                    chArray[index] = chArray2[(chArray2.Length - 1) - j];
                    index--;
                }
            }
            index = 0;
            byte num6 = 0;
            do
            {
                this.DichSo(chArray[num6], (byte)index);
                index++;
                num6 = (byte)(num6 + 1);
                num9 = 7;
            }
            while (num6 <= num9);
        }

        /// <summary>
        /// Truyền số sang bảng điện tử
        /// </summary>
        private string Transmission()
        {
            string[] str = SerialPort.GetPortNames();
            string str_return = "";
            if (str.Length <= 0)
            {
                str_return = ("Máy của bạn không có cổng COM. Không thực hiện được thao tác này");
            }
            else
            {
                byte num2;
                int num4;

                if (!this.portCOM.IsOpen)
                {
                    this.portCOM.Open();
                }
                else
                {

                    str_return = ("Cổng COM đang được sử dụng bởi chương trình khác");
                    return str_return;
                }
                byte[] buffer = new byte[10];
                int num3 = 1;
                do
                {
                    buffer[num3 - 1] = 0x22;
                    num3++;
                    num4 = 10;
                }
                while (num3 <= num4);
                this.portCOM.DiscardInBuffer();
                this.portCOM.Write(buffer, 0, 10);
                do
                {
                    try
                    {
                        num2 = (byte)this.portCOM.ReadByte();
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        if (exception.Message == "The operation has timed out.")
                        {
                            str_return = ("Kiểm tra thiết bị hoặc cáp truyền");
                        }
                        this.portCOM.Close();
                        return str_return;
                    }
                }
                while (this.portCOM.BytesToRead != 0);
                if (num2 != 3)
                {
                    str_return = ("Kiểm tra thiết bị hoặc cáp truyền");
                    this.portCOM.Close();
                }
                else
                {
                    int num = num2;
                    this.portCOM.DiscardInBuffer();
                    this.portCOM.Write(this.Data, 0, 14);
                    do
                    {
                        try
                        {
                            num2 = (byte)this.portCOM.ReadByte();
                        }
                        catch (Exception exception3)
                        {
                            Exception exception2 = exception3;
                            this.portCOM.Close();
                            str_return = ("Truyền lỗi");
                            return str_return;
                        }
                    }
                    while (this.portCOM.BytesToRead != 0);
                    if (num2 != num)
                    {
                        this.portCOM.Close(); str_return = ("Truyền lỗi");
                    }
                    else
                    {
                        this.portCOM.Close();
                    }
                }
            }
            return str_return;
        }


        /// <summary>
        /// 1:OK
        /// 2:lỗi thiết bị hoặc cáp truyền 
        /// Hiển thị số thứ tự ra ngoài bản điện tử LED
        /// </summary>
        /// <param name="text">Text truyền </param>
        public string ShowView(string text)
        {
            string mb = "";
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
            {
                int sott = -1;
                if (int.TryParse(text, out sott))
                {


                    this.Text = text;
                    byte num2;
                    this.Data = new byte[14];
                    this.TinhduLieu();
                    byte index = 7;
                    do
                    {
                        this.Data[index] = this.Data[index - 7];
                        index = (byte)(index + 1);
                        num2 = 13;
                    }
                    while (index <= num2);
                    mb = this.Transmission();
                    return mb;
                }
                else return mb;
            }
            else
            {
                return mb;
            }

        }


        /// <summary>
        /// Dừng luồng COM
        /// </summary>
        public void Stop()
        {
            this.Data = new byte[14];
            this.Transmission();
        }

        /// <summary>
        /// Hàm test cổng COM xem cổng đã được kết nối hay chưa
        /// </summary>
        /// <param name="portName"></param>
        public string Test(string portName)
        {
            string mb = "";
            //string[] str = SerialPort.GetPortNames();
            if (string.IsNullOrEmpty(portName) || string.IsNullOrWhiteSpace(portName))
            {
                mb=("Máy tính của bạn không có cổng COM. Không thực hiện thao tác này");
                return mb;
            }
            else
            {
                byte num=0;
                int num3;
                this.portCOM.PortName = portName;
                this.portCOM.ReadTimeout = 0x7d0;
                this.portCOM.DiscardNull = true;
                if (!this.portCOM.IsOpen)
                {
                    this.portCOM.Open();
                }
                else
                {
                    mb = ("Cổng COM đang được sử dụng bởi chương trình khác, hãy tắt bỏ chương trình đó và thử lại");
                    return mb;
                }
                byte[] buffer = new byte[10];
                int num2 = 1;
                do
                {
                    buffer[num2 - 1] = 0x11;
                    num2++;
                    num3 = 10;
                }
                while (num2 <= num3);
                this.portCOM.DiscardInBuffer();
                this.portCOM.Write(buffer, 0, 10);
                do
                {
                    try
                    {
                        num = (byte)this.portCOM.ReadByte();
                    }
                    catch (Exception exception1)
                    {

                        Exception exception = exception1;
                        if (exception.Message == "The operation has timed out.")
                        {
                           mb=("Kiểm tra thiết bị hoặc cáp truyền");
                        }
                        this.portCOM.Close();

                        return mb;

                    }
                }
                while (this.portCOM.BytesToRead != 0);
                if (num == 3)
                {

                    //("Kiểm tra thành công");
                    this.portCOM.Close();
                    return mb;
                }
                else
                {
                  mb=("Kiểm tra thiết bị hoặc cáp truyền");
                    this.portCOM.Close();
                    return mb;
                }
            }

        }
    }
}
