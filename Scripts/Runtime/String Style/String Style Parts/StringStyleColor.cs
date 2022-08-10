using System;
using UnityEngine;

namespace Graffiti.Internal {
[Serializable]
internal struct StringStyleColor {

	[SerializeField] private string    _strColor;
	[SerializeField] private ColorType _graffitiColorType;

	[SerializeField] private GffColor.Modifier _modifier;

	internal bool HasColor() => (_graffitiColorType != ColorType.Undefined) || (_strColor != null);


	/// <remarks> Only one color parameter will be applied (+modificator) </remarks>
	internal StringStyleColor __SetColor(ColorType          colorType,
	                                     string             strColor,
	                                     GffColor.Modifier modifier) {
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

	private GffColor GetColorVariant(ColorType color) {
		switch (_modifier) {
			default:
			case GffColor.Modifier.None:  return GraffitiConfigSo.Palette.FindColors(color);
			case GffColor.Modifier.Dark:  return GraffitiConfigSo.Palette.FindColors(color).Clone().MakeDarker();
			case GffColor.Modifier.Light: return GraffitiConfigSo.Palette.FindColors(color).Clone().MakeLighter();
		}
	}
}
}
