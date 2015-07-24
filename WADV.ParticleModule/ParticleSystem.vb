Imports System.Windows
Imports System.Windows.Shapes
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports WADV.Core.API
Imports WADV.ParticleModule.ParticleModel

''' <summary>
''' 粒子系统
''' </summary>
Public NotInheritable Class ParticleSystem : Inherits DependencyObject
    Public Shared ReadOnly ParticlePositionProperty As DependencyProperty = DependencyProperty.Register("ParticlePosition", GetType(Point), GetType(ParticleSystem))
    Public Shared ReadOnly ParticleLifeProperty As DependencyProperty = DependencyProperty.Register("ParticleLife", GetType(Integer), GetType(ParticleSystem))
    Public Shared ReadOnly ParticleSpeedProperty As DependencyProperty = DependencyProperty.Register("ParticleSpeed", GetType(Vector), GetType(ParticleSystem))
    Public Shared ReadOnly ParticleVelocityProperty As DependencyProperty = DependencyProperty.Register("ParticleVelocity", GetType(Vector), GetType(ParticleSystem))
    Private ReadOnly _particleList As New List(Of NormalParticleModel) '生存的粒子
    Private ReadOnly _useEllipse As Boolean '是否使用原型粒子
    Private _renderImage As RenderTargetBitmap '渲染用图像
    Private _renderTarget As Shape '渲染用图形
    Private _element As Panel '绑定到的界面元素
    Private _runnable As Boolean '是否可以继续运行
    Private _surplusFrame As Integer '距离下次产生粒子的帧数
    Private _random As New Random(Date.Now.Millisecond)

    ''' <summary>
    ''' 粒子初始坐标，这是一个依赖项属性
    ''' </summary>
    ''' <returns></returns>
    Public Property ParticlePosition As Point
        Get
            Return GetValue(ParticlePositionProperty)
        End Get
        Set(value As Point)
            SetValue(ParticlePositionProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 粒子初始生存帧数，这是一个依赖项属性
    ''' </summary>
    ''' <returns></returns>
    Public Property ParticleLife As Integer
        Get
            Return GetValue(ParticleLifeProperty)
        End Get
        Friend Set(value As Integer)
            SetValue(ParticleLifeProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 粒子初始速度，这是一个依赖项属性
    ''' </summary>
    ''' <returns></returns>
    Public Property ParticleSpeed As Vector
        Get
            Return GetValue(ParticleSpeedProperty)
        End Get
        Protected Friend Set(value As Vector)
            SetValue(ParticleSpeedProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 粒子初始加速度，这是一个依赖项属性
    ''' </summary>
    ''' <returns></returns>
    Public Property ParticleVelocity As Vector
        Get
            Return GetValue(ParticleVelocityProperty)
        End Get
        Protected Friend Set(value As Vector)
            SetValue(ParticleVelocityProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 生成粒子的最大大小
    ''' </summary>
    ''' <returns></returns>
    Public Property MaxSize As Vector

    ''' <summary>
    ''' 生成粒子的最小大小
    ''' </summary>
    ''' <returns></returns>
    Public Property MinSize As Vector

    ''' <summary>
    ''' 最多同时维持的粒子数
    ''' </summary>
    ''' <returns></returns>
    Public Property MaxCount As Integer

    ''' <summary>
    ''' 粒子的生成频率，即每隔多少帧生成一次粒子
    ''' </summary>
    ''' <returns></returns>
    Public Property Frequency As Integer

    ''' <summary>
    ''' 每次生成的粒子数目
    ''' </summary>
    ''' <returns></returns>
    Public Property GenerateCount As Integer

    ''' <summary>
    ''' 粒子系统使用的粒子模型
    ''' </summary>
    ''' <returns></returns>
    Public Property ParticleModel As Type

    ''' <summary>
    ''' 粒子使用的画刷
    ''' </summary>
    ''' <returns></returns>
    Public Property Brush As Brush
        Get
            Return _renderTarget.Fill
        End Get
        Set(value As Brush)
            _renderTarget.Fill = value
        End Set
    End Property

    ''' <summary>
    ''' 获得一个新的粒子系统
    ''' </summary>
    ''' <param name="useEllipse">是否使用圆形粒子，否则将使用方形粒子</param>
    Public Sub New(useEllipse As Boolean)
        Me._useEllipse = useEllipse
        If useEllipse Then
            _renderTarget = New Ellipse
        Else
            _renderTarget = New Rectangle
        End If
        Send("[PARTICLE]SYSTEM_DECLARE")
    End Sub

    ''' <summary>
    ''' 绑定粒子系统到界面元素，这将会撤销和前一个元素的绑定
    ''' </summary>
    ''' <param name="element">要绑定到的元素</param>
    Public Sub Bind(element As Panel)
        Unbind()
        _renderImage = New RenderTargetBitmap(element.Width, element.Height, 96, 96, PixelFormats.Bgra32)
        element.Background = New ImageBrush(_renderImage)
        _element = element
        Send("[PARTICLE]SYSTEM_BIND")
    End Sub

    ''' <summary>
    ''' 撤销粒子系统和界面元素的绑定
    ''' </summary>
    Public Sub Unbind()
        If _element Is Nothing Then Exit Sub
        _renderImage.Clear()
        _element.Background = Nothing
        _element = Nothing
        Send("[PARTICLE]SYSTEM_UNBIND")
    End Sub

    ''' <summary>
    ''' 更新所有粒子的状态
    ''' </summary>
    Friend Function UpdateLogic() As Boolean
        Dim i = 0
        Dim target As NormalParticleModel
        While i < _particleList.Count
            target = _particleList(i)
            If target.Update() Then target.UpdatePosition()
            target.Life -= 1
            If target.Life <= 0 Then
                target.Destroy()
                _particleList.Remove(target)
            Else
                i += 1
            End If
        End While
        If _surplusFrame = 0 Then
            _surplusFrame = Frequency
            For i = 1 To GenerateCount
                Dim item As NormalParticleModel = Activator.CreateInstance(ParticleModel)
                item.Life = ParticleLife
                item.Position = ParticlePosition
                item.Speed = ParticleSpeed
                item.Velocity = ParticleVelocity
                item.Size = New Vector(_random.Next(MinSize.X, MaxSize.X), _random.Next(MinSize.Y, MaxSize.Y))
                item.Initialise()
                _particleList.Add(item)
            Next
        End If
        Return _runnable
    End Function

    ''' <summary>
    ''' 呈现所有生存的粒子
    ''' </summary>
    Friend Sub UpdateRender()
        _renderImage.Clear()
        For Each item In _particleList
            _renderTarget.Width = item.Size.X
            _renderTarget.Width = item.Size.Y
            _renderTarget.Margin = New Thickness(item.Position.X, item.Position.Y, 0, 0)
            _renderImage.Render(_renderTarget)
        Next
    End Sub

    ''' <summary>
    ''' 开始生成粒子
    ''' </summary>
    Public Sub Start()
        If _runnable Then Exit Sub
        _runnable = True
        PluginInterface.LoopReceiver.Add(Me)
        Send("[PARTICLE]SYSTEM_START")
    End Sub

    ''' <summary>
    ''' 暂停生成粒子
    ''' </summary>
    Public Sub Pause()
        _runnable = False
        Send("[PARTICLE]SYSTEM_PAUSE")
    End Sub

    ''' <summary>
    ''' 停止生成粒子
    ''' </summary>
    Public Sub [Stop]()
        Pause()
        Unbind()
        Send("[PARTICLE]SYSTEM_STOP")
    End Sub
End Class