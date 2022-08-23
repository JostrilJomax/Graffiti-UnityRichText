#if UNITY_EDITOR

using System;
using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Graffiti.Internal {
internal static class GraffitiAssetDatabase {

    /// <summary> Creates a asset in a folder, using provided asset instance and asset path. </summary>
    /// <returns> Success or failure. </returns>
    public static bool CreateAsset<T>(
        [NotNull] T assetInstance,
        [NotNull] string assetPath)
            where T : Object
    {
        assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);

        if (string.IsNullOrEmpty(assetPath)) {
            return false;
        }

        AssetDatabase.CreateAsset(assetInstance, assetPath);
        AssetDatabase.SaveAssets();

        return true;
    }

    /// <summary> Uses <see cref="GetPathByClassName"/> to find the folder where file will be created. </summary>
    /// <returns> Success or failure. </returns>
    public static bool CreateAsset<T>(
        [NotNull] T assetInstance,
        [CanBeNull] string nameofClass,
        [NotNull] string rootFolder,
        [CanBeNull] string relativePath,
        [NotNull] string assetName)
            where T : Object
    {
        string assetPath = GetPathByClassName(
            nameofClass ?? nameof(T),
            rootFolder,
            relativePath,
            assetName);

        if (string.IsNullOrEmpty(assetPath)) {
            return false;
        }

        AssetDatabase.CreateAsset(assetInstance, assetPath);
        AssetDatabase.SaveAssets();

        return true;
    }

    /// <summary>
    ///     • The following code tries to get path (3) from path (1): <br/> (1) .../RootFolder/.../ClassName.cs <br/> (2)
    ///     .../RootFolder <br/> (3) .../RootFolder/ ..RelativePath.. /AssetName.asset <br/> <br/> • How exactly it works:
    ///     <br/> It finds path (1) by searching all directories for a NameofClass.cs file, then removes from the path (1)
    ///     everything up to the root folder, as in path (2), then adds provided RelativePath and AssetName with .asset
    ///     extension. <br/> <br/> • Why are we searching by class name ? <br/> 1) We can be somewhat sure that a file has the
    ///     same name as the class it contains, so with help of the IDE and the nameof() keyword, the link can be made quite
    ///     strong. <br/> 2) Folders can have duplicate names, with classes there is at least some limitation: classes cannot
    ///     have the same name in the same namespace. <br/> <br/> • What happens if I have a class with the same name somewhere
    ///     else? <br/> Then both classes will be ignored and asset path will be empty <br/> <br/> • What happens if I put
    ///     plugin's folder in folder with the same name? <br/> It won't matter. Search for root folder is made from the end,
    ///     so it does not matter what folders there are before the root folder. <br/>
    /// </summary>
    /// <param name="rootFolder">
    ///     The name of a root folder with no slashes.
    ///     <example> "Root Folder" </example>
    /// </param>
    /// <param name="relativePath">
    ///     Additional path after foot folder, should start with a slash and should end with no slash,
    ///     you can pass null here.
    ///     <example> "/Resources" </example>
    /// </param>
    /// <param name="nameofClass">
    ///     Should not end with extension. The class name should be as unique as possible.
    ///     <example> nameof(IThinkThisClassWontBeRepeatedAnywhereInTheProject) </example>
    /// </param>
    /// <param name="assetName">
    ///     Name of a ScriptableObject (asset) that you want to create. No slashes, no extensions.
    ///     <example> "My Brand New Asset" </example>
    /// </param>
    /// <returns> </returns>
    [NotNull] [Pure]
    public static string GetPathByClassName(
        [NotNull] string nameofClass,
        [NotNull] string rootFolder,
        [CanBeNull] string relativePath,
        [NotNull] string assetName)
    {
        nameofClass += ".cs";
        assetName += ".asset";
        relativePath = relativePath == null ? "" : relativePath + "/";

        string assetPath = string.Empty;

        string[] possibleClassPaths = FindPathToFile(nameofClass, false);
        if (possibleClassPaths.Length != 1) {
            return assetPath;
        }

        int rootFolderIndex = possibleClassPaths[0].LastIndexOf(rootFolder, StringComparison.Ordinal);
        Range rootFolderPathRange = ..(rootFolderIndex + rootFolder.Length);
        string destinationFolder = possibleClassPaths[0][rootFolderPathRange] + relativePath;
        assetPath = AssetDatabase.GenerateUniqueAssetPath(destinationFolder + assetName);

        return assetPath;
    }

    [NotNull] [ItemNotNull] [Pure]
    public static string[] FindPathToFile([NotNull] string fileName, bool addCsExtensionToFileName)
    {
        string[] paths = FindFullPathToFile(fileName, addCsExtensionToFileName);
        for (int i = 0; i < paths.Length; i++)
            paths[i] = paths[i][(Application.dataPath.Length - "Assets\\".Length + 1)..];

        return paths;
    }

    /// <summary>
    ///     Returns all founded paths to files with given name. Paths have forward slashes "/", starts from your drive
    ///     letter (C:/...)
    /// </summary>
    /// <remarks> Idea: https://answers.unity.com/questions/306751/get-script-path.html </remarks>
    [NotNull] [ItemNotNull] [Pure]
    public static string[] FindFullPathToFile([NotNull] string fileName, bool addCsExtensionToFileName)
    {
        fileName = addCsExtensionToFileName ? fileName + ".cs" : fileName;
        string[] foundedFiles = Directory.GetFiles(Application.dataPath, fileName, SearchOption.AllDirectories);

        if (foundedFiles.Length == 0) {
            GraffitiDebug.LogError($"No script with name {fileName}.");
            return Array.Empty<string>();
        }

        if (foundedFiles.Length > 1) {
            GraffitiDebug.LogWarning($"Found several files with name {fileName}.");
        }

        for (int i = 0; i < foundedFiles.Length; i++) foundedFiles[i] = foundedFiles[i].Replace("\\", "/");

        return foundedFiles;
    }

}
}

#endif
