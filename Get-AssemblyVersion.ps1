<#
The MIT License (MIT)
 
Copyright (c) 2015 Objectivity Bespoke Software Specialists
 
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
 
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

# https://www.powershellgallery.com/packages/PSCI/1.0.4/Content/modules%5Cbuild%5CPublicHelpers%5CGet-AssemblyVersion.ps1
#>
     <#
    .SYNOPSIS
    Gets version from assembly version file.
 
    .PARAMETER Path
    Path to the assemblyinfo file or directory that contains $FileMask in one of its subdirectories.
 
    .PARAMETER VersionAttribute
    Version attribute to get - see http://stackoverflow.com/questions/64602/what-are-differences-between-assemblyversion-assemblyfileversion-and-assemblyin.
 
    .PARAMETER FileMask
    File mask to use if $Path is a directory (by default 'AssemblyInfo.cs')
 
    .EXAMPLE
    Get-AssemblyVersion -Path 'c:\myproject'
 
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

    if (!$VersionAttribute) {
        $VersionAttribute = 'AssemblyVersion'
    }
    if (!$FileMask) {
        $FileMask = 'AssemblyInfo.cs'
    }

	$ErrorActionPreference = 'Stop'

	if($verbose) {
	   $VerbosePreference = $verbose
	}
	
	$Path = ($Path | Resolve-Path).ProviderPath
	Write-Verbose $Path
    if (Test-Path -LiteralPath $Path -PathType Leaf) {
        $file = $Path
    } else {
        $file = @(Get-ChildItem -Path $Path -File -Filter $FileMask -Recurse | Select-Object -ExpandProperty FullName)
        if (!$file) {
            throw "Cannot find any '$FileMask' files at '$Path'."
        }
        if ($file.Count -gt 1) {
            throw "Found more than one '$FileMask' files at '$Path': $($file -join ', ')"
        }
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
