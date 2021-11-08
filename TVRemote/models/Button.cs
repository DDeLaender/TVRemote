using System;

namespace TVRemote.models
{
    class Button
    {
        public ButtonName Name { get; set; }

        public ButtonType Type { get; set; }

        public DateTime PressTime { get; set; }

        public Button (ButtonName name, ButtonType type)
        {
            Name = name;
            Type = type;
        }

        public void Press()
        {
            PressTime = DateTime.Now;
        }
    }
}
