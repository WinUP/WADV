using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;
using WADV.Core.Exception;

namespace WADV.Core.System {
    internal sealed class MessageService {
        private static readonly MessageService SingleInstance = new MessageService();
        private readonly Thread _messageLooper;
        private readonly ConcurrentQueue<Message> _messages;
        private readonly char[] _lastMessage;
        private bool _status;

        private MessageService() {
            _status = false;
            _messages = new ConcurrentQueue<Message>();
            _lastMessage = new char[64];
            _messageLooper = new Thread(MessageLooperContent) {
                Name = "[SYSTEM] Messenger",
                IsBackground = true
            };
        }

        /// <summary>
        /// Instance of Message service
        /// </summary>
        /// <returns></returns>
        internal static MessageService Instance() {
            return SingleInstance;
        }

        /// <summary>
        /// Get or set status of Message service
        /// </summary>
        internal bool Status {
            get { return _status; }
            set {
                if (value == _status) return;
                _status = value;
                if (_status) _messageLooper.Start();
            }
        }

        /// <summary>
        /// Send a new message
        /// </summary>
        /// <param name="message"></param>
        internal void Send(Message message) {
            Monitor.Enter(_messages);
            _messages.Enqueue(message);
            Monitor.Pulse(_messages);
            Monitor.Exit(_messages);
        }

        private void MessageLooperContent() {
            lock (_messages) {
                while (_status) {
                    while (!_messages.IsEmpty) {
                        if (!_messages.TryDequeue(out var message)) {
                            throw GameException.New(ExceptionType.MessageDequeueFailed)
                                .Value("Message", GameException.NoValue)
                                .At("MessageService")
                                .How("Deque message failed")
                                .Save();
                        }
#if DEBUG
                        Debug.WriteLine(
                            DateTime.Now.ToString(
                                $"HH:mm:ss,fff[{message.Mask}] {message.Content}: {message.Parameter.ToString()} & {message.NumericalParameter}"));
#endif
                        Configuration.Receivers.MessageReceivers.Map(message);
                        lock (_lastMessage) {
                            int i;
                            for (i = 0; i < message.Content.Length; i++) {
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