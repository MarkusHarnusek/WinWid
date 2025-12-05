using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace WinWid
{
    /// <summary>
    /// Interaction logic for Clock.xaml
    /// </summary>
    public partial class Clock : Window
    {
        private Config.ClockConfig config;

        public Clock(Config.ClockConfig config)
        {
            this.config = config;
            InitializeComponent();
            TransformToPos();
        }

        /// <summary>
        /// Gets the width and height of the screen where this window is currently displayed
        /// </summary>
        /// <returns>A tuple containing the screen width and height in pixels</returns>
        private (double Width, double Height) GetCurrentScreenDimensions()
        {
            var source = PresentationSource.FromVisual(this);
            if (source?.CompositionTarget == null)
            {
                return (SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
            }

            var transformToDevice = source.CompositionTarget.TransformToDevice;
            var screenBounds = SystemParameters.WorkArea;

            double screenWidth = screenBounds.Width * transformToDevice.M11;
            double screenHeight = screenBounds.Height * transformToDevice.M22;

            return (screenWidth, screenHeight);
        }

        /// <summary>
        /// Moves the clock window to the desired position
        /// </summary>
        private void TransformToPos()
        {
            double width = GetCurrentScreenDimensions().Width;
            double height = GetCurrentScreenDimensions().Height;

            Width = width / config.WidthRatio;
            Height = height/ config.HeightRatio;
            Left = ( width - Width ) / 2;
            Top = height / config.TopOffsetRatio;
        }
    }
}
