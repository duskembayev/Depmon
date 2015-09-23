Function indicator-process
{
    param (
        $sourceCode,
        $groupCode,
        $resourceCode,
        $indicator
    )
    $indicatorCode = $indicator.indicatorCode
    
    Write-Host "    [${indicatorCode}]: starting"

    $command = [string]::Format("command-{0}", $indicator.command)

    $contextProps = @{
        sourceCode = $sourceCode
        groupCode = $groupCode
        resourceCode = $resourceCode
        indicatorCode = $indicatorCode
    }
    $commandContext = New-Object -TypeName psobject -Property $contextProps
    $commandArgs = $indicator.args
    
    $rows = &$command -context $commandContext -contextArgs $commandArgs
    foreach($row in $rows)
    {
        Export-Csv -Path $attachmentName -InputObject $row -NoTypeInformation -Append
    }
    
    Write-Host "    [${indicatorCode}]: finished"
}

Function file-create
{
    param($fileName)

    New-Item $fileName -type file -force
}

Function command-web-state
{
    param(
        $context,
        $contextArgs
    )

    $uri = $contextArgs.uri

    Write-Host "        Connecting [${uri}]... " -NoNewline

    $response = Invoke-WebRequest $uri
    $statusCode = $response.StatusCode
    $statusDesc = $response.StatusDescription
    if ($statusCode = 200)
    {
        $level = 'Normal'
    }
    else
    {
        $level = 'Warning'
    }

    $result = Create-Result -context $context -indicatorValue $statusCode -indicatorDescription $statusDesc -level $level

    Write-Host "${statusCode} ${statusDesc}" -ForegroundColor Yellow
    return $result
}

Function command-harddrive-state
{
    param (
        $context,
        $contextArgs
    )

    $server = $contextArgs.server
    Write-Host "        Checking HDDs for [${server}]"

    if ($contextArgs.username -ne '')
    {
        $securePassword = $contextArgs.password | ConvertTo-SecureString -AsPlainText -Force
        $cred = New-Object PSCredential($contextArgs.username, $securePassword)
        $disks = Get-WmiObject -Class Win32_LogicalDisk -ComputerName $server -Filter "DriveType=3" -Credential $cred
    }
    else
    {
        $disks = Get-WmiObject -Class Win32_LogicalDisk -ComputerName $server -Filter "DriveType=3"
    }

    
    [PSCustomObject[]]$result = @()
    foreach($disk in $disks)
    {
        if ($contextArgs.drives.Count -gt 0 -and ! $contextArgs.drives.contains([String]$disk.DeviceID[0]))
        {
            continue
        }
        $level = 'Normal'
        [String]$diskID = $disk.DeviceID
        $result += Create-Result -context $context -indicatorValue $disk.Size -indicatorDescription ($diskID + ' Total Space') -level $level
        $result += Create-Result -context $context -indicatorValue $disk.FreeSpace -indicatorDescription ($diskID + ' Free Space') -level $level
        $perc = $disk.FreeSpace * 100 / $disk.Size;
        if ($perc -lt 10) 
        {
            $level = 'Warning';
        } 
        else 
        { 
            if ($perc -lt 5) 
            {
                $level = 'Error';
            }
        }
        $result += Create-Result -context $context -indicatorValue $perc -indicatorDescription ($diskID + ' Percentage') -level $level
    }
    return $result
}

Function Create-Result
{
    param (
        $context,
        $indicatorValue,
        $indicatorDescription,
        $level
    )

    return [PSCustomObject]@{
                    'SourceCode' = $context.sourceCode;
                    'GroupCode' = $context.groupCode;
                    'ResourceCode' = $context.resourceCode;
                    'IndicatorCode' = $context.indicatorCode;
                    'IndicatorValue' = $indicatorValue;
                    'IndicatorDescription' = $indicatorDescription;
                    'Level' = $level;
                    'CheckedAt' = [DateTime]::Now
                }
}

Function Send-Result 
{
     $reportersFile = "reporters.json"
     $fileExist = Test-Path $reportersFile
     if (!$fileExist) 
     {
        Write-Host "$reportersFile don't exitst" -ForegroundColor Red
        return
     }
     $reportersParams = Get-Content $reportersFile | ConvertFrom-Json
     foreach($reporter in $reportersParams.reporters) 
     {
        $reporter
        Write-Host "Sending Email"
        [String[]]$toList = @()
        foreach ($to in $reporter.recipients)
        {
            $toList += $to
        }
        Send-Result-ToEmail -to $toList -from $reporter.from -username $reporter.user -password $reporter.password -smtpHost $reporter.smtpHost -port $reporter.port -useSsl $reporter.useSsl -subject $reporter.subject -attachments $attachmentName
     }
}

Function Send-Result-ToEmail
{
    param(
        $to,
        $from,

        $username,
        $password,
        $smtpHost,
        $port,
        $useSsl,

        $subject,
        $body,
        $attachments
    )
    $secStrPassword = 'healthmonitor123' | ConvertTo-SecureString -AsPlainText -Force
    $cred = New-Object PSCredential($username, $secStrPassword)

    if ($useSsl) 
    {
        Send-MailMessage -To $to -Subject $subject -Attachments $attachments -From $from -SmtpServer $smtpHost -Port $port -UseSsl -Credential $cred 
    }
    else 
    {
        Send-MailMessage -To $to -Subject $subject -Attachments $attachments -From $from -SmtpServer $smtpHost -Port $port -Credential $cred
    }
}