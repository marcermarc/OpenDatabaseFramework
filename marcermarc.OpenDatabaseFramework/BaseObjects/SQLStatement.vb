Imports System.Collections.Generic

'Comments comming soon

Public Class SqlStatement
    Private m_Statement As String
    Private m_Items As Dictionary(Of String, Object)

    Sub New(in_Statement As String)
        m_Statement = in_Statement
        m_Items = New Dictionary(Of String, Object)
    End Sub

    Sub New()
        m_Statement = ""
        m_Items = New Dictionary(Of String, Object)
    End Sub

    Public Property Statement() As String
        Get
            Return m_Statement
        End Get
        Set(ByVal value As String)
            m_Statement = value
        End Set
    End Property

    Friend Property Items() As Dictionary(Of String, Object)
        Get
            Return m_Items
        End Get
        Set(ByVal value As Dictionary(Of String, Object))
            m_Items = value
        End Set
    End Property

    ''' <summary>
    ''' Adds a SQL-parameter
    ''' </summary>
    ''' <param name="in_Name">the name of the parameter</param>
    ''' <param name="in_Value">the value for the parameter</param>
    Sub AddItem(in_Name As String, in_Value As Object)
        m_Items.Add(in_Name, in_Value)
    End Sub

    ''' <summary>
    ''' Get the SQL-parameter value to the specific name
    ''' </summary>
    ''' <param name="in_Name">the name of the parameter</param>
    ''' <returns>the value of the parameter</returns>
    ''' <exception cref="Exceptions .NotFoundException">If the name was not found in the parameterlist</exception>
    Function GetItem(in_Name As String) As Object
        If m_Items.ContainsKey(in_Name) Then
            Return m_Items(in_Name)
        Else
            Throw New Exceptions.NotFoundException(in_Name & " was not found.")
        End If
    End Function
End Class
