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
        double acceleration = 0;
        double space;
        public MainWindow()
        {
            InitializeComponent();
            GameCanvas.Focus();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += Haladas;
            Start();
            
        }

        private void Haladas(object? sender, EventArgs e)
        {
            double bribPos = Canvas.GetTop(birb);
            Canvas.SetTop(birb, bribPos + gravity + acceleration);
            acceleration += 0.5;
            EdgeHitting(sender, e);
            TrunkMove(sender, e);

        }

        private void jump(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                gravity = -15;
                acceleration = 0;
                RotateTransform rotateUp = new RotateTransform(-10);
                birb.RenderTransform = rotateUp; 
            }
        }

        private void jumpEnd(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                gravity = 8;
                RotateTransform rotateDown = new RotateTransform(10);
                birb.RenderTransform = rotateDown;
            }
        }

        private void EdgeHitting(object sender, EventArgs e)
        {
            double bribPos = Canvas.GetTop(birb);
            if (bribPos < 0 || bribPos > 400)
            {
                timer.Stop();
                MessageBox.Show("Game Over");

            }
        }

        private void TrunkMove(object sender, EventArgs e)
        {
            foreach (var item in GameCanvas.Children)
            {
                if (item is Image && (string)(item as Image).Tag == "trunk1" && (string)(item as Image).Tag == "trunk2" && (string)(item as Image).Tag == "trunk3")
                {
                    Image trunk = item as Image;
                    double pos = Canvas.GetLeft(trunk);
                    if (pos < -100)
                    {
                        Canvas.SetLeft(trunk, 800);
                    }
                    else
                    {
                        Canvas.SetLeft(trunk, pos - 5);
                    }
                }
            }
        }



        private void Start()
        {
            foreach (var item in GameCanvas.Children)
            {
                Image trunk = item as Image;
                switch ((string)(trunk).Tag)
                {
                    case "trunk1":
                        Canvas.SetLeft(trunk, 800);
                        break;
                    case "trunk2":
                        Canvas.SetLeft(trunk, 1200);
                        break;
                    case "trunk3":
                        Canvas.SetLeft(trunk, 1600);
                        break;
                }
                timer.Start();
            }

        }


    }
}