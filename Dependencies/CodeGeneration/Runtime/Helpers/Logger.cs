using System.Diagnostics;

#if UNITY_5_3_OR_NEWER // If we are in Unity (any version)
using Debug = UnityEngine.Debug;
#endif

namespace CodeGeneration.Internal.Helpers {
public static class Logger {

    [Conditional("UNITY_5_3_OR_NEWER")]
    public static void LogInfo(string msg) => Debug.Log(msg);

    [Conditional("UNITY_5_3_OR_NEWER")]
    public static void LogWarning(string msg) => Debug.LogWarning(msg);

    [Conditional("UNITY_5_3_OR_NEWER")]
    public static void LogError(string msg) => Debug.LogError(msg);

}
}
