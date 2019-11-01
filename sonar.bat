docker run -d --name sonarqube -p 9000:9000 -v ./conf:/opt/sonarqube/conf -v ./data:/opt/sonarqube/data -v ./logs:/opt/sonarqube/logs -v ./extensions:/opt/sonarqube/extensions sonarqube
dotnet D:\downloads\sonar-scanner-msbuild-4.6.2.2108-netcoreapp2.0\SonarScanner.MSBuild.dll begin /k:"eng-serv" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="9ffb1d6f857eec87b81a565d3fc0d8b7d054f391"
dotnet build TV_App.sln
dotnet D:\downloads\sonar-scanner-msbuild-4.6.2.2108-netcoreapp2.0\SonarScanner.MSBuild.dll end /d:sonar.login="9ffb1d6f857eec87b81a565d3fc0d8b7d054f391" 
start http://localhost:9000