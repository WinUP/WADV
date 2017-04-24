using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using WADV.Core.API;
using WADV.Core.Attribute;
using WADV.Core.Exception;
using WADV.Core.NETCore.Module;
using WADV.Core.Receiver;
using WADV.Core.System;
using WADV.Core.Utility;

namespace WADV.Core.Module {
    internal class Loader {
        private static readonly List<string> LoadedList = new List<string>();

        /// <summary>
        /// Load a module from file
        /// </summary>
        /// <param name="name">Module name</param>
        internal static void LoadModule(string name) {
            var location = Location.Combine(Location.Type.Module, $"{name}/{name}.dll");
            if (LoadedList.Contains(location)) return;
            if (!File.Exists(location)) {
                throw GameException.New(ExceptionType.FileCannotFound)
                    .Value("Path", location)
                    .At("Module.Loader")
                    .How("Cannot find target module")
                    .Save();
            }
            LoadAssembly(AssemblyLoadContext.Default.LoadFromAssemblyPath(location), location);
        }

        /// <summary>
        /// Load a module from stream
        /// </summary>
        /// <param name="target">Module data</param>
        /// <param name="location">Module's location</param>
        internal static void LoadModule(Stream target, string location) {
            if (LoadedList.Contains(location)) return;
            LoadAssembly(AssemblyLoadContext.Default.LoadFromStream(target), location);
        }

        private static void LoadAssembly(Assembly assembly, string location) {
            var types =
                Configuration.Receivers.ModuleLoadReceivers.Map(new ReferenceValue<Type[]>(assembly.GetTypes())).Value;
            foreach (var type in types
                .Where(e => e.GetTypeInfo().GetCustomAttribute<AutoInitializeAttribute>() != null)) {
                var info = type.GetTypeInfo();
                if (info.GetInterface("WADV.Core.Module.IModuleInitializer") != null)
                    if (!((IModuleInitializer) Activator.CreateInstance(type)).Initialize()) {
                        var exception = GameException.New(ExceptionType.PluginInitializeFailed)
                            .At("Module.Loader")
                            .Value("Path", location)
                            .How("Plugin rejected its loading progress")
                            .Save();
                        if (Configuration.Variable.ThrowPluginInitializeFailed) throw exception;
                        return;
                    }
                if (info.GetInterface("WADV.Core.Receiver.IGameInitializer") != null) {
                    Configuration.Receivers.GameInitializeReceivers.AddToRoot(
                        (IGameInitializer) Activator.CreateInstance(type));
                }
                if (info.GetInterface("WADV.Core.Receiver.IGameDestructor") != null) {
                    Configuration.Receivers.GameDestructReceivers.AddToRoot(
                        (IGameDestructor) Activator.CreateInstance(type));
                }
                if (info.GetInterface("WADV.Core.Receiver.IMessager") != null) {
                    Configuration.Receivers.MessageReceivers.AddToRoot(
                        (IMessager) Activator.CreateInstance(type));
                }
                if (info.GetInterface("WADV.Core.Receiver.IModuleLoader") != null) {
                    Configuration.Receivers.ModuleLoadReceivers.AddToRoot(
                        (IModuleLoader) Activator.CreateInstance(type));
                }
                if (info.GetInterface("WADV.Core.Receiver.INavigator") != null) {
                    Configuration.Receivers.NavigateReceivers.AddToRoot(
                        (INavigator) Activator.CreateInstance(type));
                }
                if (info.GetInterface("WADV.Core.Receiver.IRenderer") != null) {
                    Configuration.Receivers.RenderReceivers.AddToRoot(
                        (IRenderer) Activator.CreateInstance(type));
                }
                if (info.GetInterface("WADV.Core.Receiver.IUpdater") != null) {
                    Configuration.Receivers.UpdateReceivers.AddToRoot(
                        (IUpdater) Activator.CreateInstance(type));
                }
            }
            LoadedList.Add(location);
            MessageService.Instance().Send(new Message {
                Content = "MODULE_LOADED",
                Mask = Configuration.Variable.SystemMessageMask
            });
        }
    }
}