Imports System.Collections.ObjectModel
Imports System.Windows.Controls
Imports System.Windows.Media.Animation
Imports Neo.IronLua
Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Public Class PluginInitialise : Implements IPluginInitialise
        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            Dim target As New Core.Script.Field With {.Name = "achievement"}
            target.Content.Add("ready", New Action(Of String, String)(AddressOf Extension.Plugin.Ready))
            target.Content.Add("style", New Func(Of String, Border)(AddressOf Extension.Plugin.Style))
            target.Content.Add("animation", New Func(Of String, Storyboard)(AddressOf Extension.Plugin.Animation))
            target.Content.Add("item", New Core.Script.Field With {.Name = "item"})
            Dim childTarget = target.Content.Item("item")
            childTarget.Content.Add("new", New Func(Of String, String, Func(Of LuaResult), Func(Of LuaResult), Achievement)(AddressOf Extension.ExtensionForScript.Item_New))
            childTarget.Content.Add("add", New Action(Of Achievement)(AddressOf Extension.Item.Add))
            childTarget.Content.Add("get", New Func(Of String, Achievement)(AddressOf Extension.Item.Get))
            childTarget.Content.Add("delete", New Action(Of String)(AddressOf Extension.Item.Delete))
            childTarget.Content.Add("list", New Func(Of ReadOnlyCollection(Of Achievement))(AddressOf Extension.Item.List))
            childTarget.Content.Add("save", New Action(AddressOf Extension.Item.Save))
            childTarget.Content.Add("load", New Action(AddressOf Extension.Item.Load))
            target.Content.Add("property", New Core.Script.Field With {.Name = "property"})
            childTarget = target.Content.Item("property")
            childTarget.Content.Add("add", New Action(Of String)(AddressOf Extension.Property.Add))
            childTarget.Content.Add("get", New Func(Of String, Integer)(AddressOf Extension.Property.Get))
            childTarget.Content.Add("set", New Action(Of String, Integer)(AddressOf Extension.Property.Set))
            childTarget.Content.Add("addData", New Action(Of String, Integer)(AddressOf Extension.Property.AddData))
            childTarget.Content.Add("register", New Action(Of String, Achievement)(AddressOf Extension.Property.Register))
            childTarget.Content.Add("registerByName", New Action(Of String, String)(AddressOf Extension.Property.Register))
            childTarget.Content.Add("unregister", New Action(Of String, Achievement)(AddressOf Extension.Property.Unregister))
            childTarget.Content.Add("unregisterByName", New Action(Of String, String)(AddressOf Extension.Property.Unregister))
            childTarget.Content.Add("delete", New Action(Of String)(AddressOf Extension.Property.Delete))
            childTarget.Content.Add("save", New Action(AddressOf Extension.Property.Save))
            childTarget.Content.Add("load", New Action(AddressOf Extension.Property.Load))
            Script.RegisterField(target)
            Return True
        End Function
    End Class
End Namespace