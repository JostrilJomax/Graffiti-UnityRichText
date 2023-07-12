using JetBrains.Annotations;
using UnityEngine;

namespace CodeGeneration.Internal.Helpers {
internal static class StringExtensions {

    public static string InAngleBrackets(this string self, string content)
        => $"{self}<{content}>";

    public static string Dot(this string self, string str1)
        => string.Join('.', self, str1);
    public static string Dot(this string self, string str1, string str2)
        => string.Join('.', self, str1, str2);
    public static string Dot(this string self, string str1, string str2, string str3)
        => string.Join('.', self, str1, str2, str3);

    public static string CommaSpace(this string self, string str1)
        => string.Join(", ", self, str1);
    public static string CommaSpace(this string self, string str1, string str2)
        => string.Join(", ", self, str1, str2);
    public static string CommaSpace(this string self, string str1, string str2, string str3)
        => string.Join(", ", self, str1, str2, str3);

    public static string SelfOrDefault([CanBeNull] this string self, [NotNull] string default_, bool isAllowed)
    {
        if (!string.IsNullOrEmpty(self))
            return self;

        if (!isAllowed) {
            // TODO: Add error handling
            Debug.LogError("!");
        }
        return default_;

    }
}
}
