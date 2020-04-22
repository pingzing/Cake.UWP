using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Cake.UWP
{
    public sealed partial class Candle : UserControl
    {
        private uint _flickerTicks;

        private SolidColorBrush _candlestickColor = new SolidColorBrush(Colors.Red);
        public SolidColorBrush CandlestickColor
        {
            get => _candlestickColor;
            set
            {
                Candlestick.Fill = value;
            }
        }

        public Candle()
        {
            this.InitializeComponent();
        }

        public void TickFlicker()
        {
            YellowLeft.Opacity = 0;
            RedLeft.Opacity = 0;
            YellowCenter.Opacity = 0;
            RedCenter.Opacity = 0;
            YellowRight.Opacity = 0;
            RedRight.Opacity = 0;

            if (_flickerTicks % 3 == 0)
            {
                YellowLeft.Opacity = 1;
                RedLeft.Opacity = 1;
            }
            else if (_flickerTicks % 3 == 1)
            {
                YellowCenter.Opacity = 1;
                RedCenter.Opacity = 1;
            }
            else if (_flickerTicks % 3 == 2)
            {
                YellowRight.Opacity = 1;
                RedRight.Opacity = 1;
            }
            _flickerTicks++;
        }

        public void SnuffOut()
        {
            YellowLeft.Opacity = 0;
            RedLeft.Opacity = 0;
            YellowCenter.Opacity = 0;
            RedCenter.Opacity = 0;
            YellowRight.Opacity = 0;
            RedRight.Opacity = 0;
            _flickerTicks = 0;
        }
    }
}
