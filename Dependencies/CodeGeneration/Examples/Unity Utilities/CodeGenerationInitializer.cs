using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Logger = CodeGeneration.Internal.Helpers.Logger;

namespace CodeGeneration.Examples {
public static class CodeGenerationInitializer {

    [MenuItem("Plugins/Code Generation/Generate Code Blocks")]
    public static void Run_CodeBlocksGeneration()
    {
        string text = CodeBlocksGenerator.Generate();
        string path = RequestFilePath(
            "Generated.CodeBlocks",
            "cs",
            @"\Plugins\code-generation\Examples\Code Blocks");

        WriteToFile(path, text);
    }

    [MenuItem("Plugins/Code Generation/Generate Color Palette")]
    public static void Run_ColorPaletteGeneration()
    {
        string text = ColorPaletteGenerator.Generate();
        string path = RequestFilePath(
            "Generated.ColorPalette",
            "cs",
            @"\Plugins\code-generation\Examples\Colors");

        WriteToFile(path, text);
    }


    [CanBeNull]
    private static string RequestFilePath([NotNull] string fileName, [NotNull] string fileExtension, [NotNull] string defaultRelativePath)
    {
        string path = EditorUtility.SaveFilePanel(
            $"Select where to generate create new {fileName}.{fileExtension} file",
            Application.dataPath + defaultRelativePath,
            fileName,
            fileExtension);

        // If cancelled
        if (string.IsNullOrEmpty(path))
            return null;

        // Making path relative to Unity's root folder
        path = path.Remove(0, Application.dataPath.Length - "Assets".Length);

        bool doGenerateUniquePass =
            File.Exists(path) &&
            EditorUtility.DisplayDialog(
                "File with the same name already exists",
                "Do you want to create file with different name?",
                "Yes",
                "Cancel");

        if (doGenerateUniquePass)
            path = AssetDatabase.GenerateUniqueAssetPath(path);

        if (File.Exists(path)) {
            Logger.LogWarning("Can't create a file: file with same name already exists!");
            return null;
        }

        return path;
    }

    private static void WriteToFile([CanBeNull] string path, [CanBeNull] string text)
    {
        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(text))
            return;

        File.WriteAllText(path, text);
        AssetDatabase.Refresh();
    }

}
}
