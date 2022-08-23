using JetBrains.Annotations;

namespace Graffiti {
public static class GraffitiProperties {

    // What can be returned:
    // (1) Default Instance
    // (2) ScriptableObject Instance
    // (3) Provided Instance - NOT IMPLEMENTED

    [NotNull] public static ColorPalette Palette
        => GraffitiSettingsSo.Instance == null ? ColorPalette.DefaultInstance : GraffitiSettingsSo.Instance.Palette;

    [NotNull] public static GraffitiConfig Config
        => GraffitiSettingsSo.Instance == null ? GraffitiConfig.DefaultInstance : GraffitiSettingsSo.Instance.Config;

}
}
