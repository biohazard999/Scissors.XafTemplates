@echo OFF

REM Recreate the bin direcoty (only for the demo, so .git does not get polluted)
IF EXIST bin rmdir /S /Q bin
mkdir bin && pushd bin

REM ================= 
REM Usage starts here 
REM ================= 

REM Create an empty 'editors' module for the TokenEditor and cd into it
mkdir DemoCenter && pushd DemoCenter

REM Create the application
dotnet new xaf-win -o src -n Acme.DemoCenter

REM Create a sln
dotnet new sln

REM Add projects to sln
dotnet sln add src\Acme.DemoCenterApp\Acme.DemoCenterApp.csproj
dotnet sln add src\Acme.DemoCenterApp.Win\Acme.DemoCenterApp.Win.csproj
dotnet sln add src\Acme.DemoCenter.Win\Acme.DemoCenter.Win.csproj

REM Build and restore 
dotnet restore && dotnet build

popd
popd
echo Done