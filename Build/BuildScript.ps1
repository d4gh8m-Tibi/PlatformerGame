param(
    [Parameter()]
    $unityPath="D:\Programs\2021.3.11f1\Editor\Unity.exe",
    [Parameter()]
    $proPath="D:\Projects\Toolsof\PlatformerGame"
)
echo "Starting Build"
Start-Process -Wait -FilePath $unityPath -ArgumentList "-quit", "-batchmode", "-projectPath $proPath", "-executeMethod BuildScript.PerformBuild"
echo "Finished Build"