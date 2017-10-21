Import-Module PSFTP

$msiFileName = dir "./VTS Agent Installer/Output/*.MSI" -Name
$msiFileFullPathName = [string]::Format("./VTS Agent Installer/Output/{0}", $msiFileName)
$agentVersion = $msiFileName.Substring(9,8)
$agentDownloadUrl = [string]::Format("http://www.vtsautomonitoring.by/dl/{0}", $agentVersion)

# Upload new Agent to FTP

$PWord = ConvertTo-SecureString –String "4edtPnXQxJE2shBXwBKoFclxlTrohxJQJREat9wC0lJN9rnwEsmC05gvQHTE" –AsPlainText -Force
$User = "vts"
$Credential = New-Object –TypeName System.Management.Automation.PSCredential –ArgumentList $User, $PWord
Set-FTPConnection -Credentials $Credential -Server ftp://waws-prod-db3-003.ftp.azurewebsites.windows.net -Session AzureSiteConnection -UsePassive 
$Session = Get-FTPConnection -Session AzureSiteConnection 
get-childitem "./VTS Agent Installer/Output/*.MSI" | Add-FTPItem -Session $Session -Path /site/wwwroot/Dl -Overwrite

# Update database with version info

$DownloadLink = [string]::Format("http://www.vtsautomonitoring.by/dl/VtsAgent_{0}.msi", $agentVersion)
$Query = [string]::Format("INSERT INTO AgentVersion (VersionString, DownloadLink, ReleasedDate) values ('{0}', '{1}', CURRENT_TIMESTAMP)", $agentVersion, $DownloadLink)
$ServerInstance = "lnzr0fzklo.database.windows.net"
$GetQuery = [string]::Format("select Id from agentversion where versionString='{0}'", $agentVersion)
$User = "vtsautomonitoring"
$Password = "primary1datastore#"
$Result = Invoke-Sqlcmd -Query $GetQuery -ServerInstance $ServerInstance -Database "VTS2" -Username $User -Password $Password
If ([string]::IsNullOrEmpty($Result))
{
	Invoke-Sqlcmd -Query $Query -ServerInstance $ServerInstance -Database "VTS2" -Username $User -Password $Password
}

