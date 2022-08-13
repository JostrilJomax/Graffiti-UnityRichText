using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Graffiti.Internal;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Graffiti {

[CreateAssetMenu(fileName = ScriptableObjectAssetName, menuName = "ScriptableObjects/" + ScriptableObjectAssetName)]
public class GraffitiConfigSo : ScriptableObject {

	public static string __nameof_config       = nameof(_config);
	public static string __nameof_colorPalette = nameof(_colorPaletteSo);

	public const bool AllowToCreateInstanceInResourcesFolder = true;
	public const bool AllowDeepSearchByClassName = true;

	private const string ScriptableObjectAssetName = "Graffiti Config";
	private const string ScriptableObjectAssetFullName = ScriptableObjectAssetName + ".asset";
	private static readonly string DefaultPathToGraffitiResourcesFolder =
			$"Assets/Plugins/{nameof(Graffiti)}/Resources/{ScriptableObjectAssetFullName}";


	internal static ColorPalette   Palette => _instance == null ? ColorPalette.DefaultInstance : _instance.GetPalette;
	internal static GraffitiConfig Config  => _instance == null ? GraffitiConfig.DefaultInstance : _instance._config;

	private ColorPalette GetPalette => _colorPaletteSo == null ? ColorPalette.DefaultInstance : _colorPaletteSo.Palette;

	[Tooltip("The palette that will be used to colorize text. If it's null, the default palette will be used.")]
	[CanBeNull]
	[SerializeField]
	private ColorPaletteSo _colorPaletteSo;

	[NotNull]
	[SerializeField]
	private GraffitiConfig  _config = new GraffitiConfig();

	private static GraffitiConfigSo _instance;


	internal static void Initialize() {

		LoadInstanceFromResourcesFolder();

		if (_instance != null)
			return;

		GraffitiDebug.LogWarning($"Can't load settings from Resources folder (.../Resource/{ScriptableObjectAssetName}).");

		if (AllowToCreateInstanceInResourcesFolder && Application.isEditor) {
			Editor_CreateInstanceInResourcesFolder();
			LoadInstanceFromResourcesFolder();
			if (_instance != null) return;
			GraffitiDebug.LogError("Failed to Create config in Resources folder.");
		}



	}

	private static void LoadInstanceFromResourcesFolder() {
		_instance = Resources.Load<GraffitiConfigSo>(ScriptableObjectAssetName);
	}


	[Conditional("UNITY_EDITOR")]
	private static void Editor_CreateInstanceInResourcesFolder() {

		var assetInstance = ScriptableObject.CreateInstance<GraffitiConfigSo>();

		string assetPath = GetDefaultAssetPath();

		if (string.IsNullOrEmpty(assetPath) && AllowDeepSearchByClassName)
			assetPath = GetAssetPath_SearchByClassName();

		if (string.IsNullOrEmpty(assetPath))
			return;

		AssetDatabase.CreateAsset(assetInstance, assetPath);
		AssetDatabase.SaveAssets();

		static string GetAssetPath_SearchByClassName() {

			// • The following code tries to get path (3) from path (1):
			// (1) .../Graffiti/Scripts/.../ClassName.cs
			// (2) .../Graffiti
			// (3) .../Graffiti/Resources/AssetName.asset\

			// • How exactly it works:
			//  We receive path (1), then remove everything up to the root folder,
			// as in path (2), and then make path (3)

			// • Why can't we just find folder as in path (2) ?
			//  We can, but I think that it is a less secure approach, as for classes
			// there is at least some restriction in naming (there can be no classes with same name
			// in one namespace).

			// • What happens if I have a class with the same name somewhere else?
			//  Then both classes will be ignored and asset path will be empty

			// • What happens if I put plugin's folder in folder with the same name?
			//  It won't matter. Search for root folder is made from the end,
			// so it does not matter what folders there are before the root folder.

			const string classNameToSearchBy = nameof(GraffitiConfigSo) + ".cs";

			string   assetPath          = string.Empty;
			string[] possibleClassPaths = GraffitiAssetDatabase.FindPathToFile(classNameToSearchBy, addCsExtensionToFileName: false);

			if (possibleClassPaths.Length != 1)
				return assetPath;


			int    rootFolderIndex     = possibleClassPaths[0].LastIndexOf(GraffitiInfo.RootFolderName, StringComparison.Ordinal);
			Range  rootFolderPathRange = ..(rootFolderIndex + GraffitiInfo.RootFolderName.Length);
			string resourcesPath       = possibleClassPaths[0][rootFolderPathRange] + "/Resources";
			assetPath = AssetDatabase.GenerateUniqueAssetPath(resourcesPath + "/" + ScriptableObjectAssetFullName);

			return assetPath;
		}

		static string GetDefaultAssetPath() =>
			AssetDatabase.GenerateUniqueAssetPath(DefaultPathToGraffitiResourcesFolder);
	}
}
}
