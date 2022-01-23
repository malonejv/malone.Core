rem ----------------------------------------------------------------------------
rem batch-file to copy *.nupkg and *.snupkg to outputdirectory
rem
rem Syntax:
rem		DebugNuget-GeneratePackage.bat ConfigurationName SolutionDir ProjectPath TargetDir TargetName 
rem
rem ----------------------------------------------------------------------------

@echo off

set SolutionDir=%1
set SolutionDir=%2
set ProjectPath=%3
set TargetDir=%4
set TargetName=%5

if %ConfigurationName% == DebugNuget (
nuget pack %ProjectPath% -Suffix dev -Properties Configuration=DebugNuget -IncludeReferencedProjects -Symbols -SymbolPackageFormat snupkg
xcopy "%TargetDir%%TargetName%*.*nupkg %SolutionDir%..\malone.Core.DevLocalNugets" /SY
rm "%TargetDir%%TargetName%*.*nupkg"
)