Namespace Enumeration
    ''' <summary>
    ''' 组件解绑结果
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ComponentUnbindingResult
        ''' <summary>
        ''' 解绑失败
        ''' </summary>
        ''' <remarks></remarks>
        Success
        ''' <summary>
        ''' 找不到需要解绑的组件
        ''' </summary>
        ''' <remarks></remarks>
        CannotFind
        ''' <summary>
        ''' 解绑被否决
        ''' </summary>
        ''' <remarks></remarks>
        Cancel
    End Enum
End Namespace