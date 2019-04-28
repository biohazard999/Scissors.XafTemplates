# Scissors.XafTemplates

[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg)](http://commitizen.github.io/cz-cli/) [![Build status](https://mgrundner.visualstudio.com/Scissors/_apis/build/status/Scissors-ASP.NET%20Core-CI)](https://mgrundner.visualstudio.com/Scissors/_build/latest?definitionId=52)

This project contains dotnet new templates for [DevExpress](https://www.devexpress.com/) [expressAppFramework](https://www.devexpress.com/products/net/application_framework/) and [Scissors](https://github.com/biohazard999/Scissors.FeatureCenter).

## Installation

### All templates

```cmd
dotnet new -i Scissors.XafTemplates
```

### Individual templates

```cmd
dotnet new -i Scissors.XafTemplates.Module.CSharp
dotnet new -i Scissors.XafTemplates.Module.Win.CSharp
```

## Available templates

- `dotnet new xaf-module`: Creates an empty base module
- `dotnet new xaf-win-module`: Creates an empty windows-forms module

## Samples & Demos

Currently the templates are rather basic, so usage is a little bit limited, but they work for us right now. Feel free to file an [issue](https://github.com/biohazard999/Scissors.XafTemplates/issues) or send an [pull-request](https://github.com/biohazard999/Scissors.XafTemplates/pulls).

See [demos](./demos/README.md) for more details.

### Empty Modules

#### BaseModule

```cmd
dotnet new xaf-module -o Base -n Scissors.Modules.TokenEditor
```

#### WindowsFormsModule

```cmd
dotnet new xaf-win-module -o Win -n Scissors.Modules.TokenEditor
```

## Building & Development

### Prerequisites

- Node >= [10.15.3 (LTS)](https://nodejs.org/dist/v10.15.3/node-v10.15.3-x64.msi)
- NPM >= 6.9.0
  - `npm i npm@6.9.0` or `npm i npm@latest`
- .NET Core >= [SDK 2.2](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.203-windows-x64-installer)

### Building

For local development, make sure to uninstall all templates using `build Uninstall:Debug` after that, you can use `build Install:Debug` and work on the templates.

```txt
Task                          Description
================================================================================
Clean                         Cleans build artifacts
Pack                          Builds the templates into nuget packages using SemVer

Uninstall                     Uninstalls the templates via nuget
Install                       Installs the locally created templates via nuget

Uninstall:Debug               Uninstalls the debug version (folder) of the templates
Install:Debug                 Installs the debug version (folder) of the templates

u                             Shorthand for Uninstall
i                             Shorthand for Install

u:d                           Shorthand for Uninstall:Debug
i:d                           Shorthand for Install:Debug

Default                       Runs the default target Pack
```

> Use `tools\Cake\Cake.exe --showdescription` for the actual build tasks

### Contributing

This project uses branch protection and is [Commitizen friendly](http://commitizen.github.io/cz-cli/).

First run `npm i` to install the dependencies for committing.

To perform a change switch to a new branch (from `master`):

`git checkout -b topic/featurename`

Start you work. Commit often and in small chunks by running through the wizard.

- `git add .` or stage your changes by hand.
- `npm run cz`
- `git push`

Push your work and perform a [pull-request](https://github.com/biohazard999/Scissors.XafTemplates/pulls).
