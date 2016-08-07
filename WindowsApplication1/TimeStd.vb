Public Class TimeStd
    Dim start_time As DateTime
    Dim stop_time As DateTime
    Dim elapsed_time As TimeSpan

    Sub startTime()
        Me.start_time = Now
    End Sub

    Sub endTime()
        Me.stop_time = Now
        elapsed_time = stop_time.Subtract(start_time)
        Console.WriteLine("걸린시간 : " + elapsed_time.TotalSeconds.ToString("0.000000"))
    End Sub

End Class
