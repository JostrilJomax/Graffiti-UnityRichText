using System.Diagnostics;
using Graffiti.Internal;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti {
[CreateAssetMenu(fileName = ScriptableObjectName, menuName = GraffitiInfo.AssetMenu.Prefix + ScriptableObjectName)]
public class GraffitiSettingsSo : ScriptableObject {

    private const string ScriptableObjectName = "Graffiti Settings";

    public static readonly string __nameof_colorPalette = nameof(_colorPaletteSo);
    public static readonly string __nameof_config       = nameof(graffitiConfig);

    private static readonly string ResourcesAssetDefaultPath
            = $"{GraffitiInfo.Directory.Default.ResourcesFolderPath}/{ScriptableObjectName}.asset";

    [Tooltip("The palette that will be used to colorize text. If it's null, the default palette will be used.")]
    [CanBeNull] [SerializeField]
    private ColorPaletteSo _colorPaletteSo;

    [NotNull] [SerializeField]
    private GraffitiConfig graffitiConfig = new GraffitiConfig();

    [NotNull] internal ColorPalette   Palette => _colorPaletteSo == null ? ColorPalette.DefaultInstance : _colorPaletteSo.Palette;
    [NotNull] internal GraffitiConfig Config  => graffitiConfig;


    [CanBeNull] public static GraffitiSettingsSo Instance { get; private set; }


    internal static void Initialize()
    {
        if (LoadAsset()) {
            return;
        }

        if (GraffitiInfo.AssetCreation.IsAllowed) {
            CreateAsset();
            LoadAsset();
        }
    }

    private static bool LoadAsset() => (Instance = Resources.Load<GraffitiSettingsSo>(ScriptableObjectName)) != null;

    [Conditional("UNITY_EDITOR")]
    private static void CreateAsset()
    {
        var assetInstance = CreateInstance<GraffitiSettingsSo>();

        if (GraffitiInfo.AssetCreation.IsAllowedInDefaultFolder) {
            if (GraffitiAssetDatabase.CreateAsset(assetInstance, ResourcesAssetDefaultPath)) {
                return;
            }
        }

        if (GraffitiInfo.AssetCreation.IsAllowedInRelativeToClassFileFolder) {
            GraffitiAssetDatabase.CreateAsset(
                assetInstance,
                GraffitiInfo.AssetCreation.ClassNameThatIsSearched,
                GraffitiInfo.Directory.RootFolderName,
                GraffitiInfo.Directory.ResourcesFolder,
                ScriptableObjectName);
        }
    }

}
}
