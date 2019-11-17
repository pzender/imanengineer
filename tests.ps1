try {
    $testProjectPath = "C:\Users\Przemek\Documents\repos\imanengineer\TV_AppTests"
    $testSettingsPath = "C:\Users\Przemek\Documents\repos\imanengineer\TV_AppTests\test.runsettings"
    $testResultsFolder = "C:\Users\Przemek\Documents\repos\imanengineer\TV_AppTests\TestResults"

    if (-not (Test-Path $testProjectPath)) 
    {
        throw [System.IO.FileNotFoundException] "$testProjectPath not found."
    }
    if (-not (Test-Path $testSettingsPath)) 
    {
        throw [System.IO.FileNotFoundException] "$testSettingsPath not found."
    }
    if (-not (Test-Path $testResultsFolder)) 
    {
        throw [System.IO.FileNotFoundException] "$testResultsFolder not found."
    }

    dotnet test $testProjectPath --settings:$testSettingsPath 
    $recentCoverageFile = Get-ChildItem -File -Filter *.coverage -Path $testResultsFolder -Name -Recurse | Select-Object -First 1;
    write-host 'Test Completed'  -ForegroundColor Green

    C:\Users\Przemek\.nuget\packages\microsoft.codecoverage\16.4.0\build\netstandard1.0\CodeCoverage\CodeCoverage.exe analyze  /output:$testResultsFolder\MyTestOutput.coveragexml  $testResultsFolder'\'$recentCoverageFile
    write-host 'CoverageXML Generated'  -ForegroundColor Green

    dotnet C:\Users\Przemek\.nuget\packages\reportgenerator\4.3.6\tools\netcoreapp2.1\ReportGenerator.dll "-reports:$testResultsFolder\MyTestOutput.coveragexml" "-targetdir:$testResultsFolder\coveragereport"
    write-host 'CoverageReport Published'  -ForegroundColor Green

}
catch {
    write-host "Caught an exception:" -ForegroundColor Red
    write-host "Exception Type: $($_.Exception.GetType().FullName)" -ForegroundColor Red
    write-host "Exception Message: $($_.Exception.Message)" -ForegroundColor Red
}
