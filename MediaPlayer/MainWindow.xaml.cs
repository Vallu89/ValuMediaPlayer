using Microsoft.Win32;
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
using System.Windows.Threading;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer time;
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            if (time != null)
                time.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            player.Pause();
            if (time != null)
                time.Stop();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            player.Stop();
            if (time != null)
                time.Stop();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                player.Source = new Uri(ofd.FileName);
                
                player.Play();
                

            }
            else
            {
                MessageBox.Show("Nie zaznaczono pliku");
            }

        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Value = player.Position.TotalSeconds;
        }

        private void player_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }

        private void player_MediaOpened(object sender, RoutedEventArgs e)
        {
            timer.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
            time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += timer_Tick;
            totalTimeOfWatchedVideo.Text = player.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
            if (time != null)
                time.Start();
            
        }

        private void timer_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            player.Position = TimeSpan.FromSeconds(timer.Value);


        }
    }
}
