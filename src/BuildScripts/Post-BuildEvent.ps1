<#
.SYNOPSIS
Runs pre-buildevent tasks.
 
.PARAMETER SolutionPath
Path of the solution file (.sln).
 
.PARAMETER Environment
Environment in which the version number would be updated (by default 'Development').
 
.PARAMETER Configuration
Configuration of build process (by default Debug).
 
.PARAMETER OutputDir
Path where the artifacts will be copied.
 
.EXAMPLE
.\Post-BuildEvent -SolutionPath $SolutionPath -Environment $Environment -Configuration $Configuration -OutputDir $OutputDir'
 
#>
[CmdletBinding()]
param (
    [parameter(Mandatory=$true)]
	[ValidateScript({ Test-Path -Path $_ -PathType Leaf })]
    [string]
    $SolutionPath,
	[ValidateSet('Development','Production','Local')]
	[string]
	$Environment="Development",
	[ValidateSet('DebugNuget','Debug','Release')]
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
    
	if (!$Environment) {
		$Environment = 'Development'
	}

    if($Environment -eq 'Development' -and $Configuration -ne "DebugNuget"){
        return 
	}

    if($Environment -eq 'Development'){
        $Configuration = 'Debug'
    }else{
        $Configuration = 'Release'
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
    
    $OutputDir = ($OutputDir | Resolve-Path).ProviderPath
 
    $PropagateVerbose=($PSCmdlet.MyInvocation.BoundParameters["Verbose"].IsPresent -eq $true)
        
    Write-Verbose "Defined variables:"
    Write-Verbose "  Current script: $($MyInvocation.MyCommand.Path)"
    $ScriptsDir = $MyInvocation.MyCommand.Path | Split-Path

    $GetProjectsScript="$ScriptsDir\Get-ProjectsInSolution.ps1"
    $PackProjectsScript="$ScriptsDir\Pack-Projects.ps1"
    
    Write-Verbose "  GetProjectsScript: $GetProjectsScript"
    
    Write-Verbose "Setting up the solution projects"
    Write-Verbose " Command: Invoke-Expression `"$GetProjectsScript -Path `"$SolutionPath`" -Type `"NonTestProjects`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))`""
    Invoke-Expression "$GetProjectsScript -Path `"$SolutionPath`" -Type `"NonTestProjects`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))" -Outvariable Projects
    Write-Verbose " Projects count: $($Projects.count)"

    $projectsParam=$projects | foreach{ "$_, "}
    Write-Verbose "Projects: $projectsParam"

    Write-Verbose "Packing the solution projects"
    Write-Verbose " Command: Invoke-Expression `"$PackProjectsScript -Projects `"$projectsParam`" -PackingProperties `"$PackingProperties`" -Configuration `"$Configuration`" -OutputDir `"$OutputDir`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))`""
    Invoke-Expression "$PackProjectsScript -Projects `"$Projects`" -Configuration `"$Configuration`" -OutputDir `"$OutputDir`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))"
