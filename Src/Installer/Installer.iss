#define Src "..\..\..\builds\ExpSok\Release-Obfuscated\ExpSok.exe"
#define SrcRaw "..\..\..\builds\ExpSok\Release-Raw"
#define FileVerStr GetFileVersion(Src)
#define ShortVerStr Copy(FileVerStr, 1, RPos(".", FileVerStr)-1)
#define UIVerStr Copy(FileVerStr, 1, RPos(".", FileVerStr)-1) + " (" + Copy(FileVerStr, RPos(".", FileVerStr)+1) + ")"

[Files]
Source: {#Src}; DestDir: {app}; DestName: ExpSok.exe;
Source: {#SrcRaw}\Translations\*; DestDir: {app}\Translations;
Source: {#SrcRaw}\expsok.chm; DestDir: {app}; DestName: ExpSok.chm;
Source: "..\OriginalLevels.txt"; DestDir: {app};
Source: "..\Timwi.txt"; DestDir: {app};
Source: LicenseAgreement.rtf; DestDir: {app};
Source: dotNetFx40_Client_setup.exe; Flags: dontcopy

[InnoIDE_Settings]
UseRelativePaths=true

[Setup]
AppCopyright=© 2006-2011 CuteBits
AppName=Expert Sokoban
DefaultDirName={pf}\Expert Sokoban
OutputDir=..\..\..\builds\ExpSok\Installer
OutputBaseFilename=ExpertSokobanSetup-{#ShortVerStr}
AppID={{62058A0A-DECE-43A8-9B56-52D161CE0EE7}
SolidCompression=yes
Compression=lzma2/max
InternalCompressLevel=max
CompressionThreads=1
ShowLanguageDialog=auto
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
LicenseFile=LicenseAgreement.rtf
AppMutex=Global\ExpSokMutex7FDC0158CF9E

[Icons]
Name: "{group}\{cm:UninstallProgram, Expert Sokoban}"; Filename: {uninstallexe};
Name: {group}\Expert Sokoban; Filename: {app}\ExpSok.exe; Comment: "Expert Sokoban is a Sokoban game aimed at the expert."; 

[Code]

procedure ViewLicenseButtonClick(Sender: TObject);
var WordpadLoc: String;
	RetCode: Integer;
begin
  RegQueryStringValue(HKLM, 'SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WORDPAD.EXE', '', WordpadLoc);

  // on NT/2000 it's a REG_EXPAND_SZ, so expand constant ProgramFiles
  StringChange(WordpadLoc, '%ProgramFiles%', ExpandConstant('{pf}'));
  // remove " at begin and end pf string
  StringChange(WordpadLoc, '"', '');

  try
    ExtractTemporaryFile('LicenseAgreement.rtf')
  except
    MsgBox('Cannot extract license file.', mbError, mb_Ok);
  end;

  if not Exec(WordpadLoc, '"' + ExpandConstant('{tmp}\LicenseAgreement.rtf') + '"', '', SW_SHOW, ewNoWait, RetCode) then
    MsgBox('Cannot display license file.', mbError, mb_Ok);
end;

procedure CurPageChanged(CurPageID: Integer);
var ViewLicenseButton: TButton;
begin
  if CurPageID = wpLicense then begin
    ViewLicenseButton := TButton.Create(WizardForm.LicenseMemo.Parent);
    ViewLicenseButton.Caption := '&View in WordPad';
    ViewLicenseButton.Width := 120;
    ViewLicenseButton.Left := WizardForm.LicenseMemo.Left +
                        WizardForm.LicenseMemo.Width - ViewLicenseButton.Width;
    ViewLicenseButton.Top := WizardForm.LicenseMemo.Top +
                        WizardForm.LicenseMemo.Height + 16;
    ViewLicenseButton.OnClick := @ViewLicenseButtonClick;
    ViewLicenseButton.Parent := WizardForm.LicenseAcceptedRadio.Parent;
  end;
end;

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
    if SuppressibleMsgBox('Expert Sokoban requires .NET Framework 4.0 Client Profile or higher. You do not have it installed. Would you like to install it now?', mbConfirmation, MB_YESNO, IDNO) = IDYES then begin
      ExtractTemporaryFile('dotNetFx40_Client_setup.exe');
      Exec(ExpandConstant('{tmp}\dotNetFx40_Client_setup.exe'), '', '', SW_SHOW, ewNoWait, ResultCode);
    end;
  end;

  // "Upgrade" from an NSIS installation
  if RegQueryStringValue(HKCU, 'Software\ExpertSokoban', 'Install location', ExpSokOldPath) then begin
    if FileExists(ExpSokOldPath + '\Uninstall.exe') then begin
      if SuppressibleMsgBox('An earlier version of Expert Sokoban is installed on your system. This must be uninstalled first. Don''t worry, your maps and progress will be preserved. Proceed?', mbConfirmation, MB_OKCANCEL, IDCANCEL) = IDOK then begin
        // Copy the settings file
        ForceDirectories(ExpandConstant('{userappdata}\ExpSok\UpgradeFromNSIS'));
        FileCopy(ExpSokOldPath + '\ExpSok.settings.xml', ExpandConstant('{userappdata}\ExpSok\ExpSok.Settings.xml'), True);
        FileCopy(ExpSokOldPath + '\ExpSok.settings.xml', ExpandConstant('{userappdata}\ExpSok\ExpSok.Settings.xml.backup'), False);
        // Copy all maps to the profile dir, as a backup
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
    // Move all the maps to the install dir, overwriting any existing files
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