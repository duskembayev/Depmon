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

$sourceCode = $params.sourceCode;
Write-Host "Group: [${sourceCode}]"

foreach ($object in $params.objects)
{
    $groupCode = $object.groupCode
    Write-Host "Begin processing object: [${groupCode}]"

    foreach ($resource in $object.resources)
    {
        $resourceCode = $resource.resourceCode
        Write-Host "  Resource: [${resourceCode}]"

        foreach ($indicator in $resource.indicators)
        {
            indicator-process -indicator $indicator -sourceCode $sourceCode -groupCode $groupCode -resourceCode $resourceCode
        }
    }
}

#Send-Result