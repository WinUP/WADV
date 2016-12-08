using System;
using WADV.Core.NETCore.RAL;
using WADV.Core.NETCore.Receiver;
using WADV.Core.NETCore.System;
using WADV.Core.NETCore.Utility;

namespace WADV.Core.NETCore.Configuration
{
    internal class Receivers
    {
        internal static ReceiverList<IMessager, Message> MessageReceivers;
        internal static ReceiverList<ILooper, int> LoopReceivers;
        internal static ReceiverList<IRenderer, int> RenderReceivers;
        internal static ReceiverList<INavigator, NavigationParameter> NavigateReceivers;
        internal static ReceiverList<IModuleLoader, Type[]> ModuleLoadReceivers;
        internal static ReceiverList<IGameInitializer, int> GameInitializeReceivers;
        internal static ReceiverList<IGameDestructor, CancelEventArgs> GameDestructReceivers;

        private static bool MessageMapper(ReceiverList<IMessager, Message> list, IMessager receiver, Message parameter)
        {
            return (parameter.Mask & receiver.Mask) == 0 || receiver.Receive(parameter);
        }
        private static bool LoopMapper(ReceiverList<ILooper, int> list, ILooper receiver, int parameter)
        {
            return receiver.Loop(parameter);
        }
        private static bool RenderMapper(ReceiverList<IRenderer, int> list, IRenderer receiver, int parameter)
        {
            return receiver.Render(parameter);
        }
        private static bool NavigateMapper(ReceiverList<INavigator, NavigationParameter> list, INavigator receiver, NavigationParameter parameter)
        {
            return receiver.Navigate(parameter);
        }
        private static bool ModuleLoadMapper(ReceiverList<IModuleLoader, Type[]> list, IModuleLoader receiver, Type[] parameter)
        {
            return receiver.Initialize(parameter);
        }
        private static bool GameInitializeMapper(ReceiverList<IGameInitializer, int> list, IGameInitializer receiver, int parameter)
        {
            return receiver.Initialize();
        }
        private static bool GameDestructMapper(ReceiverList<IGameDestructor, CancelEventArgs> list, IGameDestructor receiver, CancelEventArgs parameter)
        {
            return receiver.Destruct(parameter);
        }

        internal static void Prepare()
        {
            MessageReceivers?.Clear();
            LoopReceivers?.Clear();
            RenderReceivers?.Clear();
            NavigateReceivers?.Clear();
            ModuleLoadReceivers?.Clear();
            GameInitializeReceivers?.Clear();
            GameDestructReceivers?.Clear();
            MessageReceivers = new ReceiverList<IMessager, Message>(MessageMapper);
            LoopReceivers = new ReceiverList<ILooper, int>(LoopMapper);
            RenderReceivers = new ReceiverList<IRenderer, int>(RenderMapper);
            NavigateReceivers = new ReceiverList<INavigator, NavigationParameter>(NavigateMapper);
            ModuleLoadReceivers = new ReceiverList<IModuleLoader, Type[]>(ModuleLoadMapper);
            GameInitializeReceivers = new ReceiverList<IGameInitializer, int>(GameInitializeMapper);
            GameDestructReceivers = new ReceiverList<IGameDestructor, CancelEventArgs>(GameDestructMapper);
        }
    }
}
