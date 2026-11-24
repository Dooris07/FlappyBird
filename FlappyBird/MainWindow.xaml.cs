using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FlappyBird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        double gravity;
        int jumpHeight = 10;
        string[] except = ["birb", "normal", "foggy"];
        Random r = new Random();
        int kimentSzamlalo = 0;
        Image firstTrunk;
        public MainWindow()
        {
            InitializeComponent();
            GameCanvas.Focus();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            Start();
            timer.Tick += Haladas;



        }

        private void Haladas(object? sender, EventArgs e)
        {
            double bribPos = Canvas.GetTop(birb);
            Canvas.SetTop(birb, bribPos + gravity);
            EdgeHitting();
            TrunkMove();
            HitboxCheck();
            if (gravity < jumpHeight)
            {
                gravity++;
                BirdRotation(gravity);
            }
            
        }

        private void BirdRotation(double gravity)
        {
            
            RotateTransform rotation = new RotateTransform(gravity);
            birb.RenderTransform = rotation;
        }

        private void HitboxCheck()
        {
            Rect birbHitbox = new Rect(Canvas.GetLeft(birb) + 5, Canvas.GetTop(birb) + 10, birb.ActualWidth, birb.ActualHeight - 10);

            foreach (Image item in GameCanvas.Children.OfType<Image>())
            {
                if (!except.Contains(item.Tag))
                {
                    Rect trunkHitbox = new Rect(Canvas.GetLeft(item) + 50, Canvas.GetTop(item), item.ActualWidth - 70, item.ActualHeight);
                    if (trunkHitbox.IntersectsWith(birbHitbox))
                    {
                        MessageBox.Show("Game over");

                        timer.Stop();
                    }

                }
            }
        }

        private void jump(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                gravity = -jumpHeight;
            }
        }

        private void EdgeHitting()
        {
            double bribPos = Canvas.GetTop(birb);
            if (bribPos < 0 || bribPos > 400)
            {
                timer.Stop();
                MessageBox.Show("Game Over");

            }
        }

        private void TrunkMove()
        {
            kimentSzamlalo = 0;
            foreach (Image item in GameCanvas.Children.OfType<Image>())
            {
                if (!except.Contains(item.Tag))
                {
                    double posLeft = Canvas.GetLeft(item);

                    if (posLeft < -155)
                    {
                        Canvas.SetLeft(item, 1600);
                        if (kimentSzamlalo == 1)
                        {
                            double botPosTop = Canvas.GetTop(item);
                            double topPosTop = Canvas.GetTop(firstTrunk);
                            int felLeMozgas;
                            do
                            {
                                felLeMozgas = r.Next(-50, 50);
                            }
                            while (topPosTop + felLeMozgas < 20 - item.Height || botPosTop + felLeMozgas > 420);
                            Canvas.SetTop(item, botPosTop + felLeMozgas);
                            Canvas.SetTop(firstTrunk, topPosTop + felLeMozgas);
                            kimentSzamlalo = 0;
                        }
                        else
                        {
                            firstTrunk = item;
                            kimentSzamlalo++;
                        }
                    }
                    else
                    {
                        Canvas.SetLeft(item, posLeft - 5);
                    }
                }
            }
        }



        private void Start()
        {
            foreach (var item in GameCanvas.Children)
            {
                Image trunk = item as Image;
                switch ((trunk).Tag)
                {
                    case "trunk1":
                        Canvas.SetLeft(trunk, 800);
                        break;
                    case "trunk2":
                        Canvas.SetLeft(trunk, 1150);
                        break;
                    case "trunk3":
                        Canvas.SetLeft(trunk, 1500);
                        break;
                    case "trunk4":
                        Canvas.SetLeft(trunk, 1850);
                        break;
                    case "trunk5":
                        Canvas.SetLeft(trunk, 2200);
                        break;
                }

            }
            foreach (Image item in GameCanvas.Children.OfType<Image>())
            {
                if (!except.Contains(item.Tag))
                {
                    double posLeft = Canvas.GetLeft(item);

                    if (kimentSzamlalo == 1)
                    {
                        double botPosTop = Canvas.GetTop(item);
                        double topPosTop = Canvas.GetTop(firstTrunk);
                        int felLeMozgas;
                        do
                        {
                            felLeMozgas = r.Next(-80, 80);
                        }
                        while (topPosTop + felLeMozgas < 20 - item.Height || botPosTop + felLeMozgas > 420);
                        Canvas.SetTop(item, botPosTop + felLeMozgas);
                        Canvas.SetTop(firstTrunk, topPosTop + felLeMozgas);
                        kimentSzamlalo = 0;
                    }
                    else
                    {
                        firstTrunk = item;
                        kimentSzamlalo++;
                    }


                }
            }
            timer.Start();
        }


    }
}