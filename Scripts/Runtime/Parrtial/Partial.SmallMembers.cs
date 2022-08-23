// This file is NOT generated.

using Graffiti.Internal;
using JetBrains.Annotations;

namespace Graffiti {
public partial class StringStyle {
    public IOnlyColor Dark  => PrepareColorModification(GffColor.Modifier.Dark);
    public IOnlyColor Light => PrepareColorModification(GffColor.Modifier.Light);

    // public StringStyle Bold              => PrepareFontStyle(UnityBuildInFontStyleType.Bold  );
    // public StringStyle Italic            => PrepareFontStyle(UnityBuildInFontStyleType.Italic);
    public StringStyle Size(int size) => PrepareSize(size);
}
}

namespace Graffiti {
public partial class StyledString {

    [PublicAPI] public IOnlyColor Dark     { get {LastStyle.PrepareColorModification(GffColor.Modifier.Dark  ); return this; } }
    [PublicAPI] public IOnlyColor Light    { get {LastStyle.PrepareColorModification(GffColor.Modifier.Light ); return this; } }
    [PublicAPI] public StyledString Bold   { get {LastStyle.PrepareFontStyle(UnityBuildInFontStyleType.Bold  ); return this; } }
    [PublicAPI] public StyledString Italic { get {LastStyle.PrepareFontStyle(UnityBuildInFontStyleType.Italic); return this; } }

    [PublicAPI] public StyledString Size(int size)
    {
        LastStyle.PrepareSize(size);
        return this;
    }
}
}

namespace Graffiti {
public static partial class Style {
    [PublicAPI] public static StringStyle.IOnlyColor Dark  => StringStyle.Create().PrepareColorModification(GffColor.Modifier.Dark);
    [PublicAPI] public static StringStyle.IOnlyColor Light => StringStyle.Create().PrepareColorModification(GffColor.Modifier.Light);

    // [PublicAPI] public static StringStyle Bold             => StringStyle.Create().PrepareFontStyle(UnityBuildInFontStyleType.Bold  );
    // [PublicAPI] public static StringStyle Italic           => StringStyle.Create().PrepareFontStyle(UnityBuildInFontStyleType.Italic);
    [PublicAPI] public static StringStyle Size(int size) => StringStyle.Create().PrepareSize(size);
}
}
