using TVRemote.models;
using TVRemote.repositories;
using System.Threading;

namespace TVRemote
{
    class Program
    {
        static void Main(string[] args)
        {
            var tv = new Tv();
            var remote = new Remote(tv);
            remote.PressButton(ButtonName.Power);
            remote.PressButton(ButtonName.VolumeUp);
            remote.PressButton(ButtonName.ChannelDown);
           
            for (int i =25; i>0; i--)
            {
                remote.PressButton(ButtonName.ChannelUp);
            }
           
            remote.PressButton(ButtonName.ChannelUp);
            remote.PressButton(ButtonName.ChannelDown);
            remote.PressButton(ButtonName.ChannelDown);
            remote.PressButton(ButtonName.ChannelUp);
            remote.PressButton(ButtonName.Five);
            Thread.Sleep(1000);
            remote.PressButton(ButtonName.Three);
            Thread.Sleep(1000);
            remote.PressButton(ButtonName.VolumeUp);
            remote.PressButton(ButtonName.Four);
            Thread.Sleep(1000);

            remote.PressButton(ButtonName.Two);
   
            Thread.Sleep(4000);
            remote.PressButton(ButtonName.Nine);
            Thread.Sleep(4000);
            remote.PressButton(ButtonName.Six);
           

        }
    }
}
