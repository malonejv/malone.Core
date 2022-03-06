<#
.SYNOPSIS
Runs Pack-Manully tasks.

.EXAMPLE
.\Pack-Manually

#>
[CmdletBinding()]
param (
)

	$ErrorActionPreference = 'Stop'

	Write-Verbose "Verbose: '$($PSBoundParameters['Verbose'])'"
	if($PSBoundParameters['Verbose'] -and $VerbosePreference -ne 'Continue') {
		Write-Verbose "VerbosePreference: $VerbosePreference"
		$VerbosePreference = 'Continue'
		Write-Verbose "VerbosePreference setted to: $VerbosePreference"

		Write-Host ""
	}

	if($Environment -eq 'Local' -and $Configuration -ne "DebugNuget"){
		return
	}

	if($PSBoundParameters['Verbose']) {
		Write-Host ""
	}

	$ScriptsName = [System.IO.Path]::GetFileNameWithoutExtension($($MyInvocation.MyCommand.Path | Split-Path -Leaf))
	Write-Verbose "Script name: $ScriptsName"

	if($PSBoundParameters['Verbose']) {
		Write-Host ""
	}

	$PropagateVerbose=($PSCmdlet.MyInvocation.BoundParameters["Verbose"].IsPresent -eq $true)
	$Environment = $env:MALONECORE_ENVIRONMENT
	$SolutionDir=".\"
	$SolutionPath=".\malone.Core.sln"
	$Configuration="DebugNuget"
	$OutputDir=$env:PKG_ARTIFACTS_DIR
	$ShouldPack=$env:MALONECORE_SHOULDPACK
	$Verbose=$env:MALONECORE_VERBOSE
	
	.\BuildScripts\Pre-BuildEvent.ps1 -SolutionDir $SolutionDir -Environment $Environment -Verbose:([bool]::parse($Verbose))
	
	msbuild $SolutionPath /t:Build /p:Configuration=$Configuration
	
    .\BuildScripts\Post-BuildEvent.ps1 -SolutionPath $SolutionPath -Environment $Environment -Configuration $Configuration -OutputDir $OutputDir -ShouldPack:([bool]::parse($ShouldPack)) -Verbose:([bool]::parse($Verbose))