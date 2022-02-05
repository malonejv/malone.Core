#Based on https://gist.github.com/jstangroome/557222
#requires -version 2.0
[CmdletBinding()]
param (
    [parameter(Mandatory=$true)]
    [ValidateScript({ Test-Path -Path $_ -PathType Leaf })]
    [string]
    $Path,
	[ValidateSet('All','NonTestProjects','TestProjects')]
	[string]
	$Type = "All"
)
 
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
 
$Path = ($Path | Resolve-Path).ProviderPath
 
$SolutionRoot = $Path | Split-Path
 


if ($Type -eq "NonTestProjects") 
{ 
$SolutionProjectPattern = @"
(?x)
^ Project \( " \{ FAE04EC0-301F-11D3-BF4B-00C04F79EFBC \} " \)
\s* = \s*
" (?<name> (?!.*Test.*)[^"]* ) " , \s+
" (?<path> [^"]* ) " , \s+
"@
} elseif ($Type -eq "TestProjects") 
{ 
$SolutionProjectPattern = @"
(?x)
^ Project \( " \{ FAE04EC0-301F-11D3-BF4B-00C04F79EFBC \} " \)
\s* = \s*
" (?<name> (.*Test.*)[^"]* ) " , \s+
" (?<path> [^"]* ) " , \s+
"@
}
else{
	$SolutionProjectPattern = @"
(?x)
^ Project \( " \{ FAE04EC0-301F-11D3-BF4B-00C04F79EFBC \} " \)
\s* = \s*
" (?<name> [^"]* ) " , \s+
" (?<path> [^"]* ) " , \s+
"@
}
 
Get-Content -Path $Path |
    ForEach-Object {
        if ($_ -match $SolutionProjectPattern) {
            $ProjectPath = $SolutionRoot | Join-Path -ChildPath $Matches['path']
            $ProjectPath = ($ProjectPath | Resolve-Path).ProviderPath
			Write-Host $ProjectPath
        }
    }