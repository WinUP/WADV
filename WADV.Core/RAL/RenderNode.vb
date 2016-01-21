Namespace RAL
    ''' <summary>
    ''' 表示一个渲染节点<br></br>
    ''' RenderNode是RAL中所有可直接渲染的结构在渲染抽象层中的最终基类，表示层级渲染结构中的一个可渲染元素
    ''' </summary>
    ''' <typeparam name="T">这个节点的子节点的类型</typeparam>
    Public MustInherit Class RenderNode(Of T)
        Protected ReadOnly ChildList As New Dictionary(Of String, T)

        ''' <summary>
        ''' 获得一个指定名称的新的渲染节点
        ''' </summary>
        ''' <param name="name">渲染节点的名称</param>
        Public Sub New(name As String)
            Me.Name = name
        End Sub

        ''' <summary>
        ''' 获得一个分配了随机名称的新的渲染节点
        ''' </summary>
        Public Sub New()
            Name = Guid.NewGuid.ToString.ToUpper
        End Sub

        ''' <summary>
        ''' 获取当前渲染节点的名称
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Name As String

        ''' <summary>
        ''' 获取当前渲染节点的所有一级子节点构成的数组
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property AllChildren As T()
            Get
                Return ChildList.Values.ToArray
            End Get
        End Property

        ''' <summary>
        ''' 根据名称获取该渲染节点的一级子节点
        ''' </summary>
        ''' <param name="key">子节点的名称</param>
        ''' <returns></returns>
        Default Public ReadOnly Property Children(key As String) As T
            Get
                Return ChildList(key)
            End Get
        End Property
    End Class
End Namespace