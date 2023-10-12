// See https://aka.ms/new-console-template for more information
using SigSpec.CodeGeneration.TypeScript;
using SigSpec.Core;

Console.WriteLine("Hello, World!");

var settings = new SigSpecGeneratorSettings();
var generator = new SigSpecGenerator(settings);

// TODO: Add PR to SignalR Core with new IHubDescriptionCollectionProvider service
var document = await generator.GenerateForHubsAsync(ProofOfConcept.Program.HubList);

var json = document.ToJson();
Console.WriteLine("\nGenerated SigSpec document:");
Console.WriteLine(json);
Console.ReadKey();

var tsCodeGeneratorSettings = new SigSpecToTypeScriptGeneratorSettings();
var tsCodeGenerator = new SigSpecToTypeScriptGenerator(tsCodeGeneratorSettings);
var file = tsCodeGenerator.GenerateFile(document);

Console.WriteLine("\n\nGenerated SigSpec TypeScript code:");
Console.WriteLine(file);
Console.ReadKey();