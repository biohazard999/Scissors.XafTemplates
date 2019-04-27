# Scissors.XafTemplates

This project contains dotnet new templates for [DevExpress](https://www.devexpress.com/) [expressAppFramework](https://www.devexpress.com/products/net/application_framework/) and [Scissors](https://github.com/biohazard999/Scissors.FeatureCenter)

## Installation

## Available templates

- `dotnet new xaf-module`: Creates an empty base module
- `dotnet new xaf-win-module`: Creates an empty windows-forms module

## Samples & Demos

Currently the templates are rather basic, so usage is a little bit limited, but they work for us right now. Feel free to file an [issue]() or send an [pull-request]().

See [demos](./demos/README.md) for more details

### Empty Modules

#### BaseModule

```cmd
dotnet new xaf-module -o Base -n Scissors.Modules.TokenEditor
```

#### WindowsFormsModule

```cmd
dotnet new xaf-win-module -o Win -n Scissors.Modules.TokenEditor
```
