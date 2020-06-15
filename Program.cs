using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace AzureSettingsConverter
{
    class Program
    {
        private const string Version = "0.0.1";
        private const string AppSettingsFile = "appsettings.json";
        private const string OutputFile = "appsettings.txt";

        static void Main(string[] args)
        {
            Console.Title = $"AzureSettingsConverter v{Version}";

            Console.WriteLine($"Reading {AppSettingsFile}...");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(AppSettingsFile);

            var configuration = builder.Build();
            Console.WriteLine($"{AppSettingsFile} successfully readed.");

            Console.WriteLine($"Converting {AppSettingsFile} to Azure settings...");

            var azureSettings = configuration.ToAzureSettings();
            using (var writer = File.CreateText($"{OutputFile}"))
            {
                writer.WriteLine(azureSettings.ToJSON());
            }

            Console.WriteLine($"{azureSettings.ToList()?.Count} setting(s) converted from {AppSettingsFile}.");
            Console.WriteLine($"Output written to {OutputFile}.");

            Console.ReadLine();
        }
    }
}
