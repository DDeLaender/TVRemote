using System;
using System.Collections.Generic;
using System.Text;
using TVRemote.models;
using TVRemote.repositories;

namespace TvRemote.FakeRepositories
{
    public class FakeRegistryRepository : BaseRepository
    {
        DateTime currentTime = DateTime.Now;

        public struct RegistryItem
        {
            public ButtonName ButtonName;
            public ButtonType ButtonType;
            public DateTime Time;

            public RegistryItem(ButtonName buttonName, ButtonType buttonType, DateTime time)
            {
                this.ButtonName = buttonName;
                this.ButtonType = buttonType;
                this.Time = time;
            }  
        }
        public List<RegistryItem> registry = new List<RegistryItem> { };

        public override string TableName
        {
            get
            {
                return "Registry";
            }
        }

        public void RegisterButtonClick(ButtonType buttonType, ButtonName buttonName)
        {
            Console.WriteLine("Registry ButtonClick");
            registry.Add(new RegistryItem(buttonName, buttonType, currentTime));
        }

        public List<int> NumbersClicked()
        {
            Console.WriteLine("fakeRepository Start");
            List<int> numbers = new List<int>();
            foreach(RegistryItem item in registry)
            {
                Console.WriteLine($"name is {item.ButtonName}");
                var timeDifference = item.Time - currentTime;
                int milliseconds = (int)timeDifference.TotalMilliseconds;
                if (milliseconds<= 3000 && item.ButtonType == ButtonType.Number)
                {
                    numbers.Add((int)item.ButtonName);
                }
            }
            return numbers;
        }
        public void AddTime(int milliseconds)
        {
            currentTime.AddMilliseconds(milliseconds);
        }
        public override void CreateRepository(){ }
    }
}
       
