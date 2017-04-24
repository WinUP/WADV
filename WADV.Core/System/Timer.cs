using System.Threading;
using WADV.Core.Configuration;

namespace WADV.Core.System {
    /// <summary>
    /// Timer
    /// </summary>
    public class Timer {
        private static readonly Timer SingleInstance = new Timer();
        private readonly Thread _timerLooper;
        private bool _status;

        private Timer() {
            _timerLooper = new Thread(TimerLoopContent) {
                Name = "[SYSTEM] Timer",
                IsBackground = true
            };
        }

        /// <summary>
        /// Instance of Timer
        /// </summary>
        /// <returns></returns>
        internal static Timer Instance() {
            return SingleInstance;
        }

        /// <summary>
        /// Get or set status of TImer
        /// </summary>
        internal bool Status {
            get { return _status; }
            set {
                if (value == _status) return;
                _status = value;
                if (value) _timerLooper.Start();
            }
        }

        /// <summary>
        /// Get or set timespan between two tick (new value will be set only after next tick)
        /// </summary>
        internal int Span { get; set; }

        private void TimerLoopContent() {
            while (_status) {
                MessageService.Instance().Send(new Message {
                    Content = "TICK",
                    Mask = Variable.SystemMessageMask
                });
                Thread.Sleep(Span);
            }
        }
    }
}