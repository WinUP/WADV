Imports WADV
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace Executer

    Public Class Executer
        Private Shared nextLine As Integer '下一个要解释的行
        Private Shared scriptData As New List(Of String) '临时脚本数据
        Private Shared fileContent() As String '文件内容
        Private Shared markList As New Dictionary(Of String, Integer) '跳转列表字典
        Private Shared characterNow As String = "" '当前对话角色名
        Private Shared effectNow As String = "" '当前对话文字特效
        Private Shared readingNow As Integer = -1 '现在对话的语音ID
        Private Shared soundList As New Dictionary(Of String, Integer) '正在播放的声音列表
        Private Shared imageList As New Dictionary(Of String, Integer) '正在显示的立绘列表
        Protected Friend Shared canSave, canLoad As Boolean

        Protected Friend Shared Sub JumpToMark(name As String)
            If Not markList.ContainsKey(name) Then
                ShowMessage("跳转标记" & name & "不存在，无法跳转")
                Exit Sub
            End If
            nextLine = markList(name)
        End Sub

        ''' <summary>
        ''' 加载游戏数据文件
        ''' </summary>
        ''' <param name="fileName">文件路径(Script目录下)</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub LoadFile(fileName As String)
            fileContent = System.IO.File.ReadAllLines(PathAPI.GetPath(PathAPI.Script, fileName), System.Text.Encoding.Default)
            markList.Clear()
            Dim param() As String
            For i As Integer = 0 To fileContent.Length - 1
                param = GetParameter(fileContent(i))
                If param(0) = "M" Then
                    markList.Add(param(1), i)
                End If
            Next
            nextLine = 0
        End Sub

        Protected Friend Shared Sub ShowMessage(content As String)
            ScriptAPI.ShowMessage(content & Environment.NewLine & "当前行号：" & nextLine, "提示")
        End Sub

        Protected Friend Shared Sub ExecuteData(fileName As String, startLine As Integer, endLine As Integer)
            LoadFile(fileName)
            Dim param() As String
            Dim operateType As Char
            While (nextLine <= endLine OrElse endLine = -1) AndAlso nextLine < fileContent.Length
                If readingNow > -1 Then
                    MediaModule.API.SoundAPI.StopSound(readingNow)
                    readingNow = -1
                End If
                operateType = fileContent(nextLine)(0)
                If operateType <> "[" Then
                    If operateType = "#" Then
                        scriptData.AddRange(GetParameter(fileContent(nextLine)))
                        RunScript()
                    Else
                        TextModule.API.TextAPI.ShowReular(New String() {GetParameter(fileContent(nextLine))(0)}, New String() {characterNow}, effectNow)
                    End If
                Else
                    param = GetParameter(fileContent(nextLine))
                    operateType = param(0)
                    Select Case operateType
                        Case "S" '声音
                            Dim name = param(1)
                            Select Case param(2)
                                Case "BGM"
                                    If soundList.ContainsKey(name) Then
                                        ShowMessage("名字是" & name & "的声音已经存在，不能添加新的")
                                        Exit Select
                                    End If
                                    soundList.Add(name, MediaModule.API.SoundAPI.PlayBGM(PathAPI.GetPath(PathAPI.Resource, param(3))))
                                Case "READ"
                                    If soundList.ContainsKey(name) Then
                                        ShowMessage("名字是" & name & "的声音已经存在，不能添加新的")
                                        Exit Select
                                    End If
                                    soundList.Add(name, MediaModule.API.SoundAPI.PlayReading(PathAPI.GetPath(PathAPI.Resource, param(3))))
                                Case "EFFECT"
                                    If soundList.ContainsKey(name) Then
                                        ShowMessage("名字是" & name & "的声音已经存在，不能添加新的")
                                        Exit Select
                                    End If
                                    soundList.Add(name, MediaModule.API.SoundAPI.PlayEffect(PathAPI.GetPath(PathAPI.Resource, param(3))))
                                Case "EFFECTW"
                                    MediaModule.API.SoundAPI.PlayEffectAndWait(PathAPI.GetPath(PathAPI.Resource, param(3)))
                                Case "PAUSE"
                                    If Not soundList.ContainsKey(name) Then
                                        ShowMessage("名字是" & name & "的声音不存在，所以无法暂停")
                                    End If
                                    MediaModule.API.SoundAPI.PauseSound(soundList(name))
                                Case "RESUME"
                                    If Not soundList.ContainsKey(name) Then
                                        ShowMessage("名字是" & name & "的声音不存在，所以无法恢复播放")
                                    End If
                                    MediaModule.API.SoundAPI.ResumeSound(soundList(name))
                                Case "STOP"
                                    If Not soundList.ContainsKey(name) Then
                                        ShowMessage("名字是" & name & "的声音不存在，所以无法停止")
                                    End If
                                    MediaModule.API.SoundAPI.StopSound(soundList(name))
                            End Select
                        Case "I" '背景图
                            ImageModule.API.ImageAPI.Show(PathAPI.GetPath(PathAPI.Resource, param(3)), param(1), param(2), "MainGrid")
                            WindowAPI.GetDispatcher.Invoke(Sub() WindowAPI.GetWindow.Background = New ImageBrush(BitmapFrame.Create(New Uri(PathAPI.GetPath(PathAPI.Resource, param(3))))))
                            WindowAPI.GetDispatcher.Invoke(Sub() WindowAPI.GetGrid.Background = Nothing)
                        Case "P" '对话角色
                            characterNow = param(1)
                        Case "E" '对话文字特效
                            effectNow = param(1)
                        Case "R" '带声音的对话
                            MediaModule.API.SoundAPI.StopSound(readingNow)
                            readingNow = MediaModule.API.SoundAPI.PlayReading(param(2))
                            TextModule.API.TextAPI.ShowReular(New String() {param(1)}, New String() {characterNow}, effectNow)
                        Case "C" '选择支
                            Dim choiceAndMark = param.Skip(1).Take(param.Length - 3)
                            Dim choices = If(choiceAndMark.Count Mod 2 = 0, choiceAndMark.Take(choiceAndMark.Count / 2), choiceAndMark.Take((choiceAndMark.Count - 1) / 2))
                            Dim marks = choiceAndMark.Skip(choices.Count)
                            ChoiceModule.API.ChoiceAPI.ShowRegular(choices.ToArray, param(param.Length - 1), param(param.Length - 2))
                            Dim answer = ChoiceModule.API.ChoiceAPI.Answer
                            Dim toMark = marks(choices.ToList.IndexOf(answer))
                            If Not markList.ContainsKey(toMark) Then
                                ShowMessage("跳转标记" & toMark & "不存在，无法跳转")
                                Exit Select
                            End If
                            nextLine = markList(toMark)
                        Case "M" '标记
                            If param(2) = "SKIP" Then
                                nextLine += 1
                                param = GetParameter(fileContent(nextLine))
                                While param(0) <> "M"
                                    nextLine += 1
                                    param = GetParameter(fileContent(nextLine))
                                End While
                                nextLine -= 1
                            End If
                        Case "T" '立绘
                            Dim name = param(1)
                            Select Case param(2)
                                Case "SHOW"
                                    If imageList.ContainsKey(name) Then
                                        ShowMessage("名字是" & name & "的图片已经存在，不能添加新的")
                                        Exit Select
                                    End If
                                    imageList.Add(name, ImageModule.API.TachieAPI.ShowRegular(PathAPI.GetPath(PathAPI.Resource, param(3)),
                                                                                              param(4), param(5), If(param(6) = "TRUE", True, False),
                                                                                              param(7), param(8), param(9), param(10), param(11),
                                                                                              param(12).Substring(1, param(12).Length - 2).Split(",")))
                                Case "CHANGE"
                                    If Not imageList.ContainsKey(name) Then
                                        ShowMessage("名字是" & name & "的图片不存在，所以不能修改")
                                        Exit Select
                                    End If
                                    ImageModule.API.TachieAPI.Change(imageList(name), param(3), param(4), param(5))
                                Case "EFFECT"
                                    If Not imageList.ContainsKey(name) Then
                                        ShowMessage("名字是" & name & "的图片不存在，所以不能修改")
                                        Exit Select
                                    End If
                                    ImageModule.API.TachieAPI.EffectRegular(imageList(name),
                                                                            param(3), param(4), If(param(5) = "TRUE", True, False),
                                                                            param(6).Substring(1, param(6).Length - 2).Split(","))
                                Case "EFFECTN"
                                    If Not imageList.ContainsKey(name) Then
                                        ShowMessage("名字是" & name & "的图片不存在，所以不能修改")
                                        Exit Select
                                    End If
                                    ImageModule.API.TachieAPI.EffectNowRegular(imageList(name),
                                                                               param(3), param(4), If(param(5) = "TRUE", True, False),
                                                                               param(6).Substring(1, param(6).Length - 2).Split(","))
                                Case "HIDE"
                                    If Not imageList.ContainsKey(name) Then
                                        ShowMessage("名字是" & name & "的图片不存在，所以不能修改")
                                        Exit Select
                                    End If
                                    ImageModule.API.TachieAPI.Delete(imageList(name))
                            End Select
                        Case "V" '视频
                            Select Case param(1)
                                Case "PLAY"
                                    MediaModule.API.VideoAPI.PlayAndWait(param(2), If(param(3) = "TRUE", True, False))
                                Case "PLAYN"
                                    MediaModule.API.VideoAPI.PlayVideo(param(2), False)
                                Case "PAUSE"
                                    MediaModule.API.VideoAPI.PauseVideo()
                                Case "RESUME"
                                    MediaModule.API.VideoAPI.ResumeVideo()
                                Case "STOP"
                                    MediaModule.API.VideoAPI.StopVideo()
                            End Select
                        Case "G" '游戏系统
                            '游戏系统有存读档、变成游戏数据文件、显示菜单、等待指定帧数、设置存读档可行性

                        Case Else '其他
                            Exit Select
                    End Select
                End If
                nextLine += 1
            End While
            API.GameAPI.ShowMenu(Nothing, Nothing)
        End Sub

        ''' <summary>
        ''' 执行所有缓存的脚本
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub RunScript()
            If scriptData.Count > 0 Then
                SaveLoad.Save.CanSave = False
                SaveLoad.Load.CanLoad = False
                ScriptAPI.RunStringAndWait(String.Join(Environment.NewLine, scriptData))
                scriptData.Clear()
                If canSave Then SaveLoad.Save.CanSave = True
                If canLoad Then SaveLoad.Load.CanLoad = True
            End If
        End Sub

        ''' <summary>
        ''' 拆分一行游戏数据内的所有变量
        ''' </summary>
        ''' <param name="line">游戏数据</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function GetParameter(line As String) As String()
            Dim returnData As New List(Of String)
            '拆分脚本或对话
            If line(0) <> "[" Then
                '拆分脚本
                If line(0) = "#" Then
                    If line(line.Length - 1) = "#" AndAlso line.Length > 1 Then '单行脚本
                        returnData.Add(line.Substring(1, Math.Max(line.Length - 2, 0)))
                    Else '多行脚本
                        returnData.Add(line.Substring(1))
                        nextLine += 1
                        Dim content = fileContent(nextLine)
                        While (content(content.Length - 1) <> "#")
                            returnData.Add(content)
                            nextLine += 1
                            content = fileContent(nextLine)
                        End While
                        returnData.Add(content.Substring(0, content.Length - 1))
                    End If
                Else '对话
                    returnData.Add(line)
                End If
                Return returnData.ToArray
            End If
            '拆分游戏指令
            line = line.Substring(1, line.Length - 2) & " "
            Dim startIndex, stopIndex, length As Integer
            length = line.Length
            While startIndex < length
                If line(startIndex) = "'" Then
                    stopIndex = line.IndexOf("'", startIndex + 1)
                    returnData.Add(line.Substring(startIndex + 1, stopIndex - startIndex - 1))
                    startIndex = stopIndex + 2
                Else
                    stopIndex = line.IndexOf(" ", startIndex)
                    returnData.Add(line.Substring(startIndex, stopIndex - startIndex))
                    startIndex = stopIndex + 1
                End If
            End While
            Return returnData.ToArray
        End Function



    End Class

End Namespace