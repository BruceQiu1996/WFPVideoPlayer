using System;
using System.Collections.Generic;
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

namespace VideoPlayer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_tick);
        }
        void timer_tick(object sender,EventArgs e)
        {
            Progress.Value = Media.Position.TotalSeconds;
        }
        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            Media.Play();
        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            Media.Pause();
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            Media.Stop();
        }

        private void Voice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Media.Volume = (double)Voice.Value;
        }

        private void Progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Media.Position = TimeSpan.FromSeconds(Progress.Value);
        }

        private void drop1_Drop(object sender, DragEventArgs e)
        {
            string filename = (string)((DataObject)e.Data).GetFileDropList()[0];
            Media.Source = new Uri(filename);
            Media.LoadedBehavior = MediaState.Manual;
            Media.UnloadedBehavior = MediaState.Manual;
            Media.Volume= (double)Voice.Value;
            Media.Play();
        }

        private void Media_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = Media.NaturalDuration.TimeSpan;
            Progress.Maximum = ts.TotalSeconds;
            timer.Start();
        }
    }
}
