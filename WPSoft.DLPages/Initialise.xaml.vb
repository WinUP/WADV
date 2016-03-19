Imports System.Security.AccessControl
Imports WADV.Core.Enumeration

Class Initialise
    Private Sub Initialise_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim image As New ImageBrush(New BitmapImage(API.Path.CombineUri(PathType.Resource, "image\black_logo.png")))
        image.Stretch = Stretch.UniformToFill
        image.TileMode = TileMode.None
        image.Viewbox = New Rect(0, 0, 1, 1)
        image.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox
        CenterImage.Background = image
        Dim tmpThread As New System.Threading.Thread(AddressOf DoInitialise)
        tmpThread.IsBackground = False
        tmpThread.Name = "游戏初始化线程"
        tmpThread.Priority = System.Threading.ThreadPriority.AboveNormal
        tmpThread.Start()
    End Sub

    Private Sub ChangeText(message As String)
        Dispatcher.Invoke(Sub() MessageText.Text = message)
    End Sub

    Private Sub DoInitialise()
        ChangeText("检查科学法则")
        Dim access = IO.Directory.GetAccessControl(API.Path.UserFile).GetAccessRules(True, True, GetType(System.Security.Principal.NTAccount)).OfType(Of FileSystemAccessRule)()


        '!检查文件夹权限
        ChangeText("扫描居民和生态系统")
        '!检查文件完整性
        ChangeText("启动承载系统")
        '!导入成就
        For Each tmpProperty In {"游戏运行次数", "CG显示次数"}
            WADV.WPF.AchievementModule.Extension.Property.Add(tmpProperty)
        Next
        ChangeText("已就绪")
        System.Threading.Thread.Sleep(200)
        Message.Send("[GAME]INITIAL_DATA_READY")
    End Sub
End Class
