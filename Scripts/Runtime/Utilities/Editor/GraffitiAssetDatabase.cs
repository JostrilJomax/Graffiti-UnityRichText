#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.Internal {
internal static class GraffitiAssetDatabase {

	[NotNull, ItemNotNull, Pure]
	public static string[] FindPathToFile([NotNull] string fileName, bool addCsExtensionToFileName) {
		var paths = FindFullPathToFile(fileName, addCsExtensionToFileName);
		for (int i = 0; i < paths.Length; i++)
			paths[i] = paths[i][(Application.dataPath.Length - "Assets\\".Length + 1)..];

		return paths;
	}

	/// <summary> Returns all founded paths to files with given name. Paths have forward slashes "/", starts from your drive letter (C:/...) </summary>
	/// <remarks> Idea: https://answers.unity.com/questions/306751/get-script-path.html </remarks>
	[NotNull, ItemNotNull, Pure]
	public static string[] FindFullPathToFile([NotNull] string fileName, bool addCsExtensionToFileName) {

		fileName = addCsExtensionToFileName ? fileName + ".cs" : fileName;
		string[] foundedFiles = Directory.GetFiles(Application.dataPath, fileName, SearchOption.AllDirectories);

		if (foundedFiles.Length == 0) {
			GraffitiDebug.LogError($"No script with name {fileName}.");
			return Array.Empty<string>();
		}
		if (foundedFiles.Length > 1) {
			GraffitiDebug.LogWarning($"Found several files with name {fileName}.");
		}

		for (int i = 0; i < foundedFiles.Length; i++) {
			int indexOfSlashPrecedingFileName = foundedFiles[i].LastIndexOf(fileName, StringComparison.Ordinal) - 1;
			foundedFiles[i] = foundedFiles[i][..indexOfSlashPrecedingFileName].Replace("\\", "/");
		}

		return foundedFiles;
	}
}
}

#endif
