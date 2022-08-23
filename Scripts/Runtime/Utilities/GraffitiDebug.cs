using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Graffiti.Internal {
/// <summary> Helper class for debugging. </summary>
/// <remarks>
///     All logging methods are with [Conditional] attribute. That means you can easily enable/disable debugging
///     without any loose in performance or time. <br/><b> To enable/disable </b> debugging you need to add/remove the
///     appropriate <see cref="ENABLE_LOGGING"/> in Unity's Project Settings. <br/><b> Full path: </b> Edit/Project
///     Settings/Player/Other Settings/Script Compilation
/// </remarks>
internal static class GraffitiDebug {
    private const string ENABLE_LOGGING = "GRAFFITI_ENABLE_LOGGING";
    private const string LOG_PREFIX = "<size=10><b><color=#3ab>Graffiti:</color></b></size> "; //.Stylize().Size(10).Bold.Blue
    [Conditional(ENABLE_LOGGING)] internal static void Log(string message) => Debug.Log(FormatLog(message));
    [Conditional(ENABLE_LOGGING)] internal static void LogWarning(string message) => Debug.LogWarning(FormatLog(message));
    [Conditional(ENABLE_LOGGING)] internal static void LogError(string message) => Debug.LogError(FormatLog(message));

    [Conditional(ENABLE_LOGGING)] internal static void Assert(bool condition, string message)
    {
        if (condition) Debug.Log(FormatLog(message));
    }

    // These methods use logical AND with conditional attribute
    [Conditional("UNITY_EDITOR")]
    internal static void LogInEditor(string message) => Log(message);

    private static string FormatLog(string message) => string.Concat(LOG_PREFIX, message);
}
}
