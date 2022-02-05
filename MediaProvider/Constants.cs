using System;
using System.IO;
using System.Reflection;

namespace MediaProvider
{
    public static class Constants
    {
        public static string DefaultMediaFolderEnvVarName = "EISENBAHN_MEDIA_DIRECTORY";

        public static string GetMediaFolder()
        {
            if(string.IsNullOrEmpty(Environment.GetEnvironmentVariable(DefaultMediaFolderEnvVarName)) == false)
            {
                return Environment.GetEnvironmentVariable(DefaultMediaFolderEnvVarName);
            }
            Assembly assembly = Assembly.GetAssembly(typeof(MediaProvider));
            return Path.GetFullPath(Path.Combine(assembly.Location, "..", "..", "..", ".."));
        }
    }
}
