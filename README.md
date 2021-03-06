# Outputting Build Numbers in your Azure DevOps Pipeline Artifacts
https://terencegolla.com/.net/outputting-build-numbers-in-your-azure-devops-pipeline-artifacts/

In the process of deploying and then testing code there is always that question in the back of your head... Is the code I’m testing really the code I deployed?   

In the world of the Azure DevOps pipelines the easiest way to identify the code you deploy is by the pipeline build number. 

![Azure CI Build Pipeline](Azure-CI-Build-Pipeline.png "Azure CI Build Pipeline")

What I’m going to talk about in the article is how you can output and then reference this build number with your deployed code.  

The process requires that you first add a task to your pipeline YAML file that outputs the build number to a file in your artifacts which you can then access through your code.  A complete example can be found on GitHub at [https://github.com/tgolla/BuildNumbersInAzurePipelineArtifactsExample](https://github.com/tgolla/BuildNumbersInAzurePipelineArtifactsExample).

In your Azure pipeline YAML file (```azure-pipelines.yml```) you will need to add the following PowerShell task before the build task (```DotNetCoreCLI@2... Command: ‘build’```).

```yaml
# Replace AzureBuildNumber.txt content with actual build number.
- task: PowerShell@2
  inputs:
    targetType: 'inline'
    script: |
      New-Item $(Build.SourcesDirectory)/BuildNumbersInAzurePipelineArtifactsExample/AzureBuildNumber.txt -Force
      Set-Content $(Build.SourcesDirectory)/BuildNumbersInAzurePipelineArtifactsExample/AzureBuildNumber.txt '$(Build.BuildNumber)'
```

The task executes a PowerShell script that first creates (New Item) the AzureBuildNumber.txt file in the projects root folder and then adds (Set-Content) the build number.  If the file already exists, the -Force parameter deletes the existing file and content before recreating the file.

In your code add the following method.

```csharp
public static string AzureBuildNumber
{
    get
    {
        string azureBuildNumber = "Unknown";

        try
        {
            System.IO.StreamReader file = new System.IO.StreamReader("AzureBuildNumber.txt");

            azureBuildNumber = file.ReadLine() ?? "Unknown";
        }
        catch
        {
            // Do nothing.
        }

        return azureBuildNumber;
    }
}
```

The method will read the AzureBuildNumber.txt file from the projects deployed root folder and return the build number or ‘Unknown’ should the file be missing or empty.  The complete example defines an AssemblyInfo class that also returns the assembly name and informational version.

Note: The assembly informational version is defined in the XML project file (```BuildNumbersInAzurePipelineArtifactsExample.csproj```) as part of the property group.  By default, this is not automatically added to the project file and will need to be added manually or through the project's properties tab under Package -> Package version: which must initially be changed to something other than 1.0.0.

```xml
<PropertyGroup>
  <TargetFramework>netcoreapp3.1</TargetFramework>
  <Version>1.0.1</Version>
</PropertyGroup>
```
