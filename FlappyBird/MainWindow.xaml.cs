using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FlappyBird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        double gravity = 8;
        public MainWindow()
        {
            InitializeComponent();
            GameCanvas.Focus();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += Haladas;
            timer.Start();
        }

        private void Haladas(object? sender, EventArgs e)
        {
            double bribPos = Canvas.GetTop(birb);
            Canvas.SetTop(birb, bribPos + gravity);
        }

        private void jump(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                gravity = -8;
            }
        }

        private void jumpEnd(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                gravity = 8;
            }
        }
    }
}