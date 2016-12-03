Namespace RAL.Tool
    Public MustInherit Class AudioPlayer : Implements IDisposable
        Private _volume As Integer = 0
        Private _position As Double = 0.0

        Public Sub New(id As Integer)
            PlayerId = id
        End Sub

        Public ReadOnly Property PlayerId As Integer

        Public Overridable Property Cycle As Boolean = False

        Public Overridable Property CycleCount As Integer = 0

        Public Property Volume As Integer
            Get
                Return _volume
            End Get
            Set(value As Integer)
                If value < 0 OrElse value > 10000 Then Throw New Exception.VolumeOutOfRangeException
                SetVolume(value)
                _volume = value
            End Set
        End Property

        Protected MustOverride Sub SetVolume(value As Integer)

        Public Overridable ReadOnly Property Duration As Double = 0.0

        Public Property Position As Double
            Get
                Return _position
            End Get
            Set(value As Double)
                If value < 0.0 OrElse value > Duration Then Throw New Exception.PositionOutOfRangeException
                SetPosition(value)
                _position = value
            End Set
        End Property

        Protected MustOverride Sub SetPosition(value As Integer)

        Public MustOverride Sub Play()

        Public MustOverride Sub Pause()

        Public MustOverride Sub Finish()

        Public Overridable Sub Dispose() Implements IDisposable.Dispose
            Finish()
        End Sub
    End Class
End Namespace