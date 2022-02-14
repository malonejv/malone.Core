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
	Write-Verbose $CurrentVersionPath
	
	$currentVersion=Get-Content "$CurrentVersionPath" | ConvertFrom-Json
	Write-Verbose $currentVersion
	
	$assemblyVersionMatch=[regex]::Match($AssemblyVersion,"(\d+)\.(\d+)\.(\d+)\.(\d+)").captures
	$assemblyVersionMajor=$assemblyVersionMatch.Groups[1].value
	$assemblyVersionMinor=$assemblyVersionMatch.Groups[2].value
	$assemblyVersionPatch=$assemblyVersionMatch.Groups[3].value
	$assemblyVersionRevision=$assemblyVersionMatch.Groups[4].value
	Write-Verbose $AssemblyVersion
	
	$newAssemblyVersionMajor=0
	$newAssemblyVersionMinor=0
	$newAssemblyVersionPatch=0
	$newAssemblyVersionRevision=0
	
	if($BranchType -eq "Development"){
		$currentVersionMatch=[regex]::Match($currentVersion.development,"(\d+)\.(\d+)\.(\d+)\.(\d+)").captures
		
		$currentMajor=$currentVersionMatch.Groups[1].value
		$currentMinor=$currentVersionMatch.Groups[2].value
		$currentPatch=$currentVersionMatch.Groups[3].value
		$currentRevision=$currentVersionMatch.Groups[4].value
		
		if($assemblyVersionMajor -lt $currentMajor){
			$newAssemblyVersionMajor=$currentMajor
			$newAssemblyVersionMinor=$currentMinor
			$newAssemblyVersionPatch=$currentPatch+1
		}elseif($assemblyVersionMajor -gt $currentMajor){
			$newAssemblyVersionMajor=$assemblyVersionMajor
			$newAssemblyVersionMinor=$assemblyVersionMinor
			$newAssemblyVersionPatch=$assemblyVersionPatch
		}elseif($assemblyVersionMajor -eq $currentMajor){
			$newAssemblyVersionMajor=$currentMajor
			if($assemblyVersionMinor -lt $currentMinor){
				$newAssemblyVersionMinor=$currentMinor
				$newAssemblyVersionPatch=$currentPatch+1
			}elseif($assemblyVersionMinor -gt $currentMinor){
				$newAssemblyVersionMinor=$assemblyVersionMinor
				$newAssemblyVersionPatch=$assemblyVersionPatch
			}elseif($assemblyVersionMinor -eq $currentMinor){
				$newAssemblyVersionMinor=$currentMinor
				
				if($assemblyVersionPatch -gt $currentPatch){
					$newAssemblyVersionPatch=$assemblyVersionPatch
					$newAssemblyVersionRevision=$assemblyVersionRevision
				}else{
					$newAssemblyVersionPatch=$currentPatch
					$newAssemblyVersionRevision=$currentRevision+1
				}
			}
		}
	}elseif($BranchType -eq "Production") {
		$currentVersionMatch=[regex]::Match($currentVersion.production,"(\d+)\.(\d+)\.(\d+)\.(\d+)").captures
		$currentMajor=$currentVersionMatch.Groups[1].value
		$currentMinor=$currentVersionMatch.Groups[2].value
		$currentPatch=$currentVersionMatch.Groups[3].value
		$currentRevision=$currentVersionMatch.Groups[4].value
		$newAssemblyVersionRevision=0
		
		if($assemblyVersionMajor -lt $currentMajor){
			$newAssemblyVersionMajor=$currentMajor
			$newAssemblyVersionMinor=$currentMinor
			$newAssemblyVersionPatch=$currentPatch+1
		}elseif($assemblyVersionMajor -gt $currentMajor){
			$newAssemblyVersionMajor=$assemblyVersionMajor
			$newAssemblyVersionMinor=$assemblyVersionMinor
			$newAssemblyVersionPatch=$assemblyVersionPatch
		}elseif($assemblyVersionMajor -eq $currentMajor){
			$newAssemblyVersionMajor=$currentMajor
			if($assemblyVersionMinor -lt $currentMinor){
				$newAssemblyVersionMinor=$currentMinor
				$newAssemblyVersionPatch=$currentPatch+1
			}elseif($assemblyVersionMinor -gt $currentMinor){
				$newAssemblyVersionMinor=$assemblyVersionMinor
				$newAssemblyVersionPatch=$assemblyVersionPatch
			}elseif($assemblyVersionMinor -eq $currentMinor){
				$newAssemblyVersionMinor=$currentMinor
				if($assemblyVersionPatch -gt $currentPatch){
					$newAssemblyVersionPatch=$assemblyVersionPatch
				}else{
					$newAssemblyVersionPatch=$currentPatch+1
				}
			}
		}
	}
	
	
	$newAssemblyVersion="$newAssemblyVersionMajor.$newAssemblyVersionMinor.$newAssemblyVersionPatch.$newAssemblyVersionRevision"
	(Get-Content "$AssemblyVersionFilePath").replace("$AssemblyVersion", "$newAssemblyVersion") | Set-Content "$AssemblyVersionFilePath"

	if($BranchType -eq "Development"){
		$currentVersion.development=$newAssemblyVersion
	}else{
		$currentVersion.production=$newAssemblyVersion
	}
	Set-Content -Value ($currentVersion | convertTo-Json) -Path "$CurrentVersionFilePath"	
	
	return $newAssemblyVersion
	
	
	
	
