#include <vcclr.h>
using namespace System::Windows;
using namespace System::Windows::Media;
using namespace System::Windows::Media::Imaging;
using namespace WADV::AppCore::API;

namespace ImageEffect {

	public class BaseEffect {
	public:
		BaseEffect(System::String^ fileName, int duration);
		int GetWidth();
		int GetHeight();
		int GetDuration();
		bool IsComplete();
		virtual gcroot<BitmapSource^> GetNextImageState();
	private:
		int duration;
		int width;
		int height;
	protected:
		gcroot<array<unsigned char>^> pixel;
		bool complete;
		int length;
	};

	public class FadeInEffect : public BaseEffect
	{
	public:
		FadeInEffect(System::String^ fileName, int duration);
		gcroot<BitmapSource^> GetNextImageState();
	private:
		int opacityPerFrame;
	};

}