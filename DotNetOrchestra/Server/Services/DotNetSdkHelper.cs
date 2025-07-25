using System.Diagnostics;
using System.IO.Compression;
using System.Xml.Linq;

namespace DotNetOrchestra.Server.Services
{
    public static class DotNetSdkHelper
    {
        public static int GetSdkTypeAsInt(string sdkType) => sdkType switch
        {
            "Microsoft.NET.Sdk" => 0,
            "Microsoft.NET.Sdk.Web" => 1,
            "Microsoft.NET.Sdk.WindowsDesktop" => 2,
            "Microsoft.NET.Sdk.Worker" => 3,
            "Microsoft.NET.Sdk.BlazorWebAssembly" => 4,
            "Microsoft.NET.Sdk.Razor" => 5,
            _ => -1
        };

        public static async Task<string> GetSdkTypeAsync(byte[] data)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), $"note_{Guid.NewGuid():N}");
            Directory.CreateDirectory(tempPath);

            try
            {
                await using (var memoryStream = new MemoryStream(data))
                using (var archive = new ZipArchive(memoryStream))
                {
                    archive.ExtractToDirectory(tempPath);
                }

                var csprojPath = Directory
                    .EnumerateFiles(tempPath, "*.csproj", SearchOption.AllDirectories)
                    .FirstOrDefault() ?? throw new InvalidOperationException("Не найден .csproj файл в архиве.");

                var doc = XDocument.Load(csprojPath);
                var sdkAttr = doc.Root?.Attribute("Sdk")?.Value;

                if (string.IsNullOrWhiteSpace(sdkAttr))
                    throw new InvalidOperationException("Тип SDK не определён в .csproj.");

                return sdkAttr;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ошибка чтения .csproj.", ex);
            }
            finally
            {
                if (Directory.Exists(tempPath))
                    Directory.Delete(tempPath, true);
            }
        }

        public static async Task<Process> LaunchAppAsync(byte[] data)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "noteapp", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);

            await using (var memoryStream = new MemoryStream(data))
            using (var archive = new ZipArchive(memoryStream))
            {
                archive.ExtractToDirectory(tempDir);
            }

            var exeFile = Directory
                .EnumerateFiles(tempDir, "*.exe", SearchOption.AllDirectories)
                .FirstOrDefault() ?? throw new FileNotFoundException("Не найден .exe файл в приложении.");

            var startInfo = new ProcessStartInfo
            {
                FileName = exeFile,
                WorkingDirectory = Path.GetDirectoryName(exeFile)!,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();

            return process;
        }
    }
}
