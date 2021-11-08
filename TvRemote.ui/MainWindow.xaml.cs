using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TVRemote.models; 

namespace TvRemote.ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Tv tv = new Tv();
        static Remote remote = new Remote(tv);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Power_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.Power);
        }

        private void VolumeUp_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.VolumeUp);
        }

        private void VolumeDown_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.VolumeDown); 
        }

        private void ChannelUp_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.ChannelUp);
        }

        private void ChannelDown_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.ChannelDown);
        }

        private void One_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.One);
        }

        private void Two_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.Two);
        }

        private void Three_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.Three);
        }

        private void Four_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.Four);
        }

        private void Five_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.Five);
        }

        private void Six_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.Six);
        }

        private void Seven_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.Seven);
        }

        private void Eight_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.Eight);
        }

        private void Nine_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.Nine);
        }

        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            remote.PressButton(ButtonName.Zero);
        }
    }
}
