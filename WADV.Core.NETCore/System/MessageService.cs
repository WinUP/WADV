using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using WADV.Core.NETCore.Exception;

namespace WADV.Core.NETCore.System
{
    internal sealed class MessageService
    {
        private static readonly MessageService SingleInstance = new MessageService();
        private readonly Thread _messageLooper;
        private readonly ConcurrentQueue<Message> _messages;
        private readonly char[] _lastMessage;
        private bool _status;

        private MessageService()
        {
            _status = false;
            _messages = new ConcurrentQueue<Message>();
            _lastMessage = new char[64];
            _messageLooper = new Thread(MessageLooperContent)
            {
                Name = "[SYSTEM] Message",
                IsBackground = true
            };
        }

        internal static MessageService Instance()
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
                if (_status) _messageLooper.Start();
            }
        }

        internal void Send(Message message)
        {
            Monitor.Enter(_messages);
            _messages.Enqueue(message);
            Monitor.Pulse(_messages);
            Monitor.Exit(_messages);
        }

        private void MessageLooperContent()
        {
            lock (_messages)
            {
                while (_status)
                {
                    while (!_messages.IsEmpty)
                    {
                        Message message;
                        if (!_messages.TryDequeue(out message))
                        {
                            throw new MessageDequeueFailedException();
                        }
#if DEBUG
                        Debug.WriteLine(DateTime.Now.ToString($"HH:mm:ss,fff {message}"));
#endif
                        Configuration.Receivers.MessageReceivers.Map(message);
                        lock (_lastMessage)
                        {
                            int i;
                            for (i = 0; i < message.Content.Length; i++)
                            {
                                _lastMessage[i] = message.Content[i];
                            }
                            _lastMessage[i] = '\0';
                            Monitor.PulseAll(_lastMessage);
                        }
                    }
                    Monitor.Wait(_messages);
                }
            }
        }
    }
}
