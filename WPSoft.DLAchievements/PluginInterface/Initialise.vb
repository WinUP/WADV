﻿Imports WADV.AppCore
Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Public Class Initialise : Implements IInitialise

        Public Function Initialising() As Boolean Implements WADV.AppCore.PluginInterface.IInitialise.Initialising
            If Not My.Computer.FileSystem.FileExists(PathAPI.GetPath(PathType.UserFile, "first_run")) Then Return True
            Properties.Register()
            AchieveAPI.Add(New A_系统启动)
            Return True
        End Function

    End Class

End Namespace
