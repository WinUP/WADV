#include "Config.h"
#include "ImageEffect.h"
using namespace Config;
using namespace ImageEffect;
using namespace WADV::AppCore::API;

namespace API {

	public ref class ImageAPI
	{
	public:
		static bool Show(System::String^ fileName, System::String^ effectName, int duration, System::String^ contentName);
	};
}