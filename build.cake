#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0"

var target = string.IsNullOrEmpty(Argument("target", "Default")) ? "Default" : Argument("target", "Default");

var artifactsDirectory = "./artifacts";

var nuspecs = new []
{
    "./src/Xaf.Module/Scissors.XafTemplates.Module.CSharp.nuspec",
    "./src/Xaf.Module.Win/Scissors.XafTemplates.Module.Win.CSharp.nuspec"
};

Task("Clean")
    .Description("Cleans build artifacts")
    .Does(() => 
    {
        if(DirectoryExists(artifactsDirectory)) DeleteDirectory(artifactsDirectory, new DeleteDirectorySettings
        {
            Force = true,
            Recursive = true
        });
    });

Task("Pack")
    .Description("Builds the templates into nuget packages using SemVer")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var gitVersion = GitVersion(new GitVersionSettings
        {
            UpdateAssemblyInfo = false
        });

        foreach(var nuspec in nuspecs)
        {
            NuGetPack(nuspec, new NuGetPackSettings
            {
                OutputDirectory = artifactsDirectory,
                Version = gitVersion.NuGetVersionV2
            });
        }
    });

Task("Uninstall")
    .Description("Uninstalls the templates via nuget")
    .Does(() =>
    {
        foreach(var nuspec in nuspecs)
        {
            var nugetPackage = File(nuspec).Path.GetFilenameWithoutExtension();
            Information($"Uninstalling {nugetPackage}");
            DotNetCoreTool($"new -u {nugetPackage}");
        }
    });

Task("Install")
    .Description("Installs the locally created templates via nuget")
    .IsDependentOn("Uninstall")
    .IsDependentOn("Uninstall:Debug")
    .IsDependentOn("Pack")
    .Does(() =>
    {
        foreach(var nuspec in nuspecs)
        {
            var nugetPackage = File(nuspec).Path.GetFilenameWithoutExtension();
            Information($"Installing {nugetPackage}");
            DotNetCoreTool($"new -i {nugetPackage} --nuget-source {artifactsDirectory}");
        }
    });

Task("Uninstall:Debug")
    .Description("Uninstalls the debug version (folder) of the templates")
    .IsDependentOn("Uninstall")
    .Does(() =>
    {
        foreach(var nuspec in nuspecs)
        {
            var dir = File(nuspec).Path.GetDirectory().MakeAbsolute(Context.Environment).FullPath.Replace("/", @"\");
            Information($"Uninstalling {dir}");
            DotNetCoreTool($"new -u {dir}");
        }
    });

Task("Install:Debug")
    .Description("Installs the debug version (folder) of the templates")
    .IsDependentOn("Uninstall")
    .IsDependentOn("Uninstall:Debug")
    .Does(() =>
    {
        foreach(var nuspec in nuspecs)
        {
            var dir = File(nuspec).Path.GetDirectory();
            Information($"Installing {dir}");
            DotNetCoreTool($"new -i {dir}");
        }
    });

#region ShortHands

Task("u")
    .Description("Shorthand for Uninstall")
    .IsDependentOn("Uninstall");

Task("i")
    .Description("Shorthand for Install")
    .IsDependentOn("Install");

Task("u:d")
    .Description("Shorthand for Uninstall:Debug")
    .IsDependentOn("Uninstall:Debug");

Task("i:d")
    .Description("Shorthand for Install:Debug")
    .IsDependentOn("Install:Debug");

#endregion

Task("Default")
    .Description("Runs the default target Pack")
    .IsDependentOn("Pack");

RunTarget(target);