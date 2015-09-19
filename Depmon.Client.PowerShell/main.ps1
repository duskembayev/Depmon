param (
	[string]$paramsFile = "params.json",
    [string]$scriptPath = (Split-Path $MyInvocation.InvocationName),
    [string]$Global:attachmentName = "results.csv"
)

Write-Host "Script path [$scriptPath]"
Write-Host "Opening parameters file [${paramsFile}]... " -NoNewline
$params = Get-Content $paramsFile | ConvertFrom-Json
Write-Host "OK" -ForegroundColor Yellow

Write-Host "Installing functions... " -NoNewline
. .\functions.ps1
Write-Host "OK" -ForegroundColor Yellow

Write-Host "Creating file [${attachmentName}]..."
file-create -fileName $attachmentName
Write-Host "OK" -ForegroundColor Yellow

$groupCode = $params.groupCode;
Write-Host "Group: [${groupCode}]"

foreach ($object in $params.objects)
{
    $objectCode = $object.objectCode
    Write-Host "Begin processing object: [${objectCode}]"

    foreach ($resource in $object.resources)
    {
        $resourceCode = $resource.resourceCode
        Write-Host "  Resource: [${resourceCode}]"

        foreach ($indicator in $resource.indicators)
        {
            indicator-process -indicator $indicator -groupCode $groupCode -objectCode $objectCode -resourceCode $resourceCode
        }
    }
}

#Send-Result