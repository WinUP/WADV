using System.Threading;
using WADV.Core.NETCore.Configuration;

namespace WADV.Core.NETCore.System
{
    public class Timer
    {
        private static readonly Timer SingleInstance = new Timer();
        private readonly Thread _timerLooper;
        private bool _status;

        private Timer()
        {
            _timerLooper = new Thread(TimerLoopContent)
            {
                Name = "[SYSTEM] Timer",
                IsBackground = true
            };
        }

        internal static Timer Instance()
        {
            return SingleInstance;
        }

        internal bool Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (value == _status) return;
                _status = value;
                if (value) _timerLooper.Start();
            }
        }
        internal int Span { get; set; }

        private void TimerLoopContent()
        {
            while (_status)
            {
                MessageService.Instance().Send(new Message
                {
                    Content = "",
                    Mask = Variable.SystemMessageMask
                });
                Thread.Sleep(Span);
            }
        }
    }
}
