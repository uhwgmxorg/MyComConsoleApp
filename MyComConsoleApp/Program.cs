/******************************************************************************/
/*                                                                            */
/*   Program: MyComConsoleApp                                                 */
/*   Just a smal example for a C# console application with nice colorers      */
/*                                                                            */
/*   17.1.2014 1.0.0.0 uhwgmxorg Start                                        */
/*                                                                            */
/******************************************************************************/
using System;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyComConsoleApp
{
    class Program
    {
        public delegate void TheComThreadMessageHandler(object sender, SerialDataReceivedEventArgs e);

        private static string COMPort { get; set; }

        private static string Command1 = "§K10";
        private static string Command2 = "§F00";
        private static string Command3 = "§F01";
        private static string Command4 = "§W0x";
        private static string Command5 = "§Gyy";
        private static string Command6 = "§???";

        private static string CommandEnd = "\r\n";

        private static string ReceiveString { get; set; }
        private static string SendString { get; set; }
        private static SerialPort serialPort;

        /// <summary>
        /// PrintMenu
        /// </summary>
        static void PrintMenu()
        {
            string Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ScreenOutput(0, 0, "Program MyComConsoleApp Version " + Version, ConsoleColor.Cyan);

            int X = 30, Y = 3;
            ScreenOutput(X, Y + 0, "1 Command " + Command1);
            ScreenOutput(X, Y + 1, "2 Command " + Command2);
            ScreenOutput(X, Y + 2, "3 Command " + Command3);
            ScreenOutput(X, Y + 3, "4 Command " + Command4);
            ScreenOutput(X, Y + 4, "5 Command " + Command5);
            ScreenOutput(X, Y + 5, "6 Command " + Command6);
            ScreenOutput(X, Y + 6, "7 Send Hallo");
            ScreenOutput(X, Y + 7, "c Clear Recieve Buffer");
            ScreenOutput(X, Y + 8, "p Print Parameters");
            ScreenOutput(X, Y + 9, "x For Exit");
            ScreenOutput(X, Y + 11, "press a key for choice", ConsoleColor.Green);

            ComCallBack(null, null);
        }

        /// <summary>
        /// SendCommand1
        /// </summary>
        private static void SendCommand1()
        {
            ClearScreen();
            ScreenOutput(30, 3, "Send Command1: " + Command1);

            SendString = Command1 + CommandEnd;
            serialPort.Write(SendString);

            ScreenOutput(30, 8, "press any key to continue", ConsoleColor.Green);
            Console.ReadKey();
            ClearScreen();
        }

        /// <summary>
        /// SendCommand2
        /// </summary>
        private static void SendCommand2()
        {
            ClearScreen();
            ScreenOutput(30, 3, "Send Command2: " + Command2);

            SendString = Command2 + CommandEnd;
            serialPort.Write(SendString);

            ScreenOutput(30, 8, "press any key to continue", ConsoleColor.Green);
            Console.ReadKey();
            ClearScreen();
        }

        /// <summary>
        /// SendCommand3
        /// </summary>
        private static void SendCommand3()
        {
            ClearScreen();
            ScreenOutput(30, 3, "Send Command3: " + Command3);

            SendString = Command3 + CommandEnd;
            serialPort.Write(SendString);

            ScreenOutput(30, 8, "press any key to continue", ConsoleColor.Green);
            Console.ReadKey();
            ClearScreen();
        }

        /// <summary>
        /// SendCommand4
        /// </summary>
        private static void SendCommand4()
        {
            ClearScreen();
            ScreenOutput(30, 3, "Send Command4: " + Command4);

            SendString = Command4 + CommandEnd;
            serialPort.Write(SendString);

            ScreenOutput(30, 8, "press any key to continue", ConsoleColor.Green);
            Console.ReadKey();
            ClearScreen();
        }

        /// <summary>
        /// SendCommand5
        /// </summary>
        private static void SendCommand5()
        {
            ClearScreen();
            ScreenOutput(30, 3, "Send Command5: " + Command5);

            SendString = Command5 + CommandEnd;
            serialPort.Write(SendString);

            ScreenOutput(30, 8, "press any key to continue", ConsoleColor.Green);
            Console.ReadKey();
            ClearScreen();
        }

        /// <summary>
        /// SendCommand6
        /// </summary>
        private static void SendCommand6()
        {
            ClearScreen();
            ScreenOutput(30, 3, "Send Command6: " + Command6);

            SendString = Command6 + CommandEnd;
            serialPort.Write(SendString);

            ScreenOutput(30, 8, "press any key to continue", ConsoleColor.Green);
            Console.ReadKey();
            ClearScreen();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void SendSomthing()
        {
            ClearScreen();
            string SS = "Hallo";
            ScreenOutput(30, 3, "Send " + SS);

            SendString = SS;
            serialPort.Write(SendString);

            ScreenOutput(30, 8, "press any key to continue", ConsoleColor.Green);
            Console.ReadKey();
            ClearScreen();
        }

        /// <summary>
        /// PrintParams
        /// </summary>
        private static void PrintParams()
        {
            ClearScreen();

            int X = 30, Y = 3;
            ScreenOutput(X, Y + 0, "Settings:");
            ScreenOutput(X, Y + 2, "COMPort   " + COMPort);
            ScreenOutput(X, Y + 3, "BaudRate  " + serialPort.BaudRate.ToString());
            ScreenOutput(X, Y + 4, "Parity    " + serialPort.Parity.ToString());
            ScreenOutput(X, Y + 5, "StopBits  " + serialPort.StopBits.ToString());
            ScreenOutput(X, Y + 6, "DataBits  " + serialPort.DataBits.ToString());
            ScreenOutput(X, Y + 7, "Handshake " + serialPort.Handshake.ToString());

            ScreenOutput(30, Y + 9, "press any key to continue", ConsoleColor.Green);
            Console.ReadKey();
            ClearScreen();
        }

        /// <summary>
        /// MenuDispatcher
        /// </summary>
        /// <param name="key"></param>
        private static void MenuDispatcher(char key)
        {
            switch (key)
            {
                case '1':
                    SendCommand1();
                    break;
                case '2':
                    SendCommand2();
                    break;
                case '3':
                    SendCommand3();
                    break;
                case '4':
                    SendCommand4();
                    break;
                case '5':
                    SendCommand5();
                    break;
                case '6':
                    SendCommand6();
                    break;
                case '7':
                    SendSomthing();
                    break;
                case 'c':
                    ReceiveString = "";
                    ClearScreen();
                    ComCallBack(null, null);
                    break;
                case 'p':
                    PrintParams();
                    break;
                case 'x':
                    break;
                default:
                    ClearScreen();
                    Console.Beep();
                    Console.Beep();
                    ScreenOutput(28, 5, "You pressed a wrong key !!", ConsoleColor.Red, ConsoleColor.White);
                    Console.ReadKey();
                    ClearScreen();
                    break;
            }
        }

        /// <summary>
        /// ScreenOutput
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        static void ScreenOutput(int x, int y, string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
            Console.ResetColor();
        }
        static void ScreenOutput(int x, int y, string text, ConsoleColor foregroundColor)
        {
            ScreenOutput(x, y, text, foregroundColor, ConsoleColor.Black);
        }
        static void ScreenOutput(int x, int y, string text)
        {
            ScreenOutput(x, y, text, ConsoleColor.Gray);
        }
        static void ScreenOutput(string text)
        {
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// ClearScreen
        /// </summary>
        static void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// InitComInterface
        /// </summary>
        private static void InitComInterface()
        {
            try
            {
                serialPort = new SerialPort(COMPort);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(ComCallBack);
                serialPort.BaudRate = 9600;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;
                serialPort.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("The specified COM port (" + COMPort + ") is not available. Please take a different!\n");
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// ComCallBack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static void ComCallBack(object sender, SerialDataReceivedEventArgs e)
        {
            int x = 15,y = 17, len = 46;
            if (sender != null)
            {
                SerialPort sp = (SerialPort)sender;
                string InData = sp.ReadExisting();
                ReceiveString += InData;
            }
            ReceiveString = ReceiveString.Replace("\r", "*").Replace("\n", "*").Replace("\t", "*");
            if (ReceiveString.Length > len - 3)
                ReceiveString = ReceiveString.Substring(ReceiveString.Length - (len-3));
            ScreenOutput(x, y + 0, "+-- Receive Buffer ---------------------------+");
            ScreenOutput(x, y + 1, "|"); ScreenOutput(x + len, y + 1, "|");
            ScreenOutput(x, y + 2, "|"); ScreenOutput(x + 2,   y + 2, ReceiveString); ScreenOutput(x + len, y + 2, "|");
            ScreenOutput(x, y + 3, "|"); ScreenOutput(x + len, y + 3, "|");
            ScreenOutput(x, y + 4, "+---------------------------------------------+");
        }

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ConsoleKeyInfo KeyInfo;
            char Key;

            var p = Properties.Settings.Default;
            COMPort = p.COMPort;
            ReceiveString = "";
            SendString = "";
            InitComInterface();

            ClearScreen();
            do
            {
                PrintMenu();

                KeyInfo = Console.ReadKey();
                Key = KeyInfo.KeyChar;

                MenuDispatcher(Key);
            }
            while (Key != 'x');

            ScreenOutput("\nEnd.");
        }
    }
}
