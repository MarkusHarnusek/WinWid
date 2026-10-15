using System.Windows;

namespace WinWid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Config.Config Config { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Visibility = Visibility.Hidden;
            Config = new Config.Config();

            if (Config.ClockConfig.Enable)
            {
                Clock clock = new Clock(Config.ClockConfig);
                clock.Show();
                clock.Visibility = Visibility.Visible;
            }
        }
    }
}