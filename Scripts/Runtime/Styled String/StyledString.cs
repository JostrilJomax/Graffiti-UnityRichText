using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Graffiti.Internal;

namespace Graffiti {
[Serializable]
public partial class StyledString {
    public StyledString([NotNull] string str, [NotNull] StringStyle stl)
    {
        String = str;
        Styles.Add(stl);
    }

    public StyledString([NotNull] string str, [NotNull] IEnumerable<StringStyle> stls)
    {
        String = str;
        foreach (StringStyle stl in stls)
            Styles.Add(stl);
    }

    public string            String { get; set; }
    public List<StringStyle> Styles { get; private set; } = new List<StringStyle>(1);

    protected StringStyle LastStyle => Styles[^1];

    public override string ToString()
    {
        if (GraffitiProperties.Config.Disabled
         || string.IsNullOrEmpty(String)
         || GraffitiStylist.IsOnlySeparators(String)
         || Styles[0] == null
         || Styles[0].IsEmpty) {
            return String;
        }

        return Styles.Count == 1
                ? StyledStringRenderer.Render(Styles[0], String).ToString()
                : StyledStringRenderer.Render(Styles.ToArray(), String).ToString();
    }

    public static implicit operator string(StyledString styledString) => styledString.ToString();
}
}
