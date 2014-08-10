#include "Stdafx.h"
#include "Config.h"

namespace Config {

	void ImageConfig::ReadConfig() {
		XmlDocument^ file = gcnew XmlDocument();
		file->Load(PathAPI::GetPath(PathAPI::UserFile(), L"WADV.ImageModule.xml"));
		_bytePerPixel = System::Convert::ToInt32(file->SelectSingleNode(L"/config/bytePerPixel")->InnerXml);
		_dpi = System::Convert::ToInt32(file->SelectSingleNode(L"/config/dpi")->InnerXml);
	}

	void ImageConfig::WriteConfig() {
		XmlDocument^ file = gcnew XmlDocument();
		file->Load(PathAPI::GetPath(PathAPI::UserFile(), L"WADV.ImageModule.xml"));
		file->SelectSingleNode(L"/config/bytePerPixel")->InnerXml = _bytePerPixel.ToString();
		file->SelectSingleNode(L"/config/dpi")->InnerXml = _dpi.ToString();
		file->Save(PathAPI::GetPath(PathAPI::UserFile(), L"WADV.ImageModule.xml"));
	}

	delegate BitmapSource^ ConvertDelegate(int width, int height, array<unsigned char>^ pixel);
	BitmapSource^ ConvertFunction(int width, int height, array<unsigned char>^ pixel) {
		return BitmapSource::Create(width, height, ImageConfig::DPI, ImageConfig::DPI, PixelFormats::Bgra32, BitmapPalettes::WebPaletteTransparent, pixel, width * ImageConfig::BytePerPixel);
	}

	BitmapSource^ ImageConfig::ConvertToImage(int width, int height, array<unsigned char>^ pixel) {
		ConvertDelegate^ tmpDelegate = gcnew ConvertDelegate(ConvertFunction);
		return (BitmapSource^)WindowAPI::GetDispatcher()->Invoke(tmpDelegate);
	}

}