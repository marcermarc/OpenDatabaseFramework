Imports System
Imports System.ComponentModel.Design
Imports System.Globalization
Imports Microsoft.VisualStudio.Shell
Imports Microsoft.VisualStudio.Shell.Interop

''' <summary>
''' Command handler
''' </summary>
Public NotInheritable Class cmdDatabaseFirst

    ''' <summary>
    ''' Command ID.
    ''' </summary>
    Public Const CommandId As Integer = 256

    ''' <summary>
    ''' Command menu group (command set GUID).
    ''' </summary>
    Public Shared ReadOnly CommandSet As New Guid("2b15f8e0-0ca0-4e15-8ae4-2b3236541832")

    ''' <summary>
    ''' VS Package that provides this command, not null.
    ''' </summary>
    Private ReadOnly package As Package

    ''' <summary>
    ''' Initializes a new instance of the <see cref="cmdDatabaseFirst"/> class.
    ''' Adds our command handlers for menu (the commands must exist in the command table file)
    ''' </summary>
    ''' <param name="package">Owner package, not null.</param>
    Private Sub New(package As Package)
        If package Is Nothing Then
            Throw New ArgumentNullException("package")
        End If

        Me.package = package
        Dim commandService As OleMenuCommandService = Me.ServiceProvider.GetService(GetType(IMenuCommandService))
        If commandService IsNot Nothing Then
            Dim menuCommandId = New CommandID(CommandSet, CommandId)
            Dim menuCommand = New MenuCommand(AddressOf Me.MenuItemCallback, menuCommandId)
            commandService.AddCommand(menuCommand)
        End If
    End Sub

    ''' <summary>
    ''' Gets the instance of the command.
    ''' </summary>
    Public Shared Property Instance As cmdDatabaseFirst

    ''' <summary>
    ''' Get service provider from the owner package.
    ''' </summary>
    Private ReadOnly Property ServiceProvider As IServiceProvider
        Get
            Return Me.package
        End Get
    End Property

    ''' <summary>
    ''' Initializes the singleton instance of the command.
    ''' </summary>
    ''' <param name="package">Owner package, Not null.</param>
    Public Shared Sub Initialize(package As Package)
        Instance = New cmdDatabaseFirst(package)
    End Sub

    ''' <summary>
    ''' This function is the callback used to execute the command when the menu item is clicked.
    ''' See the constructor to see how the menu item is associated with this function using
    ''' OleMenuCommandService service and MenuCommand class.
    ''' </summary>
    ''' <param name="sender">Event sender.</param>
    ''' <param name="e">Event args.</param>
    Private Sub MenuItemCallback(sender As Object, e As EventArgs)
        Dim message = String.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", Me.GetType().FullName)
        Dim title = "Command1"

        ' Show a message box to prove we were here
        VsShellUtilities.ShowMessageBox(
            Me.ServiceProvider,
            message,
            title,
            OLEMSGICON.OLEMSGICON_INFO,
            OLEMSGBUTTON.OLEMSGBUTTON_OK,
            OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST)
    End Sub
End Class
