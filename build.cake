#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0"

var target = string.IsNullOrEmpty(Argument("target", "Default")) ? "Default" : Argument("target", "Default");

var artifactsDirectory = "./artifacts";

var nuspecs = new []
{
    "./src/Xaf.Module/Scissors.XafTemplates.Module.CSharp.nuspec",
    "./src/Xaf.Module.Win/Scissors.XafTemplates.Module.Win.CSharp.nuspec"
};

Task("Clean")
    .Does(() => DeleteDirectory(artifactsDirectory, new DeleteDirectorySettings
    {
        Force = true,
        Recursive = true
    }));

Task("Pack")
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

Task("Default")
    .IsDependentOn("Pack");

RunTarget(target);