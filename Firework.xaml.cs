using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Cake.UWP
{
    public sealed partial class Firework : UserControl
    {
        public Firework()
        {
            InitializeComponent();
        }

        public void Explode()
        {
            Opacity = 1;
            ExplosionStoryboard.Completed += ExplosionStoryboard_Completed;
            ExplosionStoryboard.Seek(TimeSpan.Zero);
            ExplosionStoryboard.Begin();
        }

        private void ExplosionStoryboard_Completed(object sender, object e)
        {
            ExplosionStoryboard.Completed -= ExplosionStoryboard_Completed;
            Opacity = 0;
        }
    }
}
