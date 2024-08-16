#!/bin/bash
set -euo pipefail

project=$1
dot_notation=$(echo "$project" | tr / .)
package_name="SamHowes.Extensions.$dot_notation"
bin_folder="$project/bin/Release"
package_path="$bin_folder/$package_name"

api_key=$(cat ~/.samhowes/api_keys/nuget)

echo "=== Deleted previous packages ==="
rm -rf "$bin_folder"

echo "==== Packing $project =============================================================="
dotnet pack $project

echo "==== Pushing $package_name =============================================================="
dotnet nuget push "$package_path.*" -s https://api.nuget.org/v3/index.json -k "$api_key"