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
	[ValidateScript({ Test-Path -Path $_ -PathType Leaf })]
    [string]
    $SolutionPath,
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

	$Configuration = 'DebugNuget'

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

	$PreBuildEvent="$ScriptsDir\Pre-BuildEvent.ps1"
	$PostBuildEvent="$ScriptsDir\Post-BuildEvent.ps1"
    $SolutionDir = $SolutionPath | Split-Path -Parent

	Write-Verbose " Command: Invoke-Expression `"$PreBuildEvent -SolutionDir `"$SolutionDir`" -Environment Local -Verbose:([bool]::parse(`"$PropagateVerbose`"))`""
	Invoke-Expression "$PreBuildEvent -SolutionDir `"$SolutionDir`" -Environment Local -Verbose:([bool]::parse(`"$PropagateVerbose`"))"

    # Find the latest release
    $(Invoke-WebRequest "https://github.com/microsoft/vswhere/releases/latest").BaseResponse.ResponseUri -match "tag/(.*)$"

    # Download the vswhere.exe
    Invoke-WebRequest "https://github.com/microsoft/vswhere/releases/download/$($Matches[1])/vswhere.exe" -OutFile "vswhere.exe"

    & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" `
    -version "[16.0,18.0)" <# Visual Studio 2019 and 2022 #> `
    -products *  <# Edition of Visual Studio to search for
                    https://learn.microsoft.com/en-us/visualstudio/install/workload-and-component-ids?WT.mc_id=DT-MVP-5003978 #> `
    -requires Microsoft.Component.MSBuild `
    -prerelease            <# include prerelease versions #> `
    -latest                <# Only return the newest version that matches the criteria #> `
    -utf8 -format json     <# print the results in JSON format #>

    $vs = & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -version "[16.0,18.0)" -products * -requires Microsoft.Component.MSBuild -prerelease -latest -utf8 -format json | ConvertFrom-Json
    $msbuild = Join-Path $vs[0].installationPath "MSBuild/Current/Bin/MSBuild.exe"

    & $msbuild $SolutionPath -t:Rebuild -p:Configuration=$Configuration

	Write-Verbose " Command: Invoke-Expression `"$PostBuildEvent -SolutionPath `"$SolutionPath`" -Environment Local -Configuration `"$Configuration`" --OutputDir `"$OutputDir`" -ShouldPack:([bool]::parse(`"$true`")) -Verbose:([bool]::parse(`"$PropagateVerbose`"))`""
	Invoke-Expression "$PostBuildEvent -SolutionPath `"$SolutionPath`" -Environment Local -Configuration $Configuration --OutputDir `"$OutputDir`" -ShouldPack:([bool]::parse(`"$true`"))  -Verbose:([bool]::parse(`"$PropagateVerbose`"))"
