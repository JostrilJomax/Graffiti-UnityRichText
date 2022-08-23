using System;
using Graffiti.Internal;
using UnityEngine;

namespace Graffiti {
/// <summary> Contains one color in different formats. </summary>
[Serializable]
public struct GffColor {
    public enum Modifier {
        None,
        Dark,
        Light,
    }

    public static readonly string _nameof_UnityColor = nameof(UnityColor);
    public static readonly string _nameof_ShortHex   = nameof(ShortHex);

    public bool HasColor;


    public GffColor(GffColor color) : this(color.UnityColor, color.ShortHex) { }

    public GffColor(Color unityColor, string shortHex)
    {
        HasColor = true;
        UnityColor = unityColor;
        ShortHex = shortHex;
    }

    [field: SerializeField] public Color UnityColor { get; private set; }

    /// <summary> Hexadecimal value of 4 characters: "#RGB" </summary>
    [field: SerializeField] public string ShortHex { get; private set; }

    /// <remarks> Only short hex value is passed here, but full hex value can be added later. </remarks>
    public string GetHexValue() => ShortHex;

    public GffColor Clone()       => new GffColor(UnityColor, ShortHex);
    public GffColor MakeDarker()  => ChangeLuminosity(-0.3f);
    public GffColor MakeLighter() => ChangeLuminosity(0.3f);


    private GffColor ChangeLuminosity(float value)
    {
        Color.RGBToHSV(UnityColor, out float H, out float S, out float V);
        V = Mathf.Clamp01(V + value);
        UnityColor = Color.HSVToRGB(H, S, V, false);
        ShortHex = ColorConvertor.ToShortHexColor(UnityColor);
        return this;
    }
}
}
