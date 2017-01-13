Imports System
Imports marcermarc.OpenDatabaseFramework

Friend Class Unique(Of T)
    Implements IUnique(Of T)

    Private m_lock As Object 'Object to synclock
    Private m_value As T

    Public Property Value As T Implements IUnique(Of T).Value
        Get
            Return m_value
        End Get
        Set(value As T)
            m_value = value
        End Set
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If GetType(T).IsAssignableFrom(GetType(IDisposable)) Then
                    CType(m_value, IDisposable).Dispose()
                End If
            End If

            m_value = Nothing
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
