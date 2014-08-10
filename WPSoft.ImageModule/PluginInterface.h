using namespace WADV::AppCore::API;
using namespace WADV::AppCore::Plugin;

namespace PluginInterface {

	public ref class Initialise : public IInitialise {
	public:
		virtual bool StartInitialising();
	};

	public ref class Script : public IScriptFunction {
	public:
		virtual void StartRegisting(LuaInterface::Lua^ ScriptVM);
	};

	public ref class Loop : public ILooping {
	public:
		virtual bool StartLooping();
		virtual void StartRendering(System::Windows::Window^ window);
	};

}