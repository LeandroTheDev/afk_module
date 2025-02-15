dotnet run --project ./CakeBuild/CakeBuild.csproj -- "$@"
rm -rf "$VINTAGE_STORY/Mods/afkmodule"
cp -r ./Releases/afkmodule "$VINTAGE_STORY/Mods/afkmodule"