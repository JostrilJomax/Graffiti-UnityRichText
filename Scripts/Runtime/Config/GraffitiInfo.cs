using System;
using UnityEngine;

namespace Graffiti {
public static class GraffitiInfo {

	public const string Version         = "1.0.0";

	public const string PluginFolder    = "Assets/Plugin";
	public const string RootFolderName  = "Graffiti v0.6";
	public const string ResourcesFolder = "/Resources";

	public static readonly string ResourcesFolderPath = $"{PluginFolder}/{RootFolderName}/{ResourcesFolder}";

	public static bool   AllowAssetCreation  => Application.isEditor;
	public const  bool   AllowAssetCreationInDefaultFolder             = true;
	public const  bool   AllowAssetCreationInRelativeToClassFileFolder = true;

	public static readonly DateTime VersionDate = new DateTime(2022, 2, 6);
}
}
