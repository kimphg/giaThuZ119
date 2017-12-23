TITLE NuGet Packager
@ECHO ON

CD %~dp0
CD ..
nuget pack SharpVisa\SharpVisaCLI\package.nuspec
PAUSE

