Friend NotInheritable Class SentenceList : Inherits Queue(Of Sentence)
    Friend Sub Enqueue(value As Sentence)
        MyBase.Enqueue(value)

    End Sub

    Friend Function Dequeue() As Sentence
        Dim target = MyBase.Dequeue

    End Function
End Class
