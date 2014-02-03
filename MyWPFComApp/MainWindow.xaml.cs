/******************************************************************************/
/*                                                                            */
/*   Program: MyWPFComApp                                                     */
/*   Example for using serial comports                                        */
/*                                                                            */
/*   17.1.2014 0.0.0.0 uhwgmxorg Start                                        */
/*                                                                            */
/******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace MyWPFComApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void TheComThreadMessageHandler(object sender, SerialDataReceivedEventArgs e);

        private SerialPort serialPort;

        /// <summary>
        /// Properties with 
        /// OnPropertyChanged
        /// </summary>
        private string receiveString;
        public string ReceiveString { get { return receiveString; } set { receiveString = value; OnPropertyChanged("ReceiveString"); } }
        private string sendString;
        public string SendString { get { return sendString; } set { sendString = value; OnPropertyChanged("SendString"); } }

        /// <summary>
        /// Properties
        /// <summary>
        public virtual Dispatcher DispatcherObject { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            ReceiveString = "";
            SendString = "";

            InitComInterface();
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// button_ClearReceive_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ClearReceive_Click(object sender, RoutedEventArgs e)
        {
            ReceiveString = "";
        }

        /// <summary>
        /// button_ClearSent_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ClearSent_Click(object sender, RoutedEventArgs e)
        {
            SendString = "";
        }

        /// <summary>
        /// button_Sent_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Sent_Click(object sender, RoutedEventArgs e)
        {
            serialPort.Write(SendString);
        }

        /// <summary>
        /// button_Close_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        #endregion

        /******************************/
        /*      Menu Events           */
        /******************************/
        #region Menu Events

        #endregion

        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        /// <summary>
        /// DataReceivedHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            // Attention! thread-context-change
            if (DispatcherObject.Thread != Thread.CurrentThread)
            {
                DispatcherObject.Invoke(new TheComThreadMessageHandler(DataReceivedHandler), DispatcherPriority.ApplicationIdle, sender, e);
            }
            else
            {
                SerialPort sp = (SerialPort)sender;
                string InData = sp.ReadExisting();
                ReceiveString += InData;
            }
        }

        #endregion

        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// OnPropertyChanged
        /// </summary>
        /// <param name="p"></param>
        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        /// <summary>
        /// InitComInterface
        /// </summary>
        private void InitComInterface()
        {
            var p = Properties.Settings.Default;
            try
            {
                DispatcherObject = Dispatcher.CurrentDispatcher;
                serialPort = new SerialPort(p.COMPort);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                serialPort.BaudRate = 9600;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;
                serialPort.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("The specified COM port ("+p.COMPort+") is not available. Please take a different!\n");
                MessageBox.Show(e.ToString());
            }
        }

        #endregion
    }
}
