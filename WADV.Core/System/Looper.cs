using System;
using System.Threading;
using WADV.Core.Configuration;

namespace WADV.Core.System {
    internal class Looper {
        private static readonly Looper SingleInstance = new Looper();
        private bool _status;
        private readonly Thread _loopLooper;

        private Looper() {
            _loopLooper = new Thread(LoopingContent) {
                IsBackground = true,
                Name = "[SYSTEM] Looper"
            };
        }

        /// <summary>
        /// Instance of Looper
        /// </summary>
        /// <returns></returns>
        internal static Looper Instance() {
            return SingleInstance;
        }

        /// <summary>
        /// Get or set status of Looper
        /// </summary>
        internal bool Status {
            get { return _status; }
            set {
                if (value == _status) return;
                _status = value;
                if (value) _loopLooper.Start();
            }
        }

        /// <summary>
        /// Get or set ticks between two loop
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///   1 Tick = 1/10000 MS
        /// </remarks>
        internal int Span { get; set; }

        /// <summary>
        /// Get frame count after game started
        /// </summary>
        /// <returns></returns>
        internal int Frames { get; private set; }

        private void LoopingContent() {
            var gameWindow = Variable.MainWindow;
            MessageService.Instance().Send(new Message {
                Content = "LOOP_START",
                Mask = Variable.SystemMessageMask
            });
            while (_status) {
                var timeNow = DateTime.Now.Ticks;
                Receivers.UpdateReceivers.Map(Frames);
                gameWindow.RunRenderMapper(Receivers.RenderReceivers.Map);
                var sleepTime = (int) ((timeNow + Span - DateTime.Now.Ticks) / 10000);
                if (sleepTime > 0) Thread.Sleep(sleepTime);
                Frames++;
            }
            MessageService.Instance().Send(new Message {
                Content = "LOOP_STOP",
                Mask = Variable.SystemMessageMask
            });
        }
    }
}