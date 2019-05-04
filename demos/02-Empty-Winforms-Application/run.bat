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
dotnet sln add src\Acme.DemoCenter.App\Acme.DemoCenter.App.csproj
dotnet sln add src\Acme.DemoCenter.App.Win\Acme.DemoCenter.App.Win.csproj
dotnet sln add src\Acme.DemoCenter.Win\Acme.DemoCenter.Win.csproj

REM Build and restore 
dotnet restore && dotnet build

REM Start the application
dotnet run DemoCenter.sln -p src\Acme.DemoCenter.Win\Acme.DemoCenter.Win.csproj

popd
popd
echo Done