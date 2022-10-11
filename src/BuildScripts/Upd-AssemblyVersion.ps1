<#
.SYNOPSIS
Updates the version number and updates assembly version file.

.PARAMETER CurrentVersionFilePath
Path to the CurrentVersion file where is stored a json with Development and Production version numbers.

.PARAMETER AssemblyVersionFilePath
Path to the assemblyinfo file.

.PARAMETER AssemblyFileVersion
Version number used in AssemblyFileVersion attribute.

.PARAMETER AssemblyVersion
Version number used in AssemblyVersion attribute.

.PARAMETER AssemblyInformationalVersion
Version number used in AssemblyInformationalVersion attribute.

.PARAMETER Environment
Environment in which the version number would be updated (by default 'Development').

.EXAMPLE
.\Upd-AssemblyVersion -CurrentVersionFilePath $CurrentVersionFilePath -AssemblyVersionFilePath $AssemblyVersionFilePath -AssemblyFileVersion $AssemblyFileVersion [-AssemblyVersion=$AssemblyVersion] [-AssemblyInformationalVersion=$AssemblyInformationalVersion] [-Environment=$Environment]
#>

[CmdletBinding()]
param (

	[parameter(Mandatory=$true)]
	[ValidateScript({ Test-Path -Path $_ -PathType Leaf })]
	[string]
	$CurrentVersionFilePath,

	[parameter(Mandatory=$true)]
	[ValidateScript({ Test-Path -Path $_ -PathType Leaf })]
	[string]
	$AssemblyVersionFilePath,

	[parameter(Mandatory=$true)]
	[string]
	$AssemblyFileVersion,

	[string]
	$AssemblyVersion,

	[string]
	$AssemblyInformationalVersion,

	[parameter(Mandatory=$true)]
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
		Write-Host ""

	}

	$ScriptsName = [System.IO.Path]::GetFileNameWithoutExtension($($MyInvocation.MyCommand.Path | Split-Path -Leaf))
	Write-Verbose "Script name: $ScriptsName"

	if($PSBoundParameters['Verbose'] -and $VerbosePreference -ne 'Continue') {
		Write-Host ""
	}

	Write-Verbose "Input Patameters:"
	$PSBoundParameters.GetEnumerator() | ForEach {
			Write-Verbose "  $_"
	}

	if($PSBoundParameters['Verbose'] -and $VerbosePreference -ne 'Continue') {
		Write-Host ""
	}

	$PropagateVerbose=($PSCmdlet.MyInvocation.BoundParameters["Verbose"].IsPresent -eq $true)

	Write-Verbose "Defined variables:"
	$CurrentVersionPath = ($CurrentVersionFilePath | Resolve-Path).ProviderPath
	Write-Verbose "  Current Version Path: $CurrentVersionPath"

	$currentVersion=Get-Content "$CurrentVersionPath" | ConvertFrom-Json
	Write-Verbose "  Current Version: $currentVersion"

	$regexExpression="(\d+)\.(\d+)\.(\d+)\.(\d+)"
	Write-Verbose "  Regular expression: $regexExpression"

	if($PSBoundParameters['Verbose'] -and $VerbosePreference -ne 'Continue') {
		Write-Host ""
	}

	$assemblyVersionMatch=[regex]::Match($AssemblyFileVersion,$regexExpression).captures
	$assemblyVersionMajor=$assemblyVersionMatch.Groups[1].value
	$assemblyVersionMinor=$assemblyVersionMatch.Groups[2].value
	$assemblyVersionPatch=$assemblyVersionMatch.Groups[3].value
	$assemblyVersionRevision=$assemblyVersionMatch.Groups[4].value

	$newAssemblyVersionMajor=0
	$newAssemblyVersionMinor=0
	$newAssemblyVersionPatch=0
	$newAssemblyVersionRevision=0

	if($Environment -eq "Development"){
		$currentVersionMatch=[regex]::Match($currentVersion.development,$regexExpression).captures
		Write-Verbose "Current Version match: $($currentVersionMatch.count)"

		$currentMajor=$currentVersionMatch.Groups[1].value
		$currentMinor=$currentVersionMatch.Groups[2].value
		$currentPatch=$currentVersionMatch.Groups[3].value
		$currentRevision=$currentVersionMatch.Groups[4].value
		Write-Verbose "Current match groups: $currentMajor.$currentMinor.$currentPatch.$currentRevision"

		if($assemblyVersionMajor -lt $currentMajor){
			$newAssemblyVersionMajor=$currentMajor
			$newAssemblyVersionMinor=$currentMinor
			$newAssemblyVersionPatch=([int]$currentPatch)+1
		}elseif($assemblyVersionMajor -gt $currentMajor){
			$newAssemblyVersionMajor=$assemblyVersionMajor
			$newAssemblyVersionMinor=0
			$newAssemblyVersionPatch=0
		}elseif($assemblyVersionMajor -eq $currentMajor){
			$newAssemblyVersionMajor=$currentMajor
			if($assemblyVersionMinor -lt $currentMinor){
				$newAssemblyVersionMinor=$currentMinor
				$newAssemblyVersionPatch=([int]$currentPatch)+1
			}elseif($assemblyVersionMinor -gt $currentMinor){
				$newAssemblyVersionMinor=$assemblyVersionMinor
			}elseif($assemblyVersionMinor -eq $currentMinor){
				$newAssemblyVersionMinor=$currentMinor

				if([int]$assemblyVersionPatch -gt [int]$currentPatch){
					$newAssemblyVersionPatch=$assemblyVersionPatch
				}else{
					$newAssemblyVersionPatch=([int]$currentPatch)+1
				}
			}
		}
		$newAssemblyVersionRevision=0
	}elseif($Environment -eq "Local"){
		$newAssemblyVersionMajor=$assemblyVersionMajor
		$newAssemblyVersionMinor=$assemblyVersionMinor
		$newAssemblyVersionPatch=$assemblyVersionPatch
		$newAssemblyVersionRevision=([int]$assemblyVersionRevision)+1
	}elseif($Environment -eq "Production") {
		$regexExpression="(\d+)\.(\d+)\.(\d+)"
		Write-Verbose "  Regular expression for Production: $regexExpression"

		$currentVersionMatch=[regex]::Match($currentVersion.production,$regexExpression).captures
		Write-Verbose "Current Version match: $($currentVersionMatch.count)"

		$currentMajor=$currentVersionMatch.Groups[1].value
		$currentMinor=$currentVersionMatch.Groups[2].value
		$currentPatch=$currentVersionMatch.Groups[3].value
		$currentRevision=0
		Write-Verbose "Current match groups: $currentMajor.$currentMinor.$currentPatch.$currentRevision"

		$newAssemblyVersionRevision=0

		if($assemblyVersionMajor -lt $currentMajor){
			$newAssemblyVersionMajor=$currentMajor
			$newAssemblyVersionMinor=$currentMinor
			$newAssemblyVersionPatch=([int]$currentPatch)+1
		}elseif($assemblyVersionMajor -gt $currentMajor){
			$newAssemblyVersionMajor=$assemblyVersionMajor
			$newAssemblyVersionMinor=$assemblyVersionMinor
			$newAssemblyVersionPatch=$assemblyVersionPatch
		}elseif($assemblyVersionMajor -eq $currentMajor){
			$newAssemblyVersionMajor=$currentMajor
			if($assemblyVersionMinor -lt $currentMinor){
				$newAssemblyVersionMinor=$currentMinor
				$newAssemblyVersionPatch=([int]$currentPatch)+1
			}elseif($assemblyVersionMinor -gt $currentMinor){
				$newAssemblyVersionMinor=$assemblyVersionMinor
				$newAssemblyVersionPatch=$assemblyVersionPatch
			}elseif($assemblyVersionMinor -eq $currentMinor){
				$newAssemblyVersionMinor=$currentMinor
				if($assemblyVersionPatch -gt $currentPatch){
					$newAssemblyVersionPatch=$assemblyVersionPatch
				}else{
					$newAssemblyVersionPatch=([int]$currentPatch)+1
				}
			}
		}
	}

	$newAssemblyVersion="$newAssemblyVersionMajor.$newAssemblyVersionMinor.$newAssemblyVersionPatch"
	$newAssemblyFileVersion="$newAssemblyVersionMajor.$newAssemblyVersionMinor.$newAssemblyVersionPatch.$newAssemblyVersionRevision"

	#Change AssemblyInformationalVersion
	if($AssemblyInformationalVersion -and ($AssemblyFileVersion -ne $AssemblyInformationalVersion)){
		if($Environment -eq "Production"){
			$newAssemblyInformationalVersion=$newAssemblyVersion

			(Get-Content "$AssemblyVersionFilePath").replace("AssemblyInformationalVersion(`"$AssemblyVersion`")", "AssemblyInformationalVersion(`"$newAssemblyInformationalVersion`")") | Set-Content "$AssemblyVersionFilePath"
			(Get-Content "$AssemblyVersionFilePath").replace("AssemblyInformationalVersion(`"$AssemblyFileVersion-dev`")", "AssemblyInformationalVersion(`"$newAssemblyInformationalVersion`")") | Set-Content "$AssemblyVersionFilePath"
			(Get-Content "$AssemblyVersionFilePath").replace("AssemblyInformationalVersion(`"$AssemblyFileVersion-beta`")", "AssemblyInformationalVersion(`"$newAssemblyInformationalVersion`")") | Set-Content "$AssemblyVersionFilePath"
		}else{
			$indexDash=($AssemblyInformationalVersion.IndexOf("-"))
			if($indexDash -eq -1){
				$informationalVersion=$AssemblyInformationalVersion
				$prefix=""
			}else{
				$informationalVersion=$AssemblyInformationalVersion.substring(0,$indexDash)
				$prefix=$AssemblyInformationalVersion.substring($indexDash)
			}

			if($Environment -eq "Development"){
				$newAssemblyInformationalVersion="$newAssemblyFileVersion-beta"
				(Get-Content "$AssemblyVersionFilePath").replace("AssemblyInformationalVersion(`"$informationalVersion$prefix`")", "AssemblyInformationalVersion(`"$newAssemblyInformationalVersion`")") | Set-Content "$AssemblyVersionFilePath"
			}
			if($Environment -eq "Local"){
				$newAssemblyInformationalVersion="$newAssemblyFileVersion-dev"
				(Get-Content "$AssemblyVersionFilePath").replace("AssemblyInformationalVersion(`"$informationalVersion$prefix`")", "AssemblyInformationalVersion(`"$newAssemblyInformationalVersion`")") | Set-Content "$AssemblyVersionFilePath"
			}
		}
	}

	#Change AssemblyInformationalVersion and/or AssemblyFileVersion
	(Get-Content "$AssemblyVersionFilePath").replace("$AssemblyFileVersion", "$newAssemblyFileVersion") | Set-Content "$AssemblyVersionFilePath"

	#Change AssemblyVersion
	if($AssemblyVersion){
		(Get-Content "$AssemblyVersionFilePath").replace("$AssemblyVersion", "$newAssemblyVersion") | Set-Content "$AssemblyVersionFilePath"
	}

	Write-Verbose "New Assembly Version: $newAssemblyVersion"
	Write-Verbose "New Assembly File Version: $newAssemblyFileVersion"
	Write-Verbose "New Assembly Informational Version: $newAssemblyInformationalVersion"

	if($Environment -eq "Development" -Or $Environment -eq "Production"){
		if($Environment -eq "Development"){
			$currentVersion.development=$newAssemblyFileVersion
		}else{
			$currentVersion.production=$newAssemblyVersion
		}
		Write-Verbose "Updated Current Version File: $currentVersion"
		Set-Content -Value ($currentVersion | convertTo-Json) -Path "$CurrentVersionFilePath"
	}

	return $newAssemblyVersion