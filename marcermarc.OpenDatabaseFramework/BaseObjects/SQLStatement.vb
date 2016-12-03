Imports System.Collections.Generic
Public Class SqlStatement
    Sub New(in_Statement As String)
        m_Statement = in_Statement
        m_Items = New Dictionary(Of String, Object)
    End Sub

    Sub New()
        m_Statement = ""
        m_Items = New Dictionary(Of String, Object)
    End Sub

    Private m_Statement As String
    Public Property Statement() As String
        Get
            Return m_Statement
        End Get
        Set(ByVal value As String)
            m_Statement = value
        End Set
    End Property

    Private m_Items As Dictionary(Of String, Object)
    Friend Property Items() As Dictionary(Of String, Object)
        Get
            Return m_Items
        End Get
        Set(ByVal value As Dictionary(Of String, Object))
            m_Items = value
        End Set
    End Property

    Sub AddItem(in_Name As String, in_Value As Object)
        m_Items.Add(in_Name, in_Value)
    End Sub
End Class
