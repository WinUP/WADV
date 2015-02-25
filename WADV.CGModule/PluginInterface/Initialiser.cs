using System;
using WADV.AppCore.API;
using WADV.AppCore.PluginInterface;
using WADV.CGModule.API;

namespace WADV.CGModule.PluginInterface
{
    internal sealed class Initialiser : IInitialise
    {

        public bool Initialising()
        {
            ScriptAPI.RegisterInTableSync("api_cg", "init", new Action<int>(ConfigAPI.Init), true);
            ScriptAPI.RegisterInTableSync("api_cg", "show", new Func<string, string, int, string, bool>(ImageAPI.Show));
            ScriptAPI.RegisterInTableSync("api_cg", "clean", new Func<string, bool>(ImageAPI.Clean));
            MessageAPI.SendSync("[CG]INIT_LOAD_FINISH");
            return true;
        }

    }
}
