using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Cake.UWP
{
    public sealed partial class MainPage : Page
    {
        private int _flashTicks;
        private uint _numCandles = 1;
        private bool _flashingAndFireworks = false;
        private bool _candlesBurning = false;

        private SolidColorBrush _blackBrush = new SolidColorBrush(Colors.Black);
        private SolidColorBrush _redBrush = new SolidColorBrush(Colors.Red);
        private SolidColorBrush _greenBrush = new SolidColorBrush(Colors.Green);
        private SolidColorBrush _blueBrush = new SolidColorBrush(Colors.Blue);
        private SolidColorBrush[] _candleColors = new SolidColorBrush[3];

        private DispatcherTimer _textFlashingTimer = new DispatcherTimer();
        private DispatcherTimer _candleFlickerTimer = new DispatcherTimer();
        private DispatcherTimer _fireworksTimer = new DispatcherTimer();

        public MainPage()
        {
            _candleColors[0] = _redBrush;
            _candleColors[1] = _greenBrush;
            _candleColors[2] = _blueBrush;

            this.InitializeComponent();

            _textFlashingTimer.Interval = TimeSpan.FromSeconds(1);
            _textFlashingTimer.Tick += TextFlashingTimer_Tick;

            _candleFlickerTimer.Interval = TimeSpan.FromMilliseconds(16);
            _candleFlickerTimer.Tick += CandleFlickerTimer_Tick;

            CandleCanvas.Children.Add(new Candle());

            UpdateStateFromSettings();
        }

        private void ToggleBurningButton_Checked(object sender, RoutedEventArgs e)
        {
            _candlesBurning = true;
            UpdateBurningState();
        }

        private void ToggleBurningButton_Unchecked(object sender, RoutedEventArgs e)
        {
            _candlesBurning = false;
            UpdateBurningState();
        }

        private void UpdateBurningState()
        {
            if (!_candlesBurning)
            {
                foreach (var candle in CandleCanvas.Children.OfType<Candle>())
                {
                    _candleFlickerTimer.Stop();
                    candle.SnuffOut();
                }
            }
            else
            {
                _candleFlickerTimer.Start();
            }
        }

        private void CandleFlickerTimer_Tick(object sender, object e)
        {
            foreach (var candle in CandleCanvas.Children.OfType<Candle>())
            {
                candle.TickFlicker();
            }
        }

        private void Flyout_Opened(object sender, object e)
        {
            CakeTextBox.Text = CakeText.Text;
            NumberOfCandlesBox.Text = _numCandles.ToString();
            FireworksCheckbox.IsChecked = _flashingAndFireworks;
        }

        private void ApplySettingsButton_Click(object sender, RoutedEventArgs e)
        {
            CakeText.Text = CakeTextBox.Text;

            if (uint.TryParse(NumberOfCandlesBox.Text, out uint newCandles))
            {
                _numCandles = newCandles;
            }

            _flashingAndFireworks = FireworksCheckbox.IsChecked.Value;

            UpdateStateFromSettings();
            SettingsFlyout.Hide();
        }

        private void UpdateStateFromSettings()
        {
            UpdateNumCandles(_numCandles);
            UpdateTextFlashing(_flashingAndFireworks);
            UpdateFireworks(_flashingAndFireworks);
        }

        private void UpdateNumCandles(uint numCandles)
        {
            CandleCanvas.Children.Clear();
            if (numCandles == 0)
            {
                return;
            }

            _candleFlickerTimer.Stop();
            _fireworksTimer.Stop();


            uint spacePerCandle = 540u / numCandles;

            for (int i = 0; i < numCandles; i++)
            {
                var brush = _candleColors[i % 3];
                var candle = new Candle { CandlestickColor = brush };
                Canvas.SetLeft(candle, (spacePerCandle / 2) + (i * spacePerCandle) - 8);
                if (i % 2 == 0)
                {
                    Canvas.SetTop(candle, -5);
                }
                else
                {
                    Canvas.SetTop(candle, 5);
                }
                CandleCanvas.Children.Add(candle);
            }

            if (_candlesBurning) { _candleFlickerTimer.Start(); }
            if (_flashingAndFireworks) { _fireworksTimer.Start(); }
        }

        private void UpdateTextFlashing(bool flashingAndFireworks)
        {
            if (flashingAndFireworks && !_textFlashingTimer.IsEnabled)
            {
                _textFlashingTimer.Start();
            }
            else if (!flashingAndFireworks && _textFlashingTimer.IsEnabled)
            {
                _textFlashingTimer.Stop();
            }
        }

        private void TextFlashingTimer_Tick(object sender, object e)
        {
            CakeText.Foreground = _flashTicks % 2 == 0 ? _blackBrush : _redBrush;
            _flashTicks++;
        }

        private void UpdateFireworks(bool flashingAndFireworks)
        {
            //throw new NotImplementedException();
        }
    }
}
