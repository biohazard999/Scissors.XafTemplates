#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0"

var target = string.IsNullOrEmpty(Argument("target", "Default")) ? "Default" : Argument("target", "Default");

Task("Pack")
    .Does(() =>
    {
        Information("Task is running");
    });

Task("Default")
    .IsDependentOn("Pack");

RunTarget(target);