# https://www.powershellgallery.com/packages/PSCI/1.0.4/Content/modules%5Cbuild%5CPublicHelpers%5CGet-AssemblyVersion.ps1

<#
.SYNOPSIS
Gets projects attached to the visual studio solution file.

.PARAMETER Path
Path to the assemblyinfo file or directory that contains $FileMask in one of its subdirectories.

.PARAMETER VersionAttribute
Version attribute to get - see http://stackoverflow.com/questions/64602/what-are-differences-between-assemblyversion-assemblyfileversion-and-assemblyin.

.PARAMETER FileMask
File mask to use if $Path is a directory (by default 'AssemblyInfo.cs')

.EXAMPLE
.\Get-AssemblyVersion -Path $Path [-VersionAttribute=$VersionAttribute] [-FileMask=$FileMask]
#>

[CmdletBinding()]
[OutputType([string])]
param(

	[Parameter(Mandatory=$true)]
	[string]
	$Path,

	[Parameter(Mandatory=$false)]
	[string]
	[ValidateSet($null, 'AssemblyVersion', 'AssemblyFileVersion', 'AssemblyInformationalVersion')]
	$VersionAttribute = 'AssemblyVersion',

	[Parameter(Mandatory=$false)]
	[string]
	$FileMask = 'AssemblyInfo.cs'

)
	$ErrorActionPreference = 'Stop'

	Write-Host ""

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

	if (!$VersionAttribute) {
		$VersionAttribute = 'AssemblyVersion'
	}
	if (!$FileMask) {
		$FileMask = 'AssemblyInfo.cs'
	}

	if($PSBoundParameters['Verbose'] -and $VerbosePreference -ne 'Continue') {
		Write-Host ""
	}

	$PropagateVerbose=($PSCmdlet.MyInvocation.BoundParameters["Verbose"].IsPresent -eq $true)

	Write-Verbose "Resolving path"
	$Path = ($Path | Resolve-Path).ProviderPath
	Write-Verbose "Resolved path: $Path"

	if (Test-Path -LiteralPath $Path -PathType Leaf) {
		$file = $Path
	} else {
		Write-Verbose "Checking file path"
		$file = @(Get-ChildItem -Path $Path -File -Filter $FileMask -Recurse | Select-Object -ExpandProperty FullName)
		if (!$file) {
			throw "Cannot find any '$FileMask' files at '$Path'."
		}
		if ($file.Count -gt 1) {
			throw "Found more than one '$FileMask' files at '$Path': $($file -join ', ')"
		}
		Write-Verbose "Resolved file path: $file[0]"
		$file = $file[0]
	}

	$regex = '{0}\(\"([^\"]*)\"\)' -f $VersionAttribute
	$match = Select-String -Path $file -Encoding UTF8 -Pattern $regex
	if (!$match) {
		throw "Cannot find following regex: '$regex' in file '$file'. Please ensure it's valid AssemblyInfo file."
	}
	$result = $match.Matches.Groups[1].Value
	Write-Verbose "Got $VersionAttribute='$result' from file '$file'"

	return $result



	$regex = '{0}\(\"([^\"]*)\"\)' -f $VersionAttribute
	$match = Select-String -Path "D:\Apps\GitHub\Otros\Prueba-BuildEvent\src\GlobalAssemblyInfo.cs" -Encoding UTF8 -Pattern $regex