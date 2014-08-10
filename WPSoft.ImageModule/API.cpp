#include "Stdafx.h"
#include "API.h"
using namespace System::Collections::Generic;


namespace API {

	bool ImageAPI::Show(System::String^ fileName, System::String^ effectName, int duration, System::String^ contentName) {
		/*Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Name = effectName AndAlso tmpClass.Namespace = "WADV.ImageModule.ImageEffect" Select tmpClass
			If classList.Count < 1 Then Return False
			Dim effect As ImageEffect.BaseEffect = Activator.CreateInstance(classList.FirstOrDefault, New Object() {fileName, duration})
			Dim content = WindowAPI.GetChildByName(Of Panel)(WindowAPI.GetWindow, contentName)
			If content Is Nothing Then Return False
			Dim loopContent As New PluginInterface.ImageLoop(effect, content)
			LoopingAPI.AddLoop(loopContent)
			LoopingAPI.WaitLoop(loopContent)
			Return True*/
		array<System::Type^>^ classList = System::Reflection::Assembly::GetCallingAssembly()->GetTypes();
		System::Type^ effectType;
		for each (System::Type^ var in classList)
			if (var->Name == effectName && var->Namespace == L"") effectType = var;
		if (!effectType) return false;
		BaseEffect effect = (BaseEffect)System::Activator::CreateInstance(effectType, gcnew array < Object^ > {fileName, duration});
	}

}