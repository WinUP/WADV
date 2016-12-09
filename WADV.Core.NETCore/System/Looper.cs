using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WADV.Core.NETCore.Configuration;

namespace WADV.Core.NETCore.System
{
    internal class Looper
    {
        private static readonly Looper SingleInstance = new Looper();
        private bool _status;
        private readonly Thread _loopLooper;

        private Looper()
        {
            _loopLooper = new Thread(LoopingContent)
            {
                IsBackground = true,
                Name = "[SYSTEM] Loop"
            };
        }

        internal static Looper Instance()
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
                if (value) _loopLooper.Start();
            }
        }

        /// <summary>
        /// 获取或设置两次逻辑循环间的时间间隔(Tick)
        /// </summary>
        /// <value>新的间隔</value>
        /// <returns></returns>
        /// <remarks>
        ///   1 Tick = 1/10000 MS
        /// </remarks>
        internal int Span { get; set; }

        /// <summary>
        /// 获取当前的帧计数
        /// </summary>
        /// <returns></returns>
        internal int Frames { get; private set; }

        private void LoopingContent()
        {
            dynamic gameWindow = Configuration.System.MainWindow;
            MessageService.Instance().Send(new Message
            {
                Content = "[SYSTEM] LOOP_START",
                Mask = Variable.SystemMessageMask
            });
            while (_status)
            {
                var timeNow = DateTime.Now.Ticks;
                Receivers.UpdateReceivers.Map(Frames);
                Receivers.RenderReceivers.Map(Frames);
                var sleepTime = (int) ((timeNow + Span - DateTime.Now.Ticks)/10000);
                if (sleepTime > 0) Thread.Sleep(sleepTime);
                Frames += 1;
            }
            MessageService.Instance().Send(new Message
            {
                Content = "[SYSTEM] LOOP_ATOP",
                Mask = Variable.SystemMessageMask
            });
        }

    }
}
