using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;

namespace ZJYC_TOOL
{
    class CHK
    {
        public void Chk()
        {
            FileStream File1 = new FileStream("GUI_MultiGas.bin", FileMode.Open);
            FileStream File2 = new FileStream("ST-GUI_MultiGas.bin", FileMode.Open);

            for (int i = 0; i < File1.Length; i++)
            {
                if (File1.ReadByte() != File2.ReadByte())
                {
                    Console.WriteLine("{0}", i);
                }
            }
            Console.ReadLine();
        }
    }
    class HexToBin
    {
        class HexRecord
        {
            private string Header = ":";
            public byte DataLen = 0;
            public UInt16 Address = 0;
            public byte DataType = 0;
            public byte[] Data;
            private byte Chk = 0;

            private byte[] HexStrToByteArray(string hexStr)
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

            private bool ChkSum(string OneLineOfHexFile)
            {
                byte[] Temp = HexStrToByteArray(OneLineOfHexFile.Substring(1, OneLineOfHexFile.Length - 3));
                byte Chk = Convert.ToByte(OneLineOfHexFile.Substring(OneLineOfHexFile.Length - 2, 2), 16);
                int Sum = 0;
                foreach (byte b in Temp) Sum += b;

                if (Chk != (byte)(256 - (Sum % 256))) return false;

                return true;
            }

            public HexRecord(string OneLineOfHexFile)
            {
                if (OneLineOfHexFile == "") return;
                if (OneLineOfHexFile[0].ToString() != Header) return;
                if (ChkSum(OneLineOfHexFile) == false) return;
                try
                {
                    DataLen = Convert.ToByte(OneLineOfHexFile.Substring(1, 2), 16);
                    Address = Convert.ToUInt16(OneLineOfHexFile.Substring(3, 4), 16);
                    DataType = Convert.ToByte(OneLineOfHexFile.Substring(7, 2), 16);
                    Data = HexStrToByteArray(OneLineOfHexFile.Substring(9, DataLen * 2));
                    Chk = Convert.ToByte(OneLineOfHexFile.Substring(9 + DataLen * 2, 2), 16);
                }
                catch
                {

                }
            }

        }
        private List<HexRecord> HexRecords = new List<HexRecord>();
        ///输出到BIN文件
        public void WritByteToFile(byte[] BinBuff, string BinFileName)
        {
            using (FileStream Writer = new FileStream(BinFileName, FileMode.OpenOrCreate))
            {
                Writer.Write(BinBuff, 0, BinBuff.Length);
            }
        }
        /// 将数据读取到数组
        public byte[] ReadHexFileToByte(string HexFileName)
        {
            byte[] BinBuff = new byte[0];
            try
            {
                ///文件读入list
                using (StreamReader Reader = new StreamReader(HexFileName))
                {
                    string Line = "";
                    while ((Line = Reader.ReadLine()) != "")
                    {
                        HexRecord hexRecord = new HexRecord(Line);
                        if (hexRecord.DataType == 0x01) break;
                        HexRecords.Add(hexRecord);
                    }
                }
                ///统计最大地址和最小地址
                int MinAddr = int.MaxValue;
                int MaxAddr = int.MinValue;
                int Shift = 0;
                foreach (HexRecord Record in HexRecords)
                {
                    if (Record.DataType == 0x04)
                    {
                        Shift = ((Record.Data[0] << 8) | Record.Data[1]) << 16;
                    }
                    int CurAddrStart = Shift + Record.Address;
                    int CurAddrEnd = Shift + Record.Address + Record.DataLen;
                    MinAddr = (MinAddr > CurAddrStart ? CurAddrStart : MinAddr);
                    MaxAddr = (MaxAddr < CurAddrEnd ? CurAddrEnd : MaxAddr);
                }
                ///填充
                BinBuff = new byte[MaxAddr - MinAddr];
                Shift = 0;
                foreach (HexRecord Record in HexRecords)
                {
                    if (Record.DataType == 0x04)
                    {
                        Shift = ((Record.Data[0] << 8) | Record.Data[1]) << 16;
                    }
                    if (Record.DataType == 0x00)
                    {
                        int CurAddr = Shift + Record.Address - MinAddr;
                        Array.Copy(Record.Data, 0, BinBuff, CurAddr, Record.DataLen);
                    }
                }
            }
            catch
            {
                Console.WriteLine("ERROR");
                Console.ReadKey();
            }
            return BinBuff;
        }
    }
}
