using System;
using Graffiti.Internal;
using UnityEngine;

namespace Graffiti {
/// <summary> Contains one color in different formats. </summary>
[Serializable]
public struct Color3 {

	/// <summary> Color's value in Unity's Color struct </summary>
	public Color  UnityColor;

	/// <summary> Color's Hex value of format: "#RRGGBB" </summary>
	public string Hex;

	/// <summary> Color's Short Hex value of format: "#RGB" </summary>
	public string ShortHex;

	public Color3(Color3 color) : this(color.UnityColor, color.Hex, color.ShortHex) { }

	public Color3(Color unityColor, string hex, string shortHex) {
		UnityColor = unityColor;
		Hex = hex;
		ShortHex = shortHex;
	}

	/// <summary> Returns Color's Hex value according to current config (Full Hex value or Short Hex value). </summary>
	public string GetHexValue() =>
		GraffitiConfigSo.Config.HexColorUsage == HexColorUsage.ShortHex ? ShortHex : Hex;

	public Color3 MakeDarker(Color  color) => ChangeLuminosity(color, -0.3f);
	public Color3 MakeLighter(Color color) => ChangeLuminosity(color, 0.3f);


	private Color3 ChangeLuminosity(Color color, float value) {
		Color.RGBToHSV(color, out float H, out float S, out float V);
		V += value;
		UnityColor = Color.HSVToRGB(H, S, V, false);
		Hex = ColorConvertor.ToHexColor(UnityColor);
		ShortHex = ColorConvertor.ToShortHexColor(UnityColor);
		return this;
	}
}
}
