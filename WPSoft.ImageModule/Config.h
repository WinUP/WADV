using namespace System::Xml;
using namespace System::Windows::Media;
using namespace System::Windows::Media::Imaging;
using namespace WADV::AppCore::API;

namespace Config {
	
	public ref class ImageConfig {
	public:
		static void ReadConfig();
		static void WriteConfig();
		static property int DPI {
			int get() { return _dpi; }
			void set(int value) {
				_dpi = value;
				WriteConfig();
			}
		}
		static property int BytePerPixel {
			int get() { return _bytePerPixel; }
			void set(int value) {
				_bytePerPixel = value;
				WriteConfig();
			}
		}
		static BitmapSource^ ConvertToImage(int width, int height, array<unsigned char>^ pixel);
	private:
		static int _bytePerPixel;
		static int _dpi;
	};
}