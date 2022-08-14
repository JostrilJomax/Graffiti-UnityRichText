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

[CreateAssetMenu(fileName = ScriptableObjectName, menuName = "ScriptableObjects/" + ScriptableObjectName)]
public class GraffitiConfigSo : ScriptableObject {

	public static readonly string __nameof_config       = nameof(_config);
	public static readonly string __nameof_colorPalette = nameof(_colorPaletteSo);

	private const string ScriptableObjectName = "Graffiti Config";

	private static readonly string ResourcesAssetDefaultPath = $"{GraffitiInfo.ResourcesFolderPath}/{ScriptableObjectName}.asset";


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
		if (LoadAsset())
			return;

		if (GraffitiInfo.AllowAssetCreation) {
			CreateAsset();
			LoadAsset();
		}
	}

	private static bool LoadAsset() =>
		(_instance = Resources.Load<GraffitiConfigSo>(ScriptableObjectName)) != null;

	[Conditional("UNITY_EDITOR")]
	private static void CreateAsset() {

		var assetInstance = CreateInstance<GraffitiConfigSo>();

		if (GraffitiInfo.AllowAssetCreationInDefaultFolder)
			if (GraffitiAssetDatabase.CreateAsset(assetInstance, ResourcesAssetDefaultPath))
				return;

		if (GraffitiInfo.AllowAssetCreationInRelativeToClassFileFolder)
			GraffitiAssetDatabase.CreateAsset(
					assetInstance: assetInstance,
					nameofClass: nameof(GraffitiConfigSo),
					rootFolder: GraffitiInfo.RootFolderName,
					relativePath: "/Resources",
					assetName: ScriptableObjectName);
	}
}
}
