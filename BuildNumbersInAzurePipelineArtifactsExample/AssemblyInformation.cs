using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildNumbersInAzurePipelineArtifactsExample
{
    public static class AssemblyInformation
    {
        /// <summary>
        /// The assembly name.
        /// </summary>
        public static string AssemblyName => Assembly.GetEntryAssembly().GetName().Name;

        /// <summary>
        /// The assembly informational version.
        /// </summary>
        public static string AssemblyInformationalVersion => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        /// <summary>
        /// Returns the Azure build number.
        /// </summary>
        /// <remarks>
        /// For the actual Azure build number to be displayed the following PowerShell task must be placed in the 
        /// azure-pipelines.yml file before the DotNetCoreCLI@2 build task.
        /// 
        /// # Replace AzureBuildNumber.txt content with actual build number.
        /// - task: PowerShell@2
        ///   inputs:
        ///     targetType: 'inline'
        ///     script: |
        ///       New-Item $(Build.SourcesDirectory)/BuildNumbersInAzurePipelineArtifactsExample/AzureBuildNumber.txt -Force
        ///       Set-Content $(Build.SourcesDirectory)/BuildNumbersInAzurePipelineArtifactsExample/AzureBuildNumber.txt '$(Build.BuildNumber)' 
        /// </remarks>
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

        /// <summary>
        /// Returns the assembly information.
        /// </summary>
        /// <returns>The assembly information object.</returns>
        public static object InformationObject()
        {
            return new
            {
                AssemblyName,
                AssemblyInformationalVersion,
                AzureBuildNumber
            };
        }
    }
}
