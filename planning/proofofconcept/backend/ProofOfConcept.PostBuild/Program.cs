// See https://aka.ms/new-console-template for more information
using NSwag;
using NSwag.CodeGeneration.TypeScript;
using SigSpec.CodeGeneration.TypeScript;
using SigSpec.Core;

namespace ProofOfConcept.PostBuild
{
    public class Program
    {
        private const string SpecFolder = @"..\..\specs";

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Generating SigSpec...");
            var sigSpecDocument = await GenerateSigSpecDocument();

            Console.WriteLine("Saving SigSpec Document...");
            SaveSigSpecDocument(sigSpecDocument, $"{SpecFolder}\\sigspec.json");

            Console.WriteLine("Generating and Saving SigSpec to TS...");
            GenerateAndSaveSigSpecToTypeScript(sigSpecDocument, $"{SpecFolder}\\API_SIGNALR.ts");

            Console.WriteLine("Getting OpenAPI...");
            var openApiDocument = await GetOpenApiDocument($"{SpecFolder}\\rest_openapi.json");

            Console.WriteLine("Generating and Saving OpenAPI to TS...");
            GenerateAndSaveOpenApiToTypeScript(openApiDocument, $"{SpecFolder}\\API_REST.ts");

            Console.WriteLine("Done");
        }

        public static async Task<SigSpecDocument> GenerateSigSpecDocument()
        {
            var settings = new SigSpecGeneratorSettings();
            var generator = new SigSpecGenerator(settings);

            return await generator.GenerateForHubsAsync(ProofOfConcept.Program.HubList);
        }

        public static void SaveSigSpecDocument(SigSpecDocument document, params string[] savePaths)
        {
            var sigSpecJson = document.ToJson();

            DeleteAndWrite(sigSpecJson, savePaths);
        }

        public static void GenerateAndSaveSigSpecToTypeScript(SigSpecDocument document, params string[] savePaths)
        {
            var settings = new SigSpecToTypeScriptGeneratorSettings();
            var generator = new SigSpecToTypeScriptGenerator(settings);
            var code = generator.GenerateFile(document);

            DeleteAndWrite(code, savePaths);
        }

        public static async Task<OpenApiDocument> GetOpenApiDocument(string path)
        {
            var openApiFile = File.ReadAllText(path);
            return await OpenApiDocument.FromJsonAsync(openApiFile);
        }

        public static void GenerateAndSaveOpenApiToTypeScript(OpenApiDocument document, params string[] savePaths)
        {
            var settings = new TypeScriptClientGeneratorSettings();
            var generator = new TypeScriptClientGenerator(document, settings);
            var code = generator.GenerateFile();

            DeleteAndWrite(code, savePaths);
        }

        private static void DeleteAndWrite(string content, params string[] paths)
        {
            foreach(var path in paths)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.WriteAllText(path, content);
            }
        }
    }
}
