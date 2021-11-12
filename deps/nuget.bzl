load("@rules_msbuild//deps:public_nuget.bzl", "FRAMEWORKS", "PACKAGES")
load("@rules_msbuild//dotnet:defs.bzl", "nuget_deps_helper", "nuget_fetch")

def nuget_deps():
    nuget_fetch(
        name = "nuget",
        packages = {
            "FluentAssertions/6.2.0": ["net5.0"],
            "Humanizer.Core/2.11.10": ["net5.0"],
            "Microsoft.CodeAnalysis.CSharp.Workspaces/3.10.0": ["net5.0"],
            "Microsoft.CodeAnalysis.CSharp/3.10.0": ["net5.0"],
            "Microsoft.CodeAnalysis.Workspaces.Common/3.10.0": ["net5.0"],
            "Microsoft.Extensions.Configuration.Binder/5.0.0": ["net5.0"],
            "Microsoft.Extensions.Configuration.FileExtensions/5.0.0": ["net5.0"],
            "Microsoft.NET.Test.Sdk/16.7.1": ["net5.0"],
            "Microsoft.SqlServer.DacFx/150.5290.2-preview": ["net5.0"],
            "RulesMSBuild.Runfiles/": ["net5.0"],
            "YamlDotNet/11.2.1": ["net5.0"],
            "coverlet.collector/1.3.0": ["net5.0"],
            "xunit.runner.visualstudio/2.4.3": ["net5.0"],
            "xunit/2.4.1": ["net5.0"],
        },
        target_frameworks = ["net5.0"],
        use_host = True,
        deps = nuget_deps_helper(FRAMEWORKS, PACKAGES),
    )
