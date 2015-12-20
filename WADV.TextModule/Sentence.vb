''' <summary>
''' 表示一个句子
''' </summary>
Public Structure Sentence
    ''' <summary>
    ''' 句子内容
    ''' </summary>
    Public Content As String
    ''' <summary>
    ''' 讲话者
    ''' </summary>
    Public Speaker As String
    ''' <summary>
    ''' 是否已读
    ''' </summary>
    Public IsRead As Boolean
    ''' <summary>
    ''' 音频文件路径(Resource目录下)
    ''' </summary>
    Public VoiceFile As String
End Structure