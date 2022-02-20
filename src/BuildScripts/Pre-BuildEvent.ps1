<#
.SYNOPSIS
Runs pre-buildevent tasks.
 
.PARAMETER SolutionDir
Path where the solution file (.sln) is located.
 
.PARAMETER TargetDir
Path of the output files of the build.
 
.PARAMETER Environment
Environment in which the version number would be updated (by default 'Development').
 
.EXAMPLE
.\Pre-BuildEvent -SolutionDir $SolutionDir -TargetDir $TargetDir -Environment $Environment'
 
#>
[CmdletBinding()]
param (
    [parameter(Mandatory=$true)]
    [string]
    $SolutionDir,
    [parameter(Mandatory=$true)]
    [string]
    $TargetDir,
	[ValidateSet('Development','Production','Local')]
	[string]
	$Environment="Development"
)

    $ErrorActionPreference = 'Stop'

    Write-Verbose "Verbose: '$($PSBoundParameters['Verbose'])'"
    if($PSBoundParameters['Verbose'] -and $VerbosePreference -ne 'Continue') {
        Write-Verbose "VerbosePreference: $VerbosePreference"
        $VerbosePreference = 'Continue'
        Write-Verbose "VerbosePreference setted to: $VerbosePreference"
    
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

    $GetAssemblyScript="$ScriptsDir\Get-AssemblyVersion.ps1"
    $UpdAssemblyScript="$ScriptsDir\Upd-AssemblyVersion.ps1"

    $AssemblyVersionPath="$($SolutionDir)GlobalAssemblyInfo.cs"
    $CurrentVersionPath="$($SolutionDir)CurrentVersion"
    
	if (!$Environment) {
		$Environment = 'Development'
	}

    Write-Verbose "  GetAssemblyScript: $GetAssemblyScript"
    Write-Verbose "  UpdAssemblyScript: $UpdAssemblyScript"
    Write-Verbose "  CurrentVersionPath: $CurrentVersionPath"
    Write-Verbose "  AssemblyVersionPath: $AssemblyVersionPath"
    Write-Verbose "  Environment: $Environment"
    
	if($PSBoundParameters['Verbose']) {
        Write-Host ""
	}

    Write-Verbose "Finding current Assembly Version"
    Write-Verbose " Command: Invoke-Expression `"$GetAssemblyScript -Path `"$AssemblyVersionPath`" -VersionAttribute `"AssemblyFileVersion`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))`""
    Invoke-Expression "$GetAssemblyScript -Path `"$AssemblyVersionPath`" -VersionAttribute `"AssemblyFileVersion`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))" -Outvariable AssemblyFileVersion
    Invoke-Expression "$GetAssemblyScript -Path `"$AssemblyVersionPath`" -VersionAttribute `"AssemblyVersion`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))" -Outvariable AssemblyVersion
    Invoke-Expression "$GetAssemblyScript -Path `"$AssemblyVersionPath`" -VersionAttribute `"AssemblyInformationalVersion`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))" -Outvariable AssemblyInformationalVersion
    Write-Verbose " Current Assembly Version: $AssemblyFileVersion"
    
    Write-Verbose "Updating Assembly Version"
    Write-Verbose " Command: Invoke-Expression `"$UpdAssemblyScript -CurrentVersionFilePath `"$CurrentVersionPath`" -AssemblyVersionFilePath `"$AssemblyVersionPath`" -AssemblyFileVersion `"$AssemblyFileVersion`" -AssemblyVersion `"$AssemblyVersion`" -AssemblyInformationalVersion `"$AssemblyInformationalVersion`" -Environment `"$Environment`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))`""
    Invoke-Expression "$UpdAssemblyScript -CurrentVersionFilePath `"$CurrentVersionPath`" -AssemblyVersionFilePath `"$AssemblyVersionPath`" -AssemblyFileVersion `"$AssemblyFileVersion`" -AssemblyVersion `"$AssemblyVersion`" -AssemblyInformationalVersion `"$AssemblyInformationalVersion`" -Environment `"$Environment`" -Verbose:([bool]::parse(`"$PropagateVerbose`"))" -Outvariable NewAssemblyVersion
    Write-Verbose " New Assembly Version: $NewAssemblyVersion"
