if (-Not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] 'Administrator')) {
    Write-Host 'Script must be executed with administrator privilegues'; 
}
else {
    sc.exe create SampleWorker-Worker1 binPath= "$($pwd.Path)\SampleWorkerService.exe --ServiceName=Worker1"
    sc.exe create SampleWorker-Worker2 binPath= "$($pwd.Path)\SampleWorkerService.exe --ServiceName=Worker2"
}

Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');