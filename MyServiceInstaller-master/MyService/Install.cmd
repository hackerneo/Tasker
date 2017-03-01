@Echo Off
Rem %1 can be /U or /Uninstall
Rem Supported Parameters ["Default Value"]:
Rem ServiceName ["MyService"]
Rem DisplayName [Not Specified]
Rem Description [Not Specified]
Rem StartType (one of Automatic, Manual, Disabled) ["Automatic"]
Rem DelayedAutoStart (when StartType is Automatic, one of True or False) ["False"]
Rem DependedOn (separated by semicolon) [Not Specified]
Rem Account (one of LocalService, NetworkService, LocalSystem, User) ["LocalService"]
Rem UserName (when Account is "User") [Not Specified]
Rem Password (when Account is "User") [Not Specified]
Rem Command (command line parameters for the service) [Not Specified]

Set ServiceName=Custom Service Name
Set DisplayName=**Custom Display Name
Set Description=Custom Description
Set StartType=Automatic
Set DelayedAutoStart=
Set DependedOn=MSSQL$DENALI;MSSQLSERVER
Set Account=LocalSystem
Set UserName=
Set Password=
Set Command=/Additional -Parameters

Set Install=%~1

@If "%~1" == "" (
  Set Install=/Install
) Else If "%~1" == "/U" (
  Set Install=/Uninstall
)

"MyService" %Install% ^
  /ServiceName="%ServiceName%" ^
  /DisplayName="%DisplayName%" ^
  /Description="%Description%" ^
  /StartType="%StartType%" ^
  /DelayedAutoStart="%DelayedAutoStart%" ^
  /DependedOn="%DependedOn%" ^
  /Account="%Account%" ^
  /UserName="%UserName%" ^
  /Password="%Password%" ^
  /Command="%Command%"

@Pause
