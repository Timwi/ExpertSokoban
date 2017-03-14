#define SrcDir "..\..\builds\Release"
#define SrcFile "..\..\builds\Release\ExpSok.exe"
#define FileVerStr GetFileVersion(SrcFile)
#define ShortVerStr Copy(FileVerStr, 1, RPos(".", FileVerStr)-1)
#define UIVerStr Copy(FileVerStr, 1, RPos(".", FileVerStr)-1) + " (" + Copy(FileVerStr, RPos(".", FileVerStr)+1) + ")"

[Files]
Source: {#SrcFile}; DestDir: {app}; DestName: ExpSok.exe;
Source: {#SrcDir}\Translations\*; DestDir: {app}\Translations;
Source: {#SrcDir}\expsok.chm; DestDir: {app}; DestName: ExpSok.chm;
Source: "..\OriginalLevels.txt"; DestDir: {app};
Source: "..\Timwi.txt"; DestDir: {app};
Source: dotNetFx40_Client_setup.exe; Flags: dontcopy

[InnoIDE_Settings]
UseRelativePaths=true

[Setup]
AppCopyright=© 2006-2017 Timwi Heizmann, Roman Starkov
AppName=Expert Sokoban
DefaultDirName={code:GetDefaultDirName}
OutputDir=..\..\builds\Installer
OutputBaseFilename=ExpertSokobanSetup-{#ShortVerStr}
AppID={{62058A0A-DECE-43A8-9B56-52D161CE0EE7}
SolidCompression=yes
Compression=lzma2/max
InternalCompressLevel=max
CompressionThreads=1
ShowLanguageDialog=yes
DefaultGroupName=Expert Sokoban
UninstallDisplayName=Expert Sokoban
AppPublisher=CuteBits
AppPublisherURL=http://www.cutebits.com/ExpertSokoban/ContactUs
AppSupportURL=http://www.cutebits.com/ExpertSokoban
AppVersion={#UIVerStr}
UninstallDisplayIcon={app}\ExpSok.exe
WizardImageFile=SideBanner.bmp
WizardSmallImageFile=TopRight.bmp
AppendDefaultDirName=false
ArchitecturesInstallIn64BitMode=x64
AllowNoIcons=true
DefaultDialogFontName=Segoe UI
AppMutex=Global\ExpSokMutex7FDC0158CF9E
DirExistsWarning=no

[Icons]
Name: "{group}\{cm:UninstallProgram, Expert Sokoban}"; Filename: {uninstallexe};
Name: {group}\Expert Sokoban; Filename: {app}\ExpSok.exe; Comment: {cm:ShortcutDescription}; 

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl,Translations\English.isl";
Name: "de"; MessagesFile: "compiler:Languages\German.isl,Translations\German.isl";
Name: "ru"; MessagesFile: "compiler:Languages\Russian.isl,Translations\Russian.isl";

[Code]

function IsNetFrameworkV4Installed(): Boolean;
begin
  Result := 
      RegValueExists(HKLM,'Software\Microsoft\NET Framework Setup\NDP\v4\Client', 'Install')
      or
      RegValueExists(HKLM,'Software\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install');
end;

function InitializeSetup(): Boolean;
var
  ResultCode: Integer;
  ExpSokOldPath: String;
  FindRec: TFindRec;
begin
  Result := True;
  if not IsNetFrameworkV4Installed() then begin
    if SuppressibleMsgBox(ExpandConstant('{cm:FrameworkNotInstalledPrompt}'), mbConfirmation, MB_YESNO, IDNO) = IDYES then begin
      ExtractTemporaryFile('dotNetFx40_Client_setup.exe');
      Exec(ExpandConstant('{tmp}\dotNetFx40_Client_setup.exe'), '', '', SW_SHOW, ewNoWait, ResultCode);
    end;
  end;

  // "Upgrade" from an NSIS installation
  if RegQueryStringValue(HKCU, 'Software\ExpertSokoban', 'Install location', ExpSokOldPath) then begin
    if FileExists(ExpSokOldPath + '\Uninstall.exe') then begin
      if SuppressibleMsgBox(ExpandConstant('{cm:FoundOldInstallerPropmt}'), mbConfirmation, MB_OKCANCEL, IDCANCEL) = IDOK then begin
        // Copy the settings file
        ForceDirectories(ExpandConstant('{userappdata}\ExpSok\UpgradeFromNSIS'));
        FileCopy(ExpSokOldPath + '\ExpSok.settings.xml', ExpandConstant('{userappdata}\ExpSok\ExpSok.Settings.xml'), True);
        FileCopy(ExpSokOldPath + '\ExpSok.settings.xml', ExpandConstant('{userappdata}\ExpSok\ExpSok.Settings.xml.backup'), False);
        // Copy all level files to the profile dir, as a backup
        if FindFirst(ExpSokOldPath + '\*.txt', FindRec) then begin
          try
            repeat
              if FindRec.Attributes and FILE_ATTRIBUTE_DIRECTORY = 0 then
                FileCopy(ExpSokOldPath + '\' + FindRec.Name, ExpandConstant('{userappdata}\ExpSok\' + FindRec.Name), False);
            until not FindNext(FindRec);
          finally
            FindClose(FindRec);
          end;
        end;
        // Run the uninstaller
        Exec(ExpSokOldPath + '\Uninstall.exe', '', '', SW_SHOW, ewWaitUntilTerminated, ResultCode);
        Result := True;
      end else begin
        Result := False;
      end;
    end;
  end;
end;

// This function is acutally used as an indication that the setup has completed successfully.
function GetCustomSetupExitCode(): Integer;
var
  AppDataExpSok, DestFile: String;
  FindRec: TFindRec;
begin
  AppDataExpSok := ExpandConstant('{userappdata}\ExpSok\');
  Result := 0;
  if DirExists(AppDataExpSok + 'UpgradeFromNSIS') then begin
    // Move all the level files to the install dir, overwriting any existing files
    if FindFirst(AppDataExpSok + '*.txt', FindRec) then begin
      try
        repeat
          if FindRec.Attributes and FILE_ATTRIBUTE_DIRECTORY = 0 then begin
            DestFile := ExpandConstant('{app}\' + FindRec.Name);
            DeleteFile(DestFile);
            RenameFile(AppDataExpSok + FindRec.Name, DestFile);
          end;
        until not FindNext(FindRec);
      finally
        FindClose(FindRec);
      end;
    end;
    // Delete the marker directory
    RemoveDir(AppDataExpSok + 'UpgradeFromNSIS');
  end;
end;

function GetDefaultDirName(Param: String): String;
var
  ExpSokOldPath: String;
begin
  if RegQueryStringValue(HKCU, 'Software\ExpertSokoban', 'Install location', ExpSokOldPath) then begin
    Result := ExpSokOldPath;
  end else begin
    Result := ExpandConstant('{pf}\Expert Sokoban');
  end;
end;
