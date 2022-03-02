<#
.SYNOPSIS
Runs pre-buildevent tasks.

.PARAMETER SolutionPath
Path of the solution file (.sln).

.PARAMETER Environment
Environment in which the version number would be updated (by default 'Development').

.PARAMETER Configuration
Configuration of build process (by default Debug).

.PARAMETER BuildArtifactsPath
Path where the build-artifacts where generated.

.PARAMETER OutputDir
Path where the pack-artifacts will be copied.

.PARAMETER ShouldPack
Where the packing process should execute.

.EXAMPLE
.\Post-BuildEvent -SolutionPath $SolutionPath -Environment $Environment -Configuration $Configuration -BuildArtifactsPath $BuildArtifactsPath -OutputDir $OutputDir -ShouldPack $ShouldPack'

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
    [ValidateScript({ Test-Path -Path $_ -PathType Container })]
    [string]
    $BuildArtifactsPath,
    [string]
    $OutputDir,
    [bool]
    $ShouldPack=$false
)
    if($ShouldPack -eq $true){
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

        if($Environment -eq 'Local' -and $Configuration -ne "DebugNuget"){
            return
	    }elseif($Environment -eq 'Production'){
            $Configuration = 'Release'
        }else{
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

        #$OutputDir = ($OutputDir | Resolve-Path).ProviderPath

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

        $projects | foreach{ $projectsParam+="$_, " }

        if($Environment -eq 'Development' -or $Environment -eq 'Production'){
            Write-Verbose "Copying build-artifacts"
            Copy-Item "$BuildArtifactsPath" -Destination "$solutionDir" -Force -Recurse -Container
        }

        Write-Verbose "Packing the solution projects"
        Write-Verbose " Command: Invoke-Expression `"$PackProjectsScript -Projects `"$projectsParam`" -PackingProperties `"$PackingProperties`" -Configuration `"$Configuration`" -OutputDir `"$OutputDir`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))`""
        Invoke-Expression "$PackProjectsScript -Projects `"$projectsParam`" -Configuration `"$Configuration`" -OutputDir `"$OutputDir`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))"
    }