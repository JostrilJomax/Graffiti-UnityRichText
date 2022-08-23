using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Graffiti.Internal {
public class CodeGenerationInitializer {
    [InitializeOnLoadMethod]
    public static void Initialize() => GenerateFile_StylizationOptions();

    private static void GenerateFile_StylizationOptions()
    {
        string fileDestinationPath = GetFileDestinationPath();
        string fileContent = StylizationOptionsGenerator.GenerateCode();

        if (string.IsNullOrEmpty(fileDestinationPath) || string.IsNullOrEmpty(fileContent)) {
            return;
        }

        Task task = WriteToFile(fileDestinationPath, fileContent);

        static async Task WriteToFile(string path, string text)
        {
            Debug.Log("Started writing to: " + path);
            await File.WriteAllTextAsync(path, text);
        }
    }


    private static string GetFileDestinationPath()
    {
        const string pointerClass = nameof(CodeGenerationInitializer);
        const string resultFileName = "Partial.Generated.StylizationOptions2.cs";

        string[] paths = GraffitiAssetDatabase.FindFullPathToFile(pointerClass, true);

        if (paths.Length != 1) {
            Debug.LogError("Incorrect path to file.");
            return null;
        }

        string pointerClassPath = paths[0];
        string fileDestinationPath = pointerClassPath.Replace($"Generators/{pointerClass}.cs", $"Generated/{resultFileName}");

        if (File.Exists(fileDestinationPath)) {
            Debug.LogWarning("Can't generate file, it already exists.");
            return null;
        }

        return fileDestinationPath;
    }
}
}
