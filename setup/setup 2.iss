; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Gerador de quest�es"
#define MyAppVersion "1.0.8"
#define MyAppPublisher "Prorsoft Ltda"
#define MyAppURL "www.prorsoft.com.br"
#define MyAppExeName "Gerador de Quest�es Alternativas.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{A8557409-8CC6-41D0-8856-4C15C546F1C7}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName=c:\gerador_de_questaoes
DisableProgramGroupPage=yes
OutputDir=D:\TRABALHO\PROJETO\Gerador de Quest�es Alternativas\setup
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "brazilianportuguese"; MessagesFile: "compiler:Languages\BrazilianPortuguese.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "D:\TRABALHO\PROJETO\Gerador de Quest�es Alternativas\Gerador de Quest�es Alternativas\bin\Debug\Gerador de Quest�es Alternativas.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\TRABALHO\PROJETO\Gerador de Quest�es Alternativas\Gerador de Quest�es Alternativas\bin\Debug\DADOS.mdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\TRABALHO\PROJETO\Gerador de Quest�es Alternativas\Gerador de Quest�es Alternativas\bin\Debug\ExcelLibrary.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\TRABALHO\PROJETO\Gerador de Quest�es Alternativas\Gerador de Quest�es Alternativas\bin\Debug\Gerador de Quest�es Alternativas.vshost.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\TRABALHO\PROJETO\Gerador de Quest�es Alternativas\Gerador de Quest�es Alternativas\bin\Debug\Microsoft.Office.Interop.Excel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\TRABALHO\PROJETO\Gerador de Quest�es Alternativas\Gerador de Quest�es Alternativas\bin\Debug\office.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

