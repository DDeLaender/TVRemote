using System;
using System.Collections.Generic;
using TVRemote.repositories;
using TVRemote.Configuration;
using TvRemote.FakeRepositories;

namespace TVRemote.models
{ 
    public class Remote
    {
        //field
        private readonly Dictionary<ButtonName, Button> _buttons = new Dictionary<ButtonName, Button>
        {
            {ButtonName.Power, new Button(ButtonName.Power, ButtonType.Power)},
            {ButtonName.VolumeUp, new Button(ButtonName.VolumeUp,ButtonType.Volume)},
            {ButtonName.VolumeDown, new Button(ButtonName.VolumeDown, ButtonType.Volume)},
            {ButtonName.ChannelUp, new Button (ButtonName.ChannelUp, ButtonType.Channel)},
            {ButtonName.ChannelDown, new Button (ButtonName.ChannelDown, ButtonType.Channel)},
            {ButtonName.Zero, new Button (ButtonName.Zero, ButtonType.Number)},
            {ButtonName.One, new Button (ButtonName.One, ButtonType.Number)},
            {ButtonName.Two, new Button (ButtonName.Two, ButtonType.Number)},
            {ButtonName.Three, new Button (ButtonName.Three, ButtonType.Number)},
            {ButtonName.Four, new Button (ButtonName.Four, ButtonType.Number)},
            {ButtonName.Five, new Button (ButtonName.Five, ButtonType.Number)},
            {ButtonName.Six, new Button (ButtonName.Six, ButtonType.Number)},
            {ButtonName.Seven, new Button (ButtonName.Seven, ButtonType.Number)},
            {ButtonName.Eight, new Button (ButtonName.Eight, ButtonType.Number)},
            {ButtonName.Nine, new Button (ButtonName.Nine, ButtonType.Number)}
        };
        public dynamic Registry 
        {
            get 
            { 
                if(Configuration.Configuration.mode == Mode.Production)
                {
                    return new RegistryRepository();
                }
                else
                {
                    return new FakeRegistryRepository();
                }
            }
        }

        //properties
        public Tv TvSelected { get; set; }
        

        //constructor
        public Remote(Tv tv)
        {
            TvSelected = tv;
        }

        //methods
        public void PressButton(ButtonName buttonName)
        {
            var button = _buttons[buttonName];
            int[] subset;

            button.Press();

            if (button.Type == ButtonType.Power)
            {
                TvSelected.SwitchPower();
                return;
            }

            if (!TvSelected.Powered)
            {
                return;
            }
            
            switch (button.Type)
            {
                case ButtonType.Volume:
                    if (button.Name == ButtonName.VolumeUp)
                    {
                        TvSelected.VolumeUp();
                    }
                    else
                    {
                        TvSelected.VolumeDown();
                    }
                    break;
                case ButtonType.Channel:
                    if (button.Name == ButtonName.ChannelUp)
                    {
                        TvSelected.ChannelUp();
                    }
                    else
                    {
                        TvSelected.ChannelDown();
                    }
                    break;
                case ButtonType.Number:
                    List<int> numbers = Registry.NumbersClicked();
                    
                    if ( numbers.Count >=3)
                    {
                        subset = numbers.GetRange(0, 3).ToArray();
                    }
                    else
                    {
                        subset = numbers.GetRange(0, numbers.Count).ToArray();
                    }
                    TvSelected.GoToChannel(Int32.Parse(string.Join("", subset)));
                    break;
                default:
                    break;
            }
        }
    }
}
