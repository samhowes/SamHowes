workspace(name = "samhowes")

# bzl:generated start
load("@bazel_tools//tools/build_defs/repo:http.bzl", "http_archive")

http_archive(
    name = "rules_msbuild",
    sha256 = "f4d51c470245f8a2b2b47a4632f32b4416773ced557b4906ab1922ea14c22313",
    urls = ["https://github.com/samhowes/rules_msbuild/releases/download/0.0.16/rules_msbuild-0.0.16.tar.gz"],
)

load("@rules_msbuild//dotnet:deps.bzl", "msbuild_register_toolchains", "msbuild_rules_dependencies")

msbuild_rules_dependencies()

# See https://dotnet.microsoft.com/download/dotnet for valid versions
msbuild_register_toolchains(version = "host")

load("//:deps/nuget.bzl", "nuget_deps")

# bzl:generated end

# gazelle:nuget_macro deps/nuget.bzl%nuget_deps
nuget_deps()