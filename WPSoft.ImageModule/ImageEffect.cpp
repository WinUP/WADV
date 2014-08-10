#include "Stdafx.h"
#include "ImageEffect.h"
#include "Config.h"

namespace ImageEffect {

	BaseEffect::BaseEffect(System::String^ fileName, int duration) {
		gcroot<FormatConvertedBitmap^> content = gcnew FormatConvertedBitmap();
		content->BeginInit();
		content->DestinationPalette = BitmapPalettes::WebPaletteTransparent;
		content->DestinationFormat = PixelFormats::Bgra32;
		content->Source = gcnew BitmapImage(gcnew System::Uri(PathAPI::GetPath(PathAPI::Resource(), fileName)));
		content->EndInit();
		width = content->PixelWidth;
		height = content->PixelHeight;
		pixel = gcnew array<unsigned char>(width * height * Config::ImageConfig::BytePerPixel-1);
		content->CopyPixels(pixel, width * Config::ImageConfig::BytePerPixel, 0);
		this->duration = duration;
		length = pixel->Length;
	}

	gcroot<BitmapSource^> BaseEffect::GetNextImageState() {
		complete = true;
		return Config::ImageConfig::ConvertToImage(width, height, pixel);
	}

	int BaseEffect::GetWidth() {
		return width;
	}
	int BaseEffect::GetHeight() {
		return height;
	}
	int BaseEffect::GetDuration() {
		return duration;
	}
	bool BaseEffect::IsComplete() {
		return complete;
	}

	FadeInEffect::FadeInEffect(System::String^ fileName, int duration):BaseEffect(fileName, duration) {
		opacityPerFrame = 255 / duration;
		if (opacityPerFrame < 1) opacityPerFrame = 1;
		int i = 3;
		while (i < pixel->Length) {
			pixel[i] = 0;
			i += 4;
		}
	}

	gcroot<BitmapSource^> FadeInEffect::GetNextImageState() {
		int i = 3;
		while (i < length) {
			if (pixel[i] + opacityPerFrame < 256) pixel[i] += opacityPerFrame;
			else pixel[i] = 255;
			i += 4;
		}
		if (pixel[3] = 255) complete = true;
		return Config::ImageConfig::ConvertToImage(GetWidth(), GetHeight(), pixel);
	}

}