<#
.SYNOPSIS
Gets the projects of the visual studio solution.

.PARAMETER $ProjectsParam
String of comma separated projects path.

.PARAMETER Configuration
Configuration of build process (by default Debug).

.PARAMETER OutputDir
Path where the artifacts will be copied.

.EXAMPLE
.\Pack-Projects -Projects $Projects -Configuration $Configuration -OutputDir $OutputDir

#>
[CmdletBinding()]
param (
	[parameter(Mandatory=$true)]
	[string]
    $ProjectsParam,
	[ValidateSet('Debug','DebugNuget','Release')]
	[string]
	$Configuration="Debug",
    [parameter(Mandatory=$true)]
    [string]
    $OutputDir
)

    $ErrorActionPreference = 'Stop'

    Write-Verbose "Verbose: '$($PSBoundParameters['Verbose'])'"
    if($PSBoundParameters['Verbose'] -and $VerbosePreference -ne 'Continue') {
        Write-Verbose "VerbosePreference: $VerbosePreference"
        $VerbosePreference = 'Continue'
        Write-Verbose "VerbosePreference setted to: $VerbosePreference"

        Write-Host ""
    }

	if (!$Configuration) {
		$Configuration = 'Debug'
	}

	if($PSBoundParameters['Verbose']) {
        Write-Host ""
	}

    $ScriptsName = [System.IO.Path]::GetFileNameWithoutExtension($($MyInvocation.MyCommand.Path | Split-Path -Leaf))
    Write-Verbose "Script name: $ScriptsName"

    if($PSBoundParameters['Verbose']) {
        Write-Host ""
	}

    Write-Verbose "Input Patameters:"
    $PSBoundParameters.GetEnumerator() | ForEach {
            Write-Verbose "  $_"
    }

	if($PSBoundParameters['Verbose']) {
        Write-Host ""
	}

    $PropagateVerbose=($PSCmdlet.MyInvocation.BoundParameters["Verbose"].IsPresent -eq $true)

    Write-Verbose "Defined variables:"
    Write-Verbose "  Current script: $($MyInvocation.MyCommand.Path)"
    $ScriptsDir = $MyInvocation.MyCommand.Path | Split-Path

    $Projects=$projectsParam.split(',')
    $ProjectsCount = $Projects.count
    Write-Verbose "  Projects count: $ProjectsCount"

    New-Item "$OutputDir" -ItemType Directory  -Force
	#$ProjectsCount-1 -> El -1 es porque cuando concateno con , le estoy poniendo , al último elemento.
    for ($projectsCounter=0; $projectsCounter -lt $ProjectsCount-1; $projectsCounter++) {
        $projectPath=$Projects[$projectsCounter].Trim()
        $projectsName = Split-Path $projectPath -Leaf
        Write-Verbose "Path: $projectPath"
        Write-Verbose "Packing Project: $([int]$projectsCounter+1). $projectsName"
	    Write-Progress `
		    -Activity "Packing progress:" `
		    -PercentComplete ([int](100 * $([int]$projectsCounter+1) / $ProjectsCount)) `
		    -CurrentOperation ("Completed {0}%" -f ([int](100 * $([int]$projectsCounter+1) / $ProjectsCount))) `
		    -Status ("Packing project: [{0}]" -f ($projectsName)) `
		    -Id 1

        if (!$Configuration -eq 'Debug') {
            nuget pack "$projectPath" -Properties Configuration="$Configuration" -Symbols -SymbolPackageFormat snupkg -IncludeReferencedProjects -OutputDirectory "$OutputDir"
	    }else{
            nuget pack "$projectPath" -Properties Configuration="$Configuration" -IncludeReferencedProjects -OutputDirectory "$OutputDir"
        }
        
    }
