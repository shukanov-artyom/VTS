cd "VTS Shared" 
call Build.cmd

cd "..\VTS Agent" 
call Build.cmd

cd "..\VTS Agent Installer" 
call Build.cmd

REM Deprecated Silverlight App
REM cd "..\VTS Monitor" 
REM call Build.cmd

cd "..\VTS Webswervice" 
call Build.cmd

cd "..\VTS Website" 
call Build.cmd

cd "..\VTS Agent Lite"
call Build.cmd

cd "..\VTS Version Modifier"
call Build.cmd
cd ..