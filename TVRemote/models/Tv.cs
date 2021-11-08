using System;
using System.Collections.Generic;

namespace TVRemote.models
{
    public class Tv
    {
        // fields
        private bool _powered = false;
        private int _currentVolume;
        private int _currentChannel = 1;
        private readonly int _minVolume = 0;
        private readonly int _maxVolume = 60;
        private readonly int _maxChannel = 999;
        private readonly int _minChannel = 1;
        private List<string> _messages = new List<string> { };

        // properties
        public string Messages
        {
            get
            {
                var retrievedMessages = String.Join("\n", _messages.ToArray());
                _messages.Clear();
                return retrievedMessages;
            }
            set
            {
                _messages.Add(value);
            }
        }
        public bool Powered
        {
            get => _powered;
            private set
            {
                if (value == true)
                {
                    _currentVolume = 20;
                    _powered = value;
                    Messages += $"Tv on\nVolume:{_currentVolume}\nChannel:{_currentChannel}";
                }
                else
                {
                    _powered = value;
                    Messages += "Tv Off";
                    return;
                }
            }
        }
        public int CurrentVolume
        {
            get
            {
                return _currentVolume;
            }
            set
            {
                if (value > _maxVolume || value < _minVolume)
                {
                    Messages += $"Volume:{_currentVolume}";
                    return;
                }
                else
                {
                    _currentVolume = value;
                    Messages += $"Volume:{_currentVolume}";
                }

            }
        }
        public int CurrentChannel
        {
            get
            {
                return _currentChannel;
            }
            set
            {
                if (value > _maxChannel)
                {
                    _currentChannel = _minChannel;
                }
                else if (value < _minChannel)
                {
                    _currentChannel = _maxChannel;
                }
                else
                {
                    _currentChannel = value;
                }
                Messages += $"Channel:{_currentChannel}";
            }
        }

        // constructor
        public Tv()
        {

        }

        // methods
        public void SwitchPower()
        {
            Powered = !Powered;
        }

        public void VolumeUp()
        {
            CurrentVolume += 1;
        }

        public void VolumeDown()
        {
            CurrentVolume -= 1;
        }

        public void ChannelUp()
        {
            CurrentChannel += 1;
        }

        public void ChannelDown()
        {
            CurrentChannel -= 1;
        }

        public void GoToChannel(int channel)
        {
            CurrentChannel = channel;
        }

    }
}
