function RewriteDllPath {
	param ([System.Xml.XmlElement]$referenceNode, [string]$newDllPath)
	if ($newDllPath | Test-Path) {
		$_.HintPath = $newDllPath
		Write-Host "-> $($newDllPath)"
	}
	else {
		Write-Warning "Dll `"$($newDllPath)`" does not exist. Path will not be re-written."
	}
}

$apiProjectPath = '..\TrainworksModdingTools\TrainworksModdingTools.csproj'

$bepInExDefaultPath = 'C:\Program Files (x86)\Steam\steamapps\workshop\content\1102190\2187468759\BepInEx'
$mtDefaultPath = 'C:\Program Files (x86)\Steam\steamapps\common\Monster Train'

$bepInExPath = Read-Host "Enter path to BepInEx root folder (defaults to `"$($bepInExDefaultPath)`")"
$bepInExPath = ($bepInExDefaultPath,$bepInExPath)[[bool]$bepInExPath]

if (-not ($bepInExPath | Test-Path)) {
	throw 'The entered path does not exist.'
}

$mtPath = Read-Host "Enter path to Monster Train root folder (defaults to `"$($mtDefaultPath)`")"
$mtPath = ($mtDefaultPath,$mtPath)[[bool]$mtPath]

if (-not ($mtPath | Test-Path)) {
	throw 'The entered path does not exist.'
}

$bepInExPath = $bepInExPath.Replace('/', '\').TrimEnd('\')
$mtPath = $mtPath.Replace('/', '\').TrimEnd('\')

$apiProject = [xml](Get-Content $apiProjectPath)

$references = $apiProject.SelectNodes("//ItemGroup/Reference")  | Where-Object { -not ([string]::IsNullOrEmpty($_.Include)) }

Write-Host "`n--- Rewriting HintPaths ---"

# Rewrite paths to dlls in MonsterTrain_Data\Managed directory
$references | Where-Object{($_.Include -match '^UnityEngine') -or ($_.Include -match '^Unity.') -or ($_.Include -eq 'Assembly-CSharp')} | foreach {
	$dllName = "$($_.Include).dll"
	$newDllPath = Join-Path -Path $mtPath -ChildPath "MonsterTrain_Data\Managed\$($dllName)"
	RewriteDllPath $_ $newDllPath.ToString()
}

# Rewrite paths to dlls in BepInEx\core directory
$references | Where-Object{($_.Include -match '^BepInEx.') -or ($_.Include -eq 'BepInEx') -or ($_.Include -eq '0Harmony')} | foreach {
	$dllName = "$($_.Include).dll"
	$newDllPath = Join-Path -Path $bepInExPath -ChildPath "core\$($dllName)"
	RewriteDllPath $_ $newDllPath.ToString()
}

Write-Host "---------------------------`n"

$apiProject.Save($apiProjectPath)
