#include "Stdafx.h"
#include "PluginInterface.h"
#include "Config.h"

namespace PluginInterface {

	bool Initialise::StartInitialising() {
		Config::ImageConfig::ReadConfig();
		return true;
	}

	void Script::StartRegisting(LuaInterface::Lua^ ScriptVM) {
		ScriptAPI::RegisterFunction(System::Reflection::Assembly::GetExecutingAssembly()->GetTypes(), "API", "SIM");
	}

	bool Loop::StartLooping() {
		return true;
	}
	
	void Loop::StartRendering(System::Windows::Window^ window) {

	}

}