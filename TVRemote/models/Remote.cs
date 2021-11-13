using System;
using System.Collections.Generic;
using System.Threading;

namespace TVRemote.models
{
    class NumberHistoryChecker
    {

        //properties
        private Tv TvSelected { get; set; }
        private List<Button> NumberHistory { get; set; }
        private int ExpectedCount { get; set; }
        private int WaitTime { get; set; }
        private int ChannelNumber
        {
            get
            {
                string channelNumber = "";
                foreach (Button numberButton in NumberHistory)
                {
                    channelNumber += numberButton.Name.ToString("D");
                }
                return Int32.Parse(channelNumber);
            }
        }

        // constructor
        public NumberHistoryChecker(List<Button> numberHistory, Tv tvSelected, int expectedCount, int waitTime)
        {
            NumberHistory = numberHistory;
            TvSelected = tvSelected;
            ExpectedCount = expectedCount;
            WaitTime = waitTime;
        }

        //methods
        public void ThreadProc()
        {
            Thread.Sleep(WaitTime); // 3 sec in milliseconds
            
            if (NumberHistory.Count == ExpectedCount)
            {
                TvSelected.GoToChannel(ChannelNumber);
                NumberHistory.Clear();
            }
        }
    }
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

        //properties
        public Tv TvSelected { get; set; }
        private List<Button> NumberHistory = new List<Button> { };
        private List<Thread> AllThreads = new List<Thread> { };
        public int WaitTime { get; set; }


        //constructor
        public Remote(Tv tv)
        {
            TvSelected = tv;
            WaitTime = 3000; // 3 sec
        }

        //methods

        private int RunningThreadCount()
        {
            int threadCount = 0;
            foreach (Thread thread in AllThreads)
            {
                if (thread.IsAlive)
                {
                    threadCount += 1;
                }
            }
            return threadCount;
        }

        public void PressButton(ButtonName buttonName)
        {
            var button = _buttons[buttonName];

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
           
            if (RunningThreadCount() >0 && button.Type != ButtonType.Number)
            {
                Console.WriteLine("Evaluating previous number input. You are unable to press any buttons except number buttons.");
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

                    NumberHistory.Add(button);

                    if (NumberHistory.Count > 3)
                    {
                        TvSelected.GoToChannel(999);
                        NumberHistory.Clear();
                    }
                    else if (NumberHistory.Count == 1)
                    {
                        var numberHistoryChecker = new NumberHistoryChecker(NumberHistory, TvSelected, 1,WaitTime);
                        var thread1 = new Thread(new ThreadStart(numberHistoryChecker.ThreadProc));
                        thread1.Name = "ThreadOne";
                        thread1.Start();
                        AllThreads.Add(thread1);

                    }
                    else if (NumberHistory.Count == 2)
                    {
                        var numberHistoryChecker = new NumberHistoryChecker(NumberHistory, TvSelected, 2, WaitTime);
                        var thread2 = new Thread(new ThreadStart(numberHistoryChecker.ThreadProc));
                        thread2.Name = "ThreadTwo";
                        thread2.Start();
                        AllThreads.Add(thread2);

                    }
                    else
                    {
                        var numberHistoryChecker = new NumberHistoryChecker(NumberHistory, TvSelected, 3, WaitTime);
                        var thread3 = new Thread(new ThreadStart(numberHistoryChecker.ThreadProc));
                        thread3.Name = "ThreadThree";
                        thread3.Start();
                        AllThreads.Add(thread3);
                    }
                    break;
                default:
                    break;


            }
        }
    }
}
