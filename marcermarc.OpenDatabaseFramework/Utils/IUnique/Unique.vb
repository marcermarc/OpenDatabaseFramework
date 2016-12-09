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
End Class
