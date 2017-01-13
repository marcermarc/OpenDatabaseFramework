Imports System
Imports System.Collections.Generic
Imports System.Threading

Friend Class ThreadUnique(Of T)
    Implements IUnique(Of T), IDisposable

    Private m_lock As Object 'Object to synclock
    Private m_values As Dictionary(Of Integer, T)

    Public Property Value As T Implements IUnique(Of T).Value
        Get
            SyncLock m_lock
                If m_values.ContainsKey(Thread.CurrentThread.ManagedThreadId) Then
                    Return m_values(Thread.CurrentThread.ManagedThreadId)
                Else
                    Return Nothing
                End If
            End SyncLock
        End Get
        Set(value As T)
            SyncLock m_lock
                If m_values.ContainsKey(Thread.CurrentThread.ManagedThreadId) Then
                    m_values(Thread.CurrentThread.ManagedThreadId) = value
                Else
                    m_values.Add(Thread.CurrentThread.ManagedThreadId, value)
                End If
            End SyncLock
        End Set
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If GetType(T).IsAssignableFrom(GetType(IDisposable)) Then
                    For Each v As T In m_values.Values
                        CType(v, IDisposable).Dispose()
                    Next
                End If
            End If

            m_values = Nothing
        End If
        disposedValue = True
    End Sub

    'Protected Overrides Sub Finalize()
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
