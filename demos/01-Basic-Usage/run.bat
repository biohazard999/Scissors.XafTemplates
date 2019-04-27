@echo OFF

REM Recreate the bin direcoty (only for the demo, so .git does not get polluted)
IF EXIST bin rmdir /S /Q bin && mkdir bin && pushd bin


REM ================= 
REM Usage starts here 
REM ================= 

REM Create an empty 'editors' module for the TokenEditor and cd into it
mkdir TokenEditors && pushd TokenEditors

REM Create the base module
dotnet new xaf-module -o Base -n Scissors.Modules.TokenEditor
REM Create the winforms module
dotnet new xaf-win-module -o Win -n Scissors.Modules.TokenEditor

pushd Win
REM Add a reference from Win to Base
dotnet add reference ..\Base\Scissors.Modules.TokenEditor.csproj
popd 

REM Build and restore Base
pushd Base && dotnet restore && dotnet build && popd
REM Build and restore Win
pushd Win && dotnet restore && dotnet build && popd

popd
popd
echo Done