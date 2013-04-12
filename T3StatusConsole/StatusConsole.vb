Option Strict On

Imports System.Threading
Imports T3

Module StatusConsole

    Private _host As String
    Private _port As Integer
    Private _abSide As String = "A"
    Private _interval As Integer = 5000
    Private _machine As Machine
    Private _lastStatus As Status

    Private _poller As Thread

    Sub Main()

        Console.WriteLine("Please enter the host name or IP address of the CNC:")
        _host = Console.ReadLine()

        Console.WriteLine("Please enter the port number: [6003]")
        Dim port As String = Console.ReadLine()
        If Not Integer.TryParse(port, _port) Then _port = 6003


        Console.CursorVisible = False
        _machine = New Machine(_host, _port)
        _machine.Connect()

        _poller = New Thread(AddressOf UpdateWorker)
        _poller.IsBackground = True
        _poller.Start()

        Do
            Dim key As ConsoleKeyInfo = Console.ReadKey(True)
            Select Case key.Key
                Case ConsoleKey.X
                    Exit Do
                Case ConsoleKey.T
                    _abSide = If(_abSide = "A", "B", "A")
                    UpdateAxes(_lastStatus)
                Case ConsoleKey.F
                    _interval = Math.Max(1000, _interval - 1000)
                    Prompt("Polling every " & _interval \ 1000 & "s.")
                Case ConsoleKey.S
                    _interval = Math.Min(60000, _interval + 1000)
                    Prompt("Polling every " & _interval \ 1000 & "s.")
                Case ConsoleKey.P
                    If _poller.IsAlive Then
                        _poller.Abort()
                        Prompt("Paused. Press [P] to resume.")
                    Else
                        _poller = New Thread(AddressOf UpdateWorker)
                        _poller.IsBackground = True
                        _poller.Start()
                    End If
                Case ConsoleKey.D
                    'dump to a file
            End Select
        Loop

        If _poller.IsAlive Then _poller.Abort()
        _machine.Disconnect()

        SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Console.Clear()
    End Sub

    Private Sub UpdateWorker()
        Try
            Do
                Dim status As Status = _machine.GetStatus()
                Display(status)
                _lastStatus = status
                Thread.Sleep(_interval)
            Loop
        Catch ta As ThreadAbortException
        Catch ex As Exception
            Prompt(ex.Message)
        End Try
    End Sub

    Private Sub SetColors(ByVal forecolor As ConsoleColor, ByVal backcolor As ConsoleColor)
        Console.ForegroundColor = forecolor
        Console.BackgroundColor = backcolor
    End Sub

    Private Sub Prompt(ByVal message As String)
        Console.SetCursorPosition(0, (Console.WindowHeight - 3) \ 2)
        SetColors(ConsoleColor.Black, ConsoleColor.White)

        Console.Write("".PadRight(Console.WindowWidth))
        If message.Length > Console.WindowWidth Then
            Console.Write(message.Substring(0, Console.WindowWidth))
        Else
            Dim i As Integer = (Console.WindowWidth - message.Length) \ 2
            Dim text As New String(" "c, i)
            Console.Write((text & message).PadRight(Console.WindowWidth))
        End If
        Console.Write("".PadRight(Console.WindowWidth))
    End Sub

    Private Sub DrawMachineLine()
        Console.SetCursorPosition(0, 0)
        SetColors(ConsoleColor.White, ConsoleColor.DarkBlue)

        Dim text As String = "MACHINE " & _host & ":" & _port
        text = text.PadRight(Console.WindowWidth)
        Console.Write(text)
    End Sub

    Private Sub DrawAlarm(ByVal alarm As Alarm)
        Console.SetCursorPosition(0, 1)
        If alarm.Level = MachineAlarmLevel.None Then
            SetColors(ConsoleColor.White, ConsoleColor.DarkBlue)

            Console.Write("".PadRight(Console.WindowWidth))
        Else
            SetColors(ConsoleColor.White, ConsoleColor.Red)

            Dim alarmText As String = alarm.Number & " "
            Select Case alarm.Level
                Case MachineAlarmLevel.A
                    alarmText &= "ALARM-A "
                Case MachineAlarmLevel.B
                    alarmText &= "ALARM-B "
                Case MachineAlarmLevel.C
                    alarmText &= "ALARM-C "
                Case MachineAlarmLevel.D
                    alarmText &= "ALARM-P "
                Case MachineAlarmLevel.P
                    alarmText &= "ALARM-P "
            End Select
            alarmText &= alarm.Message

            alarmText = alarmText.PadRight(Console.WindowWidth)
            Console.Write(alarmText)

        End If
    End Sub

    Private Sub DrawMode(ByVal mode As MachineMode)
        Console.SetCursorPosition(0, 3)

        SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Dim modes As String = "AUTO  MDI  MANUAL  PROGRAM  PARAMETER  ZERO  TOOL  MACMAN".PadRight(Console.WindowWidth)
        Console.Write(modes)


        Dim activeMode As String = ""
        Select Case mode
            Case MachineMode.Auto
                activeMode = "AUTO"
            Case MachineMode.MDI
                activeMode = "MDI"
            Case MachineMode.Manual
                activeMode = "MANUAL"
            Case MachineMode.Program
                activeMode = "PROGRAM"
            Case MachineMode.Parameter
                activeMode = "PARAMETER"
            Case MachineMode.ZeroPoint
                activeMode = "ZERO"
            Case MachineMode.ToolData
                activeMode = "TOOL"
            Case MachineMode.MacMan
                activeMode = "MACMAN"
            Case Else
        End Select

        Dim index As Integer = modes.IndexOf(activeMode)
        If index > -1 Then
            Console.SetCursorPosition(index, 3)
            SetColors(ConsoleColor.Black, ConsoleColor.White)
            Console.Write(activeMode)
        End If

    End Sub

    Private Sub DrawStatus(ByVal status As MachineStatus)
        Console.SetCursorPosition(0, 4)
        'Alarm      Limit       Program Stop        SlideHold   STM     Running

        If (status And MachineStatus.Alarm) = MachineStatus.Alarm Then
            SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Else
            SetColors(ConsoleColor.Black, ConsoleColor.White)
        End If
        Console.Write("ALARM")
        SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Console.Write("     ")

        If (status And MachineStatus.Limit) = MachineStatus.Limit Then
            SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Else
            SetColors(ConsoleColor.Black, ConsoleColor.White)
        End If
        Console.Write("LIMIT")
        SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Console.Write("     ")

        If (status And MachineStatus.ProgramStop) = MachineStatus.ProgramStop Then
            SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Else
            SetColors(ConsoleColor.Black, ConsoleColor.White)
        End If
        Console.Write("PROG STOP")
        SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Console.Write(" ")

        If (status And MachineStatus.SlideHold) = MachineStatus.SlideHold Then
            SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Else
            SetColors(ConsoleColor.Black, ConsoleColor.White)
        End If
        Console.Write("SLIDEHOLD")
        SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Console.Write(" ")

        If (status And MachineStatus.STM) = MachineStatus.STM Then
            SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Else
            SetColors(ConsoleColor.Black, ConsoleColor.White)
        End If
        Console.Write("STM")
        SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Console.Write("       ")

        If (status And MachineStatus.Running) = MachineStatus.Running Then
            SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Else
            SetColors(ConsoleColor.Black, ConsoleColor.White)
        End If
        Console.Write("RUNNING")
        SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Console.Write("   ")
    End Sub

    Private Sub DrawAxes(ByVal status As Status)

        Console.SetCursorPosition(0, 6)
        SetColors(ConsoleColor.DarkGreen, ConsoleColor.Black)
        Console.Write("NAME          POSITION    DIST TO GO      FEEDRATE")

        UpdateAxes(status)

    End Sub

    Private Sub DrawToolbar()
        Console.SetCursorPosition(0, Console.WindowHeight - 2)
        SetColors(ConsoleColor.White, ConsoleColor.DarkBlue)

        Dim text As String = "COMMANDS".PadRight(Console.WindowWidth)
        Console.Write(text)

        Dim commands As String = "[T]oggle A/B. Poll [f]aster/[s]lower. [P]ause. E[x]it. [D]ump to file."
        Console.ForegroundColor = ConsoleColor.Gray
        For Each c As Char In commands
            If c = "["c Then
                Console.Write(c)
                SetColors(ConsoleColor.White, ConsoleColor.DarkBlue)
            ElseIf c = "]"c Then
                SetColors(ConsoleColor.Gray, ConsoleColor.DarkBlue)
                Console.Write(c)
            Else
                Console.Write(c)
            End If
        Next c

        Dim padding As String = "".PadRight(Console.WindowWidth - commands.Length)
        Console.BufferWidth += 1
        Console.Write(padding)
        Console.BufferWidth -= 1
    End Sub

    Sub Display(ByVal status As Status)

        'Display is a full refresh and draws everything. It will need to be triggered at startup and on toggling A/B.
        SetColors(ConsoleColor.Gray, ConsoleColor.Black)
        Console.Clear()
        DrawMachineLine()
        DrawAlarm(status.Alarm)
        DrawMode(status.Mode.Mode)
        DrawStatus(status.Mode.Status)
        DrawAxes(status)
        DrawToolbar()
    End Sub

    Private Sub UpdateAxes(ByVal status As Status)

        Dim axes As New List(Of String)()
        For Each ax As String In status.Axis.Keys
            If (ax.EndsWith(_abSide) OrElse ax.StartsWith("S")) AndAlso Not ax.StartsWith("F") Then axes.Add(ax)
        Next ax



        For i As Integer = 0 To 10
            Console.SetCursorPosition(0, 7 + i)
            If i >= axes.Count Then
                Console.Write("".PadRight(Console.WindowWidth))
            Else
                Dim label As String = axes(i)

                SetColors(ConsoleColor.Blue, ConsoleColor.Black)
                Console.Write(label.PadRight(8))

                SetColors(ConsoleColor.White, ConsoleColor.Black)
                Console.Write(status.Axis(label).ToString().PadLeft(14))

                SetColors(ConsoleColor.Gray, ConsoleColor.Black)
                Dim text As String = If(status.Distance.ContainsKey(label), status.Distance(label).ToString().PadLeft(14), "".PadLeft(14))
                Console.Write(text)

                SetColors(ConsoleColor.DarkGreen, ConsoleColor.Black)
                If (status.Axis.ContainsKey("F" & label)) Then Console.Write(status.Axis("F" & label).ToString().PadLeft(14))

            End If
        Next i
    End Sub

End Module
