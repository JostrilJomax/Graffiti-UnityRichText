using System;
using UnityEngine;

namespace Graffiti.Internal {
[Serializable]
internal struct StringStyleColor {

	private string _strColor;
	private ColorType _graffitiColorType;

	private Color3Set.Modifier _modifier;

	internal bool HasColor() => (_graffitiColorType != ColorType.Undefined) || (_strColor != null);


	/// <remarks> Only one color parameter will be applied (+modificator) </remarks>
	internal StringStyleColor __SetColor(ColorType          colorType,
	                                     string             strColor,
	                                     Color3Set.Modifier modifier) {
		_modifier = modifier;
		if (colorType != ColorType.Undefined) _graffitiColorType = colorType;
		else if (strColor != null) _strColor = strColor;
		else _graffitiColorType = ColorType.Undefined;
		return this;
	}

	internal string GetColorHexValue() {
		if (_graffitiColorType != ColorType.Undefined)
			return GetColorVariant(_graffitiColorType).GetHexValue();
		if (_strColor != null)
			return _strColor;
		return GetColorVariant(ColorType.Default).GetHexValue();
	}

	internal Color GetUnityColor() {
		if (_graffitiColorType != ColorType.Undefined)
			return ColorConvertor.ToUnityColorFromShortHexColor(GetColorVariant(_graffitiColorType).ShortHex);
		if (_strColor != null)
			return ColorConvertor.ToUnityColor(_strColor);
		return default;
	}

	private Color3 GetColorVariant(ColorType color) {
		switch (_modifier) {
			default:
			case Color3Set.Modifier.None:  return GraffitiConfigSo.Palette.FindColorsSet(color).Value;
			case Color3Set.Modifier.Dark:  return GraffitiConfigSo.Palette.FindColorsSet(color).ValueDarker;
			case Color3Set.Modifier.Light: return GraffitiConfigSo.Palette.FindColorsSet(color).ValueLighter;
		}
	}
}
}
