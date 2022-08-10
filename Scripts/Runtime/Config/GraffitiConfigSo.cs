using System.Diagnostics;
using System.Runtime.CompilerServices;
using Graffiti.Internal;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Graffiti {

[CreateAssetMenu(fileName = ResourceName, menuName = "ScriptableObjects/" + ResourceName)]
public class GraffitiConfigSo : ScriptableObject {

	public static string __nameof_config       = nameof(_config);
	public static string __nameof_colorPalette = nameof(_colorPaletteSo);

	public static ColorPalette   Palette => _instance == null ? ColorPalette.DefaultInstance : _instance.GetPalette;
	public static GraffitiConfig Config  => _instance == null ? GraffitiConfig.DefaultInstance : _instance._config;

	private const string ResourceName = "Graffiti Config";
	private static readonly string FullPathToResource = $"Assets/Plugins/{nameof(Graffiti)}/Resources/{ResourceName}.asset";

	private ColorPalette GetPalette => _colorPaletteSo == null ? ColorPalette.DefaultInstance : _colorPaletteSo.Palette;

	[Tooltip("The palette that will be used to colorize text. If it's null, the default palette will be used.")]
	[CanBeNull]
	[SerializeField]
	private ColorPaletteSo _colorPaletteSo;

	[CanBeNull]
	[SerializeField]
	private GraffitiConfig  _config = new GraffitiConfig();

	private static GraffitiConfigSo _instance;


	internal static void Initialize() {
		if (_instance != null) return;
		LoadInstance();
		GraffitiDebug.Assert(_instance != null, $"Settings Loaded from Resources folder (.../Resources/{ResourceName})");
		if (_instance != null) return;
		GraffitiDebug.Log("Can't load settings from Resources folder. Trying to create new");
		CreateInstance();
		LoadInstance();
		if (_instance != null) return;
		GraffitiDebug.LogError("Failed to load/create settings => using default");
	}

	private static void LoadInstance() => _instance = Resources.Load<GraffitiConfigSo>(ResourceName);

	[Conditional("UNITY_EDITOR")]
	private static void CreateInstance() {
		GraffitiConfigSo asset = ScriptableObject.CreateInstance<GraffitiConfigSo>();

		string path = AssetDatabase.GenerateUniqueAssetPath(FullPathToResource);
		AssetDatabase.CreateAsset(asset, path);
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();
	}
}
}
