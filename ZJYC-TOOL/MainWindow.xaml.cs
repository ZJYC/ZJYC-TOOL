using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Interop;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace ZJYC_TOOL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        GetString getString = new GetString();

        int KEY_CSA, KEY_CSZ,KEY_CSW;
        private IntPtr MainWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case HotKey.WM_HOTKEY:
                    {
                        int sid = wParam.ToInt32();
                        if (sid == KEY_CSA)
                        {
                            string TimeStr = getString.GetCurTimeString(byte.Parse((string)ComBox_TimeFormat.SelectedValue));
                            System.Windows.Clipboard.SetDataObject(TimeStr);
                        }
                        else if (sid == KEY_CSZ)
                        {
                            string TimeStr = getString.GetCodeComment((string)ComBox_CodeCommentFormat.SelectedValue);
                            System.Windows.Clipboard.SetDataObject(TimeStr);
                        }
                        else if (sid == KEY_CSW)
                        {
                            string TimeStr = getString.GetFuncComment((string)ComBox_FuncCommentFormat_Copy.SelectedValue);
                            System.Windows.Clipboard.SetDataObject(TimeStr);
                        }
                        else
                        {

                        }
                        handled = true;
                        break;
                    }
            }

            return IntPtr.Zero;
        }

        private void HexAndNum10_Click(object sender, RoutedEventArgs e)
        {
            if(HexAndNum10_In.Text == "")
            {
                HexAndNum10_Out.Text = "You should input something...";
                return;
            }
            string Input = HexAndNum10_In.Text;
            ///"1B", "2B", "4B"
            ///"Unsigned To Hex", "Signed To Hex", "Hex To Unsigned", "Hex To Signed", "Hex To Float","Float To Hex"
            try
            {
                if ((string)Hex2Num_Length.SelectedValue == "1B")
                {
                    if ((string)Hex2Num_Type.SelectedValue == "Unsigned To Hex")
                    {
                        HexAndNum10_Out.Text = byte.Parse(Input).ToString("X");
                    }
                    else if ((string)Hex2Num_Type.SelectedValue == "Signed To Hex")
                    {
                        HexAndNum10_Out.Text = sbyte.Parse(Input).ToString("X");
                    }
                    else if ((string)Hex2Num_Type.SelectedValue == "Hex To Unsigned")
                    {
                        HexAndNum10_Out.Text = Convert.ToByte(Input, 16).ToString();
                    }
                    else if ((string)Hex2Num_Type.SelectedValue == "Hex To Signed")
                    {
                        HexAndNum10_Out.Text = Convert.ToSByte(Input, 16).ToString();
                    }
                    else
                    {
                        ;
                    }
                }
                else if ((string)Hex2Num_Length.SelectedValue == "2B")
                {
                    if ((string)Hex2Num_Type.SelectedValue == "Unsigned To Hex")
                    {
                        byte[] In = BitConverter.GetBytes(UInt16.Parse(Input));
                        Array.Reverse(In);
                        HexAndNum10_Out.Text = getString.GetStringFromByteArray(In);
                    }
                    else if ((string)Hex2Num_Type.SelectedValue == "Signed To Hex")
                    {
                        byte[] In = BitConverter.GetBytes(Int16.Parse(Input));
                        Array.Reverse(In);
                        HexAndNum10_Out.Text = getString.GetStringFromByteArray(In);
                    }
                    else if ((string)Hex2Num_Type.SelectedValue == "Hex To Unsigned")
                    {
                        HexAndNum10_Out.Text = Convert.ToUInt16(Input, 16).ToString();
                    }
                    else if ((string)Hex2Num_Type.SelectedValue == "Hex To Signed")
                    {
                        HexAndNum10_Out.Text = Convert.ToInt16(Input, 16).ToString();
                    }
                    else
                    {
                        ;
                    }
                }
                else if ((string)Hex2Num_Length.SelectedValue == "4B")
                {
                    if ((string)Hex2Num_Type.SelectedValue == "Unsigned To Hex")
                    {
                        byte[] In = BitConverter.GetBytes(UInt32.Parse(Input));
                        Array.Reverse(In);
                        HexAndNum10_Out.Text = getString.GetStringFromByteArray(In);
                    }
                    else if ((string)Hex2Num_Type.SelectedValue == "Signed To Hex")
                    {
                        byte[] In = BitConverter.GetBytes(Int32.Parse(Input));
                        Array.Reverse(In);
                        HexAndNum10_Out.Text = getString.GetStringFromByteArray(In);
                    }
                    else if ((string)Hex2Num_Type.SelectedValue == "Hex To Unsigned")
                    {
                        HexAndNum10_Out.Text = Convert.ToUInt32(Input, 16).ToString();
                    }
                    else if ((string)Hex2Num_Type.SelectedValue == "Hex To Signed")
                    {
                        HexAndNum10_Out.Text = Convert.ToInt32(Input, 16).ToString();
                    }
                    else if ((string)Hex2Num_Type.SelectedValue == "Hex To Float")
                    {
                        byte[] In = getString.HexStrToBytes(Input);
                        Array.Reverse(In);
                        float Res = BitConverter.ToSingle(In, 0);
                        HexAndNum10_Out.Text = Res.ToString();
                    }
                    else if ((string)Hex2Num_Type.SelectedValue == "Float To Hex")
                    {
                        float In = float.Parse(Input);
                        byte[] Res = BitConverter.GetBytes(In);
                        Array.Reverse(Res);
                        HexAndNum10_Out.Text = getString.GetStringFromByteArray(Res);
                    }
                    else
                    {
                        ;
                    }
                }
                else
                {
                    ;
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Input error!!");
            }

        }

        private void Button_GetRandom_Click(object sender, RoutedEventArgs e)
        {
            Int32 Start = Int32.Parse(Random_Start.Text);
            Int32 End = Int32.Parse(Random_End.Text);
            UInt16 Num = UInt16.Parse(Random_Count.Text);

            String String = "";

            if (Num == 0)
            {
                System.Windows.MessageBox.Show("您输入的数值为0");
                return;
            }
            if (Num > 10000)
            {
                System.Windows.MessageBox.Show("您输入的数值大于10000");
                return;
            }

            if (Start > End)
            {
                System.Windows.MessageBox.Show("前面的数字应不大于后面的数字");
                return;
            }

            Random RandomTemp = new Random();

            for (UInt16 cnt = 0; cnt < Num; cnt++)
            {
                String += RandomTemp.Next((int)Start, (int)End + 1).ToString();
                String += ",";
            }

            System.Windows.Clipboard.SetDataObject(String);
        }

        private void Button_GetHex_Click(object sender, RoutedEventArgs e)
        {
            if (GetHex_Input.Text != "")
            {
                string CodeType = (string)CodingType.SelectedValue;
                byte[] byteArray = Encoding.GetEncoding(CodeType).GetBytes(GetHex_Input.Text);
                //byte[] byteArray = System.Text.Encoding.Default.GetBytes(GetHex_Input.Text);
                String Output = "";
                for (int i = 0; i < byteArray.Length; i++)
                {
                    Output += byteArray[i].ToString("X");
                }
                GetHex_Output.Text = Output;
            }
        }

        private void Button_GetStrFromHex_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GetHex_Output.Text != "")
                {
                    string CodeType = (string)CodingType.SelectedValue;
                    byte[] In = getString.HexStrToBytes(GetHex_Output.Text);
                    //GetHex_Input.Text = System.Text.Encoding.Default.GetString(In);
                    GetHex_Input.Text = Encoding.GetEncoding(CodeType).GetString(In);
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Input error!!!");
            }
        }

        private void Button_TextProcess_Click(object sender, RoutedEventArgs e)
        {
            if (TextPro_Input.Text != "")
            {
                UInt16 Interval = UInt16.Parse(TextPro_Interval.Text);
                String Former = TextPro_Header.Text, Rear = TextPro_Rear.Text;
                List<Char> Output = new List<Char>();

                if (Interval == 0) return;

                for (int i = 0; i < TextPro_Input.Text.Length; i++)
                {
                    if ((i % Interval == 0))
                    {
                        //添加空格
                        if (i != 0)
                        {
                            for (int j = 0; j < Rear.Length; j++)
                            {
                                Output.Add(Rear[j]);
                            }
                            for (int j = 0; j < Former.Length; j++)
                            {
                                Output.Add(Former[j]);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < Former.Length; j++)
                            {
                                Output.Add(Former[j]);
                            }
                        }
                    }
                    Output.Add(TextPro_Input.Text[i]);
                }
                String M = String.Join("", Output.ToArray());
                TextPro_Output.Text = M;
                //textBox5.Text = M;
                System.Windows.Clipboard.SetDataObject(M);
            }
        }

        private void Button_GetCRC16_Click(object sender, RoutedEventArgs e)
        {
            if (CRC_Input.Text != "")
            {
                //去掉空格
                String Temp = CRC_Input.Text.Replace(" ", "");
                Temp = Temp.Replace("\r", "");
                Temp = Temp.Replace("\n", "");
                CRC_Input.Text = Temp;
                if (Temp.Length % 2 != 0) { System.Windows.MessageBox.Show("输入有误，长度不是2的倍数");return; }
                CRC_Input.Text = Temp;
                CRC crc = new CRC();
                string T = crc.GetCrc(Temp);
                UInt16 R = Convert.ToUInt16(T,16);
                byte[] X = BitConverter.GetBytes(R);
                Array.Reverse(X);
                R = BitConverter.ToUInt16(X,0);

                CRC_Output.Text = R.ToString("X");
                System.Windows.Clipboard.SetDataObject(CRC_Output.Text);
            }
        }

        private void Button_CopyWithBigMode_Click(object sender, RoutedEventArgs e)
        {
            UInt16 CRC = Convert.ToUInt16(CRC_Output.Text,16);
            if (CRC == 0) return;
            byte [] Temp = BitConverter.GetBytes(CRC);
            Array.Reverse(Temp);
            string Res = CRC_Input.Text + getString.GetStringFromByteArray(Temp);
            Res = Res.Replace(" ","");
            System.Windows.Clipboard.SetDataObject(Res);
        }

        private void Button_CopyWithSmlMode_Click(object sender, RoutedEventArgs e)
        {
            UInt16 CRC = Convert.ToUInt16(CRC_Output.Text,16);
            if (CRC == 0) return;
            byte[] Temp = BitConverter.GetBytes(CRC);
            //Array.Reverse(Temp);
            string Res = CRC_Input.Text + getString.GetStringFromByteArray(Temp);
            Res = Res.Replace(" ", "");
            System.Windows.Clipboard.SetDataObject(Res);
        }

        private void DedupButton_Click(object sender, RoutedEventArgs e)
        {
            if(DedupInput.Text != "")
            {
                string Input = DedupInput.Text;
                List<char> Output = new List<char>();
                foreach(char C in Input)
                {
                    if (Output.Contains(C)) continue;
                    Output.Add(C);
                    Input = Input.Replace(C.ToString(), "");
                    if (Input.Length == 0) break;
                }
                Output.Sort();
                DedupOutput.Text = string.Join("",Output);
            }
        }

        private void FindFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.RestoreDirectory = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string name = fileDialog.FileName;
                FileNameSelected.Text = name;
            }
        }

        private void FileCvtToCSource_Click(object sender, RoutedEventArgs e)
        {
            String Output = "";

            if (FileNameSelected.Text == "") return;

            CSourceContent.Text = "";

            try
            {
                using (FileStream fileStream = new FileStream(FileNameSelected.Text, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] AllBytes = new byte[fileStream.Length];
                    fileStream.Read(AllBytes, 0, AllBytes.Length);
                    Stream stream = new MemoryStream(AllBytes);
                    for (int i = 0; i < stream.Length; i++)
                    {
                        if (i != 0 && i % 16 == 0) Output += "\r\n";
                        Output += "0x";
                        Output += string.Format("{0:X2}", stream.ReadByte()); //stream.ReadByte().ToString("X");
                        Output += ",";
                    }
                    CSourceContent.Text = Output;
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show("Error occur when read file...");
            }
        }

        private void ExtractFileSelest_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.RestoreDirectory = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string name = fileDialog.FileName;
                FileNameForExtract.Text = name;
            }
        }

        private void ExtractNow_Click(object sender, RoutedEventArgs e)
        {
            if (FileNameForExtract.Text == "" || Expression.Text == "") return;
            FileExtractOutput.Text = "";
            string CodeType = (string)ExterctFileCoding.SelectedValue;

            using (StreamReader Reader = new StreamReader(FileNameForExtract.Text, System.Text.Encoding.GetEncoding(CodeType)))
            {
                string FileContent = Reader.ReadToEnd();
                string Pattern = Expression.Text;
                //string Output = FileContent.ma
                foreach (Match match in Regex.Matches(FileContent, Pattern))
                {
                    FileExtractOutput.Text += match.Value;
                }

            }
        }

        private void MutiCopyGo_Click(object sender, RoutedEventArgs e)
        {
            int Count = 0;
            if (MutiCopyInput.Text == "") return;
            try
            {
                Count = int.Parse(MutiCopyNum.Text);
            }
            catch 
            {
                System.Windows.MessageBox.Show("Wrong input!!");
                return;
            }
            if (Count <= 0) return;

            MutiCopyOutput.Text = "";
            string Temp = "";
            for(int i = 0;i < Count;i ++)
            {
                Temp += MutiCopyInput.Text;
            }

            MutiCopyOutput.Text = Temp;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ZJYC_TOOL_Loaded(object sender, RoutedEventArgs e)
        {
            HwndSource hWndSource;
            WindowInteropHelper wih = new WindowInteropHelper(this);
            hWndSource = HwndSource.FromHwnd(wih.Handle);
            //添加处理程序
            hWndSource.AddHook(MainWindowProc);

            KEY_CSA = HotKey.GlobalAddAtom("Ctrl-Shift-A");
            KEY_CSZ = HotKey.GlobalAddAtom("Ctrl-Shift-Z");
            KEY_CSW = HotKey.GlobalAddAtom("Ctrl-Shift-W");

            HotKey.RegisterHotKey(wih.Handle, KEY_CSA, HotKey.KeyModifiers.Ctrl | HotKey.KeyModifiers.Shift, (int)System.Windows.Forms.Keys.A);
            HotKey.RegisterHotKey(wih.Handle, KEY_CSZ, HotKey.KeyModifiers.Ctrl | HotKey.KeyModifiers.Shift, (int)System.Windows.Forms.Keys.Z);
            HotKey.RegisterHotKey(wih.Handle, KEY_CSW, HotKey.KeyModifiers.Ctrl | HotKey.KeyModifiers.Shift, (int)System.Windows.Forms.Keys.W);

            ComBox_TimeFormat.ItemsSource = new List<string> { "6", "5", "4", "3" };
            ComBox_TimeFormat.SelectedIndex = 0;
            ComBox_CodeCommentFormat.ItemsSource = new List<string> { "blank", "Author", "Time", "Author+Time" };
            ComBox_CodeCommentFormat.SelectedIndex = 0;

            ComBox_FuncCommentFormat_Copy.ItemsSource = new List<string> { "Normal", "Author", "Time", "Author+Time" };
            ComBox_FuncCommentFormat_Copy.SelectedIndex = 0;

            Hex2Num_Length.ItemsSource = new List<string> { "1B", "2B", "4B" };
            Hex2Num_Length.SelectedIndex = 0;

            Hex2Num_Type.ItemsSource = new List<string> { "Unsigned To Hex", "Signed To Hex", "Hex To Unsigned", "Hex To Signed", "Hex To Float","Float To Hex" };
            Hex2Num_Type.SelectedIndex = 0;

            CodingType.ItemsSource = new List<string> { "GBK", "utf-16", "hz-gb-2312", "GB18030", "utf-8", "utf-32", "utf-32BE" };
            CodingType.SelectedIndex = 0;


            ExterctFileCoding.ItemsSource = new List<string> { "GBK", "utf-16", "hz-gb-2312", "GB18030", "utf-8", "utf-32", "utf-32BE" };
            ExterctFileCoding.SelectedIndex = 0;
        }
    }
    class HotKey
    {
        /// <summary>
        /// 如果函数执行成功，返回值不为0。
        /// 如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。.NET方法:Marshal.GetLastWin32Error()
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄</param>
        /// <param name="id">定义热键ID（不能与其它ID重复）  </param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="vk">定义热键的内容,WinForm中可以使用Keys枚举转换，
        /// WPF中Key枚举是不正确的,应该使用System.Windows.Forms.Keys枚举，或者自定义正确的枚举或int常量</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,
            int id,
            KeyModifiers fsModifiers,
            int vk
            );

        /// <summary>
        /// 取消注册热键
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="id">要取消热键的ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,
            int id
            );

        [DllImport("kernel32", SetLastError = true)]
        public static extern short GlobalAddAtom(string lpString);

        [DllImport("kernel32", SetLastError = true)]
        public static extern short GlobalDeleteAtom(short nAtom);

        /// <summary>
        /// 定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
        /// </summary>
        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }
        public const int WM_HOTKEY = 0x312;
    }

}
