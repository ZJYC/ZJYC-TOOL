using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.DateTime;

namespace ZJYC_TOOL
{
    class GetString
    {
        public string GetCurTimeString(byte Type)
        {
            string Res = "";
            DateTime CurTime = DateTime.Now;

            switch (Type)
            {
                case 6:
                    Res =
                    CurTime.Year.ToString("D4") +
                    CurTime.Month.ToString("D2") +
                    CurTime.Day.ToString("D2") +
                    CurTime.Hour.ToString("D2") +
                    CurTime.Minute.ToString("D2") +
                    CurTime.Second.ToString("D2");
                    break;
                case 5:
                    Res =
                    CurTime.Year.ToString("D4") +
                    CurTime.Month.ToString("D2") +
                    CurTime.Day.ToString("D2") +
                    CurTime.Hour.ToString("D2") +
                    CurTime.Minute.ToString("D2");
                    break;
                case 4:
                    Res =
                    CurTime.Year.ToString("D4") +
                    CurTime.Month.ToString("D2") +
                    CurTime.Day.ToString("D2") +
                    CurTime.Hour.ToString("D2");
                    break;
                case 3:
                    Res =
                    CurTime.Year.ToString("D4") +
                    CurTime.Month.ToString("D2") +
                    CurTime.Day.ToString("D2");
                    break;
                default:break;
            }
            return Res;
        }
        /// "blank", "Author", "Time", "Author+Time"
        public string GetCodeComment(string Type)
        {
            string Res = "";
            if(Type == "blank")
            {
                Res = "/*  */";
            }
            else if(Type == "Author")
            {
                Res = "/* ZJYC:  */";
            }
            else if(Type == "Time")
            {
                Res = "/* " + GetCurTimeString(6) + " */";
            }
            else if (Type == "Author+Time")
            {
                Res = "/* " + GetCurTimeString(6) + "ZJYC" + " */";
            }
            else
            {
                ;
            }
            return Res;
        }
        ///"Normal", "Author", "Time", "Author+Time"
        public string GetFuncComment(string Type)
        {
            string Res = "";
            if(Type == "Normal")
            {
                Res =
                    "/*\n" +
                    "********************************************************************************\n" +
                    "*  Function       :  <>\n" +
                    "*  Description    :  <>\n" +
                    "*  Params         :  <>\n" +
                    "*  Return         :  <>\n" +
                    "********************************************************************************\n" +
                    "*/";
            }
            else if(Type == "Author")
            {
                Res =
                    "/*\n" +
                    "********************************************************************************\n" +
                    "*  Function       :  <>\n" +
                    "*  Description    :  <>\n" +
                    "*  Params         :  <>\n" +
                    "*  Return         :  <>\n" +
                    "*  Author         :  <5A4A5943>\n" +
                    "********************************************************************************\n" +
                    "*/";
            }
            else if (Type == "Time")
            {
                Res =
                    "/*\n" +
                    "********************************************************************************\n" +
                    "*  Function       :  <>\n" +
                    "*  Description    :  <>\n" +
                    "*  Params         :  <>\n" +
                    "*  Return         :  <>\n" +
                    "*  Time           :  <" + GetCurTimeString(6) + ">\n" +
                    "********************************************************************************\n" +
                    "*/";
            }
            else if (Type == "Author+Time")
            {
                Res =
                    "/*\n" +
                    "********************************************************************************\n" +
                    "*  Function       :  <>\n" +
                    "*  Description    :  <>\n" +
                    "*  Params         :  <>\n" +
                    "*  Return         :  <>\n" +
                    "*  Author         :  <5A4A5943>\n" +
                    "*  Time           :  <" + GetCurTimeString(6) + ">\n" +
                    "********************************************************************************\n" +
                    "*/";
            }
            return Res;
        }

        public string GetStringFromByteArray(byte [] Array)
        {
            string Res = "";
            foreach(byte c in Array)
            {
                Res += string.Format("{0:X02} ", c);
            }
            return Res;
        }
        public byte[] HexStrToBytes(string hexStr)
        {
            hexStr = hexStr.Replace(" ", "");

            if (string.IsNullOrEmpty(hexStr))
            {
                return new byte[0];
            }

            if (hexStr.StartsWith("0x"))
            {
                hexStr = hexStr.Remove(0, 2);
            }

            var count = hexStr.Length;

            if (count % 2 == 1)
            {
                throw new ArgumentException("Invalid length of bytes:" + count);
            }

            var byteCount = count / 2;
            var result = new byte[byteCount];
            for (int ii = 0; ii < byteCount; ++ii)
            {
                var tempBytes = Byte.Parse(hexStr.Substring(2 * ii, 2), System.Globalization.NumberStyles.HexNumber);
                result[ii] = tempBytes;
            }

            return result;
        }
    }
    class CRC
    {
        private Byte[] aucCRCHi = {
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
        0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
        0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
        0x00, 0xC1, 0x81, 0x40};

        private Byte[] aucCRCLo = {
        0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7,
        0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
        0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9,
        0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
        0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
        0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
        0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D,
        0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38,
        0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF,
        0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
        0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1,
        0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
        0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB,
        0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
        0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
        0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
        0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97,
        0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
        0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89,
        0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
        0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83,
        0x41, 0x81, 0x80, 0x40};
        /// <summary>
        /// CRC计算
        /// </summary>
        /// <returns></returns>
        private UInt16 CRC16()
        {
            Byte ucCRCHi = 0xFF;
            Byte ucCRCLo = 0xFF;
            Byte iIndex;
            Byte i = 0;

            for (; Len > 0; Len--)
            {
                iIndex = (Byte)(ucCRCLo ^ CrcInputData[i++]);
                ucCRCLo = (Byte)(ucCRCHi ^ aucCRCHi[iIndex]);
                ucCRCHi = aucCRCLo[iIndex];
            }
            return (UInt16)((ucCRCHi << 8) | ucCRCLo);
        }
        private Byte[] CrcInputData = new Byte[1000];
        private byte Len = 0;
        /// <summary>
        /// 字符串输入
        /// </summary>
        /// <returns></returns>
        private void GetInput(String str)
        {
            //String str = Console.ReadLine();
            if (str.Length == 0)
            {
                Len = 0;
                return;
            }
            Len = (Byte)(str.Length);
            if (Len % 2 != 0)
            {
                Len = 0;
                return;
            }
            for (int i = 0, j = 0; i < str.Length;)
            {
                CrcInputData[j++] = Convert.ToByte(str.Substring(0, 2), 16);
                str = str.Remove(0, 2);
            }
            Len = (Byte)(Len / 2);
        }

        public string GetCrc(String str)
        {
            String Temp = "";
            GetInput(str);
            if (Len != 0)
            {
                UInt16 Res = (UInt16)CRC16();

                Temp = Res.ToString("X");
                if (Temp.Length == 1) Temp = "000" + Temp;
                if (Temp.Length == 2) Temp = "00" + Temp;
                if (Temp.Length == 3) Temp = "0" + Temp;
                //Temp = Temp;
                //Console.WriteLine("{0:X}", Res);
            }
            return Temp;
        }
    }
}
