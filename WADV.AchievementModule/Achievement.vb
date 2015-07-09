''' <summary>
''' 一般成就的基类
''' </summary>
''' <remarks></remarks>
<Serializable> Public MustInherit Class Achievement
    Private ReadOnly _name As String
    Private ReadOnly _substance As String
    Private _isEarn As Boolean

    ''' <summary>
    ''' 获得一个新的成就实例
    ''' </summary>
    ''' <param name="name">成就的名称</param>
    ''' <param name="substance">成就的详细描述</param>
    ''' <remarks></remarks>
    Public Sub New(name As String, substance As String)
        _name = name
        _substance = substance
        _isEarn = False
    End Sub

    ''' <summary>
    ''' 检查成就是否被获得
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Sub Check()

    ''' <summary>
    ''' 注册成就到系统
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend MustOverride Sub Register()

    ''' <summary>
    ''' 成就的名称
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    ''' <summary>
    ''' 成就的详细描述
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Substance As String
        Get
            Return _substance
        End Get
    End Property

    ''' <summary>
    ''' 这个成就是否已被获得
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsEarn As Boolean
        Get
            Return _isEarn
        End Get
    End Property

    ''' <summary>
    ''' 设置这个成就围已获得
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SetEarn()
        _isEarn = True
        ShowList.Add(Me)
        Message.Send("[ACHIEVE]ACHIEVE_EARN")
        API.Achieve.Save()
    End Sub
End Class