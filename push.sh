#!/bin/bash
set -euo pipefail

project=$1
dot_notation=$(echo "$project" | tr / .)
package_name="SamHowes.Extensions.$dot_notation"
package_path="$project/bin/Release/$package_name"

api_key=$(cat ~/.samhowes/api_keys/nuget)

echo "==== Packing $project =============================================================="
dotnet pack $project

echo "==== Pushing $package_name =============================================================="
dotnet nuget push "$package_path.*" -s https://api.nuget.org/v3/index.json -k "$api_key"