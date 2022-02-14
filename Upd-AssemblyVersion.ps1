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
    $AssemblyVersion,
	
	[parameter(Mandatory=$true)]
    [ValidateSet('Production','Development')]
	[string]
	$Environment="Development"
	
)

	$ErrorActionPreference = 'Stop'

	if($verbose) {
	   $VerbosePreference = $verbose
	}
	
	$CurrentVersionPath = ($CurrentVersionFilePath | Resolve-Path).ProviderPath
	Write-Verbose "Current Version Path: $CurrentVersionPath"
	
	$currentVersion=Get-Content "$CurrentVersionPath" | ConvertFrom-Json
	Write-Verbose "Current Version: $currentVersion"
	
	$assemblyVersionMatch=[regex]::Match($AssemblyVersion,"(\d+)\.(\d+)\.(\d+)\.(\d+)").captures
	$assemblyVersionMajor=$assemblyVersionMatch.Groups[1].value
	$assemblyVersionMinor=$assemblyVersionMatch.Groups[2].value
	$assemblyVersionPatch=$assemblyVersionMatch.Groups[3].value
	$assemblyVersionRevision=$assemblyVersionMatch.Groups[4].value
	Write-Verbose "Assembly Version: $AssemblyVersion"
	
	$newAssemblyVersionMajor=0
	$newAssemblyVersionMinor=0
	$newAssemblyVersionPatch=0
	$newAssemblyVersionRevision=0
	
	Write-Verbose "Environment: $Environment"
	if($Environment -eq "Development"){
		$currentVersionMatch=[regex]::Match($currentVersion.development,"(\d+)\.(\d+)\.(\d+)\.(\d+)").captures
		Write-Verbose "Current match: $currentVersionMatch"

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
				$newAssemblyVersionPatch=0
			}elseif($assemblyVersionMinor -eq $currentMinor){
				$newAssemblyVersionMinor=$currentMinor
				
				if([int]$assemblyVersionPatch -gt [int]$currentPatch){
					$newAssemblyVersionPatch=$assemblyVersionPatch
					$newAssemblyVersionRevision=0
				}else{
					$newAssemblyVersionPatch=$currentPatch
					$newAssemblyVersionRevision=([int]$currentRevision)+1
				}
			}
		}
	}elseif($Environment -eq "Production") {
		$currentVersionMatch=[regex]::Match($currentVersion.production,"(\d+)\.(\d+)\.(\d+)\.(\d+)").captures
		$currentMajor=$currentVersionMatch.Groups[1].value
		$currentMinor=$currentVersionMatch.Groups[2].value
		$currentPatch=$currentVersionMatch.Groups[3].value
		$currentRevision=$currentVersionMatch.Groups[4].value
		$newAssemblyVersionRevision=0
		
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
				$newAssemblyVersionPatch=0
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
	
	
	$newAssemblyVersion="$newAssemblyVersionMajor.$newAssemblyVersionMinor.$newAssemblyVersionPatch.$newAssemblyVersionRevision"
	(Get-Content "$AssemblyVersionFilePath").replace("$AssemblyVersion", "$newAssemblyVersion") | Set-Content "$AssemblyVersionFilePath"

	if($Environment -eq "Development"){
		$currentVersion.development=$newAssemblyVersion
	}else{
		$currentVersion.production=$newAssemblyVersion
	}
	Write-Verbose "Current Version File: $currentVersion"

	Set-Content -Value ($currentVersion | convertTo-Json) -Path "$CurrentVersionFilePath"	
	Write-Verbose "New Assembly Version: $newAssemblyVersion"
	return $newAssemblyVersion
	
	
	
	
