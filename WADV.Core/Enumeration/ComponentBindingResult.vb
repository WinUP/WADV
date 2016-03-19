Namespace Enumeration
    ''' <summary>
    ''' 组件绑定结果
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ComponentBindingResult
        ''' <summary>
        ''' 绑定成功
        ''' </summary>
        ''' <remarks></remarks>
        Success
        ''' <summary>
        ''' 绑定已存在，不需要再次绑定
        ''' </summary>
        ''' <remarks></remarks>
        NoNeed
        ''' <summary>
        ''' 绑定被否决
        ''' </summary>
        ''' <remarks></remarks>
        Cancel
    End Enum
End Namespace