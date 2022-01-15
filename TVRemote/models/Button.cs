using System;
using TVRemote.repositories;
using TVRemote.Configuration;
using TvRemote.FakeRepositories;

namespace TVRemote.models
{
    class Button
    {
        public ButtonName Name { get; set; }

        public ButtonType Type { get; set; }

        public dynamic Registry
        {
            get
            {
                if (Configuration.Configuration.mode == Mode.Production)
                {
                    return new RegistryRepository();
                }
                else
                {
                    return new FakeRegistryRepository();
                }
            }
        }

        public Button (ButtonName name, ButtonType type)
        {
            Name = name;
            Type = type;
        }

        public void Press()
        {
            Console.WriteLine("Registering Button Click");
            Registry.RegisterButtonClick(Type, Name);
        }
    }
}
