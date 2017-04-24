using System;
using WADV.Core.RAL;
using WADV.Core.System;
using WADV.Core.Utility;
using WADV.Core.Receiver;

namespace WADV.Core.Configuration {
    /// <summary>
    /// Receivers for game system
    /// </summary>
    internal class Receivers {
        internal static ReceiverList<IMessager, Message> MessageReceivers;
        internal static ReceiverList<IUpdater, int> UpdateReceivers;
        internal static ReceiverList<IRenderer, int> RenderReceivers;
        internal static ReceiverList<INavigator, NavigationParameter> NavigateReceivers;
        internal static ReceiverList<IModuleLoader, ReferenceValue<Type[]>> ModuleLoadReceivers;
        internal static ReceiverList<IGameInitializer, int> GameInitializeReceivers;
        internal static ReceiverList<IGameDestructor, CancelEventArgs> GameDestructReceivers;

        private static bool MessageMapper(ReceiverList<IMessager, Message> list, IMessager receiver, Message parameter) {
            if ((parameter.Mask & receiver.Mask) == 0) return false;
            if (!receiver.Receive(parameter, out var abort)) list.Delete(receiver);
            return abort;
        }

        private static bool UpdateMapper(ReceiverList<IUpdater, int> list, IUpdater receiver, int parameter) {
            if (!receiver.Update(parameter, out var abort)) list.Delete(receiver);
            return abort;
        }

        private static bool RenderMapper(ReceiverList<IRenderer, int> list, IRenderer receiver, int parameter) {
            if (!receiver.Render(parameter, out var abort)) list.Delete(receiver);
            return abort;
        }

        private static bool NavigateMapper(ReceiverList<INavigator, NavigationParameter> list, INavigator receiver,
            NavigationParameter parameter) {
            if (!receiver.Navigate(parameter, out var abort)) list.Delete(receiver);
            return abort;
        }

        private static bool ModuleLoadMapper(ReceiverList<IModuleLoader, ReferenceValue<Type[]>> list, IModuleLoader receiver,
            ReferenceValue<Type[]> parameter) {
            if (!receiver.Initialize(parameter, out var abort)) list.Delete(receiver);
            return abort;
        }

        private static bool GameInitializeMapper(ReceiverList<IGameInitializer, int> list, IGameInitializer receiver,
            int parameter) {
            if (!receiver.Initialize(out var abort)) list.Delete(receiver);
            return abort;
        }

        private static bool GameDestructMapper(ReceiverList<IGameDestructor, CancelEventArgs> list,
            IGameDestructor receiver, CancelEventArgs parameter) {
            if (!receiver.Destruct(parameter, out var abort)) list.Delete(receiver);
            return abort;
        }

        internal static void Initialize() {
            MessageReceivers?.Clear();
            UpdateReceivers?.Clear();
            RenderReceivers?.Clear();
            NavigateReceivers?.Clear();
            ModuleLoadReceivers?.Clear();
            GameInitializeReceivers?.Clear();
            GameDestructReceivers?.Clear();
            MessageReceivers = new ReceiverList<IMessager, Message>(MessageMapper);
            UpdateReceivers = new ReceiverList<IUpdater, int>(UpdateMapper);
            RenderReceivers = new ReceiverList<IRenderer, int>(RenderMapper);
            NavigateReceivers = new ReceiverList<INavigator, NavigationParameter>(NavigateMapper);
            ModuleLoadReceivers = new ReceiverList<IModuleLoader, ReferenceValue<Type[]>>(ModuleLoadMapper);
            GameInitializeReceivers = new ReceiverList<IGameInitializer, int>(GameInitializeMapper);
            GameDestructReceivers = new ReceiverList<IGameDestructor, CancelEventArgs>(GameDestructMapper);
        }
    }
}