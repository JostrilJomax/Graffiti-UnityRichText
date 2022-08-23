using UnityEditor;

namespace Graffiti.Internal {
internal static class UnitySettingsUtility {

    internal static bool IsDarkSkin
        =>
#if UNITY_EDITOR
                EditorGUIUtility.isProSkin;
#else
		true;
#endif

}
}
