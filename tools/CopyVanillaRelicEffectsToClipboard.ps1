# Gets all Types of RelicEffect from Assembly-CSharp.dll and constructs a string ready to be inserted in TrainworksModdingTools.Constants.VanillaRelicEffectTypes

$mtDefaultPath = 'C:\Program Files (x86)\Steam\steamapps\common\Monster Train'

$mtPath = Read-Host "Enter path to Monster Train root folder (defaults to `"$($mtDefaultPath)`")"
$mtPath = ($mtDefaultPath,$mtPath)[[bool]$mtPath]

$assemblyPath = Join-Path -Path $mtPath -ChildPath "MonsterTrain_Data\Managed\Assembly-CSharp.dll"

if (-not ($assemblyPath | Test-Path)) {
	throw "Assembly-CSharp.dll could not be found at $($assemblyPath)."
}

$Assembly = [Reflection.Assembly]::LoadFrom($assemblyPath)

$allTypes = $Assembly.GetTypes() | Where-Object {$_.Name -Match "^RelicEffect" -and ($_.BaseType.Name -eq 'RelicEffectBase' -or $_.BaseType.IsSubclassOf($Assembly.GetType('RelicEffectBase')))} | foreach { "public static readonly Type " + $_.Name + " = typeof(" + $_.Name + ");" }

Write-Host "Content:`r`n-----`r`n"
Write-Host $allTypes

$allTypes | clip

Write-Host "`r`n-----`r`nCopied to clipboard!`r`n"
Read-Host
