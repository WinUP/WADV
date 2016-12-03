using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WADV.Core.NETCore.System
{
    internal sealed class MessageService
    {
        private static readonly MessageService SingleInstance = new MessageService();
        private readonly Thread _messageLooper;
        private readonly ConcurrentQueue<Message> _messages;
        private bool _status;

        private MessageService()
        {
            _status = false;
            _messages = new ConcurrentQueue<Message>();
            _messageLooper = new Thread(MessageLooperContent)
            {
                Name = "[SYSTEM] Message Service",
                IsBackground = true
            };
        }

        public static MessageService Instance()
        {
            return SingleInstance;
        }

        private void MessageLooperContent()
        {
            Message message;
            lock (_messages)
            {
                while (_status)
                {
                    if (!_messages.TryDequeue(out message))
                    {
                        
                    }
#if DEBUG
                    Debug.WriteLine(DateTime.Now.ToString($"HH:mm:ss,fff {message}"));
#endif

                }
            }



//            Dim message As Message = Nothing
//            SyncLock(_list)
//                While _Status
//                    While Not _list.IsEmpty
//                        If Not _list.TryDequeue(message) Then Throw New Exception.MessageDequeueFailedException
//#If DEBUG Then
//                        Debug.WriteLine(Date.Now.ToString($"HH:mm:ss,fff {message}"))
//#End If
//                        Configuration.Receiver.MessageReceiver.Broadcast(message)
//                        SyncLock LastMessage
//                            For i = 0 To message.Content.Length - 1
//                                LastMessage(i) = message.Content(i)
//                            Next
//                            For i = message.Content.Length To 49
//                                LastMessage(i) = Nothing
//                            Next
//                            Monitor.PulseAll(LastMessage)
//                        End SyncLock
//                    End While
//                    Monitor.Wait(_list)
//                End While
//            End SyncLock
        }
    }
}
