using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Graffiti.Internal {
internal static class GraffitiInitializer {

    private static bool _isInitialized;

#if UNITY_EDITOR
    [InitializeOnLoadMethod]
    private static void InitializeInEditor() => Initialize();
#endif
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void InitializeInRuntime() => Initialize();

    private static void Initialize()
    {
        if (_isInitialized) return;
        _isInitialized = true;

        GraffitiSettingsSo.Initialize();
    }

}
}
