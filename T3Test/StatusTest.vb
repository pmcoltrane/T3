Option Strict On

Imports NUnit.Framework
Imports t3

Namespace UnitTest

    Friend Module TestData
        Private dump1 As String = IO.File.ReadAllText("dump_1.txt")
        Private dump2 As String = IO.File.ReadAllText("dump_2.txt")
        Private dump3 As String = IO.File.ReadAllText("dump_3.txt")
        Private dump4 As String = IO.File.ReadAllText("dump_4.txt")

        Public ReadOnly Status1 As New Status(dump1)
        Public ReadOnly Status2 As New Status(dump2)
        Public ReadOnly Status3 As New Status(dump3)
        Public ReadOnly Status4 As New Status(dump4)

    End Module

    <TestFixture()> Public Class AlarmTest

        <Test()> Public Sub TestAlarmNumbers()
            Assert.True(String.IsNullOrEmpty(Status1.Alarm.Number))
            Assert.True(String.IsNullOrEmpty(Status2.Alarm.Number))
            Assert.AreEqual("2227-01", Status3.Alarm.Number)
            Assert.AreEqual("3700", Status4.Alarm.Number)
        End Sub

        <Test()> Public Sub TestAlarmLevels()
            Assert.AreEqual(MachineAlarmLevel.None, Status1.Alarm.Level)
            Assert.AreEqual(MachineAlarmLevel.None, Status2.Alarm.Level)
            Assert.AreEqual(MachineAlarmLevel.B, Status3.Alarm.Level)
            Assert.AreEqual(MachineAlarmLevel.C, Status4.Alarm.Level)
        End Sub

        <Test()> Public Sub TestAlarmMessages()
            Assert.True(String.IsNullOrEmpty(Status1.Alarm.Message))
            Assert.True(String.IsNullOrEmpty(Status2.Alarm.Message))
            Assert.AreEqual("Mnemonic or local variable  A side 'WHOKNOWS'", Status3.Alarm.Message)
            Assert.AreEqual("Power save on  1E", Status4.Alarm.Message)
        End Sub

    End Class

    <TestFixture()> Public Class ModeTest

        <Test()> Public Sub TestModeMode()
            Assert.AreEqual(&H40, Status1.Mode.Mode)
            Assert.AreEqual(&H80, Status2.Mode.Mode)
            Assert.AreEqual(&H40, Status3.Mode.Mode)
            Assert.AreEqual(&H8, Status4.Mode.Mode)
        End Sub

        <Test()> Public Sub TestModeStatus()
            Assert.AreEqual(&H2, Status1.Mode.Status)
            Assert.AreEqual(&H2, Status2.Mode.Status)
            Assert.AreEqual(&H23, Status3.Mode.Status)
            Assert.AreEqual(&H3, Status4.Mode.Status)
        End Sub

        <Test()> Public Sub TestModeSequenceA()
            Assert.AreEqual("", Status1.Mode.SequenceA)
            Assert.AreEqual("", Status2.Mode.SequenceA)
            Assert.AreEqual("", Status3.Mode.SequenceA)
            Assert.AreEqual("", Status4.Mode.SequenceA)
        End Sub

        <Test()> Public Sub TestModeBlockA()
            Assert.AreEqual("0", Status1.Mode.BlockA)
            Assert.AreEqual("0", Status2.Mode.BlockA)
            Assert.AreEqual("0", Status3.Mode.BlockA)
            Assert.AreEqual("0", Status4.Mode.BlockA)
        End Sub


        <Test()> Public Sub TestModeSequenceB()
            Assert.AreEqual("", Status1.Mode.SequenceB)
            Assert.AreEqual("", Status2.Mode.SequenceB)
            Assert.AreEqual("", Status3.Mode.SequenceB)
            Assert.AreEqual("", Status4.Mode.SequenceB)
        End Sub

        <Test()> Public Sub TestModeBlockB()
            Assert.AreEqual("0", Status1.Mode.BlockB)
            Assert.AreEqual("0", Status2.Mode.BlockB)
            Assert.AreEqual("0", Status3.Mode.BlockB)
            Assert.AreEqual("0", Status4.Mode.BlockB)
        End Sub

    End Class

End Namespace
