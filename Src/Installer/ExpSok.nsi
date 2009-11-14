  !include MUI.nsh
  !include LogicLib.nsh

;--------------------------------
;General

  !define myName "Expert Sokoban"
  !define myNameNospaces "ExpertSokoban"
  !define myVer "1.2.0"
  !define myInstDir "$PROGRAMFILES\${myName}"
  !define myRegistryRoot "HKCU"
  !define myRegistry "Software\${myNameNospaces}"

  !define myPath "..\..\.."
  !define myPathBin "${myPath}\builds\Release-AnyCPU"
  !define myPathGraphics "${myPath}\main\Graphics\Installer"


  !addincludedir "${myPath}\main\tools\installer"
  !include "DotNET.nsh"



  Name "${myName}"
  OutFile "${myNameNoSpaces}-${myVer}.exe"
  InstallDir "${myInstDir}"

  ;Get installation folder from registry if available
  InstallDirRegKey "${myRegistryRoot}" "${myRegistry}" "Install location"
  ;Start Menu Folder Page Configuration
  !define MUI_STARTMENUPAGE_REGISTRY_ROOT "${myRegistryRoot}"
  !define MUI_STARTMENUPAGE_REGISTRY_KEY "${myRegistry}"
  !define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start menu folder"

  BrandingText "${myName} Installer"

  SetCompressor /SOLID lzma

;--------------------------------
;Interface Settings

  !define MUI_HEADERIMAGE
  !define MUI_HEADERIMAGE_RIGHT
  !define MUI_HEADERIMAGE_BITMAP "${myPathGraphics}\orange-r.bmp"
  !define MUI_ICON "${myPathGraphics}\orange-install.ico"
  !define MUI_UNICON "${myPathGraphics}\orange-uninstall.ico"
  !define MUI_ABORTWARNING

;--------------------------------
;Variables

  Var MUI_TEMP
  Var STARTMENU_FOLDER

;--------------------------------
;Pages

  !define MUI_PAGE_HEADER_TEXT "Information about Expert Sokoban"
  !define MUI_PAGE_HEADER_SUBTEXT "This tells you what it is you are about to install"
  !define MUI_LICENSEPAGE_TEXT_TOP "Welcome to Expert Sokoban installer!"
  !define MUI_LICENSEPAGE_TEXT_BOTTOM "Please press ''Next'' to continue."
  !define MUI_LICENSEPAGE_BUTTON "Next >"
  !insertmacro MUI_PAGE_LICENSE "Info.rtf"
  !insertmacro MUI_PAGE_LICENSE "Licence.rtf"
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_STARTMENU Application $STARTMENU_FOLDER
  !insertmacro MUI_PAGE_INSTFILES

  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES

;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "English"



;--------------------------------
;Initialise

Function .onInit
  System::Call 'kernel32::CreateMutexA(i 0, i 0, t "a2hpm9ef8sfsh2") i .r1 ?e'
  Pop $R0

  StrCmp $R0 0 +3
    MessageBox MB_OK|MB_ICONEXCLAMATION "The installer is already running."
    Abort

FunctionEnd



;--------------------------------
;Installer Sections

Section "ExpSok" MainSection

  SectionIn RO

  !insertmacro CheckDotNET "2"

  SetOutPath "$INSTDIR"
  File "${myPathBin}\ExpSok.exe"
  File "${myPathBin}\RT.Util.dll"
  File "..\OriginalLevels.txt"
  File "..\Timwi.txt"
  File "${myPathBin}\ExpSok.chm"

  SetOutPath "$INSTDIR\Translations"
  File "..\Translations\ExpSok.de.xml"
  File "..\Translations\ExpSok.eo.xml"

  ;Store installation folder
  WriteRegStr ${myRegistryRoot} ${myRegistry} "Install location" $INSTDIR

  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

  ;Add/Remove Programs entry
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${myNameNoSpaces}" \
                   "DisplayName" "${myName} ${myVer}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${myNameNoSpaces}" \
                   "UninstallString" "$INSTDIR\Uninstall.exe"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${myNameNoSpaces}" \
                   "DisplayVersion" "${myVer}"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${myNameNoSpaces}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${myNameNoSpaces}" "NoRepair" 1
  ;Program-specific entries
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${myNameNoSpaces}" \
                   "DisplayIcon" "$INSTDIR\ExpSok.exe,0"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${myNameNoSpaces}" \
                   "URLInfoAbout" "http://www.cutebits.com/ExpSok"

  ;Create shortcuts
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    CreateDirectory "$SMPROGRAMS\$STARTMENU_FOLDER"
    CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
    CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\Expert Sokoban.lnk" "$INSTDIR\ExpSok.exe"
    CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\Expert Sokoban Help.lnk" "$INSTDIR\ExpSok.chm"
  !insertmacro MUI_STARTMENU_WRITE_END

SectionEnd

;--------------------------------
;Uninstaller Section

Section "Uninstall"

  IfFileExists "$INSTDIR\ExpSok.Settings.dat" 0 delconfig_no
  MessageBox MB_YESNOCANCEL|MB_ICONQUESTION  "Would you also like to delete Expert Sokoban highscores and settings?" IDNO delconfig_no IDCANCEL delconfig_cancel
  Delete "$INSTDIR\ExpSok.Settings.dat"
delconfig_no:

  Delete "$INSTDIR\ExpSok.exe"
  Delete "$INSTDIR\RT.Util.dll"
  Delete "$INSTDIR\OriginalLevels.txt"
  Delete "$INSTDIR\Timwi.txt"
  Delete "$INSTDIR\ExpSok.chm"
  Delete "$INSTDIR\Translations\*.xml"
  RMDir "$INSTDIR\Translations"

  Delete "$INSTDIR\Uninstall.exe"
  RMDir "$INSTDIR"

  ; Remove links
  !insertmacro MUI_STARTMENU_GETFOLDER Application $MUI_TEMP
  Delete "$SMPROGRAMS\$MUI_TEMP\Uninstall.lnk"
  Delete "$SMPROGRAMS\$MUI_TEMP\Expert Sokoban.lnk"
  Delete "$SMPROGRAMS\$MUI_TEMP\Expert Sokoban Help.lnk"
  RMDir "$SMPROGRAMS\$MUI_TEMP"

  ; Clean up the registry
  DeleteRegKey ${myRegistryRoot} ${myRegistry}
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${myName}"
  GoTo done

delconfig_cancel:
  Abort "Uninstaller was aborted by the user."

done:
SectionEnd
