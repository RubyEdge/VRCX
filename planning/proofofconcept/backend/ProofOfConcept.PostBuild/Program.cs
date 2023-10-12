// See https://aka.ms/new-console-template for more information
using NSwag;
using NSwag.CodeGeneration.TypeScript;
using SigSpec.CodeGeneration.TypeScript;
using SigSpec.Core;

string specFolder = @"..\..\specs";

{
    var settings = new SigSpecGeneratorSettings();
    var generator = new SigSpecGenerator(settings);

    // TODO: Add PR to SignalR Core with new IHubDescriptionCollectionProvider service
    var document = await generator.GenerateForHubsAsync(ProofOfConcept.Program.HubList);

    {
        var sigSpecJson = document.ToJson();
        string sigspecFile = $"{specFolder}\\sigspec.json";
        if (File.Exists(sigspecFile))
        {
            File.Delete(sigspecFile);
        }
        File.WriteAllText(sigspecFile, sigSpecJson);
    }

    {
        var tsCodeGeneratorSettings = new SigSpecToTypeScriptGeneratorSettings();
        var tsCodeGenerator = new SigSpecToTypeScriptGenerator(tsCodeGeneratorSettings);
        var tsCodeFile = tsCodeGenerator.GenerateFile(document);

        string tsSignalRFile = $"{specFolder}\\API_SIGNALR.ts";
        if (File.Exists(tsSignalRFile))
        {
            File.Delete(tsSignalRFile);
        }
        File.WriteAllText(tsSignalRFile, tsCodeFile);
    }
}

{
    var openApiFile = File.ReadAllText($"{specFolder}\\rest_openapi.json");
    var document = await OpenApiDocument.FromJsonAsync(openApiFile);

    var settings = new TypeScriptClientGeneratorSettings();
    var generator = new TypeScriptClientGenerator(document, settings);
    var code = generator.GenerateFile();

    string tsRestApiFile = $"{specFolder}\\API_REST.ts";
    if (File.Exists(tsRestApiFile))
    {
        File.Delete(tsRestApiFile);
    }
    File.WriteAllText(tsRestApiFile, code);
}

