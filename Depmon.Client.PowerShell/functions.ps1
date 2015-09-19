Function indicator-process
{
    param (
        $groupCode,
        $objectCode,
        $resourceCode,
        $indicator
    )
    $indicatorCode = $indicator.indicatorCode
    
    Write-Host "    [${indicatorCode}]: starting"

    $command = [string]::Format("command-{0}", $indicator.command)

    $contextProps = @{
        groupCode = $groupCode
        objectCode = $objectCode
        resourceCode = $resourceCode
        indicatorCode = $indicatorCode
    }
    $commandContext = New-Object -TypeName psobject -Property $contextProps
    $commandArgs = $indicator.args
    
    $row = &$command -context $commandContext -contextArgs $commandArgs
    Export-Csv -Path $attachmentName -InputObject $row -NoTypeInformation -Append
    
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

    Write-Host "Connecting [${uri}]... " -NoNewline

    $response = Invoke-WebRequest $uri
    $statusCode = $response.StatusCode
    $statusDesc = $response.StatusDescription

    $result = [PSCustomObject]@{ 'GroupCode' = $context.groupCode; 'ObjectCode' = $context.objectCode; 'ResultCode' = $context.resourceCode; 'indicatorCode' = $context.indicatorCode; 'StatusCode' = $statusCode; 'StatusDesc' = $statusDesc}
    

    Write-Host "${statusCode} ${statusDesc}" -ForegroundColor Yellow
    return $result
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