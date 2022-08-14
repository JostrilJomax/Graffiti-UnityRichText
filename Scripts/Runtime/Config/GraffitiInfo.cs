using System;
using UnityEngine;

namespace Graffiti {
public static class GraffitiInfo {

	public const string PluginName = "Graffiti";
	public const string Version    = "1.0.0";

	public static readonly DateTime VersionDate = new DateTime(2022, 2, 6);


	public static class AssetMenu {
		public const string Prefix = "ScriptableObjects/" + PluginName + "/";
	}

	public static class Directory {
		public const string RootFolderName  = "Graffiti v0.6";
		public const string ResourcesFolder = "/Resources";

		public static class Default {
			public static readonly string ResourcesFolderPath
					= $"{PluginFolderPath}/{RootFolderName}/{ResourcesFolder}";

			public const string PluginFolderPath = "Assets/Plugin";
		}
	}

	public static class AssetCreation {
		public static bool   IsAllowed => Application.isEditor;
		public const  bool   IsAllowedInDefaultFolder             = true;
		public const  bool   IsAllowedInRelativeToClassFileFolder = true;
		public const  string ClassNameThatIsSearched              = nameof(GraffitiInfo);
	}
}
}
