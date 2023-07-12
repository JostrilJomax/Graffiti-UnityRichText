using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Graffiti.Internal {
public static class CodeGenerationInitializer_FilePointer {

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
        const string pointerClass = nameof(CodeGenerationInitializer_FilePointer);
        const string resultFileName = "Partial.Generated.StylizationOptions2.cs";

        string[] paths = GraffitiAssetDatabase.FindFullPathToFile(pointerClass, true);

        if (paths.Length == 0) {
            Debug.LogError("Can't find file");
            return null;
        }

        if (paths.Length > 1) {
            StringBuilder sb = new StringBuilder();
            sb.Append("There are several classes with name: " + pointerClass + ":");
            foreach (var path in paths) {
                sb.Append("\n" + path);
            }

            sb.Append("The first one will be used. This may lead to errors");
            Debug.LogWarning(sb.ToString());
        }

        string pointerClassPath = paths[0];
        string fileDestinationPath = pointerClassPath.Replace($"Generators/{pointerClass}.cs", $"Generated/{resultFileName}");

        if (File.Exists(fileDestinationPath)) {
            //Debug.LogWarning("Can't generate file, it already exists.");
            return null;
        }

        return fileDestinationPath;
    }

}
}
