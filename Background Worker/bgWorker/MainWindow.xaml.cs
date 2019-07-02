using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace bgWorker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region properties
        private readonly BackgroundWorker bgWorker;
        #endregion

        #region constructor
        public MainWindow()
        {
            InitializeComponent();
            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += DoWorkMethod;
            bgWorker.ProgressChanged += BgWorkerProgressChanged;
            bgWorker.RunWorkerCompleted += RunWorkerCompleted;
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerAsync();
        }
        #endregion

        #region methods
        public void DoWorkMethod(object sender,DoWorkEventArgs e)
        {
            for (int i = 1; i <=10; i++)
            {
                if(bgWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                System.Threading.Thread.Sleep(1000);
                bgWorker.ReportProgress(i * 10);
            }
        }

        public void RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e)
        {
            if (!(e.Cancelled))
            {
                MessageBox.Show("Working");
            }
            else
            {
                this.pgBar.Visibility = Visibility.Hidden;
            }
        }

        //this event will be raised only when we call the report progress method
        //this will report the progress of an asynchronous operation to the user
        public void BgWorkerProgressChanged(object sender,ProgressChangedEventArgs e)
        {
            this.pgBar.Value = e.ProgressPercentage;
        }


        #endregion

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            bgWorker.CancelAsync();
        }
    }
}
