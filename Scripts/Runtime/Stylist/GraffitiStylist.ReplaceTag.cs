using System;
using System.Text;
using JetBrains.Annotations;

namespace Graffiti.Internal {
internal static partial class GraffitiStylist {
    internal static class ReplaceTag {
        [Pure]
        internal static string Color(string self, string color)
        {
            int i = self.IndexOf("<color=", StringComparison.Ordinal);
            if (i == -1) {
                return AddTag.Color(self, color);
            }

            i += "<color=".Length;

            int i_end = self.IndexOf(">", i, StringComparison.Ordinal);
            if (i_end == -1) {
                return AddTag.Color(self, color);
            }

            var sb = new StringBuilder(self);
            (char[] before, char[] between, char[] after) scope
                    = GetScope(sb, i, i_end - 1);

            sb.Clear();
            sb.Append(scope.before);
            sb.Append(color);
            sb.Append(scope.after);
            return sb.ToString();
        }
    }
}
}
